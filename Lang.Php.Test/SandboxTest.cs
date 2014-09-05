using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lang.Cs.Compiler;
using Lang.Cs.Compiler.Sandbox;
using Lang.Php.Compiler;
using Xunit;

namespace Lang.Php.Test
{
    public class SandboxTest
    {
#if DEBUG
        const string Configuration = "DEBUG";
#else
        const string Configuration = "RELEASE";
#endif
        [Fact]
        public static void Wrappers()
        {
//            Console.WriteLine("Wrappers");
//            AssemblySandbox.Init();
//
//            var tInt = typeof(int);
//            var tString = typeof(string);
//            var wInt =  tInt);
//            var wInt2 = T tInt);
//            var wString = tString);
//
//            Assert.False(tInt == tString);
//            Assert.True(tInt != tString);
//
//            Assert.False(wInt == tString);
//            Assert.True(wInt != tString);
//
//            Assert.False(wInt == wString);
//            Assert.True(wInt != wString);
//
//            Assert.True(wInt == wInt2);
//            Assert.False(wInt != wInt2);
//
//            using (var sand = new AssemblySandbox(null))
//            {
//                var assemblyWrapper = sand.Wrap(tString.Assembly);
//                // var t = assemblyWrapper.Reflect(a => a.GetTypes());                              
//                var Types = assemblyWrapper.GetTypes();
//                var typeNames = Types.Select(a => a.FullName ?? a.Name).ToArray();
//
//                var w = Types.Single(a => a.FullName == "System.DateTime");
//                Assert.True(w == typeof(DateTime));
//                Assert.Equal(w, (Type)typeof(DateTime));
//            }

        }

     

        private static readonly string LangPhpWpDll = string.Format("..\\..\\..\\Extensions\\Lang.Php.Wp\\bin\\{0}\\Lang.Php.Wp.dll",
            Configuration);
        [Fact]
        public static void AdvancedSandbox()
        {
            AssemblySandbox.Init();
            using (var sand1 = new AssemblySandbox(null))
            {
                var proxy = new Proxy(sand1);
                proxy.OnAssemblyResolve += (sendr, args) =>
                {
                    Console.WriteLine("Hey" + args);
                    return null;
                };
                proxy.Test = true;
                var fileName =
                    new FileInfo(LangPhpWpDll);
                if (!fileName.Exists)
                    throw new Exception(string.Format("File {0} doesn't exit", fileName.FullName));
                var assemblyWrapper = proxy.LoadByFullFilename(fileName.FullName);

                Console.WriteLine("-------- 1");

                var all = assemblyWrapper.Reflect(a =>
                {
                    Console.WriteLine("-------- 2");
                    var g = a.GetCustomAttributes(false);
                    var gg = g.Select(q => q.GetType().AssemblyQualifiedName).ToArray();
                    Console.WriteLine(gg);
                    var ggg = typeof(RequiredTranslatorAttribute).AssemblyQualifiedName;
                    var g1 = g.Where(q => q.GetType().AssemblyQualifiedName == ggg).ToArray();
                    Console.WriteLine("-------- 3");
                    return g1;
                });
                //.GetCustomAttributes<RequiredTranslatorAttribute>();
                // var t = assemblyWrapper.Reflect(a => a.GetTypes());                              
                var Types = assemblyWrapper.GetTypes();
                var typeNames = Types.Select(a => a.FullName ?? a.Name).ToArray();
                Console.WriteLine("{0}{1}", typeNames.First(), all.FirstOrDefault());
                var w = Types.Single(a => a.FullName == "Lang.Php.Wp.Wp");
                // Assert.True(w == typeof(DateTime));
                // Assert.Equal(w, (Type)typeof(DateTime));
            }
        }
    }
}
