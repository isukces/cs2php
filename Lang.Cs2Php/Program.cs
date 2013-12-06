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
        -r filename      : csproject referenced library
        -t filename      : cs2php translation helper");
        }

        static void Main(string[] args)
        {
            AssemblyLoader.Init();
            bool showUsage = true;
            Console.Write("        ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("C# to Php");
            Console.ResetColor();
            Console.WriteLine(" compiler ver. {0}", typeof(Program).Assembly.GetName().Version);
            CompilerEngine engine = new CompilerEngine();
            try
            {
                List<string> files = new List<string>();
                string command = "";
                foreach (var arg in args)
                {
                    if (arg.StartsWith("-"))
                    {
                        command = arg.Substring(1).ToLower();
                    }
                    else
                    {
                        var fi = new FileInfo(arg);
                        var fileName = fi.FullName;

                        if (string.IsNullOrEmpty(command))
                            files.Add(fileName);
                        else
                        {
                            switch (command)
                            {
                                case "r":
                                    if (!File.Exists(fileName))
                                        throw new Exception("Referenced library " + fileName + " doesn't exist");
                                    engine.Referenced.Add(fileName);
                                    command = null;
                                    break;
                                case "t":
                                    if (!File.Exists(fileName))
                                        throw new Exception("Referenced library " + fileName + " doesn't exist");
                                    engine.TranlationHelpers.Add(fileName);
                                    command = null;
                                    break;
                                default:
                                    throw new Exception("Unknown option " + command);
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(command))
                    throw new Exception("command " + command + " has no parameter(s)");


                if (files.Count < 2)
                    throw new Exception("Invalid input options, unknown csproj file or output directory");
                if (files.Count > 2)
                    throw new Exception("Unknown parameter " + files[2]);
                engine.CsProject = files.First();
                engine.OutDir = files.Last();
                engine.Check();
                showUsage = false;
                engine.Compile("RELEASE");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success");
                Console.ResetColor();

               
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error:");
                Console.ResetColor();
                Console.WriteLine("   " + ex.Message + "\r\n");
                if (showUsage)
                    Usage();
              
            }
            Console.WriteLine("press any key...");
            Console.ReadKey();
        }


    }
}
