#define WRITE_CODE
using Lang.Php.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php.Compiler.Translator;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lang.Php.Test.Tests
{
#if !CS2PHP
    public class Base
    {
		#region Static Methods 

		// Protected Methods 

        protected static string GetCode(PhpClassMethodDefinition method)
        {
            var emiter = new PhpSourceCodeEmiter();
            var writer = new PhpSourceCodeWriter();
            writer.Clear();
            method.Emit(emiter, writer, new PhpEmitStyle());
            return writer.GetCode(true).Trim();
        }

        protected static void MethodTranslation(string MODULE, string CLASS, string METHOD)
        {
            string ExpectedCode, translatedCode, shortFilename;
            {
                var translator = Base.PrepareTranslator();
                Console.WriteLine("We have module " + translator.Modules.First().Name.Name);
                var mod = translator.Modules.Where(i => i.Name.Name == MODULE).First();
                var cl = mod.Classes[0];
                Assert.True(cl.Name == CLASS, "Invalid class name translation - attribute ScriptName");
                var method = cl.Methods.Where(i => i.Name == METHOD).First();
                translatedCode = GetCode(method);
            }

            {
                shortFilename = string.Format("{0}_{1}.txt", CLASS.Replace("\\", ""), METHOD);
                var resourceName = "Lang.Php.Test.php." + shortFilename;
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var src = typeof(Base).Assembly.GetManifestResourceStream(resourceName))
                        {
                            src.CopyTo(ms);
                            var b = ms.ToArray();
                            ExpectedCode = Encoding.UTF8.GetString(b);
                        }
                    }
                }
                catch (Exception e)
                {
                    Save(translatedCode, shortFilename);
                    throw;
                }
            }

            if (ExpectedCode != translatedCode)
                Save(translatedCode, shortFilename);

            Assert.True(ExpectedCode == translatedCode, "Invalid translation");
        }
		// Private Methods 

        private static void Save(string translatedCode, string shortFilename)
        {
#if WRITE_CODE
            var fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\php\\new\\" + shortFilename));
            fi.Directory.Create();
            System.IO.File.WriteAllText(fi.FullName, translatedCode);
#endif
        }
		// Internal Methods 

        internal static Translator PrepareTranslator()
        {
            if (translator != null)
                return translator;
            var csProject = CsProj;
            Cs2PhpCompiler comp = new Cs2PhpCompiler();
            comp.VerboseToConsole = true;
            comp.ThrowExceptions = true;
            Console.WriteLine("Try to load " + csProject);

#if DEBUG
            comp.LoadProject(csProject, "DEBUG");
#else
            comp.LoadProject(csProject, "RELEASE");
#endif


            Console.WriteLine("Preparing before compilation");
            #region Remove Lang.Php reference
            {
                // ... will be replaced by reference to dll from compiler base dir
                // I know - compilation libraries should be loaded into separate application domain
                var remove = comp.Project.MetadataReferences.Where(i => i.Display.EndsWith("Lang.Php.dll")).FirstOrDefault();
                if (remove != null)
                    comp.RemoveMetadataReferences(remove);
            }
            #endregion
            string[] filenames;
            #region We have to remove and add again references - strange
            {
                // in other cases some referenced libraries are ignored
                var refToRemove = comp.Project.MetadataReferences.OfType<MetadataFileReference>().ToList();
                foreach (var i in refToRemove)
                    comp.RemoveMetadataReferences(i);
                var ref1 = refToRemove.Select(i => i.FullPath).ToList();
                ref1.Add(Path.Combine(Directory.GetCurrentDirectory(), "Lang.Php.dll"));
                filenames = ref1.Distinct().ToArray();
            }
            #endregion

            #region Translation assemblies
            {
                comp.TranslationAssemblies.Add(typeof(Lang.Php.Framework.Extension).Assembly);
                comp.TranslationAssemblies.Add(typeof(Lang.Php.Compiler.Translator.Translator).Assembly);
            }
            #endregion

            foreach (var fileName in filenames)
            {
                var g = new MetadataFileReference(fileName, MetadataReferenceProperties.Assembly);
                comp.AddMetadataReferences(g);
                Console.WriteLine("  Add reference     {0}", g.Display);
            }


            EmitResult result;
            Console.WriteLine("Start compile");
            comp.GetCompilation(out result);
            if (!result.Success)
            {
                foreach (var i in result.Diagnostics)
                    Console.WriteLine(i.Info);
            }
            Assert.True(result.Success, "Compilation failed");
            TranslationInfo translationInfo = comp.ParseCsSource();


            translationInfo.CurrentAssembly = comp.CompiledAssembly;
            var assemblyTI = translationInfo.GetOrMakeTranslationInfo(comp.CompiledAssembly);
            var ec_BaseDir = Path.Combine(Directory.GetCurrentDirectory(), assemblyTI.RootPath.Replace("/", "\\"));
            Console.WriteLine("Output root {0}", ec_BaseDir);

            TranslationState translationState = new TranslationState(translationInfo);
            translator = new Lang.Php.Compiler.Translator.Translator(translationState);

            translator.Translate();
            return translator;
        }

		#endregion Static Methods 

		#region Static Fields 

        static Translator translator;

		#endregion Static Fields 

		#region Static Properties 

        public static string CsProj
        {
            get
            {
                // C:\programs\_CS2PHP\PUBLIC\Lang.Php.Test\bin\Debug
                var dir = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent;
                var proj = Path.Combine(dir.FullName, "Lang.Php.Test.csproj");
                return proj;
            }
        }

		#endregion Static Properties 
    }
#endif
}
