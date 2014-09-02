using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
    
    property TranslationAssemblies List<Assembly> 
    	init #
    
    property NodeTranslators NodeTranslatorsContainer 
    	init #
    
    property ModuleProcessors List<IModuleProcessor> 
    	init #
    
    property ClassTranslations Dictionary<Type, ClassTranslationInfo> Class translation info collection
    	init #
    
    property AssemblyTranslations Dictionary<Assembly, AssemblyTranslationInfo> Assembly translation info collection
    	init #
    
    property FieldTranslations Dictionary<FieldInfo, FieldTranslationInfo> Field translation info collection
    	init #
    
    property State CompileState 
    
    property KnownConstsValues Dictionary<string, KnownConstInfo> Values of known constants i.e. paths to referenced libraries
    	init #
    
    property Logs List<TranslationMessage> 
    	init #
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

        /// <summary>
        /// Sprawdza jakie klasy są w sparsowanych źródłach a następnie wypełnia wstępną informację  
        /// <see cref="ClassTranslations">ClassTranslations</see> dla tych klas
        /// </summary>
        /// <param name="knownTypes"></param>
        public void FillClassTranslations(Type[] knownTypes)
        {
#if OLD
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
            } 
#endif
            {
                _classTranslations.Clear();
                // var classes = GetClasses().Select(i => i.FullName).Distinct().ToArray();

                foreach (var type in knownTypes.Where(i => !i.IsEnum))
                    GetOrMakeTranslationInfo(type); // it is created and stored in classTranslations         
                //var a = KnownTypes.Where(i => i.IsInterface).ToArray();
                //var ii = GetClasses().Select(i => i.FullName).Distinct().ToArray();

                //foreach (var type in KnownTypes.Where(i => !i.IsEnum))
                //   GetOrMakeTranslationInfo(type); // it is created and stored in classTranslations        
            }
        }

        public ClassTranslationInfo FindClassTranslationInfo(Type t)
        {
            ClassTranslationInfo info;
            return _classTranslations.TryGetValue(t, out info) ? info : null;
        }

        /// <summary>
        /// Zwraca listę klas w sparsowanych źródłach
        /// </summary>
        /// <returns></returns>
        public FullClassDeclaration[] GetClasses()
        {
            var q = from compiled in Compiled
                    from nsDeclaration in compiled.NamespaceDeclarations
                    from classDeclaration in nsDeclaration.Members.OfType<ClassDeclaration>()
                    let fullName = nsDeclaration.Name + "." + classDeclaration.Name
                    select new FullClassDeclaration(fullName, classDeclaration, nsDeclaration);
            return q.ToArray();
        }

        /// <summary>
        /// Zwraca listę klas w sparsowanych źródłach
        /// </summary>
        /// <returns></returns>
        public FullInterfaceDeclaration[] GetInterfaces()
        {
            var q = from compiled in Compiled
                    from nsDeclaration in compiled.NamespaceDeclarations
                    from classDeclaration in nsDeclaration.Members.OfType<InterfaceDeclaration>()
                    let fullName = nsDeclaration.Name + "." + classDeclaration.Name
                    select new FullInterfaceDeclaration(fullName, classDeclaration, nsDeclaration);
            return q.ToArray();
        }

        public ClassTranslationInfo GetOrMakeTranslationInfo(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            ClassTranslationInfo cti;
            if (_classTranslations.TryGetValue(type, out cti)) return cti;
            cti = _classTranslations[type] = new ClassTranslationInfo(type, this);
            if (OnTranslationInfoCreated != null)
                OnTranslationInfoCreated(this, new TranslationInfoCreatedEventArgs { ClassTranslation = cti });
            return cti;
        }

        public AssemblyTranslationInfo GetOrMakeTranslationInfo(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            AssemblyTranslationInfo ati;
            if (_assemblyTranslations.TryGetValue(assembly, out ati)) return ati;
            ati = _assemblyTranslations[assembly] = AssemblyTranslationInfo.FromAssembly(assembly, this);
            if (OnTranslationInfoCreated != null)
                OnTranslationInfoCreated(this, new TranslationInfoCreatedEventArgs
                {
                    AssemblyTranslation = ati
                });
            return ati;
        }

        public FieldTranslationInfo GetOrMakeTranslationInfo(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                throw new ArgumentNullException("fieldInfo");
            FieldTranslationInfo fti;
            if (_fieldTranslations.TryGetValue(fieldInfo, out fti)) return fti;
            fti = _fieldTranslations[fieldInfo] = FieldTranslationInfo.FromFieldInfo(fieldInfo, this);
            if (OnTranslationInfoCreated != null)
                OnTranslationInfoCreated(this, new TranslationInfoCreatedEventArgs
                {
                    FieldTranslation = fti
                });
            return fti;
        }

        [Obsolete("maybe will be better to resolve short names while emit proces")]
        public PhpQualifiedName GetPhpType(Type t, bool doCheckAccesibility)
        {
            if (t == null)
                return null;
            if (doCheckAccesibility)
                CheckAccesibility(t);
            var classTranslationInfo = GetOrMakeTranslationInfo(t);
            var phpQualifiedName = classTranslationInfo.ScriptName.XClone();
            if (_currentType == null) return phpQualifiedName;
            if (t == _currentType)
                phpQualifiedName.CurrentEffectiveName = PhpQualifiedName.SELF;
            else if (t != typeof(object) && t == _currentType.BaseType)
                phpQualifiedName.CurrentEffectiveName = PhpQualifiedName.PARENT;
            else
            {
                var currentTypePhp = GetPhpType(_currentType, false);
                phpQualifiedName.SetEffectiveNameRelatedTo(currentTypePhp);
            }
            return phpQualifiedName;
        }

        [Obsolete("I don't think this is best way to obtain info..")]
        public ClassTranslationInfo GetTI(Type t, bool doCheckAccesibility = true)
        {
            if (t == null)
                return null;
            var a = GetPhpType(t, doCheckAccesibility);
            return _classTranslations[t];
        }

        public void Log(MessageLevels level, string text)
        {
            var a = new TranslationMessage(text, level);
            _logs.Add(a);
        }

        /// <summary>
        /// Porządkuje dane i przygotowuje cache do tłumaczeń
        /// </summary>
        public void Prepare()
        {
            var allTypes = (from a in _translationAssemblies
                            from t in a.GetTypes()
                            select t).ToArray();
            foreach (var type in allTypes)
            {
                if (type.IsInterface)
                    continue;
                var interfaces = type.GetInterfaces();
                foreach (var interfaceType in interfaces)
                {
                    var generic = interfaceType.IsGenericType
                        ? interfaceType.GetGenericTypeDefinition()
                        : interfaceType;
                    #region IPhpNodeTranslator
                    if (generic == typeof(IPhpNodeTranslator<>))
                    {
                        var genericType = interfaceType.GetGenericArguments()[0];
                        var obj = Activator.CreateInstance(type);
                        var map = type.GetInterfaceMap(interfaceType);
                        var methods = map.InterfaceMethods;
                        var methodTranslateToPhp = methods.Single(ii => ii.Name == "TranslateToPhp");
                        var gpmethod = methods.Single(ii => ii.Name == "GetPriority");
                        var bound = new NodeTranslatorBound(methodTranslateToPhp, obj, gpmethod);
                        _nodeTranslators.Add(genericType, bound);
                    }
                    #endregion
                    #region IModuleProcessor
                    // ReSharper disable once InvertIf
                    if (generic == typeof(IModuleProcessor))
                    {
                        var obj = Activator.CreateInstance(type) as IModuleProcessor;
                        _moduleProcessors.Add(obj);
                    }
                    #endregion
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Conversion {0} => {1}", _currentType, _currentMethod);
        }
        // Private Methods 

        private void CheckAccesibility(MethodBase m)
        {
            if (m == null || m.DeclaringType == _currentType)
                return;
            var tt = GetTI(m.DeclaringType, false);
            if (!tt.IsPage && !tt.IsArray) return;
            if (tt.ModuleName == CurrentTypeTranslationInfo.ModuleName) return;
            if (m is ConstructorInfo)
                throw new Exception(string.Format("Constructor {0}.{1} cannot be accessed from {2}.{3}.\r\nType {0} is marked as 'Array' or 'Page'.",
                    m.DeclaringType.FullName,
                    m,
                    CurrentType.FullName,
                    CurrentMethod));
            throw new Exception(string.Format("Method {0}.{1} cannot be accessed from {2}.{3}.\r\nType {0} is marked as 'Array' or 'Page'.",
                m.DeclaringType.FullName,
                m,
                CurrentType.FullName,
                CurrentMethod));
        }

        private void CheckAccesibility(Type type)
        {
            if (type == _currentType)
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

        #endregion Methods

        #region Delegates and Events

        // Events 

        public event EventHandler<TranslationInfoCreatedEventArgs> OnTranslationInfoCreated;

        #endregion Delegates and Events

        #region Nested Classes


        public class TranslationInfoCreatedEventArgs : EventArgs
        {
            #region Properties

            public AssemblyTranslationInfo AssemblyTranslation { get; set; }

            public ClassTranslationInfo ClassTranslation { get; set; }

            public FieldTranslationInfo FieldTranslation { get; set; }

            #endregion Properties
        }
        #endregion Nested Classes
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-02 20:00
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
        implement ToString ##CurrentAssembly## ##CurrentType## ##CurrentTypeTranslationInfo## ##CurrentMethod## ##Compiled## ##TranslationAssemblies## ##NodeTranslators## ##ModuleProcessors## ##ClassTranslations## ##AssemblyTranslations## ##FieldTranslations## ##State## ##KnownConstsValues## ##Logs##
        implement ToString CurrentAssembly=##CurrentAssembly##, CurrentType=##CurrentType##, CurrentTypeTranslationInfo=##CurrentTypeTranslationInfo##, CurrentMethod=##CurrentMethod##, Compiled=##Compiled##, TranslationAssemblies=##TranslationAssemblies##, NodeTranslators=##NodeTranslators##, ModuleProcessors=##ModuleProcessors##, ClassTranslations=##ClassTranslations##, AssemblyTranslations=##AssemblyTranslations##, FieldTranslations=##FieldTranslations##, State=##State##, KnownConstsValues=##KnownConstsValues##, Logs=##Logs##
        implement equals CurrentAssembly, CurrentType, CurrentTypeTranslationInfo, CurrentMethod, Compiled, TranslationAssemblies, NodeTranslators, ModuleProcessors, ClassTranslations, AssemblyTranslations, FieldTranslations, State, KnownConstsValues, Logs
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności CurrentAssembly; obecnie konwertowane assembly
        /// </summary>
        public const string PropertyNameCurrentAssembly = "CurrentAssembly";
        /// <summary>
        /// Nazwa własności CurrentType; Typ obecnie konwertowany
        /// </summary>
        public const string PropertyNameCurrentType = "CurrentType";
        /// <summary>
        /// Nazwa własności CurrentTypeTranslationInfo; 
        /// </summary>
        public const string PropertyNameCurrentTypeTranslationInfo = "CurrentTypeTranslationInfo";
        /// <summary>
        /// Nazwa własności CurrentMethod; obecnie konwertowana metoda
        /// </summary>
        public const string PropertyNameCurrentMethod = "CurrentMethod";
        /// <summary>
        /// Nazwa własności Compiled; 
        /// </summary>
        public const string PropertyNameCompiled = "Compiled";
        /// <summary>
        /// Nazwa własności TranslationAssemblies; 
        /// </summary>
        public const string PropertyNameTranslationAssemblies = "TranslationAssemblies";
        /// <summary>
        /// Nazwa własności NodeTranslators; 
        /// </summary>
        public const string PropertyNameNodeTranslators = "NodeTranslators";
        /// <summary>
        /// Nazwa własności ModuleProcessors; 
        /// </summary>
        public const string PropertyNameModuleProcessors = "ModuleProcessors";
        /// <summary>
        /// Nazwa własności ClassTranslations; Class translation info collection
        /// </summary>
        public const string PropertyNameClassTranslations = "ClassTranslations";
        /// <summary>
        /// Nazwa własności AssemblyTranslations; Assembly translation info collection
        /// </summary>
        public const string PropertyNameAssemblyTranslations = "AssemblyTranslations";
        /// <summary>
        /// Nazwa własności FieldTranslations; Field translation info collection
        /// </summary>
        public const string PropertyNameFieldTranslations = "FieldTranslations";
        /// <summary>
        /// Nazwa własności State; 
        /// </summary>
        public const string PropertyNameState = "State";
        /// <summary>
        /// Nazwa własności KnownConstsValues; Values of known constants i.e. paths to referenced libraries
        /// </summary>
        public const string PropertyNameKnownConstsValues = "KnownConstsValues";
        /// <summary>
        /// Nazwa własności Logs; 
        /// </summary>
        public const string PropertyNameLogs = "Logs";
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
                return _currentAssembly;
            }
            set
            {
                _currentAssembly = value;
            }
        }
        private Assembly _currentAssembly;
        /// <summary>
        /// Typ obecnie konwertowany
        /// </summary>
        public Type CurrentType
        {
            get
            {
                return _currentType;
            }
            set
            {
                if (value == _currentType) return;
                _currentType = value;
                _currentTypeTranslationInfo = GetTI(_currentType, false);
            }
        }
        private Type _currentType;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public ClassTranslationInfo CurrentTypeTranslationInfo
        {
            get
            {
                return _currentTypeTranslationInfo;
            }
        }
        private ClassTranslationInfo _currentTypeTranslationInfo;
        /// <summary>
        /// obecnie konwertowana metoda
        /// </summary>
        public MethodInfo CurrentMethod
        {
            get
            {
                return _currentMethod;
            }
            set
            {
                _currentMethod = value;
            }
        }
        private MethodInfo _currentMethod;
        /// <summary>
        /// 
        /// </summary>
        public List<CompilationUnit> Compiled
        {
            get
            {
                return _compiled;
            }
            set
            {
                _compiled = value;
            }
        }
        private List<CompilationUnit> _compiled = new List<CompilationUnit>();
        /// <summary>
        /// 
        /// </summary>
        public List<Assembly> TranslationAssemblies
        {
            get
            {
                return _translationAssemblies;
            }
            set
            {
                _translationAssemblies = value;
            }
        }
        private List<Assembly> _translationAssemblies = new List<Assembly>();
        /// <summary>
        /// 
        /// </summary>
        public NodeTranslatorsContainer NodeTranslators
        {
            get
            {
                return _nodeTranslators;
            }
            set
            {
                _nodeTranslators = value;
            }
        }
        private NodeTranslatorsContainer _nodeTranslators = new NodeTranslatorsContainer();
        /// <summary>
        /// 
        /// </summary>
        public List<IModuleProcessor> ModuleProcessors
        {
            get
            {
                return _moduleProcessors;
            }
            set
            {
                _moduleProcessors = value;
            }
        }
        private List<IModuleProcessor> _moduleProcessors = new List<IModuleProcessor>();
        /// <summary>
        /// Class translation info collection
        /// </summary>
        public Dictionary<Type, ClassTranslationInfo> ClassTranslations
        {
            get
            {
                return _classTranslations;
            }
            set
            {
                _classTranslations = value;
            }
        }
        private Dictionary<Type, ClassTranslationInfo> _classTranslations = new Dictionary<Type, ClassTranslationInfo>();
        /// <summary>
        /// Assembly translation info collection
        /// </summary>
        public Dictionary<Assembly, AssemblyTranslationInfo> AssemblyTranslations
        {
            get
            {
                return _assemblyTranslations;
            }
            set
            {
                _assemblyTranslations = value;
            }
        }
        private Dictionary<Assembly, AssemblyTranslationInfo> _assemblyTranslations = new Dictionary<Assembly, AssemblyTranslationInfo>();
        /// <summary>
        /// Field translation info collection
        /// </summary>
        public Dictionary<FieldInfo, FieldTranslationInfo> FieldTranslations
        {
            get
            {
                return _fieldTranslations;
            }
            set
            {
                _fieldTranslations = value;
            }
        }
        private Dictionary<FieldInfo, FieldTranslationInfo> _fieldTranslations = new Dictionary<FieldInfo, FieldTranslationInfo>();
        /// <summary>
        /// 
        /// </summary>
        public CompileState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        private CompileState _state;
        /// <summary>
        /// Values of known constants i.e. paths to referenced libraries
        /// </summary>
        public Dictionary<string, KnownConstInfo> KnownConstsValues
        {
            get
            {
                return _knownConstsValues;
            }
            set
            {
                _knownConstsValues = value;
            }
        }
        private Dictionary<string, KnownConstInfo> _knownConstsValues = new Dictionary<string, KnownConstInfo>();
        /// <summary>
        /// 
        /// </summary>
        public List<TranslationMessage> Logs
        {
            get
            {
                return _logs;
            }
            set
            {
                _logs = value;
            }
        }
        private List<TranslationMessage> _logs = new List<TranslationMessage>();
        #endregion Properties
    }
}
