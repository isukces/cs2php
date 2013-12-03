using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs2Php
{
    public class AssemblyLoader
    {

        public static void Load()
        {

        }

        public void Add(Assembly[] a)
        {
            loaded.AddRange(a);
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
            loaded.Add(args.LoadedAssembly);
        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Resolve assembly: {0}", args.Name);
            return LoadFromCache(args.Name);
        }

        private static Assembly LoadFromCache(string name)
        {
            var b = AppDomain.CurrentDomain.GetAssemblies();
            var a = b.Where(i => i.GetName().FullName == name).ToArray();
            if (a.Length > 0)
                return a.First();
            return null;
        }

        #endregion Static Methods

        #region Static Fields

        static List<Assembly> loaded = new List<Assembly>();

        #endregion Static Fields
    }
}
