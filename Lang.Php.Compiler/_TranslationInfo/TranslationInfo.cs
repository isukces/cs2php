using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{
    public class TranslationInfo
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="sandbox"></param>
        /// </summary>
        public TranslationInfo(AssemblySandbox sandbox)
        {
            Sandbox = sandbox;
        }
        // Public Methods 

        public void CheckAccesibility(CsharpMethodCallExpression m)
        {
            if (m == null)
                return;
            CheckAccesibility(m.MethodInfo);
        }

        /// <summary>
        ///     Sprawdza jakie klasy są w sparsowanych źródłach a następnie wypełnia wstępną informację
        ///     <see cref="ClassTranslations">ClassTranslations</see> dla tych klas
        /// </summary>
        /// <param name="knownTypes"></param>
        public void FillClassTranslations(IEnumerable<Type> knownTypes)
        {
            ClassTranslations.Clear();
            foreach (var type in knownTypes.Where(i => !i.IsEnum))
                GetOrMakeTranslationInfo(type); // it is created and stored in classTranslations  
        }

        public ClassTranslationInfo FindClassTranslationInfo(Type t)
        {
            return ClassTranslations.TryGetValue(t, out var info) ? info : null;
        }

        /// <summary>
        ///     Zwraca listę klas w sparsowanych źródłach
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
        ///     Zwraca listę klas w sparsowanych źródłach
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
                throw new ArgumentNullException(nameof(type));
            ClassTranslationInfo cti;
            if (ClassTranslations.TryGetValue(type, out cti)) return cti;
            cti = ClassTranslations[type] = new ClassTranslationInfo(type, this);
            if (OnTranslationInfoCreated != null)
                OnTranslationInfoCreated(this, new TranslationInfoCreatedEventArgs {ClassTranslation = cti});
            return cti;
        }

        public AssemblyTranslationInfo GetOrMakeTranslationInfo(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            if (AssemblyTranslations.TryGetValue(assembly, out var ati)) return ati;
            ati = AssemblyTranslations[assembly] = AssemblyTranslationInfo.FromAssembly(assembly, this);
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
                throw new ArgumentNullException(nameof(fieldInfo));
            FieldTranslationInfo fti;
            if (FieldTranslations.TryGetValue(fieldInfo, out fti)) return fti;
            if (fieldInfo.Name == "PHP_EOL")
                Debug.Write("");
            fti = FieldTranslations[fieldInfo] = FieldTranslationInfo.FromFieldInfo(fieldInfo, this);
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
            var phpQualifiedName     = classTranslationInfo.ScriptName;
            if ((object)relativeTo == null)
                return phpQualifiedName;
            if (type == relativeTo)
            {
                phpQualifiedName.ForceName = PhpQualifiedName.ClassnameSelf;
            }
            else if (type != typeof(object) && type == relativeTo.BaseType)
            {
                phpQualifiedName.ForceName = PhpQualifiedName.ClassnameParent;
            }
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
            return ClassTranslations[type];
        }

        public void Log(MessageLevels level, string text)
        {
            var a = new TranslationMessage(text, level);
            Logs.Add(a);
        }

        /// <summary>
        ///     Porządkuje dane i przygotowuje cache do tłumaczeń
        /// </summary>
        public void Prepare()
        {
            var allTypes = (from assembly in TranslationAssemblies
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
                        var genericType          = interfaceType.GetGenericArguments()[0];
                        var obj                  = Activator.CreateInstance(type);
                        var map                  = type.GetInterfaceMap(interfaceType);
                        var methods              = map.InterfaceMethods;
                        var methodTranslateToPhp = methods.Single(ii => ii.Name == "TranslateToPhp");
                        var gpmethod             = methods.Single(ii => ii.Name == "GetPriority");
                        var bound                = new NodeTranslatorBound(methodTranslateToPhp, obj, gpmethod);
                        NodeTranslators.Add(genericType, bound);
                    }

                    #endregion

                    #region IModuleProcessor

                    // ReSharper disable once InvertIf
                    if (generic == typeof(IModuleProcessor))
                    {
                        var obj = Activator.CreateInstance(type) as IModuleProcessor;
                        ModuleProcessors.Add(obj);
                    }

                    #endregion
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Conversion {0} => {1}", _currentType, CurrentMethod);
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
                throw new Exception(string.Format(
                    "Constructor {0}.{1} cannot be accessed from {2}.{3}.\r\nType {0} is marked as 'Array' or 'Page'.",
                    m.DeclaringType == null ? "?" : (m.DeclaringType.FullName ?? m.DeclaringType.Name),
                    m,
                    CurrentType.FullName,
                    CurrentMethod));
            throw new Exception(string.Format(
                "Method {0}.{1} cannot be accessed from {2}.{3}.\r\nType {0} is marked as 'Array' or 'Page'.",
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


        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public AssemblySandbox Sandbox { get; }

        /// <summary>
        ///     obecnie konwertowane assembly
        /// </summary>
        public Assembly CurrentAssembly { get; set; }

        /// <summary>
        ///     Typ obecnie konwertowany
        /// </summary>
        public Type CurrentType
        {
            get => _currentType;
            set
            {
                if (value == _currentType) return;
                _currentType                = value;
                _currentTypeTranslationInfo = GetTi(_currentType, false);
            }
        }

        /// <summary>
        ///     obecnie konwertowana metoda
        /// </summary>
        public MethodInfo CurrentMethod { get; set; }

        /// <summary>
        /// </summary>
        public List<CompilationUnit> Compiled { get; set; } = new List<CompilationUnit>();

        /// <summary>
        /// </summary>
        public List<Assembly> TranslationAssemblies { get; set; } = new List<Assembly>();

        /// <summary>
        /// </summary>
        public NodeTranslatorsContainer NodeTranslators { get; set; } = new NodeTranslatorsContainer();

        /// <summary>
        /// </summary>
        public List<IModuleProcessor> ModuleProcessors { get; set; } = new List<IModuleProcessor>();

        /// <summary>
        ///     Class translation info collection; własność jest tylko do odczytu.
        /// </summary>
        public Dictionary<Type, ClassTranslationInfo> ClassTranslations { get; } =
            new Dictionary<Type, ClassTranslationInfo>();

        /// <summary>
        ///     Assembly translation info collection
        /// </summary>
        public Dictionary<Assembly, AssemblyTranslationInfo> AssemblyTranslations { get; set; } =
            new Dictionary<Assembly, AssemblyTranslationInfo>();

        /// <summary>
        ///     Field translation info collection
        /// </summary>
        public Dictionary<FieldInfo, FieldTranslationInfo> FieldTranslations { get; set; } =
            new Dictionary<FieldInfo, FieldTranslationInfo>();

        /// <summary>
        /// </summary>
        public CompileState State { get; set; }

        /// <summary>
        ///     Values of known constants i.e. paths to referenced libraries
        /// </summary>
        public Dictionary<string, KnownConstInfo> KnownConstsValues { get; set; } =
            new Dictionary<string, KnownConstInfo>();

        /// <summary>
        /// </summary>
        public List<TranslationMessage> Logs { get; set; } = new List<TranslationMessage>();

        private ClassTranslationInfo _currentTypeTranslationInfo;
        private Type                 _currentType;


        public event EventHandler<TranslationInfoCreatedEventArgs> OnTranslationInfoCreated;


        public class TranslationInfoCreatedEventArgs : EventArgs
        {
            #region Properties

            public AssemblyTranslationInfo AssemblyTranslation { get; set; }

            public ClassTranslationInfo ClassTranslation { get; set; }

            public FieldTranslationInfo FieldTranslation { get; set; }

            #endregion Properties
        }
    }
}