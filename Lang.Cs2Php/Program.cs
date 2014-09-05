using System;
using System.Diagnostics;
using System.Linq;
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



                DoCompilation(processingContext.Engine, ref showUsage);

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

        private static void DoCompilation(ConfigData aa, ref  bool showUsage)
        {
            string domainName = "sandbox" + Guid.NewGuid();
            var domainSetup = new AppDomainSetup
            {
                ApplicationName = domainName,
                ApplicationBase = Environment.CurrentDirectory
            };
            var appDomain = AppDomain.CreateDomain(domainName, null, domainSetup);
            try
            {
                var wrapperType = typeof(CompilerEngine);
                var ce = (CompilerEngine)appDomain.CreateInstanceFrom(
                    wrapperType.Assembly.Location,
                    wrapperType.FullName).Unwrap();

                //public static void CopyFrom(ref IConfigData x, IConfigData s)
                {
                    ce.CopyFrom(aa);
                    Debug.Assert(aa.Referenced.Count == ce.Referenced.Count);
                    Debug.Assert(aa.TranlationHelpers.Count == ce.TranlationHelpers.Count);
                    Debug.Assert(aa.ReferencedPhpLibsLocations.Count == ce.ReferencedPhpLibsLocations.Count);
                }
                ce.Check();
                showUsage = false;
                ce.Compile();
            }
            finally
            {

            }
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
