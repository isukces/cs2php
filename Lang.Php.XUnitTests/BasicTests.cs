using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Lang.Cs.Compiler;
using Lang.Cs2Php;
using Lang.Php.Compiler;
using Lang.Php.Compiler.Translator;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Lang.Php.XUnitTests
{
#if !CS2PHP
    [Skip]
    public class BasicTests : Base
    {
        #region Static Methods

        // Public Methods 



        [Fact]
        public void Compile()
        {
            using (new AppConfigManipulator())
            {
                CompilerEngine.ExecuteInSeparateAppDomain(
                    ce =>
                    {
#if DEBUG
                        ce.Configuration = "DEBUG";
#else
                        ce.Configuration = "RELEASE";
#endif
                        ce.CsProject = LangPhpTestCsProj;
                        ce.OutDir = ce.BinaryOutputDir = Path.GetTempPath();
                        ce.Referenced.Clear();

                        var types = new[]
                        {
                            typeof (Func<,>),
                            typeof (EmitContext),
                            typeof (ThisExpression),
                            typeof (CompilerEngine)
                        };
                        {
                            var tmp =
                                Assembly.Load(
                                    "System.Runtime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
                            ce.Referenced.Add(tmp.GetCodeLocation().FullName);
                        }
                        ce.Referenced.AddRange(types.Select(type => type.Assembly.GetCodeLocation().FullName));


                        ce.TranlationHelpers.Clear();
                        ce.ReferencedPhpLibsLocations.Clear();
                        ce.Check();

                        using (var comp = ce.PreparePhpCompiler())
                        {

                            Console.WriteLine("Compilation");
                            var emitResult = comp.CompileCSharpProject(comp.Sandbox, ce.DllFilename);
                            if (!emitResult.Success)
                                foreach (var i in emitResult.Diagnostics.Where(a => a.Severity == DiagnosticSeverity.Error))
                                    throw new Exception("Compilation error: " + i.GetMessage());


                            var translationInfo = comp.ParseCsSource();


                            translationInfo.CurrentAssembly = comp.CompiledAssembly;
                            var assemblyTi = translationInfo.GetOrMakeTranslationInfo(comp.CompiledAssembly);
                            var ecBaseDir = Path.Combine(Directory.GetCurrentDirectory(),
                                assemblyTi.RootPath.Replace("/", "\\"));
                            Console.WriteLine("Output root {0}", ecBaseDir);

                            var translationState = new TranslationState(translationInfo);
                            var translator = new Translator(translationState);

                            translator.Translate(comp.Sandbox);

                            // =============
                            var m =
                                string.Join(", ", translator.Modules.Select(i => i.Name.Name).OrderBy(i => i)).ToArray();
                            
                            //Assert.True(m == "Lang_Php_Test_Code_MyCode, Lang_Php_Test_Code_SampleEmptyClass", m);

                            MethodTranslation(ModuleMycode, ClassMycode, "BasicMath1", translator);
                            MethodTranslation(ModuleMycode, ClassMycode, "Collections", translator);
                            MethodTranslation(ModuleMycode, ClassMycode, "CostantsAndVariables", translator);
                            MethodTranslation(ModuleMycode, ClassMycode, "Filters", translator);
                            MethodTranslation(ModuleMycode, ClassMycode, "StringConcats", translator);
                            MethodTranslation(ModuleMycode, ClassMycode, "PregTest", translator);
                            
//                            ModuleTranslation("Lang_Php_Test_Code_SampleEmptyClass", translator);
//                            ModuleTranslation("Lang_Php_Test_Code_BusinessClass", translator);
//                            ModuleTranslation("Lang_Php_Test_Code_BusinessClassDefinedConst", translator);

                            foreach (var moduleName in translator.Modules.Select(i => i.Name.Name))                            
                                ModuleTranslation(moduleName, translator);                                
                            
                        }
                        ;
                    }, AppDomain.CurrentDomain);
            }
        }


        [Fact]
        public string CSharpProject()
        {
            var csproj = LangPhpTestCsProj;
            Assert.True(File.Exists(csproj), "C# project not found");
            return csproj;
        }
        // Private Methods 



        #endregion Static Methods

        #region Fields

        const string ClassMycode = "\\MyCodePhp";
        const string ModuleMycode = "Lang_Php_Test_Code_MyCode";

        #endregion Fields
    }
#endif
}
