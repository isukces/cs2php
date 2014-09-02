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

    public partial class CompilerEngine
    {
        #region Static Methods

        // Public Methods 

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
                throw new Exception(string.Format("File {0} doesn't exist", csProject));
        }

        public void Compile()
        {
            var comp = new Cs2PhpCompiler
            {
                VerboseToConsole = true,
                ThrowExceptions = true
            };
            // Console.WriteLine("Try to load " + csProject);
            comp.LoadProject(csProject, configuration);

            Console.WriteLine("Preparing before compilation");
            /*
              #region Remove Lang.Php reference
              {
                  // ... will be replaced by reference to dll from compiler base dir
                  // I know - compilation libraries should be loaded into separate application domain
                  var remove = comp.Project.MetadataReferences.FirstOrDefault(i => i.Display.EndsWith("Lang.Php.dll"));
                  if (remove != null)
                      comp.RemoveMetadataReferences(remove);
              }
              #endregion
              string[] filenames;
              #region We have to remove and add again references - strange
              {
                  // in other cases some referenced libraries are ignored
                  List<MetadataFileReference> refToRemove = comp.Project.MetadataReferences.OfType<MetadataFileReference>().ToList();
                  foreach (var i in refToRemove)
                      comp.RemoveMetadataReferences(i);
                  var ref1 = refToRemove.Select(i => i.FilePath).Union(referenced).ToList();
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
               */
            // Dictionary<string, KnownConstInfo> referencedLibsPaths = new Dictionary<string, KnownConstInfo>();
            using (var sandbox = new AssemblySandbox())
            {
             
                var loadedAssemblies = new List<Assembly>();
                foreach (var reference in comp.Project.MetadataReferences)
                {
                    // var assemblyName = AssemblyName.GetAssemblyName(reference.Display);

                    // var assembly = Assembly.LoadFrom(reference.Display);
                    var assembly = sandbox.LoadByFullPath(reference.Display);
                    loadedAssemblies.Add(assembly);
                }




                {
                    foreach (var fileName in tranlationHelpers)
                    {
                        var assembly = sandbox.LoadByFullPath(fileName);
                        /*
                        var a = Assembly.LoadFile(i);
                         */
                        var an = assembly.GetName();
                        Console.WriteLine(" Add translation helper {0}", assembly.FullName);
                        comp.TranslationAssemblies.Add(assembly);
                    }
                }
                comp.TranslationAssemblies.Add(typeof(Php.Framework.Extension).Assembly);
                comp.TranslationAssemblies.Add(typeof(Php.Compiler.Translator.Translator).Assembly);

                //  comp.TranslationAssemblies.Add(typeof(Lang.Php.Wp.Compile.ModuleProcessor).Assembly);

                // DebugDisplayReferences(comp);
                var emitResult = comp.Compile2PhpAndEmit(sandbox, outDir, loadedAssemblies, referencedPhpLibsLocations);


                if (emitResult.Success) return;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Compilation errors:");
                Console.ResetColor();
                foreach (var i in emitResult.Diagnostics)
                    WriteCompileError(i);
            }
        }
        // Private Methods 

        /// <summary>
        /// Zamienia deklarowane referencje na te, które są w katalogu aplikacji
        /// </summary>
        /// <param name="comp"></param>
        private static void Swap(Cs2PhpCompiler comp)
        {
            var files = new DirectoryInfo(ExeDir).GetFiles("*.*");
            var metadataFileReferences = comp.Project.MetadataReferences.OfType<MetadataFileReference>().Select(reference => new
            {
                Reference = reference,
                FileShortName = new FileInfo(reference.FilePath).Name
            }).ToArray();

            /*
            var q= from i in comp.Project.MetadataReferences.OfType<MetadataFileReference>()
                   // let FileShortName = new FileInfo(i.FilePath).Name
                   join f in files on new FileInfo(i.FilePath).Name.ToLower() equals  f.Name.ToLower()
                   select 
            */
            foreach (var fileReference in metadataFileReferences)
            {
                var o = files.FirstOrDefault(i => i.Name.Equals(fileReference.FileShortName, StringComparison.OrdinalIgnoreCase));
                if (o == null) continue;
                var remove = fileReference.Reference;
                var add = new MetadataFileReference(o.FullName, MetadataReferenceProperties.Assembly);
                if (remove.Display == add.Display)
                    continue;
                comp.RemoveMetadataReferences(remove);
                comp.AddMetadataReferences(add);
                Console.WriteLine("Swap\r\n    {0}\r\n    {1}", remove.Display, add.Display);
            }
        }

        #endregion Methods

        #region Static Properties

        public static string ExeDir
        {
            get
            {
                var ea = new Uri(Assembly.GetExecutingAssembly().CodeBase, UriKind.RelativeOrAbsolute);
                var fi = new FileInfo(ea.LocalPath.Replace("/", "\\"));
                return fi.DirectoryName;
            }
        }

        public static string SlnDir
        {
            get
            {
                var di = new DirectoryInfo(ExeDir);
                return di.Parent.Parent.Parent.FullName;
            }
        }

        #endregion Static Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-01-01 18:39
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
        public const string PROPERTYNAME_CSPROJECT = "CsProject";
        /// <summary>
        /// Nazwa własności OutDir; 
        /// </summary>
        public const string PROPERTYNAME_OUTDIR = "OutDir";
        /// <summary>
        /// Nazwa własności Referenced; 
        /// </summary>
        public const string PROPERTYNAME_REFERENCED = "Referenced";
        /// <summary>
        /// Nazwa własności TranlationHelpers; 
        /// </summary>
        public const string PROPERTYNAME_TRANLATIONHELPERS = "TranlationHelpers";
        /// <summary>
        /// Nazwa własności ReferencedPhpLibsLocations; Location of referenced libraries in PHP, taken from compiler commandline option
        /// </summary>
        public const string PROPERTYNAME_REFERENCEDPHPLIBSLOCATIONS = "ReferencedPhpLibsLocations";
        /// <summary>
        /// Nazwa własności Configuration; i.e. DEBUG, RELEASE
        /// </summary>
        public const string PROPERTYNAME_CONFIGURATION = "Configuration";
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
                return csProject;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                csProject = value;
            }
        }
        private string csProject = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string OutDir
        {
            get
            {
                return outDir;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                outDir = value;
            }
        }
        private string outDir = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<string> Referenced
        {
            get
            {
                return referenced;
            }
            set
            {
                referenced = value;
            }
        }
        private List<string> referenced = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        public List<string> TranlationHelpers
        {
            get
            {
                return tranlationHelpers;
            }
            set
            {
                tranlationHelpers = value;
            }
        }
        private List<string> tranlationHelpers = new List<string>();
        /// <summary>
        /// Location of referenced libraries in PHP, taken from compiler commandline option
        /// </summary>
        public Dictionary<string, string> ReferencedPhpLibsLocations
        {
            get
            {
                return referencedPhpLibsLocations;
            }
            set
            {
                referencedPhpLibsLocations = value;
            }
        }
        private Dictionary<string, string> referencedPhpLibsLocations = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        /// <summary>
        /// i.e. DEBUG, RELEASE
        /// </summary>
        public string Configuration
        {
            get
            {
                return configuration;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                configuration = value;
            }
        }
        private string configuration = "RELEASE";
        #endregion Properties

    }
}
