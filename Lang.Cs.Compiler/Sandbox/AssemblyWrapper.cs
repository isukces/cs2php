using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lang.Cs.Compiler.Sandbox
{
    public class AssemblyWrapper : MarshalByRefObject
    {
        #region Constructors

        public AssemblyWrapper()
        {
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        [Obsolete]
        public T GetCustomAttribute<T>() where T : Attribute
        {
            return Reflect(a => a.GetCustomAttribute<T>());
        }

        [Obsolete]
        public T[] GetCustomAttributes<T>() where T : Attribute
        {
            return Reflect(a => a.GetCustomAttributes<T>()).ToArray();
        }

        public AssemblyName GetName()
        {
            return Reflect(a => a.GetName());
        }

        public Type[] GetTypes()
        {
            return Reflect(assembly => assembly.GetTypes().ToArray());
        }

        public void LoadAssembly(String assemblyPath)
        {
            try
            {
                _assemblyPath = assemblyPath;

                Uri uri;
                if (Uri.TryCreate(assemblyPath, UriKind.RelativeOrAbsolute, out uri))
                {
                    if (uri.Scheme != Uri.UriSchemeFile)
                        throw new Exception(string.Format("Unsupported protocol in URI {0}", assemblyPath));
                    assemblyPath = uri.LocalPath;
                }

                ResolveEventHandler x = (s, args) =>
                {
                    // Console.WriteLine("X1 {1} {0}", args.RequestingAssembly, args.Name);
                    Assembly assembly = Assembly.LoadFrom(args.Name);
                    // if (assembly != null)
                    return assembly;
                };
                //                AssemblyLoadEventHandler y = (sender, args) =>
                //                {
                //                    Console.WriteLine("X2 {0}", args.LoadedAssembly);
                //                };  
                AppDomain.CurrentDomain.AssemblyResolve += x;
                //AppDomain.CurrentDomain.AssemblyLoad += y;

                // Console.WriteLine("Loading {0} B" , assemblyPath);
                _wrappedAssembly = Assembly.LoadFile(assemblyPath);
                //Console.WriteLine("Loading {0} E", assemblyPath);
                var ass = AppDomain.CurrentDomain.GetAssemblies();
                //Console.WriteLine("Current domain contains {0} assemblies", ass.Length);
                AppDomain.CurrentDomain.AssemblyResolve -= x;
            }
            catch (FileNotFoundException)
            {
                // Continue loading assemblies even if an assembly can not be loaded in the new AppDomain.
            }
        }

        public TResult Reflect<TResult>(Func<Assembly, TResult> func)
        {
            // DirectoryInfo directory = new FileInfo(_assemblyPath).Directory;
            // ResolveEventHandler resolveEventHandler = (s, e) => OnResolve(e, directory);

            //AppDomain.CurrentDomain.AssemblyResolve += resolveEventHandler;

            //            var ass = AppDomain.CurrentDomain.GetAssemblies();
            //            var assembly = ass.FirstOrDefault(a => String.Compare(a.Location, _assemblyPath, StringComparison.Ordinal) == 0) ??
            //                           _assembly;
            //            Console.WriteLine("Calling func B");          
            var result = func(_wrappedAssembly);
            // Console.WriteLine("Calling func E");

            // AppDomain.CurrentDomain.AssemblyResolve -= resolveEventHandler;

            return result;
        }

        #endregion Methods

        #region Fields

        private string _assemblyPath;
        private Assembly _wrappedAssembly;

        public AssemblyWrapper(Assembly wrappedAssembly)
        {
            _wrappedAssembly = wrappedAssembly;
            _assemblyPath = wrappedAssembly.CodeBase;
        }

        #endregion Fields

        #region Properties

        public string FullName
        {
            get { return Reflect(a => a.FullName); }
        }

        public Module ManifestModule
        {
            get { return Reflect(a => a.ManifestModule); }
        }

        public Assembly WrappedAssembly
        {
            get { return _wrappedAssembly; }
        }

        #endregion Properties
    }
}

