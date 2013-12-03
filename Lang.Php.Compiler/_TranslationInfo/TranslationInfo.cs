using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property CurrentAssembly Assembly obecnie konwertowane assembly
    
    property CurrentType Type Typ obecnie konwertowany
    	OnChange currentTypeTranslationInfo = GetTI(currentType, false);
    
    property CurrentTypeTranslationInfo ClassTranslationInfo 
    	read only
    
    property CurrentMethod MethodInfo obecnie konwertowana metoda
    
    property Compiled List<CompilationUnit> 
    	init #
    
    property ClassTranslationInfos Dictionary<Type, ClassTranslationInfo> Informacje o sposobie konwersji klasy na inny język
    	init #
    
    property TranslationAssemblies List<Assembly> 
    	init #
    
    property NodeTranslators NodeTranslatorsContainer 
    	init #
    
    property ModuleProcessors List<IModuleProcessor> 
    	init #
    
    property State CompileState 
    smartClassEnd
    */
    
    public partial class TranslationInfo
    {
        #region Methods

        // Public Methods 

        public void CheckAccesibility(CsharpMethodCallExpression m)
        {
            if (m == null)
                return;
            CheckAccesibility(m.MethodInfo);
        }

        public void CheckAccesibility(MethodBase m)
        {
            if (m == null || m == null || m.DeclaringType == currentType)
                return;
            var tt = GetTI(m.DeclaringType, false);
            if (tt.IsPage || tt.IsArray)
            {                
                if (tt.ModuleName != CurrentTypeTranslationInfo.ModuleName)
                {
                    if (m is ConstructorInfo)
                        throw new Exception(string.Format("Constructor {0}.{1} cannot be accessed from {2}.{3}.\r\nType {0} is marked as 'Array' or 'Page'.",
                            m.DeclaringType.FullName,
                            m,
                            CurrentType.FullName,
                            CurrentMethod));
                    else
                        throw new Exception(string.Format("Method {0}.{1} cannot be accessed from {2}.{3}.\r\nType {0} is marked as 'Array' or 'Page'.",
                            m.DeclaringType.FullName,
                            m,
                            CurrentType.FullName,
                            CurrentMethod));
                }
            }
        }

        public void CheckAccesibility(Type type)
        {
            if (type == currentType)
                return;
            var tt = GetTI(type, false);
            if (!tt.IsPage)
                return;
            if (tt.ModuleName != CurrentTypeTranslationInfo.ModuleName)
                throw new Exception(string.Format("Type {0} cannot be accessed from {1}.{2}",
                    type.FullName,
                    CurrentType.FullName,
                    CurrentMethod));
        }

        /// <summary>
        /// Sprawdza jakie klasy są w sparsowanych źródłach a następnie wypełnia wstępną informację  
        /// <see cref="ClassTranslationInfos">ClassTranslationInfos</see> dla tych klas
        /// </summary>
        /// <param name="KnownTypes"></param>
        public void FillClassTranslationInfos(Type[] KnownTypes)
        {
            List<Type> dotnetClasses = new List<Type>();
            {
                Action<Type[]> searchNested = null;
                searchNested = (n) =>
                {
                    foreach (var i in n)
                    {
                        var thisNested = KnownTypes.Where(ii => ii.DeclaringType == i).ToArray();
                        if (thisNested.Length == 0) continue;
                        dotnetClasses.AddRange(thisNested);
                        searchNested(thisNested);
                    }
                };

                var classes = GetClasses().Select(i => i.FullName).Distinct().ToArray();
                foreach (var c in classes)
                {
                    var dotNetType = KnownTypes.Where(i => i.FullName == c).FirstOrDefault();
                    dotnetClasses.Add(dotNetType);
                }
                searchNested(dotnetClasses.ToArray());

                //var assemblies = dotnetClasses.Select(i => i.Assembly).Distinct().ToArray();
                //dotnetClasses = (from aa in assemblies
                //       from t in aa.GetTypes()
                //       where !t.IsEnum
                //       select t).ToList();
            }
            {
                classTranslationInfos.Clear();
                var classes = GetClasses().Select(i => i.FullName).Distinct().ToArray();
                foreach (var dotNetType in dotnetClasses)
                {
                    if (dotNetType.IsEnum)
                        continue;
                    // var dotNetType = KnownTypes.Where(i => i.FullName == c).FirstOrDefault();
                    var cti = new ClassTranslationInfo(dotNetType);

                    classTranslationInfos[dotNetType] = cti;
                }

            }
        }

        public ClassTranslationInfo FindClassTranslationInfo(Type t)
        {
            ClassTranslationInfo o;
            if (classTranslationInfos.TryGetValue(t, out o))
                return o;
            return null;
        }

        /// <summary>
        /// Zwraca listę klas w sparsowanych źródłach
        /// </summary>
        /// <returns></returns>
        public FullClassDeclaration[] GetClasses()
        {
            var q = from _compiled_ in Compiled
                    from nsDeclaration in _compiled_.NamespaceDeclarations
                    from classDeclaration in nsDeclaration.Members.OfType<ClassDeclaration>()
                    let fullName = nsDeclaration.Name + "." + classDeclaration.Name
                    select new FullClassDeclaration(fullName, classDeclaration, nsDeclaration);
            return q.ToArray();
        }

        public PhpQualifiedName GetPhpType(Type t, bool DoCheckAccesibility)
        {
            if (t == null)
                return null;
            if (DoCheckAccesibility)
                CheckAccesibility(t);
            ClassTranslationInfo a;
            if (!classTranslationInfos.TryGetValue(t, out a))
            {
                a = new ClassTranslationInfo(t);
                a.UpdatFromAttributes();
                a.IsReflected = true;
                classTranslationInfos[t] = a;
                // return a.ScriptName;
            }
            var b = a.ScriptName.XClone();
            if (currentType != null)
            {
                if (currentType.FullName.Contains("FormDefinition"))
                    Console.Write("");
                if (t == currentType)
                    b.CurrentEffectiveName = PhpQualifiedName.SELF;
                else if (t != typeof(object) && t == currentType.BaseType)
                    b.CurrentEffectiveName = PhpQualifiedName.PARENT;
                else
                {
                    var currentTypePhp = GetPhpType(currentType, false);
                    b.SetEffectiveNameRelatedTo(currentTypePhp);
                }
            }
            return b;

        }

        public ClassTranslationInfo GetTI(Type t, bool DoCheckAccesibility = true)
        {
            if (t == null)
                return null;
            var a = GetPhpType(t, DoCheckAccesibility);
            return classTranslationInfos[t];
        }

        /// <summary>
        /// Porządkuje dane i przygotowuje cache do tłumaczeń
        /// </summary>
        public void Prepare()
        {

            var allTypes = (from a in translationAssemblies
                            from t in a.GetTypes()
                            select t).ToArray();
            foreach (var type in allTypes)
            {
                var interfaces = type.GetInterfaces();
                foreach (var i in interfaces)
                {
                    var j = i.IsGenericType ? i.GetGenericTypeDefinition() : i;
                    #region IPhpNodeTranslator
                    if (j == typeof(IPhpNodeTranslator<>))
                    {
                        var genericType = i.GetGenericArguments()[0];
                        var obj = Activator.CreateInstance(type);
                        var map = type.GetInterfaceMap(i);
                        var methods = map.InterfaceMethods;
                        var method = methods.Where(ii => ii.Name == "TranslateToPhp").Single();
                        var gpmethod = methods.Where(ii => ii.Name == "getPriority").Single();
                        NodeTranslatorBound b = new NodeTranslatorBound(method, obj, gpmethod);
                        this.nodeTranslators.Add(genericType, b);
                    }
                    #endregion
                    #region IModuleProcessor
                    if (j == typeof(IModuleProcessor))
                    {
                        var obj = Activator.CreateInstance(type) as IModuleProcessor;
                        this.moduleProcessors.Add(obj);
                    }
                    #endregion
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Conversion {0} => {1}", currentType, currentMethod.ToString());
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-26 10:37
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class TranslationInfo 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public TranslationInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##CurrentAssembly## ##CurrentType## ##CurrentTypeTranslationInfo## ##CurrentMethod## ##Compiled## ##ClassTranslationInfos## ##TranslationAssemblies## ##NodeTranslators## ##ModuleProcessors## ##State##
        implement ToString CurrentAssembly=##CurrentAssembly##, CurrentType=##CurrentType##, CurrentTypeTranslationInfo=##CurrentTypeTranslationInfo##, CurrentMethod=##CurrentMethod##, Compiled=##Compiled##, ClassTranslationInfos=##ClassTranslationInfos##, TranslationAssemblies=##TranslationAssemblies##, NodeTranslators=##NodeTranslators##, ModuleProcessors=##ModuleProcessors##, State=##State##
        implement equals CurrentAssembly, CurrentType, CurrentTypeTranslationInfo, CurrentMethod, Compiled, ClassTranslationInfos, TranslationAssemblies, NodeTranslators, ModuleProcessors, State
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności CurrentAssembly; obecnie konwertowane assembly
        /// </summary>
        public const string PROPERTYNAME_CURRENTASSEMBLY = "CurrentAssembly";
        /// <summary>
        /// Nazwa własności CurrentType; Typ obecnie konwertowany
        /// </summary>
        public const string PROPERTYNAME_CURRENTTYPE = "CurrentType";
        /// <summary>
        /// Nazwa własności CurrentTypeTranslationInfo; 
        /// </summary>
        public const string PROPERTYNAME_CURRENTTYPETRANSLATIONINFO = "CurrentTypeTranslationInfo";
        /// <summary>
        /// Nazwa własności CurrentMethod; obecnie konwertowana metoda
        /// </summary>
        public const string PROPERTYNAME_CURRENTMETHOD = "CurrentMethod";
        /// <summary>
        /// Nazwa własności Compiled; 
        /// </summary>
        public const string PROPERTYNAME_COMPILED = "Compiled";
        /// <summary>
        /// Nazwa własności ClassTranslationInfos; Informacje o sposobie konwersji klasy na inny język
        /// </summary>
        public const string PROPERTYNAME_CLASSTRANSLATIONINFOS = "ClassTranslationInfos";
        /// <summary>
        /// Nazwa własności TranslationAssemblies; 
        /// </summary>
        public const string PROPERTYNAME_TRANSLATIONASSEMBLIES = "TranslationAssemblies";
        /// <summary>
        /// Nazwa własności NodeTranslators; 
        /// </summary>
        public const string PROPERTYNAME_NODETRANSLATORS = "NodeTranslators";
        /// <summary>
        /// Nazwa własności ModuleProcessors; 
        /// </summary>
        public const string PROPERTYNAME_MODULEPROCESSORS = "ModuleProcessors";
        /// <summary>
        /// Nazwa własności State; 
        /// </summary>
        public const string PROPERTYNAME_STATE = "State";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// obecnie konwertowane assembly
        /// </summary>
        public Assembly CurrentAssembly
        {
            get
            {
                return currentAssembly;
            }
            set
            {
                currentAssembly = value;
            }
        }
        private Assembly currentAssembly;
        /// <summary>
        /// Typ obecnie konwertowany
        /// </summary>
        public Type CurrentType
        {
            get
            {
                return currentType;
            }
            set
            {
                if (value == currentType) return;
                currentType = value;
                currentTypeTranslationInfo = GetTI(currentType, false);
            }
        }
        private Type currentType;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public ClassTranslationInfo CurrentTypeTranslationInfo
        {
            get
            {
                return currentTypeTranslationInfo;
            }
        }
        private ClassTranslationInfo currentTypeTranslationInfo;
        /// <summary>
        /// obecnie konwertowana metoda
        /// </summary>
        public MethodInfo CurrentMethod
        {
            get
            {
                return currentMethod;
            }
            set
            {
                currentMethod = value;
            }
        }
        private MethodInfo currentMethod;
        /// <summary>
        /// 
        /// </summary>
        public List<CompilationUnit> Compiled
        {
            get
            {
                return compiled;
            }
            set
            {
                compiled = value;
            }
        }
        private List<CompilationUnit> compiled = new List<CompilationUnit>();
        /// <summary>
        /// Informacje o sposobie konwersji klasy na inny język
        /// </summary>
        public Dictionary<Type, ClassTranslationInfo> ClassTranslationInfos
        {
            get
            {
                return classTranslationInfos;
            }
            set
            {
                classTranslationInfos = value;
            }
        }
        private Dictionary<Type, ClassTranslationInfo> classTranslationInfos = new Dictionary<Type, ClassTranslationInfo>();
        /// <summary>
        /// 
        /// </summary>
        public List<Assembly> TranslationAssemblies
        {
            get
            {
                return translationAssemblies;
            }
            set
            {
                translationAssemblies = value;
            }
        }
        private List<Assembly> translationAssemblies = new List<Assembly>();
        /// <summary>
        /// 
        /// </summary>
        public NodeTranslatorsContainer NodeTranslators
        {
            get
            {
                return nodeTranslators;
            }
            set
            {
                nodeTranslators = value;
            }
        }
        private NodeTranslatorsContainer nodeTranslators = new NodeTranslatorsContainer();
        /// <summary>
        /// 
        /// </summary>
        public List<IModuleProcessor> ModuleProcessors
        {
            get
            {
                return moduleProcessors;
            }
            set
            {
                moduleProcessors = value;
            }
        }
        private List<IModuleProcessor> moduleProcessors = new List<IModuleProcessor>();
        /// <summary>
        /// 
        /// </summary>
        public CompileState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        private CompileState state;
        #endregion Properties

    }
}
