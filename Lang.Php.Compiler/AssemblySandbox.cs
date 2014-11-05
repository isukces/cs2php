using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Lang.Cs.Compiler.Sandbox;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;

namespace Lang.Php.Compiler
{
    [Serializable]
    public class AssemblySandbox : MarshalByRefObject, IDisposable
    {
        #region Constructors

        public AssemblySandbox(AppDomain domain)
        {
            _useOwnDomain = domain == null;
            if (_useOwnDomain)
            {
                // var evidence = new Evidence(AppDomain.CurrentDomain.Evidence);
                //var setup = AppDomain.CurrentDomain.SetupInformation;
                //_appDomain = AppDomain.CreateDomain("sandbox" + Guid.NewGuid(), evidence, setup);

                var domainName = "sandbox" + Guid.NewGuid();
                var domainSetup = new AppDomainSetup
                {
                    ApplicationName = domainName,
                    ApplicationBase = Environment.CurrentDirectory
                };
                _appDomain = AppDomain.CreateDomain(domainName, null, domainSetup);

            }
            else _appDomain = domain;



            //            {
            //                _appDomain.AssemblyResolve += (sender, args) =>
            //                {
            //                    Console.WriteLine("B {1} {0}", args.RequestingAssembly, args.Name);
            //
            //                    RaiseOnAssemblyResolve(this);
            //
            //                    return OnAssemblyResolve != null ? OnAssemblyResolve(sender, args) : null;
            //                };
            //                _appDomain.AssemblyLoad += (sender, args) =>
            //                {
            //                    Console.WriteLine("B {0} into {1}", OO(args.LoadedAssembly), OO(sender));
            //                };
            //            }


            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        ~AssemblySandbox()
        {
            Dispose(false);
        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 

        public static void Init()
        {
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }
        // Private Methods 

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            //Console.WriteLine("A {0} into {1}", ObjectToDebugString(args.LoadedAssembly), ObjectToDebugString(sender));
            Loaded[args.LoadedAssembly.FullName] = args.LoadedAssembly;
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Console.WriteLine("A: Resolve assembly: {0}", args.Name);
            return LoadFromCache(args.Name);
        }

        private static Assembly LoadFromCache(string name)
        {
            var b = AppDomain.CurrentDomain.GetAssemblies();
            var a = b.Where(i => i.GetName().FullName == name).ToArray();
            return a.Length > 0 ? a.First() : null;
        }

        static string ObjectToDebugString(object o)
        {
            if (o == null)
                return "<NULL>";
            var appDomain = o as AppDomain;
            if (appDomain != null)
                return ((_AppDomain)o).FriendlyName;
            var assembly = o as Assembly;
            if (assembly == null) return o.ToString();
            var n = assembly.GetName();
            return string.Format("{0} {1}", n.Name, n.Version);
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void Dispose()
        {
            // ta metoda MUSI zawsze wyglądać tak samo
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Emits compiled assembly to temporary file
        /// </summary>
        /// <param name="compilation"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public AssemblyWrapper EmitCompiledAssembly(Compilation compilation, out EmitResult result, string filename)
        {
            var fi = new FileInfo(filename);
            if (fi.Directory != null) fi.Directory.Create();
            using (var fs = new FileStream(fi.FullName, FileMode.Create))
            {
                result = compilation.Emit(fs);
                if (!result.Success)
                    return null;


            }
            var assembly = LoadByFullFilename(fi.FullName);
            return assembly;


            //            using (var memoryStream = new MemoryStream())
            //            {
            //                result = compilation.Emit(memoryStream);
            //                if (!result.Success)
            //                    return null;
            //
            //                memoryStream.Flush();
            //                // var g = memoryStream.GetBuffer();
            //                var binaryData = memoryStream.ToArray();
            //                var assembly = _appDomain.Load(binaryData);
            //                return assembly;
            //            }

        }

        public AssemblyWrapper LoadByFullFilename(string fullPath)
        {
            AssemblyWrapper wrapper;
            if (_loaded.TryGetValue(fullPath, out wrapper))
                return wrapper;


            var wrapperType = typeof(AssemblyWrapper);
            wrapper = (AssemblyWrapper)_appDomain.CreateInstanceFrom(
                wrapperType.Assembly.Location,
                wrapperType.FullName).Unwrap();

            wrapper.LoadAssembly(fullPath);

            _loaded[fullPath] = wrapper;
            //_proxies[assemblyPath] = proxy;

            return wrapper;

            /*
            ResolveEventHandler
            ff = (sender, args) =>
            {
                Assembly loadedAssembly =AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies()
            .FirstOrDefault(
              asm => string.Equals(asm.FullName, args.Name,
                  StringComparison.OrdinalIgnoreCase));

                if (loadedAssembly != null)
                {
                    return loadedAssembly;
                }

                AssemblyName assemblyName =
                    new AssemblyName(args.Name);
                string dependentAssemblyFilename = fullPath;
                    //Path.Combine(directory.FullName,
                    // assemblyName.Name + ".dll");

                if (File.Exists(dependentAssemblyFilename))
                {
                    return Assembly.LoadFrom(dependentAssemblyFilename);
                }
                return Assembly.ReflectionOnlyLoad(args.Name);
            };


            var assemblyName1 = AssemblyName.GetAssemblyName(fullPath);
            assemblyName1 = new AssemblyName() { CodeBase = assemblyName1.CodeBase };

            _appDomain.AssemblyResolve += ff;

            var a = _appDomain.Load(assemblyName1);
            _appDomain.AssemblyResolve -= ff;
            return a;
             */




        }

        public AssemblyWrapper Wrap(Assembly assembly)
        {
            var fullPath = assembly.GetName().CodeBase;
            return LoadByFullFilename(fullPath);
        }
        // Protected Methods 

        protected void RaiseOnAssemblyResolve(AssemblySandbox loader)
        {
            Console.WriteLine("Raised 1");
            if (OnAssemblyResolve == null) return;
            Console.WriteLine("Raised 2");
            OnAssemblyResolve(loader, null);
        }
        // Private Methods 

        private void Dispose(bool disposing)
        {
            if (!disposing || _appDomain == null) return;
            if (_useOwnDomain)
                AppDomain.Unload(_appDomain);
            _appDomain = null;
        }

        #endregion Methods

        #region Static Fields

        static readonly Dictionary<string, Assembly> Loaded = new Dictionary<string, Assembly>();
        #endregion Static Fields

        #region Fields

        [NonSerialized]
        private AppDomain _appDomain;
        private readonly Dictionary<string, AssemblyWrapper> _loaded =
            new Dictionary<string, AssemblyWrapper>(StringComparer.OrdinalIgnoreCase);

        private bool _useOwnDomain;

        #endregion Fields

        #region Properties

        public bool Test { get; set; }

        #endregion Properties

        #region Delegates and Events

        // Events 

        public event ResolveEventHandler OnAssemblyResolve;

        #endregion Delegates and Events
    }

    public class Proxy : AssemblySandbox
    {
        #region Constructors

        public Proxy(AssemblySandbox sandbox)
            : base(null)
        {
            _sandbox = sandbox;
            OnAssemblyResolve += (s, a) =>
            {
                Console.WriteLine("I am in proxy");
                RaiseOnAssemblyResolve(_sandbox);
                return null;
            };
        }

        #endregion Constructors

        #region Fields

        private readonly AssemblySandbox _sandbox;

        #endregion Fields


        // public event ResolveEventHandler OnAssemblyResolve;
    }
}
