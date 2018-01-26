using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Lang.Cs.Compiler;
using Lang.Php;
using Lang.Php.Compiler;
using Lang.Php.Compiler.Translator;
using Lang.Php.Framework;
using Microsoft.CodeAnalysis;

namespace Lang.Cs2Php
{
    public class CompilerEngine : MarshalByRefObject, IConfigData
    {
        public static void ExecuteInSeparateAppDomain(Action<CompilerEngine> compilerEngineAction,
            AppDomain                                                        forceDomain = null)
        {
            if (compilerEngineAction == null)
                throw new ArgumentNullException(nameof(compilerEngineAction));

            var appDomain = forceDomain;
            if (appDomain == null)
            {
                var domainName  = "sandbox" + Guid.NewGuid();
                var domainSetup = new AppDomainSetup
                {
                    ApplicationName = domainName,
                    ApplicationBase = Environment.CurrentDirectory
                };
                appDomain = AppDomain.CreateDomain(domainName, null, domainSetup);
            }

            try
            {
                var wrapperType    = typeof(CompilerEngine);
                var compilerEngine = (CompilerEngine)appDomain.CreateInstanceFrom(
                    wrapperType.Assembly.Location,
                    wrapperType.FullName).Unwrap();
                compilerEngineAction(compilerEngine);
            }
            finally
            {
                if (forceDomain == null)
                    AppDomain.Unload(appDomain);
            }
        }
        // Private Methods 

        /// <summary>
        ///     Zamienia deklarowane referencje na te, które są w katalogu aplikacji
        /// </summary>
        /// <param name="comp"></param>
        private static void Swap(Cs2PhpCompiler comp)
        {
            // ReSharper disable once CSharpWarnings::CS1030
#warning 'Be careful in LINUX'
            var files = new DirectoryInfo(ExeDir).GetFiles("*.*")
                .ToDictionary(a => a.Name.ToLower(), a => a.FullName);
            var portableExecutableReferences = comp.CSharpProject.MetadataReferences
                .OfType<PortableExecutableReference>()
                .Select(
                    reference => new
                    {
                        FileShortName = new FileInfo(reference.FilePath).Name.ToLower(),
                        Reference     = reference
                    })
                .ToArray();

            foreach (var fileReference in portableExecutableReferences)
            {
                string fileFullName;
                if (!files.TryGetValue(fileReference.FileShortName, out fileFullName))
                    continue;

                var remove = fileReference.Reference;
                var add    = MetadataReference.CreateFromFile(fileFullName, MetadataReferenceProperties.Assembly);
                if (remove.Display == add.Display)
                    continue;
                comp.RemoveMetadataReferences(remove);
                comp.AddMetadataReferences(add);
                Console.WriteLine("Swap\r\n    {0}\r\n    {1}", remove.Display, add.Display);
            }
        }

        private static void WriteCompileError(Diagnostic diag)
        {
            // var info = diag.;
            switch (diag.Severity)
            {
                case DiagnosticSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Warning");
                    break;
                case DiagnosticSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Error");
                    break;
                default:
                    return;
            }

            Console.ResetColor();
            Console.WriteLine(": {0}{1}", diag.GetMessage(), diag.Location);
        }

        // Public Methods 

        public void Check()
        {
            if (!File.Exists(CsProject))
                throw new Exception(string.Format("File {0} doesn't exist", _csProject));
        }

        public void Compile()
        {
            using(var comp = PreparePhpCompiler())
            {
                var emitResult = comp.Compile2PhpAndEmit(_outDir, DllFilename, ReferencedPhpLibsLocations);
                if (emitResult.Success) return;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Compilation errors:");
                Console.ResetColor();
                foreach (var i in emitResult.Diagnostics)
                    WriteCompileError(i);
            }
        }

        /// <summary>
        ///     Loads csproj, prepares references and loads assemblies
        /// </summary>
        /// <returns></returns>
        public Cs2PhpCompiler PreparePhpCompiler()
        {
            var comp = new Cs2PhpCompiler
            {
                VerboseToConsole = true,
                ThrowExceptions  = true
            };

            // Console.WriteLine("Try to load " + csProject);
            comp.LoadProject(_csProject, _configuration);

            Console.WriteLine("Preparing before compilation");

            #region Remove Lang.Php reference

            {
                // ... will be replaced by reference to dll from compiler base dir
                // I know - compilation libraries should be loaded into separate application domain
                var remove =
                    comp.CSharpProject.MetadataReferences.FirstOrDefault(i => i.Display.EndsWith("Lang.Php.dll"));
                if (remove != null)
                    comp.RemoveMetadataReferences(remove);
            }

            #endregion

            string[] filenames;

            #region We have to remove and add again references - strange

            {
                // in other cases some referenced libraries are ignored
                var refToRemove =
                    comp.CSharpProject.MetadataReferences.OfType<PortableExecutableReference>().ToList();
                foreach (var i in refToRemove)
                    comp.RemoveMetadataReferences(i);
                var ref1 = refToRemove.Select(i => i.FilePath).Union(Referenced).ToList();
                ref1.Add(typeof(PhpDummy).Assembly.GetCodeLocation().FullName);
                ref1.AddRange(Referenced);
                filenames = ref1.Distinct().ToArray();
            }

            #endregion

            foreach (var fileName in filenames)
                comp.AddMetadataReferences(
                    MetadataReference.CreateFromFile(fileName, MetadataReferenceProperties.Assembly));

            Swap(comp);
            comp.ReferencedAssemblies = comp.CSharpProject.MetadataReferences
                .Select(reference => comp.Sandbox.LoadByFullFilename(reference.Display).WrappedAssembly)
                .ToList();
            foreach (var fileName in TranlationHelpers)
            {
                var assembly = comp.Sandbox.LoadByFullFilename(fileName).WrappedAssembly;
                // ReSharper disable once UnusedVariable
                var an = assembly.GetName();
                Console.WriteLine(" Add translation helper {0}", assembly.FullName);
                comp.TranslationAssemblies.Add(assembly);
            }

            comp.TranslationAssemblies.Add(typeof(Extension).Assembly);
            comp.TranslationAssemblies.Add(typeof(Translator).Assembly);

            return comp;
        }

        public void Set1(string[] referenced, string[] tranlationHelpers, string[] a)
        {
            Referenced                 = referenced.ToList();
            TranlationHelpers          = tranlationHelpers.ToList();
            ReferencedPhpLibsLocations = (from i in a
                select i.Split('\n')
            ).ToDictionary(aa => aa[0], aa => aa[1]);
        }

        private static string ExeDir
        {
            get
            {
                var ea = new Uri(Assembly.GetExecutingAssembly().CodeBase, UriKind.RelativeOrAbsolute);
                var fi = new FileInfo(ea.LocalPath.Replace("/", "\\"));
                return fi.DirectoryName;
            }
        }

        public string DllFilename
        {
            get
            {
                var fi   = new FileInfo(_csProject);
                var name = fi.Name;
                name     = name.Substring(0, name.Length - fi.Extension.Length);
                name     = Path.Combine(!string.IsNullOrEmpty(BinaryOutputDir) ? BinaryOutputDir : _tempDir,
                    name + ".dll");
                return name;
            }
        }


        /// <summary>
        /// </summary>
        public string CsProject
        {
            get => _csProject;
            set => _csProject = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public string OutDir
        {
            get => _outDir;
            set => _outDir = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public List<string> Referenced { get; set; } = new List<string>();

        /// <summary>
        /// </summary>
        public List<string> TranlationHelpers { get; set; } = new List<string>();

        /// <summary>
        ///     Location of referenced libraries in PHP, taken from compiler commandline option
        /// </summary>
        public Dictionary<string, string> ReferencedPhpLibsLocations { get; set; } =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        ///     i.e. DEBUG, RELEASE
        /// </summary>
        public string Configuration
        {
            get => _configuration;
            set => _configuration = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public string BinaryOutputDir
        {
            get => _binaryOutputDir;
            set => _binaryOutputDir = (value ?? string.Empty).Trim();
        }

        private readonly string _tempDir =
            Path.Combine(Path.GetTempPath(), "cs2php" + Guid.NewGuid().ToString("D"));

        private string _csProject       = string.Empty;
        private string _outDir          = string.Empty;
        private string _configuration   = "RELEASE";
        private string _binaryOutputDir = string.Empty;
    }
}