using System;
using System.Diagnostics;
using System.Linq;
using Lang.Php;
using Lang.Php.Compiler;

namespace Lang.Cs2Php
{
    class Program
    {
        #region Static Methods

        // Private Methods 

        static void Main(string[] args)
        {
            AssemblySandbox.Init();
            var showUsage = true;
            Console.Write("        ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("C# to Php");
            Console.ResetColor();
            Console.WriteLine(" compiler ver. {0}", typeof(Program).Assembly.GetName().Version);
            Console.WriteLine(" Lang.Php ver. {0}", typeof(RequiredTranslatorAttribute).Assembly.GetName().Version);

            try
            {
                var processingContext = new ArgumentProcessingContext();
                processingContext.Parse(args);
                if (processingContext.files.Count < 2)
                    throw new Exception("Invalid input options, unknown csproj file or output directory");
                if (processingContext.files.Count > 2)
                    throw new Exception("Unknown parameter " + processingContext.files[2]);
                processingContext.Engine.CsProject = processingContext.files.First();
                processingContext.Engine.OutDir = processingContext.files.Last();


                using (var a = new AppConfigManipulator())
                {
                    DoCompilation(processingContext.Engine, ref showUsage);
                }


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success");
                Console.ResetColor();


            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:");
                Console.ResetColor();
                while (exception != null)
                {
                    Console.WriteLine("   " + exception.Message + "\r\n");
                    exception = exception.InnerException;
                }
                if (showUsage)
                    Usage();
            }
            Console.WriteLine("press any key...");
            Console.ReadKey();
        }

        private static void DoCompilation(ConfigData cfg, ref  bool showUsage)
        {
            var showUsage1 = showUsage;
            CompilerEngine.ExecuteInSeparateAppDomain(
                ce =>
                {
                    ce.Configuration = cfg.Configuration;
                    ce.CsProject = cfg.CsProject;
                    ce.OutDir = cfg.OutDir;
                    ce.Referenced.Clear();
                    ce.TranlationHelpers.Clear();
                    ce.ReferencedPhpLibsLocations.Clear();

                    // src and dest can be in different application domain
                    // we need to add item by item
                    ce.Set1(cfg.Referenced.ToArray(),
                        cfg.TranlationHelpers.ToArray(),
                        cfg.ReferencedPhpLibsLocations.Select(a => a.Key + "\n" + a.Value).ToArray()
                        );
                    ce.BinaryOutputDir = cfg.BinaryOutputDir;
                    Debug.Assert(ce.Referenced.Count == cfg.Referenced.Count);
                    Debug.Assert(ce.TranlationHelpers.Count == cfg.TranlationHelpers.Count);
                    Debug.Assert(ce.ReferencedPhpLibsLocations.Count == cfg.ReferencedPhpLibsLocations.Count);
                    //ce.CopyFrom(aa);
                    ce.Check();
                    showUsage1 = false;
                    ce.Compile();
                });
            showUsage = showUsage1;
        }

        static void Usage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Usage:");
            Console.ResetColor();
            Console.WriteLine(@"cs2php csproj-file-path output-dir options
    where
        csproj-file-path : full path to c# project file
        output-dir       : output directory
    options
        -conf filename   : project configuration DEBUG or RELEASE
        -f filename      : process config file
        -r filename      : csproject referenced library
        -t filename      : cs2php translation helper");
        }

        #endregion Static Methods
    }
}
