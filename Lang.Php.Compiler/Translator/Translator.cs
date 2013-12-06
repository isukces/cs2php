using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php.Compiler;
using Lang.Php;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Info TranslationInfo 
    	read only
    
    property Modules List<PhpCodeModule> 
    	init #
    	read only
    smartClassEnd
    */

    public partial class Translator
    {
		#region Constructors 

        public Translator(TranslationState state)
        {
            this.state = state ?? new TranslationState(new TranslationInfo());
            info = state.Principles;
        }

		#endregion Constructors 

		#region Static Methods 

		// Private Methods 

        static IPhpStatement[] MkArray(IPhpStatement x)
        {
            return new IPhpStatement[] { x };
        }

		#endregion Static Methods 

		#region Methods 

		// Public Methods 

        public void EmitAll(EmitContext context)
        {
            PhpEmitStyle style = new PhpEmitStyle();
            // style.Compression = EmitStyleCompression.Compact;
            foreach (var module in this.modules)
            {
                if (module.IsEmpty)
                    continue;
                var emiter = new PhpSourceCodeEmiter();
                module.Emit(emiter, style);
            }
        }

        public void Translate()
        {
            var _classes = info.GetClasses();
            var classesToTranslate = info.ClassTranslations.Values.Where(u => !u.IsReflected).ToArray();
            foreach (ClassTranslationInfo classTI in classesToTranslate)
            {
                PhpClassDefinition phpClass;
                PhpCodeModule phpModule = GetOrMakeModuleByName(classTI.ModuleName);
                var assemblyTI = AssemblyTranslationInfo.FromAssembly(info.CurrentAssembly);
                phpModule.Name.MakeEmitPath(assemblyTI.RootPath);
                #region Szukanie / Tworzenie PhpClassDefinition
                {
                    PhpQualifiedName phpBaseClassName;
                    #region Szukanie nazwy klasy bazowej
                    {
                        var netBaseType = classTI.Type.BaseType;
                        if (netBaseType == typeof(object))
                            phpBaseClassName = null;
                        else
                        {
                            phpBaseClassName = state.Principles.GetPhpType(netBaseType, true);
                            ClassTranslationInfo baseTypeTI = state.Principles.ClassTranslations[netBaseType];
                            if (baseTypeTI.Skip)
                                phpBaseClassName = null;
                        }
                    }
                    #endregion
                    phpClass = phpModule.FindOrCreateClass(classTI.ScriptName, phpBaseClassName);
                }
                #endregion
                state.Principles.CurrentType = classTI.Type;
                state.Principles.CurrentAssembly = state.Principles.CurrentType.Assembly;
                Console.WriteLine(classTI.ModuleName);
                FullClassDeclaration[] sources = _classes.Where(i => i.FullName == classTI.Type.FullName).ToArray();
                {
                    var fileNames = classTI.Type.GetCustomAttributes<RequireOnceAttribute>().Select(i => i.Filename).Distinct().ToArray();
                    if (fileNames.Any())
                    {
                        var b = fileNames.Select(u => new PhpConstValue(u)).ToArray();
                        phpModule.RequiredFiles.AddRange(b);
                    }
                }
                #region Constructors
                {
                    foreach (var src in sources)
                    {
                        var c = src.ClassDeclaration.Members.OfType<ConstructorDeclaration>().ToArray();
                        if (c.Length > 1)
                            throw new Exception("PHP supports only one constructor per class");
                        if (c.Any())
                            TranslateConstructor(phpClass, c.First());
                    }
                }
                #endregion
                #region Metody
                {
                    foreach (var src in sources)
                        foreach (var methodDeclaration in src.ClassDeclaration.Members.OfType<MethodDeclaration>())
                            TranslateMethod(phpClass, methodDeclaration);


                }
                #endregion
                #region Własności
                {
                    foreach (var src in sources)
                        foreach (var pDeclaration in src.ClassDeclaration.Members.OfType<CsharpPropertyDeclaration>())
                            TranslateProperty(phpClass, pDeclaration);
                }
                #endregion
                #region Pola, stałe
                {
                    foreach (var src in sources)
                        foreach (var constDeclaration in src.ClassDeclaration.Members.OfType<FieldDeclaration>())
                            TranslateField(phpModule, phpClass, constDeclaration);
                }
                #endregion
                state.Principles.CurrentType = null;
                #region Wywołanie metody MAIN dla PAGE
                {
                    if (classTI.IsPage)
                    {
                        var mti = MethodTranslationInfo.FromMethodInfo(classTI.PageMethod);
                        PhpMethodCallExpression callMain = new PhpMethodCallExpression(mti.ScriptName);
                        callMain.ClassName = classTI.ScriptName.MakeAbsolute();
                        phpModule.BottomCode.Statements.Add(new PhpExpressionStatement(callMain));
                    }
                }
                #endregion
                #region includy
                {


                    List<ModuleCodeRequest> uuu = new List<ModuleCodeRequest>();
                    var cr = (phpModule as ICodeRelated).GetCodeRequests();
                    var g = cr.ToArray();
                    {
                        var clas = (from i in g.OfType<ClassCodeRequest>()
                                    where i.ClassName != null
                                    select i.ClassName.FullName).Distinct().ToArray();

                        foreach (var c in clas)
                        {
                            var m = info.ClassTranslations.Values.Where(i => i.ScriptName.FullName == c).ToArray();
                            if (m.Length != 1)
                                throw new NotSupportedException();
                            var mm = m[0];
                            var im = mm.IncludeModule;
                            if (im == null || mm.ModuleName == phpModule.Name)
                                continue;
                            var h = new ModuleCodeRequest(im);
                            uuu.Add(h);

                        }
                    }
                    {
                        var moduleRequests = (from i in g.OfType<ModuleCodeRequest>()
                                              where i.ModuleName != null
                                              select i).Union(uuu).ToArray();
                        var moduleNames = (from mReq in moduleRequests
                                           where mReq.ModuleName != phpModule.Name
                                           let mName = mReq.ModuleName
                                           where mName != null
                                           select mName
                                    ).Distinct().ToArray();

                        var rf = (from i in moduleNames
                                  let fn = i.MakeIncludePath(phpModule.Name)
                                  where fn != null
                                  select fn).ToArray();
                        phpModule.RequiredFiles.AddRange(rf);
                    }
                }
                #endregion
            }
        }

        public IPhpStatement[] TranslateStatement(IStatement x)
        {
            if (x is CSharpBase)
            {
                OptimizeOptions op = new OptimizeOptions();

                StatementSimplifier s = new StatementSimplifier(op);
                var a = new StatementTranslatorVisitor(state);
                var tmp = a.Visit(x as CSharpBase);
                List<IPhpStatement> result = new List<IPhpStatement>();
                foreach (var i in tmp)
                    result.Add(s.Visit(i as PhpSourceBase));
                return result.ToArray();
            }
            throw new Exception("Błąd translacji " + x.GetType().FullName);
        }
		// Private Methods 

        /// <summary>
        /// Gets existing or creates code module for given name
        /// </summary>
        /// <param name="requiredModuleName"></param>
        /// <returns></returns>
        PhpCodeModule GetOrMakeModuleByName(PhpCodeModuleName requiredModuleName)
        {
            var mod = modules.Where(i => i.Name == requiredModuleName).FirstOrDefault();
            if (mod == null)
            {
                mod = new PhpCodeModule() { Name = requiredModuleName };
                modules.Add(mod);
            }
            return mod;
        }

        private void Tranlate_MethodOrProperty(PhpClassDefinition phpClass, MethodInfo info, IStatement body, string overrideName)
        {
            state.Principles.CurrentMethod = info;
            try
            {
                MethodTranslationInfo mti = MethodTranslationInfo.FromMethodInfo(info);
                var phpMethod = new PhpClassMethodDefinition(string.IsNullOrEmpty(overrideName) ? mti.ScriptName : overrideName);
                phpClass.Methods.Add(phpMethod);

                if (info.IsPublic)
                    phpMethod.Visibility = Visibility.Public;
                else if (info.IsPrivate)
                    phpMethod.Visibility = Visibility.Private;
                else
                    phpMethod.Visibility = Visibility.Protected;

                phpMethod.IsStatic = info.IsStatic;
                {
                    var declaredParameters = info.GetParameters();
                    foreach (var parameter in declaredParameters)
                    {
                        PhpMethodArgument phpParameter = new PhpMethodArgument();
                        phpParameter.Name = parameter.Name;
                        phpMethod.Arguments.Add(phpParameter);
                        if (parameter.HasDefaultValue)
                        {
                            phpParameter.DefaultValue = new PhpConstValue(parameter.DefaultValue);
                        }
                    }
                }

                if (body != null)
                    phpMethod.Statements.AddRange(TranslateStatement(body));
            }
            finally
            {
                state.Principles.CurrentMethod = null;
            }
        }

        private void TranslateConstructor(PhpClassDefinition phpClass, ConstructorDeclaration md)
        {
            //   state.Principles.CurrentMethod = md.Info;
            try
            {
                // MethodTranslationInfo mti = MethodTranslationInfo.FromMethodInfo(md.Info);
                // state.Principles.CurrentMethod = 
                var phpMethod = new PhpClassMethodDefinition("__construct");
                phpClass.Methods.Add(phpMethod);

                if (md.Info.IsPublic)
                    phpMethod.Visibility = Visibility.Public;
                else if (md.Info.IsPrivate)
                    phpMethod.Visibility = Visibility.Private;
                else
                    phpMethod.Visibility = Visibility.Protected;

                phpMethod.IsStatic = md.Info.IsStatic;
                {
                    var declaredParameters = md.Info.GetParameters();
                    foreach (var parameter in declaredParameters)
                    {
                        PhpMethodArgument phpParameter = new PhpMethodArgument();
                        phpParameter.Name = parameter.Name;
                        phpMethod.Arguments.Add(phpParameter);
                        if (parameter.HasDefaultValue)
                        {
                            phpParameter.DefaultValue = new PhpConstValue(parameter.DefaultValue);
                        }
                    }
                }

                if (md.Body != null)
                    phpMethod.Statements.AddRange(TranslateStatement(md.Body));
            }
            finally
            {
                state.Principles.CurrentMethod = null;
            }
        }

        private void TranslateField(PhpCodeModule module, PhpClassDefinition phpClass, FieldDeclaration field)
        {
            PhpValueTranslator phpValueTranslator = null;
            foreach (var item in field.Items)
            {
                FieldTranslationInfo fti = null;

                if (item.OptionalFieldInfo != null)
                {
                    fti = info.GetOrMakeTranslationInfo(item.OptionalFieldInfo);
                    switch (fti.Destination)
                    {
                        case FieldTranslationDestionations.DefinedConst:
                            if (item.Value == null)
                                throw new NotSupportedException();
                            if (phpValueTranslator == null)
                                phpValueTranslator = new PhpValueTranslator(state);
                            IPhpValue definedValue = phpValueTranslator.TransValue(item.Value);
                            module.DefinedConsts.Add(new KeyValuePair<string, IPhpValue>(fti.ScriptName, definedValue));
                            break;
                        case FieldTranslationDestionations.GlobalVariable:
                            IPhpValue value;
                            // muszę na chwilę wyłączyć current type, bo to jes poza klasą generowane
                            {
                                var saveCurrentType = state.Principles.CurrentType;
                                state.Principles.CurrentType = null;
                                try
                                {

                                    if (item.Value == null)
                                    {
                                        value = new PhpConstValue(null);
                                    }
                                    else
                                    {
                                        if (phpValueTranslator == null)
                                            phpValueTranslator = new PhpValueTranslator(state);
                                        value = phpValueTranslator.TransValue(item.Value);
                                    }
                                }
                                finally
                                {
                                    state.Principles.CurrentType = saveCurrentType;
                                }
                            }

                            #region Tworzenie kodu
                            var assign = new PhpAssignExpression(PhpVariableExpression.MakeGlobal(fti.ScriptName), value);
                            module.TopCode.Statements.Add(new PhpExpressionStatement(assign));
                            #endregion
                            break;
                        case FieldTranslationDestionations.JustValue:
                            continue; // don't define
                        case FieldTranslationDestionations.NormalField:
                            {
                                var def = new PhpClassFieldDefinition();
                                var cti = state.Principles.GetTI(state.Principles.CurrentType);
                                if (cti.IsArray)
                                    continue;
                                def.IsConst = field.Modifiers.Has("const");
                                def.Name = PhpVariableExpression.AddDollar(fti.ScriptName, !def.IsConst);

                                def.IsStatic = def.IsConst || field.Modifiers.Has("static");
                                if (field.Modifiers.Has("public"))
                                    def.Visibility = Visibility.Public;
                                else if (field.Modifiers.Has("protected"))
                                    def.Visibility = Visibility.Protected;
                                else
                                    def.Visibility = Visibility.Private;

                                if (item.Value != null)
                                {
                                    if (phpValueTranslator == null)
                                        phpValueTranslator = new PhpValueTranslator(state);
                                    def.ConstValue = phpValueTranslator.TransValue(item.Value);
                                }
                                phpClass.Fields.Add(def);
                                break;
                            }
                        default:
                            throw new NotSupportedException();
                    }

                }

            }
        }

        private void TranslateMethod(PhpClassDefinition phpClass, MethodDeclaration md)
        {
            Tranlate_MethodOrProperty(phpClass, md.Info, md.Body, null);
        }

        private void TranslateProperty(PhpClassDefinition phpClass, CsharpPropertyDeclaration pd)
        {
            var pi = this.state.Principles.CurrentType.GetProperty(pd.PropertyName);
            var pti = PropertyTranslationInfo.FromPropertyInfo(pi);
            if (pti.GetSetByMethod)
            {
                if (!string.IsNullOrEmpty(pti.GetMethodName))
                {

                    var m = pd.Accessors.Where(u => u.Name == "get").FirstOrDefault();
                    if (m != null)
                        Tranlate_MethodOrProperty(phpClass, pi.GetGetMethod(), m.Statement, pti.GetMethodName);
                }
                if (!string.IsNullOrEmpty(pti.SetMethodName))
                {
                    var m = pd.Accessors.Where(u => u.Name == "set").FirstOrDefault();
                    if (m != null)
                        Tranlate_MethodOrProperty(phpClass, pi.GetSetMethod(), m.Statement, pti.SetMethodName);
                }
            }
        }

		#endregion Methods 

		#region Fields 

        TranslationState state;

		#endregion Fields 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-04 09:20
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Translator
{
    public partial class Translator
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public Translator()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Info## ##Modules##
        implement ToString Info=##Info##, Modules=##Modules##
        implement equals Info, Modules
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności Info; 
        /// </summary>
        public const string PROPERTYNAME_INFO = "Info";
        /// <summary>
        /// Nazwa własności Modules; 
        /// </summary>
        public const string PROPERTYNAME_MODULES = "Modules";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public TranslationInfo Info
        {
            get
            {
                return info;
            }
        }
        private TranslationInfo info;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public List<PhpCodeModule> Modules
        {
            get
            {
                return modules;
            }
        }
        private List<PhpCodeModule> modules = new List<PhpCodeModule>();
        #endregion Properties
    }
}
