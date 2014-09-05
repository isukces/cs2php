using Lang.Php.Compiler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Lang.Cs2Php
{

    /*
    smartClass
    option NoAdditionalFile
    
    property CsProject string 
    
    property OutDir string 
    
    property Referenced List<string> 
    	init #
    
    property TranlationHelpers List<string> 
    	init #
    
    property ReferencedPhpLibsLocations Dictionary<string,string> Location of referenced libraries in PHP, taken from compiler commandline option
    	init new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
    
    property Configuration string i.e. DEBUG, RELEASE
    	init "RELEASE"
    smartClassEnd
    */

    public partial class CompilerEngine : MarshalByRefObject, IConfigData
    {
        #region Static Methods

        // Private Methods 

        /// <summary>
        /// Zamienia deklarowane referencje na te, które są w katalogu aplikacji
        /// </summary>
        /// <param name="comp"></param>
        private static void Swap(Cs2PhpCompiler comp)
        {
            // ReSharper disable once CSharpWarnings::CS1030
#warning 'Be careful in LINUX'
            var files = new DirectoryInfo(ExeDir).GetFiles("*.*").ToDictionary(a => a.Name.ToLower(), a => a.FullName);
            var metadataFileReferences = comp.CSharpProject.MetadataReferences.OfType<MetadataFileReference>()
                .Select(
                    reference => new
                    {
                        FileShortName = new FileInfo(reference.FilePath).Name.ToLower(),
                        Reference = reference
                    })
                .ToArray();



            foreach (var fileReference in metadataFileReferences)
            {
                string fileFullName;
                if (!files.TryGetValue(fileReference.FileShortName, out fileFullName))
                    continue;

                var remove = fileReference.Reference;
                var add = new MetadataFileReference(fileFullName, MetadataReferenceProperties.Assembly);
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
            Console.WriteLine(": " + diag.GetMessage());
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void Check()
        {
            if (!File.Exists(CsProject))
                throw new Exception(string.Format("File {0} doesn't exist", _csProject));
        }

        public void Compile()
        {
            using (var comp = new Cs2PhpCompiler
            {
                VerboseToConsole = true,
                ThrowExceptions = true
            })
            {
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
                    List<MetadataFileReference> refToRemove =
                        comp.CSharpProject.MetadataReferences.OfType<MetadataFileReference>().ToList();
                    foreach (var i in refToRemove)
                        comp.RemoveMetadataReferences(i);
                    var ref1 = refToRemove.Select(i => i.FilePath).Union(_referenced).ToList();
                    ref1.Add(Path.Combine(ExeDir, "Lang.Php.dll"));
                    filenames = ref1.Distinct().ToArray();
                }

                #endregion

                foreach (var fileName in filenames)
                {
                    var g = new MetadataFileReference(fileName, MetadataReferenceProperties.Assembly);
                    comp.AddMetadataReferences(g);
#if DEBUG
                  Console.WriteLine("  Add reference     {0}", g.Display);                
#endif
                }


                Swap(comp);
                {
                    // ProjectReferences
                    foreach (var x in comp.CSharpProject.ProjectReferences.ToArray())
                    {
                        Console.WriteLine(" " + x.Aliases);
                        //hack project path
                        var xx = x.ToString();
                    }

                }
                var loadedAssemblies = new List<Assembly>();
                foreach (var reference in comp.CSharpProject.MetadataReferences)
                {
                    // var assemblyName = AssemblyName.GetAssemblyName(reference.Display);

                    // var assembly = Assembly.LoadFrom(reference.Display);
                    var assembly = comp.Sandbox.LoadByFullFilename(reference.Display);
                    loadedAssemblies.Add(assembly.WrappedAssembly);
                }


                {
                    foreach (var fileName in _tranlationHelpers)
                    {
                        var assembly = comp.Sandbox.LoadByFullFilename(fileName).WrappedAssembly;
                        /*
                            var a = Assembly.LoadFile(i);
                             */
                        // ReSharper disable once UnusedVariable
                        var an = assembly.GetName();
                        Console.WriteLine(" Add translation helper {0}", assembly.FullName);
                        comp.TranslationAssemblies.Add(assembly);
                    }
                }

                comp.TranslationAssemblies.Add(typeof(Php.Framework.Extension).Assembly);
                comp.TranslationAssemblies.Add(typeof(Php.Compiler.Translator.Translator).Assembly);


                //  comp.TranslationAssemblies.Add(typeof(Lang.Php.Wp.Compile.ModuleProcessor).Assembly);

                // DebugDisplayReferences(comp);
                var emitResult = comp.Compile2PhpAndEmit(_outDir, loadedAssemblies, _referencedPhpLibsLocations);


                if (emitResult.Success) return;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Compilation errors:");
                Console.ResetColor();
                foreach (var i in emitResult.Diagnostics)
                    WriteCompileError(i);
            }
        }

        #endregion Methods

        #region Static Properties

        private static string ExeDir
        {
            get
            {
                var ea = new Uri(Assembly.GetExecutingAssembly().CodeBase, UriKind.RelativeOrAbsolute);
                var fi = new FileInfo(ea.LocalPath.Replace("/", "\\"));
                return fi.DirectoryName;
            }
        }

        #endregion Static Properties

        /*
        public static string SlnDir
        {
            get
            {
                var di = new DirectoryInfo(ExeDir);
                return di.Parent != null && di.Parent.Parent != null && di.Parent.Parent.Parent != null
                    ? di.Parent.Parent.Parent.FullName
                    : null;
            }
        }
*/
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-03 11:57
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs2Php
{
    public partial class CompilerEngine
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public CompilerEngine()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##CsProject## ##OutDir## ##Referenced## ##TranlationHelpers## ##ReferencedPhpLibsLocations## ##Configuration##
        implement ToString CsProject=##CsProject##, OutDir=##OutDir##, Referenced=##Referenced##, TranlationHelpers=##TranlationHelpers##, ReferencedPhpLibsLocations=##ReferencedPhpLibsLocations##, Configuration=##Configuration##
        implement equals CsProject, OutDir, Referenced, TranlationHelpers, ReferencedPhpLibsLocations, Configuration
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności CsProject; 
        /// </summary>
        public const string PropertyNameCsProject = "CsProject";
        /// <summary>
        /// Nazwa własności OutDir; 
        /// </summary>
        public const string PropertyNameOutDir = "OutDir";
        /// <summary>
        /// Nazwa własności Referenced; 
        /// </summary>
        public const string PropertyNameReferenced = "Referenced";
        /// <summary>
        /// Nazwa własności TranlationHelpers; 
        /// </summary>
        public const string PropertyNameTranlationHelpers = "TranlationHelpers";
        /// <summary>
        /// Nazwa własności ReferencedPhpLibsLocations; Location of referenced libraries in PHP, taken from compiler commandline option
        /// </summary>
        public const string PropertyNameReferencedPhpLibsLocations = "ReferencedPhpLibsLocations";
        /// <summary>
        /// Nazwa własności Configuration; i.e. DEBUG, RELEASE
        /// </summary>
        public const string PropertyNameConfiguration = "Configuration";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string CsProject
        {
            get
            {
                return _csProject;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _csProject = value;
            }
        }
        private string _csProject = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string OutDir
        {
            get
            {
                return _outDir;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _outDir = value;
            }
        }
        private string _outDir = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<string> Referenced
        {
            get
            {
                return _referenced;
            }
            set
            {
                _referenced = value;
            }
        }
        private List<string> _referenced = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        public List<string> TranlationHelpers
        {
            get
            {
                return _tranlationHelpers;
            }
            set
            {
                _tranlationHelpers = value;
            }
        }
        private List<string> _tranlationHelpers = new List<string>();
        /// <summary>
        /// Location of referenced libraries in PHP, taken from compiler commandline option
        /// </summary>
        public Dictionary<string, string> ReferencedPhpLibsLocations
        {
            get
            {
                return _referencedPhpLibsLocations;
            }
            set
            {
                _referencedPhpLibsLocations = value;
            }
        }
        private Dictionary<string, string> _referencedPhpLibsLocations = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        /// <summary>
        /// i.e. DEBUG, RELEASE
        /// </summary>
        public string Configuration
        {
            get
            {
                return _configuration;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _configuration = value;
            }
        }
        private string _configuration = "RELEASE";
        #endregion Properties
    }
}
