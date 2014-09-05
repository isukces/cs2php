using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lang.Cs2Php
{
    class ArgumentProcessingContext
    {
        #region Constructors

        public ArgumentProcessingContext()
        {
#if DEBUG
            _engine.Configuration = "DEBUG";
#else
            _engine.Configuration = "RELEASE";
#endif
        }

        public ConfigData Engine
        {
            get { return _engine; }
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public void Parse(IEnumerable<string> args)
        {

            foreach (var arg in args)
                ProcessArgument(arg);
            if (!string.IsNullOrEmpty(_command))
                throw new Exception("command " + _command + " has no parameter(s)");

        }
        // Private Methods 

        private void ProcessArgument(string arg)
        {
            if (arg.StartsWith("-"))
            {
                _command = arg.Substring(1).ToLower();
                return;
            }
            if (string.IsNullOrEmpty(_command))
            {
                files.Add(ResolveFilename(arg));
                return;
            }
            switch (_command)
            {

                case "r":
                    var fileName = ResolveFilename(arg);
                    if (!File.Exists(fileName))
                        throw new Exception("Referenced library " + fileName + " doesn't exist");
                    _engine.Referenced.Add(fileName);
                    _command = null;
                    break;
                case "binout":
                    _engine.BinaryOutputDir = ResolveFilename(arg);
                    _command = null;
                    break;
                case "t":
                    fileName = ResolveFilename(arg);
                    if (!File.Exists(fileName))
                        throw new Exception("Referenced library " + fileName + " doesn't exist");
                    _engine.TranlationHelpers.Add(fileName);
                    _command = null;
                    break;
                case "lib":
                    {
                        var indexOfEqualSign = arg.IndexOf("=", StringComparison.Ordinal);
                        if (indexOfEqualSign < 0)
                            throw new Exception("Invalid data for 'lib' option. Use 'lib libraryname=path'.");
                        var lib = arg.Substring(0, indexOfEqualSign).Trim();
                        var path = arg.Substring(indexOfEqualSign + 1).Trim();
                        _engine.ReferencedPhpLibsLocations[lib] = ResolveFilename(path);
                    }
                    _command = null;
                    break;
                case "conf":
                    _engine.Configuration = arg;
                    _command = null;
                    break;
                case "f":
                    ProcessFile(ResolveFilename(arg));
                    _command = null;
                    break;
                default:
                    throw new Exception("Unknown option " + _command);

            }
        }

        void ProcessFile(string filename)
        {
            var c = _command; _command = "";
            var cd = _currentDir;
            try
            {
                _currentDir = new FileInfo(filename).DirectoryName;
                var lines = (from line in File.ReadAllLines(filename)
                             let lineTrimmed = (line ?? "").Trim()
                             where !string.IsNullOrEmpty(lineTrimmed) && !lineTrimmed.StartsWith(";")
                             select lineTrimmed).ToArray();
                // set FORMSBIN=c:\programs\_CS2PHP\
                foreach (var line in lines)
                {
                    if (line.StartsWith("set "))
                    {
                        var regex = new Regex("^set\\s+([A-Z]+)\\s*=\\s*(.*)$", RegexOptions.IgnoreCase);
                        var match = regex.Match(line);
                        if (!match.Success)
                            throw new Exception("Syntax error in \r\n" + line);
                        _names[match.Groups[1].Value.Trim()] = match.Groups[2].Value.Trim();
                        continue;
                    }
                    if (line.StartsWith("-"))
                    {
                        var regex = new Regex("^(-[A-Z]+)\\s+(.*)$", RegexOptions.IgnoreCase);
                        var match = regex.Match(line);
                        if (!match.Success)
                            throw new Exception("Syntax error in \r\n" + line);
                        ProcessArgument(match.Groups[1].Value);
                        ProcessArgument(match.Groups[2].Value);
                    }
                    else
                    {
                        ProcessArgument(line);
                    }
                }
            }
            finally
            {
                _command = c;
                _currentDir = cd;
            }
        }

        private string ResolveFilename(string filename)
        {
            bool doThisAgain;
            do
            {
                doThisAgain = false;
                var regex = new Regex("(%([A-Z]+)%)", RegexOptions.IgnoreCase);
                filename = regex.Replace(filename, match =>
                {
                    string t;
                    _names.TryGetValue(match.Groups[2].Value, out t);
                    doThisAgain = true;
                    return t;
                });

            } while (doThisAgain);

            if (!string.IsNullOrEmpty(_currentDir))
                filename = Path.Combine(_currentDir, filename);
            filename = new FileInfo(filename).FullName;
            return filename;
        }

        #endregion Methods

        #region Fields

        string _command = "";
        string _currentDir;
        private readonly ConfigData _engine = new ConfigData();
        readonly Dictionary<string, string> _names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public readonly List<string> files = new List<string>();

        #endregion Fields

        #region Nested Classes

        #endregion Nested Classes
    }
}
