using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Lang.Php.XUnitTests
{
    public class ApplicationDomains
    {
#if DEBUG
const string x = "DEBUG";
#else
        const string x = "RELEASE";
#endif
        #region Static Methods

        // Private Methods 

        private static void Print(AppDomain defaultAppDomain)
        {
            Assembly[] loadedAssemblies = defaultAppDomain.GetAssemblies();
            Console.WriteLine("Here are the assemblies loaded in {0}\n", defaultAppDomain.FriendlyName);
            foreach (Assembly a in loadedAssemblies)
                PrintAssemblyName(a.GetName());
        }

        private static void PrintAssemblyName(AssemblyName name)
        {
            Console.WriteLine("-> Name       : {0}", name.Name);
            Console.WriteLine("   Version    : {0}", name.Version);
            Console.WriteLine("   CodeBase   : {0}", name.CodeBase);
            Console.WriteLine("   FullName   : {0}", name.FullName);
            return;
            ;
            Console.WriteLine("   CultureInfo: {0}", name.CultureInfo);
            Console.WriteLine("   CultureName: {0}", name.CultureName);
            Console.WriteLine("   Flags      : {0}", name.Flags);
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        [Fact]
        public void LoadAssemblies()
        {

            const string fileToLoad1 =
                "C:\\Program Files (x86)\\Reference Assemblies\\Microsoft\\Framework\\.NETFramework\\v4.5\\Microsoft.CSharp.dll";
            string fileToLoad = string.Format("..\\..\\..\\..\\Lang.Php\\bin\\{0}\\net472\\Lang.Php.Dll", x);
            // C:\programs\_CS2PHP\PUBLIC\Lang.Php.XUnitTests\bin\Debug\net472
            AppDomain d = AppDomain.CreateDomain("testDomain");
            try
            {
                //                d.AssemblyResolve += (sender, args) =>
                //                {
                //                    Console.WriteLine("Resolver {0}, {1}", args.Name, args.RequestingAssembly);
                //                    return null;
                //                };
                //      d.AssemblyLoad += (sender, args) => 
                //         Console.WriteLine("Loaded {0}", args.LoadedAssembly.GetName().CodeBase);
                Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine(d.FriendlyName);

                fileToLoad = new FileInfo(fileToLoad).FullName;

                Console.WriteLine("Try load {0}", fileToLoad);
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(fileToLoad);

                Console.WriteLine(" resolved as:");
                PrintAssemblyName(assemblyName);

                Console.WriteLine(assemblyName);

                Assembly loadedAssembly = d.Load(assemblyName);

                var t = loadedAssembly.GetTypes();
                Console.WriteLine("Assembly {0} contains {1} types", loadedAssembly.GetName(), t.Length);

                // Print(d);
            }
            finally
            {
                AppDomain.Unload(d);
            }
        }



        #endregion Methods

    
    }
}
