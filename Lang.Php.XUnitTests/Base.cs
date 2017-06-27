#define WRITE_CODE
using System;
using System.IO;
using System.Linq;
using System.Text;
using Lang.Php.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php.Compiler.Translator;
using Lang.Php.Framework;
using Lang.Php.Test.Code;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Lang.Php.XUnitTests
{
#if !CS2PHP
    public class Base
    {
        // Private Methods 

        private static string GetCode(IEmitable method)
        {
            var emiter = new PhpSourceCodeEmiter();
            var writer = new PhpSourceCodeWriter();
            writer.Clear();
            method.Emit(emiter, writer, new PhpEmitStyle());
            return writer.GetCode(true).Trim();
        }

        private static string GetExpectedCode(string shortFilename, string translatedCode)
        {
            string expectedCode;
            var resourceName = "Lang.Php.Test.php." + shortFilename;
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var src = typeof(MyCode).Assembly.GetManifestResourceStream(resourceName))
                    {
                        if (src == null)
                            throw new Exception("Unable to load resource " + resourceName);
                        src.CopyTo(ms);
                        var b = ms.ToArray();
                        expectedCode = Encoding.UTF8.GetString(b);
                    }
                }
            }
            catch (Exception e)
            {
                Save(translatedCode, shortFilename);
                Console.WriteLine(e.Message + "\r\n" + e.StackTrace);
                throw;
            }
            return expectedCode;
        }

        private static void Save(string translatedCode, string shortFilename)
        {
#if WRITE_CODE
            var fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Lang.Php.Test\\php\\new\\" + shortFilename));
            if (fi.Directory != null)
                fi.Directory.Create();
            File.WriteAllText(fi.FullName, translatedCode);
#endif
        }
        // Internal Methods 

        internal static Translator PrepareTranslator()
        {
            if (_translator != null)
                return _translator;
            var csProject = LangPhpTestCsProj;
            using (var comp = new Cs2PhpCompiler { VerboseToConsole = true, ThrowExceptions = true })
            {
                Console.WriteLine("Try to load " + csProject);

#if DEBUG
                comp.LoadProject(csProject, "DEBUG");
#else
                comp.LoadProject(csProject, "RELEASE");
#endif

                /*
   linked with C:\programs\_CS2PHP\PUBLIC\Lang.Php.Compiler\bin\Debug\Lang.Php.Compiler.dll
   linked with C:\programs\_CS2PHP\PUBLIC\Lang.Php.Framework\bin\Debug\Lang.Php.Framework.dll
   linked with C:\programs\_CS2PHP\PUBLIC\Lang.Php.Test\bin\Debug\Lang.Php.dll
             */


                Console.WriteLine("Preparing before compilation");
                string[] removeL = "Lang.Php.Compiler,Lang.Php.Framework,Lang.Php".Split(',');

                #region Remove Lang.Php reference

                {
                    foreach (var r in removeL)
                    {
                        // ... will be replaced by reference to dll from compiler base dir
                        // I know - compilation libraries should be loaded into separate application domain
                        var remove =
                            comp.CSharpProject.MetadataReferences.FirstOrDefault(i => i.Display.EndsWith(r + ".dll"));
                        if (remove != null)
                            comp.RemoveMetadataReferences(remove);
                    }
                }

                #endregion

                string[] filenames;

                #region We have to remove and add again references - strange

                {
                    // in other cases some referenced libraries are ignored
                    var refToRemove = comp.CSharpProject.MetadataReferences.OfType<PortableExecutableReference>().ToList();
                    foreach (var i in refToRemove)
                        comp.RemoveMetadataReferences(i);
                    var ref1 = refToRemove.Select(i => i.FilePath).ToList();
                    // foreach (var r in removeL)
                    //     ref1.Add(Path.Combine(Directory.GetCurrentDirectory(), r + ".dll"));
                    ref1.Add(typeof(DirectCallAttribute).Assembly.Location);
                    ref1.Add(typeof(EmitContext).Assembly.Location);
                    ref1.Add(typeof(Extension).Assembly.Location);
                    filenames = ref1.Distinct().ToArray();
                }

                #endregion

                #region Translation assemblies

                {
                    comp.TranslationAssemblies.Add(typeof(Extension).Assembly);
                    comp.TranslationAssemblies.Add(typeof(Translator).Assembly);
                }

                #endregion

                foreach (var fileName in filenames)
                {
                    var g = MetadataReference.CreateFromFile(fileName, MetadataReferenceProperties.Assembly);
                    comp.AddMetadataReferences(g);
                    Console.WriteLine("  Add reference     {0}", g.Display);
                }

                //                using (var sandbox = new AssemblySandbox())
                //                {
                //                //
                //                Console.WriteLine("Start compile");
                //                var result = comp.CompileCSharpProject(sandbox, comp.DllFileName);
                //                if (!result.Success)
                //                {
                //                    foreach (var i in result.Diagnostics)
                //                        Console.WriteLine(i);
                //                }
                //                Assert.True(result.Success, "Compilation failed");
                //                }
                TranslationInfo translationInfo = comp.ParseCsSource();


                translationInfo.CurrentAssembly = comp.CompiledAssembly;
                var assemblyTi = translationInfo.GetOrMakeTranslationInfo(comp.CompiledAssembly);
                var ecBaseDir = Path.Combine(Directory.GetCurrentDirectory(), assemblyTi.RootPath.Replace("/", "\\"));
                Console.WriteLine("Output root {0}", ecBaseDir);

                var translationState = new TranslationState(translationInfo);
                _translator = new Translator(translationState);

                _translator.Translate(comp.Sandbox);
                return _translator;
            }
        }

        #region Methods

        // Protected Methods 

        protected void MethodTranslation(string module, string xClass, string methodName, Translator translator)
        {
            if (methodName == null) throw new ArgumentNullException("methodName");
            string expectedCode, translatedCode, shortFilename;
            {
                //  translator = PrepareTranslator();
                Console.WriteLine("We have module " + translator.Modules.First().Name.Name);
                var mod = translator.Modules.First(i => i.Name.Name == module);
                var cl = mod.Classes[0];
                Assert.True(cl.Name.FullName == xClass, "Invalid class name translation - attribute ScriptName");
                PhpClassMethodDefinition method = cl.Methods.First(i => i.Name == methodName);
                translatedCode = GetCode(method);
            }

            {
                shortFilename = string.Format("{0}_{1}.txt", xClass.Replace("\\", ""), methodName);
                expectedCode = GetExpectedCode(shortFilename, translatedCode);
            }

            if (expectedCode != translatedCode)
                Save(translatedCode, shortFilename);

            Assert.True(expectedCode == translatedCode, "Invalid translation");
        }

        protected void ModuleTranslation(string module, Translator translator)
        {
            if (module == null) throw new ArgumentNullException("module");
            if (translator == null) throw new ArgumentNullException("translator");

            string expectedCode, translatedCode, shortFilename;
            {
                //  translator = PrepareTranslator();
                Console.WriteLine("We have module " + translator.Modules.First().Name.Name);
                PhpCodeModule mod = translator.Modules.First(i => i.Name.Name == module);
                // var cl = mod.Classes[0];
                // var method = cl.Methods.First(i => i.Name == methodName);
                translatedCode = GetCode(mod);
            }

            {
                shortFilename = string.Format("module-{0}.txt", module);
                expectedCode = GetExpectedCode(shortFilename, translatedCode);
            }

            if (expectedCode != translatedCode)
                Save(translatedCode, shortFilename);

            Assert.True(expectedCode == translatedCode, "Invalid translation");
        }

        #endregion Methods

        #region Static Fields

        static Translator _translator;

        #endregion Static Fields

        #region Static Properties

        protected static string LangPhpTestCsProj
        {
            get
            {
                // C:\programs\_CS2PHP\PUBLIC\Lang.Php.Test\bin\Debug
                var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
                while (true)
                {
                    // 
                    if (new FileInfo(Path.Combine(directoryInfo.FullName, "CSToPHP.sln")).Exists)
                    {
                        var proj = Path.Combine(directoryInfo.FullName, "Lang.Php.Test", "Lang.Php.Test.OLD.csproj");
                        return proj;
                    }
                    directoryInfo = directoryInfo.Parent;
                }
            }
        }

        #endregion Static Properties
    }
#endif
}
