using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lang.Cs2Php
{
    class ArgumentProcessingContext
    {
        #region Constructors

        public ArgumentProcessingContext()
        {
#if DEBUG
            engine.Configuration = "DEBUG";
#else
            engine.Configuration = "RELEASE";
#endif
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public void Parse(string[] args)
        {

            foreach (var arg in args)
                ProcessArgument(arg);
            if (!string.IsNullOrEmpty(command))
                throw new Exception("command " + command + " has no parameter(s)");

        }

        public string ResolveFilename(string filename)
        {
            bool doThisAgain ;
            do
            {
                doThisAgain = false;
                Regex re = new Regex("(%([A-Z]+)%)", RegexOptions.IgnoreCase);
                filename = re.Replace(filename, (m) =>
                {
                    string t;
                    names.TryGetValue(m.Groups[2].Value, out t);
                    doThisAgain = true;
                    return t;
                });
               
            } while (doThisAgain);
            
            if (!string.IsNullOrEmpty(currentDir))
                filename = Path.Combine(currentDir, filename);
            filename = new FileInfo(filename).FullName;
            return filename;
        }
        // Private Methods 

        private void ProcessArgument(string arg)
        {
            if (arg.StartsWith("-"))
            {
                command = arg.Substring(1).ToLower();
                return;
            }
            if (string.IsNullOrEmpty(command))
            {
                files.Add(ResolveFilename(arg));
                return;
            }
            switch (command)
            {
                case "r":
                    var fileName = ResolveFilename(arg);
                    if (!File.Exists(fileName))
                        throw new Exception("Referenced library " + fileName + " doesn't exist");
                    engine.Referenced.Add(fileName);
                    command = null;
                    break;
                case "t":
                    fileName = ResolveFilename(arg);
                    if (!File.Exists(fileName))
                        throw new Exception("Referenced library " + fileName + " doesn't exist");
                    engine.TranlationHelpers.Add(fileName);
                    command = null;
                    break;
                case "lib":
                    {
                        var a = arg.IndexOf("=");
                        if (a < 0)
                            throw new Exception("Invalid data for 'lib' option. Use 'lib libraryname=path'.");
                        var lib = arg.Substring(0, a).Trim();
                        var path = arg.Substring(a + 1).Trim();
                        engine.LibraryPath[lib] = ResolveFilename(path);
                    }
                    command = null;
                    break;
                case "conf":
                    engine.Configuration = arg;
                    command = null;
                    break;
                case "f":
                    ProcessFile(ResolveFilename(arg));
                    command = null;
                    break;
                default:
                    throw new Exception("Unknown option " + command);

            }
        }

        void ProcessFile(string filename)
        {
            var c = command; command = "";
            var cd = currentDir;
            try
            {
                currentDir = new FileInfo(filename).DirectoryName;
                var lines = (from i in File.ReadAllLines(filename)
                             let it = (i ?? "").Trim()
                             where !string.IsNullOrEmpty(it) && !it.StartsWith(";")
                             select it).ToArray();
                // set FORMSBIN=c:\programs\_CS2PHP\
                foreach (var line in lines)
                {
                    if (line.StartsWith("set "))
                    {
                        Regex re = new Regex("^set\\s+([A-Z]+)\\s*=\\s*(.*)$", RegexOptions.IgnoreCase);
                        var m = re.Match(line);
                        if (!m.Success)
                            throw new Exception("Syntax error in \r\n" + line);
                        names[m.Groups[1].Value.Trim()] = m.Groups[2].Value.Trim();
                        continue;
                    }
                    if (line.StartsWith("-"))
                    {
                        Regex re = new Regex("^(-[A-Z]+)\\s+(.*)$", RegexOptions.IgnoreCase);
                        var m = re.Match(line);
                        if (!m.Success)
                            throw new Exception("Syntax error in \r\n" + line);
                        ProcessArgument(m.Groups[1].Value);
                        ProcessArgument(m.Groups[2].Value);
                    }
                    else
                    {
                        ProcessArgument(line);
                    }
                }
            }
            finally
            {
                command = c;
                currentDir = cd;
            }
        }

        #endregion Methods

        #region Fields

        string command = "";
        string currentDir;
        public CompilerEngine engine = new CompilerEngine();
        public List<string> files = new List<string>();
        Dictionary<string, string> names = new Dictionary<string, string>();

        #endregion Fields
    }
}
