using System;
using System.Collections.Generic;
using System.Linq;
using Lang.Cs.Compiler;
using System.Reflection;
using Lang.Cs.Compiler.Visitors;
using Lang.Cs.Compiler.VSProject;
using Lang.Php.Compiler.Translator;
using System.IO;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.MSBuild;


namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Solution Solution 
    
    property Project Project
    
    property ProjectCompilation Compilation 
        read only
    
    property CompiledAssembly Assembly 
    
    property TranslationAssemblies List<Assembly> 
        init #
    
    property VerboseToConsole bool 
    
    property ThrowExceptions bool 
    smartClassEnd
    */

    public partial class Cs2PhpCompiler
    {
        #region Static Methods

        // Private Methods 

        private static void DownloadAndUnzip(string src, string outDir, string str)
        {
            Console.WriteLine("Downloading {0}...", src);
            if (!src.ToLower().EndsWith(".zip"))
                throw new Exception("Only ZIP files are supported. Sorry.");

            var tmp = src.Substring(src.LastIndexOf('/') + 1);
            tmp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "cs2php",
                "libcache",
                tmp);
            Console.WriteLine("Cache file={0}", tmp);
            // ReSharper disable once PossibleNullReferenceException
            new FileInfo(tmp).Directory.Create();
            if (!File.Exists(tmp))
            {
                #region Download
                {
                    var webClient = new WebClient();
                    webClient.DownloadProgressChanged += (sender, e) => Console.Write("\r {0}%", e.ProgressPercentage);
                    var allDone = new ManualResetEvent(false);
                    webClient.DownloadFileCompleted += (sender, e) => allDone.Set();
                    allDone.Reset();
                    webClient.DownloadFileAsync(new Uri(src), tmp);
                    allDone.WaitOne();
                    Console.WriteLine("\rCompleted");
                }
                #endregion
            }
            #region Unzip
            {
                if (!string.IsNullOrEmpty(str))
                {
                    if (!str.EndsWith("\\"))
                        str += "\\";
                    str = str.ToLower();
                }

                Console.WriteLine("Extracting...");
                using (var za = System.IO.Compression.ZipFile.OpenRead(tmp))
                {
                    foreach (var zipArchiveEntry in za.Entries)
                    {
                        var name = zipArchiveEntry.FullName.Replace("/", "\\");
                        if (name.EndsWith("\\"))
                            continue;
                        if (!string.IsNullOrEmpty(str))
                        {
                            if (name.ToLower().StartsWith(str))
                                name = name.Substring(str.Length);
                        }
                        if (name.StartsWith("\\"))
                            name = name.Substring(1);
                        name = Path.Combine(outDir, name);
                        var fileInfo = new FileInfo(name);
                        // ReSharper disable once PossibleNullReferenceException
                        fileInfo.Directory.Create();
                        using (var stream = zipArchiveEntry.Open())
                        using (var destination = new FileStream(name, FileMode.Create))
                            stream.CopyTo(destination);
                        fileInfo.LastWriteTime = zipArchiveEntry.LastWriteTime.LocalDateTime;
                    }
                }

                //System.IO.Compression.ZipFile.ExtractToDirectory(tmp, outDir, );
                Console.WriteLine("Done.");
            }
            #endregion
        }

        protected static string GetRootPath(Assembly compiledAssembly)
        {
            var tmp = compiledAssembly.GetCustomAttribute<RootPathAttribute>();
            if (tmp == null)
                return "";
            var realOutputDir = (tmp.Path ?? "").Replace("/", "\\");
            while (realOutputDir.StartsWith("\\"))
                realOutputDir = realOutputDir.Substring(1);
            return realOutputDir;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void AddMetadataReferences(params MetadataReference[] adds)
        {
            foreach (var add in adds)
                _solution = _solution.AddMetadataReference(_project.Id, add);
            _project = _solution.Projects.Single(a => a.Id == _project.Id);
        }

        public EmitResult Compile2PhpAndEmit(AssemblySandbox sandbox, string outDir, IEnumerable<Assembly> assToScan, Dictionary<string, string> referencedPhpLibsLocations)
        {
            if (_verboseToConsole)
                Console.WriteLine("Compilation");
            var result = GetCompilation(sandbox);
            if (!result.Success)
                return result;
            GreenOk();
            if (_verboseToConsole)
                Console.WriteLine("Analize C# source");
            var info = ParseCsSource();
            {
                var realOutputDir = Path.Combine(outDir, GetRootPath(CompiledAssembly));


                var ggg = (from assembly in assToScan
                           let moduleIncludeConst = assembly.GetCustomAttribute<ModuleIncludeConstAttribute>()
                           where moduleIncludeConst != null
                           let assemblyName = assembly.GetName().Name
                           select new
                           {
                               DefinedConstName = moduleIncludeConst.ConstOrVarName,
                               AssemblyName = assemblyName,
                               RootPath = GetRootPath(assembly)
                           }).ToArray();

                foreach (var x in ggg)
                {

                    var definedConstName = x.DefinedConstName;
                    if (definedConstName.StartsWith("$"))
                        throw new NotSupportedException();
                    if (!definedConstName.StartsWith("\\"))
                        definedConstName = "\\" + definedConstName;
                    string path;
                    if (!referencedPhpLibsLocations.TryGetValue(x.AssemblyName, out path))
                        continue;
                    path = Path.Combine(path, x.RootPath);
                    String relativePath = PathUtil.MakeRelativePath(path, realOutputDir);
                    info.KnownConstsValues[definedConstName] = new KnownConstInfo(definedConstName, relativePath, false);                            //   referencedLibsPaths[definedConstName] = new KnownConstInfo(definedConstName, relativePath, false);
                }
            }

            GreenOk();
            TranslateAndCreatePhpFiles(info, outDir);
            EmitContentFiles(outDir);
            return result;
        }

        public void LoadProject(string csProj, string configuration)
        {
            if (_verboseToConsole)
                Console.WriteLine("Loading {0}", csProj);

            var buildWorkspace = MSBuildWorkspace.Create();
            _project = buildWorkspace.OpenProjectAsync(csProj).Result;
            _solution = _project.Solution;

            var parseOptions = (CSharpParseOptions)_project.ParseOptions;
            {
                var s = parseOptions.PreprocessorSymbolNames.Union(new[] { "CS2PHP" }).ToArray();
                parseOptions = parseOptions.WithPreprocessorSymbols(s);
                _solution = _solution.WithProjectParseOptions(_project.Id, parseOptions);
                _project = _solution.Projects.Single(a => a.Id == _project.Id);
            }
            //var co = new CompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            // var aa = project.ParseOptions.PreprocessorSymbolNames.ToArray();
            GreenOk();
        }

        public TranslationInfo ParseCsSource()
        {
            // must be public
            var knownTypes = GetKnownTypes();

            CheckRequiredTranslator();

            var translationInfo = new TranslationInfo();
            translationInfo.TranslationAssemblies.AddRange(_translationAssemblies);
            translationInfo.Prepare();
            // Console.WriteLine("======================");

            foreach (var tree in _projectCompilation.SyntaxTrees)
            {
                var now = DateTime.Now;
                Console.WriteLine("parse file {0}", tree.FilePath);
                var root = (CompilationUnitSyntax)tree.GetRoot();
                var state = new CompileState
                {
                    Context =
                    {
                        RoslynCompilation = _projectCompilation,
                        RoslynModel = _projectCompilation.GetSemanticModel(tree)
                    }
                };
                translationInfo.State = state;

                var langVisitor = new LangVisitor(state)
                {
                    throwNotImplementedException = true
                };
                state.Context.KnownTypes = knownTypes;
                var compilationUnit = langVisitor.Visit(root) as CompilationUnit;
                translationInfo.Compiled.Add(compilationUnit);
                Console.WriteLine("    {0:0.0} sek", DateTime.Now.Subtract(now).TotalSeconds);
            }
            Console.WriteLine("Parsowanie c# skończone, mamy drzewo definicji");
            translationInfo.FillClassTranslations(knownTypes);
            return translationInfo;
        }

        public void RemoveMetadataReferences(params MetadataReference[] removes)
        {
            foreach (var remove in removes)
                _solution = _solution.RemoveMetadataReference(_project.Id, remove);
            _project = _solution.Projects.Single(a => a.Id == _project.Id);
        }
        // Private Methods 

        /* public void UpdateCompilationOptions(CommonCompilationOptions options)
         {
             solution = solution.UpdateCompilationOptions(project.Id, options);
             project = solution.Projects.Single();
         }*/
        private void CheckRequiredTranslator()
        {
            var allAssemblies = GetKnownTypes().Select(i => i.Assembly).Distinct().ToArray();
            foreach (var assembly in allAssemblies)
                CheckRequiredTranslator(assembly);
        }

        private void CheckRequiredTranslator(Assembly assembly)
        {
            var requiredTranslatorAttribute = assembly.GetCustomAttribute<RequiredTranslatorAttribute>();
            if (requiredTranslatorAttribute == null)
                return;
            var guidAttribute = assembly.GetCustomAttribute<GuidAttribute>();
            if (guidAttribute == null)
                throw new Exception(string.Format("Assembly {0} has no guid", _compiledAssembly));
            var guid = Guid.Parse(guidAttribute.Value);
            foreach (var i in _translationAssemblies)
            {
                //var customAttributes = i.GetCustomAttributes();
                var h = i.GetCustomAttributes<PriovidesTranslatorAttribute>()
                        .Where(u => u.TranslatorForAssembly == guid);
                if (h.Any())
                    return;
            }
            throw new Exception(string.Format("There is no tranlation helper for\r\n{0}\r\n\r\n{1}\r\nis suggested.", assembly, requiredTranslatorAttribute.Suggested));

        }

        protected void EmitContentFiles(string outDir)
        {
            if (string.IsNullOrEmpty(_project.FilePath))
                return;
            var p = Project1.Load(_project.FilePath);
            var contentFiles = (from i in p.Items
                                where i.BuildAction == BuildActions.Content
                                select PathUtil.MakeWinPath(i.Name, PathUtil.SeparatorProcessing.Append)).ToArray();
            if (!contentFiles.Any())
                return;
            var attr = ReflectionUtil.GetAttribute<ResourcesDirectoryAttribute>(_compiledAssembly);
            var srcDir = PathUtil.MakeWinPath(attr == null ? null : attr.Source, PathUtil.SeparatorProcessing.Append, PathUtil.SeparatorProcessing.Append).ToLower();
            var dstDir = PathUtil.MakeWinPath(attr == null ? null : attr.Destination, PathUtil.SeparatorProcessing.Append, PathUtil.SeparatorProcessing.Append);
            // ReSharper disable once PossibleNullReferenceException
            var projectDir = new FileInfo(_project.FilePath).Directory.FullName;
            foreach (var fileName in contentFiles)
            {
                if (!fileName.ToLower().StartsWith(srcDir))
                    continue;
                var relDestFilename = dstDir + fileName.Substring(srcDir.Length);
                var dstFile = Path.Combine(outDir, relDestFilename.Substring(1));
                var srcFile = Path.Combine(projectDir, fileName.Substring(1));
                Console.WriteLine("copy {0} to {1}", fileName, relDestFilename);
                // ReSharper disable once PossibleNullReferenceException
                new FileInfo(dstFile).Directory.Create();
                File.Copy(srcFile, dstFile, true);
            }
        }

        /*
                public void DisplayRef(string title)
                {
                    Console.WriteLine(" ==== " + title);
                    foreach (var i in Project.MetadataReferences)
                        Console.WriteLine("  MetadataReference {0}", i.Display);
                    foreach (var i in Project.ProjectReferences)
                        Console.WriteLine("  ProjectReferences {0}", i.ProjectId);
                }
        */

        public EmitResult GetCompilation(AssemblySandbox sandbox)
        {
            // must be public !!!!!!!!!!
            _projectCompilation = _project.GetCompilationAsync().Result;
            _projectCompilation =
                _projectCompilation.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            _projectCompilation = _projectCompilation.WithReferences(_project.MetadataReferences);
            foreach (var i in _projectCompilation.References)
                Console.WriteLine("   linked with {0}", i.Display);
            EmitResult result;
            _compiledAssembly = sandbox.CompileAssembly(_projectCompilation, out result);
            return result;
        }

        private Type[] GetKnownTypes()
        {
            AppDomain d = AppDomain.CreateDomain(Guid.NewGuid().ToString());
            try
            {
                Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine(d.FriendlyName);


                if (_compiledAssembly == null)
                    throw new Exception("Assembly is not compiled yet");
                List<Assembly> assemblies1 = _project.MetadataReferences.Select(i => d.Load(i.Display)).ToList();
                assemblies1.Add(_compiledAssembly);

            }
            finally
            {
                AppDomain.Unload(d);
            }
            if (_compiledAssembly == null)
                throw new Exception("Assembly is not compiled yet");
            // todo:use Application domain instead of Assembly.Load
            List<Assembly> assemblies = _project.MetadataReferences.Select(i => Assembly.LoadFile(i.Display)).ToList();
            assemblies.Add(_compiledAssembly);
            return CompileState.GetAllTypes(assemblies);
        }

        protected void GreenOk()
        {
            if (!_verboseToConsole) return;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("OK");
            Console.ResetColor();
        }

        protected void TranslateAndCreatePhpFiles(TranslationInfo translationInfo, string outDir)
        {
            if (_verboseToConsole)
                Console.WriteLine("Translate C# -> Php");

            translationInfo.CurrentAssembly = _compiledAssembly;
            var assemblyTi = translationInfo.GetOrMakeTranslationInfo(_compiledAssembly);
            var ecBaseDir = Path.Combine(outDir, assemblyTi.RootPath.Replace("/", "\\"));
            Console.WriteLine("Output root {0}", ecBaseDir);

            if (!string.IsNullOrEmpty(assemblyTi.PhpPackageSourceUri))
            {
                DownloadAndUnzip(assemblyTi.PhpPackageSourceUri, ecBaseDir, assemblyTi.PhpPackagePathStrip);
                //      return;
            }
            var translationState = new TranslationState(translationInfo);
            var translator = new Translator.Translator(translationState);

            translator.Translate();

            var libName = assemblyTi.LibraryName;


            if (_verboseToConsole)
                Console.WriteLine("Create Php output files");
            #region Tworzenie plików php
            {
                // var emitContext = new EmitContext();
                var emitStyle = new PhpEmitStyle();

                translationInfo.CurrentAssembly = _compiledAssembly;// dla pewności
                foreach (var module in translator.Modules.Where(i => i.Name.Library == libName && !i.IsEmpty))
                {

                    string fileName = module.Name.MakeEmitPath(ecBaseDir, 1);
                    foreach (var modProcessor in translationInfo.ModuleProcessors)
                    {
                        modProcessor.BeforeEmit(module, translationInfo);

                    }
                    var emiter = new PhpSourceCodeEmiter();
                    module.Emit(emiter, emitStyle, fileName);

                }
            }

            #endregion
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-01 17:21
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class Cs2PhpCompiler
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public Cs2PhpCompiler()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Solution## ##Project## ##ProjectCompilation## ##CompiledAssembly## ##TranslationAssemblies## ##VerboseToConsole## ##ThrowExceptions##
        implement ToString Solution=##Solution##, Project=##Project##, ProjectCompilation=##ProjectCompilation##, CompiledAssembly=##CompiledAssembly##, TranslationAssemblies=##TranslationAssemblies##, VerboseToConsole=##VerboseToConsole##, ThrowExceptions=##ThrowExceptions##
        implement equals Solution, Project, ProjectCompilation, CompiledAssembly, TranslationAssemblies, VerboseToConsole, ThrowExceptions
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Solution; 
        /// </summary>
        public const string PropertyNameSolution = "Solution";
        /// <summary>
        /// Nazwa własności Project; 
        /// </summary>
        public const string PropertyNameProject = "Project";
        /// <summary>
        /// Nazwa własności ProjectCompilation; 
        /// </summary>
        public const string PropertyNameProjectCompilation = "ProjectCompilation";
        /// <summary>
        /// Nazwa własności CompiledAssembly; 
        /// </summary>
        public const string PropertyNameCompiledAssembly = "CompiledAssembly";
        /// <summary>
        /// Nazwa własności TranslationAssemblies; 
        /// </summary>
        public const string PropertyNameTranslationAssemblies = "TranslationAssemblies";
        /// <summary>
        /// Nazwa własności VerboseToConsole; 
        /// </summary>
        public const string PropertyNameVerboseToConsole = "VerboseToConsole";
        /// <summary>
        /// Nazwa własności ThrowExceptions; 
        /// </summary>
        public const string PropertyNameThrowExceptions = "ThrowExceptions";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Solution Solution
        {
            get
            {
                return _solution;
            }
            set
            {
                _solution = value;
            }
        }
        private Solution _solution;
        /// <summary>
        /// 
        /// </summary>
        public Project Project
        {
            get
            {
                return _project;
            }
            set
            {
                _project = value;
            }
        }
        private Project _project;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public Compilation ProjectCompilation
        {
            get
            {
                return _projectCompilation;
            }
        }
        private Compilation _projectCompilation;
        /// <summary>
        /// 
        /// </summary>
        public Assembly CompiledAssembly
        {
            get
            {
                return _compiledAssembly;
            }
            set
            {
                _compiledAssembly = value;
            }
        }
        private Assembly _compiledAssembly;
        /// <summary>
        /// 
        /// </summary>
        public List<Assembly> TranslationAssemblies
        {
            get
            {
                return _translationAssemblies;
            }
            set
            {
                _translationAssemblies = value;
            }
        }
        private List<Assembly> _translationAssemblies = new List<Assembly>();
        /// <summary>
        /// 
        /// </summary>
        public bool VerboseToConsole
        {
            get
            {
                return _verboseToConsole;
            }
            set
            {
                _verboseToConsole = value;
            }
        }
        private bool _verboseToConsole;
        /// <summary>
        /// 
        /// </summary>
        public bool ThrowExceptions
        {
            get
            {
                return _throwExceptions;
            }
            set
            {
                _throwExceptions = value;
            }
        }
        private bool _throwExceptions;
        #endregion Properties

    }
}
