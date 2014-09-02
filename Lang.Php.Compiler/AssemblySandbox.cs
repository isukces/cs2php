using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;

namespace Lang.Php.Compiler
{
    public sealed class AssemblySandbox : IDisposable
    {
        public AssemblySandbox()
        {
            _appDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString());
        }
        ~AssemblySandbox()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            // ta metoda MUSI zawsze wyglądać tak samo
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private AppDomain _appDomain;
        private void Dispose(bool disposing)
        {
            if (!disposing || _appDomain == null) return;
            AppDomain.Unload(_appDomain);
            _appDomain = null;
        }

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
            // Console.WriteLine("Load assembly: {0}", args.LoadedAssembly);
            Loaded.Add(args.LoadedAssembly);
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Resolve assembly: {0}", args.Name);
            return LoadFromCache(args.Name);
        }

        private static Assembly LoadFromCache(string name)
        {
            var b = AppDomain.CurrentDomain.GetAssemblies();
            var a = b.Where(i => i.GetName().FullName == name).ToArray();
            return a.Length > 0 ? a.First() : null;
        }

        #endregion Static Methods

        #region Static Fields

        static readonly List<Assembly> Loaded = new List<Assembly>();

        #endregion Static Fields

        public Assembly CompileAssembly(Compilation compilation, out EmitResult result)
        {

            using (var memoryStream = new MemoryStream())
            {
                result = compilation.Emit(memoryStream);
                if (!result.Success)
                    return null;

                memoryStream.Flush();
                // var g = memoryStream.GetBuffer();
                var binaryData = memoryStream.ToArray();
                var assembly = _appDomain.Load(binaryData);
                return assembly;
            }

        }

        public Assembly LoadByFullPath(string fullPath)
        {
            var assemblyName = AssemblyName.GetAssemblyName(fullPath);
            return _appDomain.Load(assemblyName);
        }
    }

}
