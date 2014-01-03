using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lang.Cs2Php
{
    class Program
    {
        #region Static Methods

        // Private Methods 

        static void Main(string[] args)
        {
            AssemblyLoader.Init();
            bool showUsage = true;
            Console.Write("        ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("C# to Php");
            Console.ResetColor();
            Console.WriteLine(" compiler ver. {0}", typeof(Program).Assembly.GetName().Version);

            try
            {
                ArgumentProcessingContext cfg = new ArgumentProcessingContext();

                cfg.Parse(args);



                if (cfg.files.Count < 2)
                    throw new Exception("Invalid input options, unknown csproj file or output directory");
                if (cfg.files.Count > 2)
                    throw new Exception("Unknown parameter " + cfg.files[2]);
                cfg.engine.CsProject = cfg.files.First();
                cfg.engine.OutDir = cfg.files.Last();
                cfg.engine.Check();
                showUsage = false;
                cfg.engine.Compile();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success");
                Console.ResetColor();


            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:");
                Console.ResetColor();
                while (ex != null)
                {
                    Console.WriteLine("   " + ex.Message + "\r\n");
                    ex = ex.InnerException;
                }
                // Console.WriteLine("   " + ex.StackTrace + "\r\n");
                if (showUsage)
                    Usage();

            }
            Console.WriteLine("press any key...");
            Console.ReadKey();
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
