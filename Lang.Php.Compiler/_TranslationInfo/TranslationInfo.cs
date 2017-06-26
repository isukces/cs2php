using System.Diagnostics;
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
    implement Constructor Sandbox
    
    property Sandbox AssemblySandbox 
        read only
    
    property CurrentAssembly Assembly obecnie konwertowane assembly
    
    property CurrentType Type Typ obecnie konwertowany
        OnChange _currentTypeTranslationInfo = GetTi(_currentType, false);
    
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
        read only
    
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
        public void FillClassTranslations(IEnumerable<Type> knownTypes)
        {
            _classTranslations.Clear();
            foreach (var type in knownTypes.Where(i => !i.IsEnum))
                GetOrMakeTranslationInfo(type); // it is created and stored in classTranslations  
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


        public MethodTranslationInfo GetOrMakeTranslationInfo(MethodBase methodInfo)
        {
            var cti = GetOrMakeTranslationInfo(methodInfo.DeclaringType);
            return MethodTranslationInfo.FromMethodInfo(methodInfo, cti);
        }

        public ClassTranslationInfo GetOrMakeTranslationInfo(Type type)
        {
            if ((object)type == null)
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
            if (fieldInfo.Name == "PHP_EOL")
                Debug.Write("");
            fti = _fieldTranslations[fieldInfo] = FieldTranslationInfo.FromFieldInfo(fieldInfo, this);
            if (OnTranslationInfoCreated != null)
                OnTranslationInfoCreated(this, new TranslationInfoCreatedEventArgs
                {
                    FieldTranslation = fti
                });
            return fti;
        }

        public PhpQualifiedName GetPhpType(Type type, bool doCheckAccesibility, Type relativeTo)
        {
            if ((object)type == null)
                return PhpQualifiedName.Empty;
            if (doCheckAccesibility)
                CheckAccesibility(type);
            var classTranslationInfo = GetOrMakeTranslationInfo(type);
            var phpQualifiedName = classTranslationInfo.ScriptName;
            if ((object)relativeTo == null)
                return phpQualifiedName;
            if (type == relativeTo)
                phpQualifiedName.ForceName = PhpQualifiedName.ClassnameSelf;
            else if (type != typeof(object) && type == relativeTo.BaseType)
                phpQualifiedName.ForceName = PhpQualifiedName.ClassnameParent;
            else
            {
                var currentTypePhp = GetPhpType(relativeTo, false, null); // recurrence
                phpQualifiedName.SetEffectiveNameRelatedTo(currentTypePhp);
            }
            return phpQualifiedName;
        }

        [Obsolete("I don't think this is best way to obtain info..")]
        public ClassTranslationInfo GetTi(Type type, bool doCheckAccesibility)
        {
            if ((object)type == null)
                return null;
            if (doCheckAccesibility)
                CheckAccesibility(type);
            return _classTranslations[type];
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
            var allTypes = (from assembly in _translationAssemblies
                            from type in assembly.GetTypes()
                            select type).ToArray();
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
            var tt = GetTi(m.DeclaringType, false);
            if (!tt.IsPage && !tt.IsArray) return;
            if (tt.ModuleName == _currentTypeTranslationInfo.ModuleName) return;
            if (m is ConstructorInfo)
                throw new Exception(string.Format("Constructor {0}.{1} cannot be accessed from {2}.{3}.\r\nType {0} is marked as 'Array' or 'Page'.",
                    m.DeclaringType == null ? "?" : (m.DeclaringType.FullName ?? m.DeclaringType.Name),
                    m,
                    CurrentType.FullName,
                    CurrentMethod));
            throw new Exception(string.Format("Method {0}.{1} cannot be accessed from {2}.{3}.\r\nType {0} is marked as 'Array' or 'Page'.",
                m.DeclaringType == null ? "?" : (m.DeclaringType.FullName ?? m.DeclaringType.Name),
                m,
                CurrentType.FullName,
                CurrentMethod));
        }

        private void CheckAccesibility(Type type)
        {
            if (type == _currentType)
                return;
            var tt = GetTi(type, false);
            if (!tt.IsPage)
                return;
            if (tt.ModuleName != _currentTypeTranslationInfo.ModuleName)
                throw new Exception(string.Format("Type {0} cannot be accessed from {1}.{2}",
                    type.FullName,
                    CurrentType.FullName,
                    CurrentMethod));
        }

        #endregion Methods

        #region Fields

        private ClassTranslationInfo _currentTypeTranslationInfo;

        #endregion Fields

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


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-08 08:33
// File generated automatically ver 2014-09-01 19:00
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
        implement ToString ##Sandbox## ##CurrentAssembly## ##CurrentType## ##CurrentMethod## ##Compiled## ##TranslationAssemblies## ##NodeTranslators## ##ModuleProcessors## ##ClassTranslations## ##AssemblyTranslations## ##FieldTranslations## ##State## ##KnownConstsValues## ##Logs##
        implement ToString Sandbox=##Sandbox##, CurrentAssembly=##CurrentAssembly##, CurrentType=##CurrentType##, CurrentMethod=##CurrentMethod##, Compiled=##Compiled##, TranslationAssemblies=##TranslationAssemblies##, NodeTranslators=##NodeTranslators##, ModuleProcessors=##ModuleProcessors##, ClassTranslations=##ClassTranslations##, AssemblyTranslations=##AssemblyTranslations##, FieldTranslations=##FieldTranslations##, State=##State##, KnownConstsValues=##KnownConstsValues##, Logs=##Logs##
        implement equals Sandbox, CurrentAssembly, CurrentType, CurrentMethod, Compiled, TranslationAssemblies, NodeTranslators, ModuleProcessors, ClassTranslations, AssemblyTranslations, FieldTranslations, State, KnownConstsValues, Logs
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="sandbox"></param>
        /// </summary>
        public TranslationInfo(AssemblySandbox sandbox)
        {
            _sandbox = sandbox;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Sandbox; 
        /// </summary>
        public const string PropertyNameSandbox = "Sandbox";
        /// <summary>
        /// Nazwa własności CurrentAssembly; obecnie konwertowane assembly
        /// </summary>
        public const string PropertyNameCurrentAssembly = "CurrentAssembly";
        /// <summary>
        /// Nazwa własności CurrentType; Typ obecnie konwertowany
        /// </summary>
        public const string PropertyNameCurrentType = "CurrentType";
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
        /// Własność jest tylko do odczytu.
        /// </summary>
        public AssemblySandbox Sandbox
        {
            get
            {
                return _sandbox;
            }
        }
        private AssemblySandbox _sandbox;
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
                _currentTypeTranslationInfo = GetTi(_currentType, false);
            }
        }
        private Type _currentType;
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
        /// Class translation info collection; własność jest tylko do odczytu.
        /// </summary>
        public Dictionary<Type, ClassTranslationInfo> ClassTranslations
        {
            get
            {
                return _classTranslations;
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
