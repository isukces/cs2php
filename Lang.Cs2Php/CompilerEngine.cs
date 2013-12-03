using Lang.Php.Compiler;
using Roslyn.Compilers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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
    smartClassEnd
    */
    
    public partial class CompilerEngine
    {
        #region Methods

        // Public Methods 
        public void Check()
        {
            if (!File.Exists(CsProject))
                throw new Exception(string.Format("File {0} doesn't exist", csProject));
        }


        /// <summary>
        /// Zamienia deklarowane referencje na te, które są w katalogu aplikacji
        /// </summary>
        /// <param name="comp"></param>
        private void Swap(Cs2PhpCompiler comp)
        {
            var aaa = typeof(Program).Assembly.GetReferencedAssemblies();
            var bbb = new DirectoryInfo( ExeDir).GetFiles("*.*");
            var h = comp.Project.MetadataReferences.OfType<MetadataFileReference>().Select(i => new { REF = i, fn = new FileInfo(i.FullPath).Name.ToLower() }).ToArray();

            foreach (var hh in h)
            {
                var o = bbb.Where(i => i.Name.ToLower() == hh.fn).FirstOrDefault();
                if (o != null)
                {
                    var remove = hh.REF;
                    var add = new MetadataFileReference(o.FullName, MetadataReferenceProperties.Assembly);
                    if (remove.Display == add.Display)
                        continue;
                    comp.RemoveMetadataReferences(remove);
                    comp.AddMetadataReferences(add);
                    Console.WriteLine("Swap\r\n    {0}\r\n    {1}", remove.Display, add.Display);
                }
            }
        }
        public void Compile()
        {
            Cs2PhpCompiler comp = new Cs2PhpCompiler();
            comp.VerboseToConsole = true;
            comp.ThrowExceptions = true;
            comp.LoadProject(Path.Combine(SlnDir, csProject));

            Console.WriteLine("Preparing before compilation");


            var ref_Lang_PHP = new MetadataFileReference(Path.Combine(ExeDir, "Lang.Php.dll"), MetadataReferenceProperties.Assembly);
            // var ref_Lang_PHP_Utils = new MetadataFileReference(Path.Combine(SlnDir, @"Lang.Php.Utils\bin\Debug\Lang.Php.Utils.dll"), MetadataReferenceProperties.Assembly);
            var remove = comp.Project.MetadataReferences.Where(i => i.Display.EndsWith("Lang.Php.dll")).Single();
            comp.RemoveMetadataReferences(remove);
            comp.AddMetadataReferences(ref_Lang_PHP);

            foreach (var i in referenced)
            {
                var g = new MetadataFileReference(i, MetadataReferenceProperties.Assembly);
                comp.AddMetadataReferences(g);
            }
            Swap(comp);

            {
                foreach (var i in tranlationHelpers)
                {
                    var a = Assembly.LoadFile(i);
                    var an = a.GetName();
                    Console.WriteLine(" Add tranlation helper {0}", a.FullName);
                    comp.TranslationAssemblies.Add(a);
                }
            }
            comp.TranslationAssemblies.Add(typeof(Lang.Php.Framework.Extension).Assembly);
            comp.TranslationAssemblies.Add(typeof(Lang.Php.Compiler.Translator.Translator).Assembly);
           
            //  comp.TranslationAssemblies.Add(typeof(Lang.Php.Wp.Compile.ModuleProcessor).Assembly);

            /// DebugDisplayReferences(comp);
            var e = comp.Compile2PhpAndEmit(outDir);
            if (!e.Success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Compilation errors:");
                Console.ResetColor();
                foreach (var i in e.Diagnostics)
                    WriteCompileError(i);
            }
        }

        public static void WriteCompileError(Roslyn.Compilers.CSharp.Diagnostic diag)
        {
            var info = diag.Info;
            switch (info.Severity)
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
            Console.WriteLine(": "+info.GetMessage());
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


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-16 15:03
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
        implement ToString ##CsProject## ##OutDir## ##Referenced## ##TranlationHelpers##
        implement ToString CsProject=##CsProject##, OutDir=##OutDir##, Referenced=##Referenced##, TranlationHelpers=##TranlationHelpers##
        implement equals CsProject, OutDir, Referenced, TranlationHelpers
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
        #endregion Properties

    }
}
