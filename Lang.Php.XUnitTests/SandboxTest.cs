using Xunit;

namespace Lang.Php.XUnitTests
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
    }
}
