﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Lang.Cs.Compiler;
using System.Reflection;
using Lang.Cs.Compiler.Visitors;
using Lang.Php.Compiler.Translator;
using System.IO;
using Lang.Php.Compiler.Source;
using System.Xml.Linq;
using Lang.Php;
using System.Runtime.InteropServices;


namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Solution ISolution 
    
    property Project IProject 
    
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
		#region Methods 

		// Public Methods 

        public void AddMetadataReferences(params MetadataReference[] adds)
        {
            foreach (var add in adds)
                solution = solution.AddMetadataReference(project.Id, add);
            project = solution.Projects.Single();
        }

        public EmitResult Compile2PhpAndEmit(string OutDir)
        {
            EmitResult result;
            if (verboseToConsole)
                Console.WriteLine("Compilation");
            GetCompilation(out result);
            if (result.Success)
            {
                GreenOk();
                if (verboseToConsole)
                    Console.WriteLine("Analize C# source");
                var info = ParseCsSource();
                GreenOk();
                TranslateAndCreatePHPFiles(info, OutDir);
            }
            return result;
        }

        public Compilation GetCompilation(out EmitResult result)
        {
            projectCompilation = project.GetCompilation() as Compilation;
            projectCompilation = projectCompilation.WithOptions(new CompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            projectCompilation = projectCompilation.WithReferences(project.MetadataReferences);
            compiledAssembly = RoslynHelper.CompileAssembly(projectCompilation, out result);
            return projectCompilation;
        }

        public void LoadProject(string csProj)
        {
            if (verboseToConsole)
                Console.WriteLine("Loading {0}", csProj);
            var a = XDocument.Load(csProj);
            var w = Workspace.LoadStandAloneProject(csProj);
            solution = w.CurrentSolution;
            project = solution.Projects.Single();
            UpdateCompilationOptions(new CompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            GreenOk();
        }

        public TranslationInfo ParseCsSource()
        {
            Type[] KnownTypes = GetKnownTypes();

            CheckRequiredTranslator();

            TranslationInfo tInfo = new TranslationInfo();
            tInfo.TranslationAssemblies.AddRange(this.translationAssemblies);
            tInfo.Prepare();
            // Console.WriteLine("======================");

            foreach (var tree in projectCompilation.SyntaxTrees)
            {
                DateTime now = DateTime.Now;
                System.Console.WriteLine("parse file {0}", tree.FilePath);
                CompilationUnitSyntax root = (CompilationUnitSyntax)tree.GetRoot();
                CompileState state = new CompileState();
                state.Context.RoslynCompilation = projectCompilation;
                state.Context.RoslynModel = projectCompilation.GetSemanticModel(tree);
                tInfo.State = state;

                LangVisitor langVisitor = new LangVisitor(state);
                langVisitor.ThrowNotImplementedException = true;
                state.Context.KnownTypes = KnownTypes;
                CompilationUnit x = langVisitor.Visit(root) as Lang.Cs.Compiler.CompilationUnit;
                tInfo.Compiled.Add(x);
                System.Console.WriteLine("    {0:0.0} sek", DateTime.Now.Subtract(now).TotalSeconds);
            }
            Console.WriteLine("Parsowanie c# skończone, mamy drzewo definicji");
            tInfo.FillClassTranslationInfos(KnownTypes);
            return tInfo;
        }

        public void RemoveMetadataReferences(params MetadataReference[] removes)
        {
            foreach (var remove in removes)
                solution = solution.RemoveMetadataReference(project.Id, remove);
            project = solution.Projects.Single();
        }

        public void TranslateAndCreatePHPFiles(TranslationInfo info, string OutDir)
        {
            if (verboseToConsole)
                Console.WriteLine("Translate C# -> Php");

            info.CurrentAssembly = compiledAssembly;
            TranslationState s = new TranslationState(info);
            Lang.Php.Compiler.Translator.Translator translator = new Lang.Php.Compiler.Translator.Translator(s);
            translator.Translate();
            if (verboseToConsole)
                Console.WriteLine("Create Php output files");
            #region Tworzenie plików php
            {
                EmitContext ec = new EmitContext();
                var ec_BaseDir = OutDir; // Path.Combine(OutDir, "phpCompiled");
                var libName = PhpCodeModuleName.LibNameFromAssembly(this.compiledAssembly);

                info.CurrentAssembly = compiledAssembly;// dla pewności
                foreach (var module in translator.Modules.Where(i => i.Name.Library == libName))
                {
                    module.Name.MakeEmitPath(ec_BaseDir);
                    foreach (var aaa in info.ModuleProcessors)
                        aaa.BeforeEmit(module, info);

                }

                translator.EmitAll(ec);
            }

            #endregion
        }

        public void UpdateCompilationOptions(CommonCompilationOptions options)
        {
            solution = solution.UpdateCompilationOptions(project.Id, options);
            project = solution.Projects.Single();
        }
		// Private Methods 

        private void CheckRequiredTranslator()
        {
            var allAssemblies = GetKnownTypes().Select(i => i.Assembly).Distinct().ToArray();
            foreach (var assembly in allAssemblies)
                CheckRequiredTranslator(assembly);
        }

        private void CheckRequiredTranslator(Assembly A)
        {
            var _req = A.GetCustomAttribute<RequiredTranslatorAttribute>();
            if (_req == null)
                return;
            var _guid = A.GetCustomAttribute<GuidAttribute>();
            if (_guid == null)
                throw new Exception(string.Format("Assembly {0} has no guid", compiledAssembly));
            var Q = Guid.Parse(_guid.Value);
            foreach (var i in translationAssemblies)
            {
                var o = i.GetCustomAttributes();
                var h = i.GetCustomAttributes<PriovidesTranslatorAttribute>()
                        .Where(u => u.TranslatorForAssembly == Q);
                if (h.Any())
                    return;
            }
            throw new Exception(string.Format("There is no tranlation helper for\r\n{0}\r\n\r\n{1}\r\nis suggested.", A, _req.Suggested));

        }

        private Type[] GetKnownTypes()
        {
            if (compiledAssembly == null)
                throw new Exception("Assembly is not compiled yet");
            var assemblies = project.MetadataReferences.Select(i => Assembly.LoadFile(i.Display)).ToList();
            assemblies.Add(compiledAssembly);
            return CompileState.GetAllTypes(assemblies);
        }

        private void GreenOk()
        {
            if (verboseToConsole)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("OK");
                Console.ResetColor();
            }
        }

		#endregion Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-16 14:26
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
        public const string PROPERTYNAME_SOLUTION = "Solution";
        /// <summary>
        /// Nazwa własności Project; 
        /// </summary>
        public const string PROPERTYNAME_PROJECT = "Project";
        /// <summary>
        /// Nazwa własności ProjectCompilation; 
        /// </summary>
        public const string PROPERTYNAME_PROJECTCOMPILATION = "ProjectCompilation";
        /// <summary>
        /// Nazwa własności CompiledAssembly; 
        /// </summary>
        public const string PROPERTYNAME_COMPILEDASSEMBLY = "CompiledAssembly";
        /// <summary>
        /// Nazwa własności TranslationAssemblies; 
        /// </summary>
        public const string PROPERTYNAME_TRANSLATIONASSEMBLIES = "TranslationAssemblies";
        /// <summary>
        /// Nazwa własności VerboseToConsole; 
        /// </summary>
        public const string PROPERTYNAME_VERBOSETOCONSOLE = "VerboseToConsole";
        /// <summary>
        /// Nazwa własności ThrowExceptions; 
        /// </summary>
        public const string PROPERTYNAME_THROWEXCEPTIONS = "ThrowExceptions";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public ISolution Solution
        {
            get
            {
                return solution;
            }
            set
            {
                solution = value;
            }
        }
        private ISolution solution;
        /// <summary>
        /// 
        /// </summary>
        public IProject Project
        {
            get
            {
                return project;
            }
            set
            {
                project = value;
            }
        }
        private IProject project;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public Compilation ProjectCompilation
        {
            get
            {
                return projectCompilation;
            }
        }
        private Compilation projectCompilation;
        /// <summary>
        /// 
        /// </summary>
        public Assembly CompiledAssembly
        {
            get
            {
                return compiledAssembly;
            }
            set
            {
                compiledAssembly = value;
            }
        }
        private Assembly compiledAssembly;
        /// <summary>
        /// 
        /// </summary>
        public List<Assembly> TranslationAssemblies
        {
            get
            {
                return translationAssemblies;
            }
            set
            {
                translationAssemblies = value;
            }
        }
        private List<Assembly> translationAssemblies = new List<Assembly>();
        /// <summary>
        /// 
        /// </summary>
        public bool VerboseToConsole
        {
            get
            {
                return verboseToConsole;
            }
            set
            {
                verboseToConsole = value;
            }
        }
        private bool verboseToConsole;
        /// <summary>
        /// 
        /// </summary>
        public bool ThrowExceptions
        {
            get
            {
                return throwExceptions;
            }
            set
            {
                throwExceptions = value;
            }
        }
        private bool throwExceptions;
        #endregion Properties
    }
}
