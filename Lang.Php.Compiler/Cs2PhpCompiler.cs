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
    
    property Sandbox AssemblySandbox 
    	init new AssemblySandbox(AppDomain.CurrentDomain)
    	read only
    
    property Solution Solution 
    
    property CSharpProject Project 
    
    property ProjectCompilation Compilation 
    	read only
    
    property TranslationAssemblies List<Assembly> 
    	init #
    
    property CompiledAssembly Assembly 
    	read only
    
    property VerboseToConsole bool 
    
    property ThrowExceptions bool 
    smartClassEnd
    */

    public sealed partial class Cs2PhpCompiler : IDisposable
    {
        #region Constructors

        ~Cs2PhpCompiler()
        {
            Dispose(false);
        }

        #endregion Constructors



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



        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void AddMetadataReferences(params MetadataReference[] adds)
        {
            foreach (var reference in adds)
            {
                Console.WriteLine("  +ref {0}", reference.Display);
                _solution = _solution.AddMetadataReference(_cSharpProject.Id, reference);
            }
            _cSharpProject = _solution.Projects.Single(a => a.Id == _cSharpProject.Id);
        }

        public EmitResult Compile2PhpAndEmit(string outDir, string dllFilename, IEnumerable<Assembly> assToScan, Dictionary<string, string> referencedPhpLibsLocations)
        {
            if (_verboseToConsole)
                Console.WriteLine("Compilation");
            var emitResult = CompileCSharpProject(_sandbox, dllFilename);
            if (!emitResult.Success)
                return emitResult;
            GreenOk();
            if (_verboseToConsole)
                Console.WriteLine("Analize C# source");
            var info = ParseCsSource();
            {
                var realOutputDir = Path.Combine(outDir, AssemblyTranslationInfo.GetRootPath(CompiledAssembly));


                var ggg = (from assembly in assToScan
                           let moduleIncludeConst = assembly.GetCustomAttribute<ModuleIncludeConstAttribute>()
                           where moduleIncludeConst != null
                           let assemblyName = assembly.GetName().Name
                           select new
                           {
                               DefinedConstName = moduleIncludeConst.ConstOrVarName,
                               AssemblyName = assemblyName,
                               RootPath = AssemblyTranslationInfo.GetRootPath(assembly)
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
            return emitResult;
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
        /// <summary>
        /// Runs C# project compilation and fills <see cref="ProjectCompilation">ProjectCompilation</see>
        /// and <see cref="CompiledAssembly">CompiledAssembly</see>.
        /// </summary>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        public EmitResult CompileCSharpProject(AssemblySandbox sandbox, string dllFilename)
        {
            // must be public !!!!!!!!!!
            _projectCompilation = _cSharpProject.GetCompilationAsync().Result;
            _projectCompilation =
                _projectCompilation.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            _projectCompilation = _projectCompilation.WithReferences(_cSharpProject.MetadataReferences);
            foreach (var i in _projectCompilation.References)
                Console.WriteLine("   linked with {0}", i.Display);
            EmitResult result;

            var tmp = sandbox.EmitCompiledAssembly(_projectCompilation, out result, dllFilename);
            _compiledAssembly = tmp != null ? tmp.WrappedAssembly : null;
            return result;
        }

        public void Dispose()
        {
            // ta metoda MUSI zawsze wyglądać tak samo
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void LoadProject(string csProj, string configuration)
        {
            if (_verboseToConsole)
                Console.WriteLine("Loading {0}", csProj);

            var buildWorkspace = MSBuildWorkspace.Create();
            _cSharpProject = buildWorkspace.OpenProjectAsync(csProj).Result;
            _solution = _cSharpProject.Solution;

            var parseOptions = (CSharpParseOptions)_cSharpProject.ParseOptions;
            {
                var s = parseOptions.PreprocessorSymbolNames.Union(new[] { "CS2PHP" }).ToArray();
                parseOptions = parseOptions.WithPreprocessorSymbols(s);
                _solution = _solution.WithProjectParseOptions(_cSharpProject.Id, parseOptions);
                _cSharpProject = _solution.Projects.Single(a => a.Id == _cSharpProject.Id);
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

            var translationInfo = new TranslationInfo(_sandbox);
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
            foreach (var reference in removes)
            {
                Console.WriteLine("  -ref {0}", reference.Display);
                _solution = _solution.RemoveMetadataReference(_cSharpProject.Id, reference);
            }
            _cSharpProject = _solution.Projects.Single(a => a.Id == _cSharpProject.Id);
        }
        // Protected Methods 

        private void Dispose(bool disposing)
        {
            if (!disposing || _sandbox == null) return;
            _sandbox.Dispose();
            _sandbox = null;
        }

        private void EmitContentFiles(string outDir)
        {
            if (string.IsNullOrEmpty(_cSharpProject.FilePath))
                return;
            var p = Project1.Load(_cSharpProject.FilePath);
            var contentFiles = (from i in p.Items
                                where i.BuildAction == BuildActions.Content
                                select PathUtil.MakeWinPath(i.Name, PathUtil.SeparatorProcessing.Append)).ToArray();
            if (!contentFiles.Any())
                return;
            var attr = _compiledAssembly.GetCustomAttribute<ResourcesDirectoryAttribute>();
            var srcDir = PathUtil.MakeWinPath(attr == null ? null : attr.Source, PathUtil.SeparatorProcessing.Append, PathUtil.SeparatorProcessing.Append).ToLower();
            var dstDir = PathUtil.MakeWinPath(attr == null ? null : attr.Destination, PathUtil.SeparatorProcessing.Append, PathUtil.SeparatorProcessing.Append);
            // ReSharper disable once PossibleNullReferenceException
            var projectDir = new FileInfo(_cSharpProject.FilePath).Directory.FullName;
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

            var a = typeof(RequiredTranslatorAttribute).Assembly.GetName().Version;
            var requiredTranslatorAttribute = assembly.GetCustomAttribute<RequiredTranslatorAttribute>();
            if (requiredTranslatorAttribute == null)
                return;
            var guidAttribute = assembly.GetCustomAttribute<GuidAttribute>();
            if (guidAttribute == null)
                throw new Exception(string.Format("Assembly {0} has no guid", _compiledAssembly));
            var guid = Guid.Parse(guidAttribute.Value);

            var q = from i in _translationAssemblies
                    from u in i.GetCustomAttributes<PriovidesTranslatorAttribute>()
                    where u.TranslatorForAssembly == guid
                    select u;
            if (q.Any())
                return;
            throw new Exception(string.Format(
                "There is no tranlation helper for\r\n{0}\r\n\r\n{1}\r\nis suggested.", assembly,
                requiredTranslatorAttribute.Suggested));
        }

        private Type[] GetKnownTypes()
        {

            if (_compiledAssembly == null)
                throw new Exception("Assembly is not compiled yet");
            var assemblies = _cSharpProject.MetadataReferences.Select(i =>
            {
                var g = i as MetadataFileReference;
                if (g != null)
                    return _sandbox.LoadByFullFilename(g.FilePath).WrappedAssembly;
                throw new NotSupportedException(i.GetType().FullName);
            }).ToList();
            assemblies.Add(_compiledAssembly);
            return CompileState.GetAllTypes(assemblies);
        }

        private void GreenOk()
        {
            if (!_verboseToConsole) return;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("OK");
            Console.ResetColor();
        }

        private void TranslateAndCreatePhpFiles(TranslationInfo translationInfo, string outDir)
        {
            if (_verboseToConsole)
                Console.WriteLine("Translate C# -> Php");

            translationInfo.CurrentAssembly = _compiledAssembly;
            AssemblyTranslationInfo assemblyTi = translationInfo.GetOrMakeTranslationInfo(_compiledAssembly);
            var ecBaseDir = Path.Combine(outDir, assemblyTi.RootPath.Replace("/", "\\"));
            Console.WriteLine("Output root {0}", ecBaseDir);

            if (!string.IsNullOrEmpty(assemblyTi.PhpPackageSourceUri))
            {
                DownloadAndUnzip(assemblyTi.PhpPackageSourceUri, ecBaseDir, assemblyTi.PhpPackagePathStrip);
                //      return;
            }
            var translationState = new TranslationState(translationInfo);
            var translator = new Translator.Translator(translationState);

            translator.Translate(_sandbox);

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


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-05 17:15
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public sealed partial class Cs2PhpCompiler
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
        implement ToString ##Sandbox## ##Solution## ##CSharpProject## ##ProjectCompilation## ##TranslationAssemblies## ##CompiledAssembly## ##VerboseToConsole## ##ThrowExceptions##
        implement ToString Sandbox=##Sandbox##, Solution=##Solution##, CSharpProject=##CSharpProject##, ProjectCompilation=##ProjectCompilation##, TranslationAssemblies=##TranslationAssemblies##, CompiledAssembly=##CompiledAssembly##, VerboseToConsole=##VerboseToConsole##, ThrowExceptions=##ThrowExceptions##
        implement equals Sandbox, Solution, CSharpProject, ProjectCompilation, TranslationAssemblies, CompiledAssembly, VerboseToConsole, ThrowExceptions
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Sandbox; 
        /// </summary>
        public const string PropertyNameSandbox = "Sandbox";
        /// <summary>
        /// Nazwa własności Solution; 
        /// </summary>
        public const string PropertyNameSolution = "Solution";
        /// <summary>
        /// Nazwa własności CSharpProject; 
        /// </summary>
        public const string PropertyNameCSharpProject = "CSharpProject";
        /// <summary>
        /// Nazwa własności ProjectCompilation; 
        /// </summary>
        public const string PropertyNameProjectCompilation = "ProjectCompilation";
        /// <summary>
        /// Nazwa własności TranslationAssemblies; 
        /// </summary>
        public const string PropertyNameTranslationAssemblies = "TranslationAssemblies";
        /// <summary>
        /// Nazwa własności CompiledAssembly; 
        /// </summary>
        public const string PropertyNameCompiledAssembly = "CompiledAssembly";
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
        /// Własność jest tylko do odczytu.
        /// </summary>
        public AssemblySandbox Sandbox
        {
            get
            {
                return _sandbox;
            }
        }
        private AssemblySandbox _sandbox = new AssemblySandbox(AppDomain.CurrentDomain);
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
        public Project CSharpProject
        {
            get
            {
                return _cSharpProject;
            }
            set
            {
                _cSharpProject = value;
            }
        }
        private Project _cSharpProject;
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
        /// Własność jest tylko do odczytu.
        /// </summary>
        public Assembly CompiledAssembly
        {
            get
            {
                return _compiledAssembly;
            }
        }
        private Assembly _compiledAssembly;
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
