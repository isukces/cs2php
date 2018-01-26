using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lang.Php.Compiler.Source
{
    public class PhpCodeModuleName : PhpSourceBase, IEquatable<PhpCodeModuleName>
    {
        /// <summary>
        ///     Creates instance of modulename not related to any .NET class (i.e. for config code)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="assemblyInfo"></param>
        public PhpCodeModuleName(string name, AssemblyTranslationInfo assemblyInfo)
        {
            AssemblyInfo = assemblyInfo ?? throw new ArgumentNullException(nameof(assemblyInfo));
            Name         = name ?? throw new ArgumentNullException(nameof(name));
        }

        public PhpCodeModuleName(Type type, AssemblyTranslationInfo assemblyInfo,
            ClassTranslationInfo      declaringTypeInfo)
        {
            if ((object)type == null)
                throw new ArgumentNullException(nameof(type));
            if ((object)type.DeclaringType != null && declaringTypeInfo == null)
                throw new ArgumentNullException(nameof(declaringTypeInfo));

            AssemblyInfo = assemblyInfo ?? throw new ArgumentNullException(nameof(assemblyInfo));

            {
                if (type.FullName == null)
                    Name = type.Name;
                else
                    Name = type.FullName.Replace(".", "_").Replace("+", "__").Replace("<", "__").Replace(">", "__");
                // take module name from parent, this can be overrided if nested class is decorated with attributes
                if (declaringTypeInfo != null && declaringTypeInfo.ModuleName != null)
                    Name = declaringTypeInfo.ModuleName.Name;
                var ats  = type.GetCustomAttributes(false);
                {
                    // ModuleAttribute
                    var moduleAttribute = type.GetCustomAttribute<ModuleAttribute>();
                    if (moduleAttribute != null)
                    {
                        Name                      = moduleAttribute.ModuleShortName;
                        OptionalIncludePathPrefix = moduleAttribute.IncludePathPrefix;
                    }
                }

                {
                    // PageAttribute
                    var pageAttribute = type.GetCustomAttribute<PageAttribute>();
                    if (pageAttribute != null)
                        Name = pageAttribute.ModuleShortName;
                }
            }
        }

        private PhpCodeModuleName()
        {
        }

        /// <summary>
        ///     Used for patch only
        /// </summary>
        public static bool IsFrameworkName(PhpCodeModuleName name)
        {
            var list = "commonlanguageruntimelibrary".Split(' ');
            return list.Contains(name.Library);
        }

        /// <summary>
        ///     Realizuje operator ==
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są równe</returns>
        public static bool operator ==(PhpCodeModuleName left, PhpCodeModuleName right)
        {
            if (left == (object)null && right == (object)null) return true;
            if (left == (object)null || right == (object)null) return false;
            return left.Library == right.Library && left._name == right._name;
        }

        /// <summary>
        ///     Realizuje operator !=
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są różne</returns>
        public static bool operator !=(PhpCodeModuleName left, PhpCodeModuleName right)
        {
            if (left == (object)null && right == (object)null) return false;
            if (left == (object)null || right == (object)null) return true;
            return left.Library != right.Library || left._name != right._name;
        }
        // Private Methods 

        private static string ProcessPath(string name, string relatedTo)
        {
            var p1     = Split(name);
            var p2     = Split(relatedTo);
            var common = 0;
            for (int i = 0, max = Math.Min(p1.Length, p2.Length); i < max; i++)
                if (p1[i] == p2[i])
                    common++;
                else
                    break;
            if (common > 0)
            {
                p1 = p1.Skip(common).ToArray();
                p2 = p2.Skip(common).ToArray();
            }

            var aa = new List<string>();
            for (var i = 0; i < p2.Length - 1; i++)
                aa.Add("..");
            aa.AddRange(p1);
            var g = string.Join("/", aa);
            //if (p1.Length == 1)
            //    return new PhpConstValue(Name + extension);
            return g;
        }

        // Private Methods 

        private static string[] Split(string name)
        {
            if (name.Contains("\\"))
                name = name.Replace("\\", "/");
            var p1   = ("/" + name).Split('/').Select(a => a.Trim()).Where(a => !string.IsNullOrEmpty(a)).ToArray();
            return p1;
        }


        /// <summary>
        ///     Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="other">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(PhpCodeModuleName other)
        {
            return other == this;
        }

        /// <summary>
        ///     Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="other">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public override bool Equals(object other)
        {
            if (!(other is PhpCodeModuleName)) return false;
            return Equals((PhpCodeModuleName)other);
        }

        /// <summary>
        ///     Zwraca kod HASH obiektu
        /// </summary>
        /// <returns>kod HASH obiektu</returns>
        public override int GetHashCode()
        {
            // Good implementation suggested by Josh Bloch
            var hash = 17;
            hash     = hash * 31 + (Library == (object)null ? 0 : Library.GetHashCode());
            hash     = hash * 31 + _name.GetHashCode();
            return hash;
        }

        // Public Methods 

        public string MakeEmitPath(string basePath, int dupa)
        {
            var p = Path.Combine(basePath, Name.Replace("/", "\\") + _extension);
            return p;
        }

        public IPhpValue MakeIncludePath(PhpCodeModuleName relatedTo)
        {
            if (relatedTo.Library == Library)
            {
                var knownPath = ProcessPath(_name + _extension, relatedTo._name + _extension);
                //dirname(__FILE__)
                var __FILE__ = new PhpDefinedConstExpression("__FILE__", null);
                var dirname  = new PhpMethodCallExpression("dirname", __FILE__);
                var path     = new PhpConstValue(PathUtil.MakeUnixPath(PathUtil.UNIX_SEP + knownPath));
                var result   = PhpBinaryOperatorExpression.ConcatStrings(dirname, path);
                return result;
            }
            else
            {
                string path      = null;
                string pathRelTo = null;
                if (PhpIncludePathExpression is PhpConstValue)
                {
                    path = (PhpIncludePathExpression as PhpConstValue).Value as string;
                    if (path == null)
                        throw new NotSupportedException();
                }
                else
                {
                    return PhpIncludePathExpression; // assume expression like MPDF_LIB_PATH . 'lib/mpdf/mpdf.php'
                }

                if (relatedTo.PhpIncludePathExpression is PhpConstValue)
                {
                    pathRelTo = (relatedTo.PhpIncludePathExpression as PhpConstValue).Value as string;
                    if (pathRelTo == null)
                        throw new NotSupportedException();
                }

                if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(path))
                {
                    var knownPath = ProcessPath(path, pathRelTo);
                    return new PhpConstValue(knownPath);
                }

                throw new NotSupportedException();              
            }
        }

        /// <summary>
        ///     Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}@{1}", _name, Library);
        }

        private void UpdateIncludePathExpression()
        {
            if (AssemblyInfo == null)
            {
                PhpIncludePathExpression = null;
                return;
            }

            var pathItems = new List<IPhpValue>();
            {
                var assemblyPath = AssemblyInfo.PhpIncludePathExpression;
                if (assemblyPath != null)
                    pathItems.Add(assemblyPath);
            }
            if (OptionalIncludePathPrefix != null && OptionalIncludePathPrefix.Any())
                foreach (var n in OptionalIncludePathPrefix)
                    if (n.StartsWith("$"))
                        pathItems.Add(new PhpVariableExpression(n, PhpVariableKind.Global));
                    else
                        pathItems.Add(new PhpDefinedConstExpression(n, null));
            pathItems.Add(new PhpConstValue(_name + Extension));
            PhpIncludePathExpression = PhpBinaryOperatorExpression.ConcatStrings(pathItems.ToArray());
        }

        public static PhpCodeModuleName Cs2PhpConfigModuleName
        {
            get
            {
                var a = new PhpCodeModuleName {Name = CS2PHP_CONFIG_MODULE_NAME};
                return a;
            }
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public AssemblyTranslationInfo AssemblyInfo { get; }

        /// <summary>
        ///     Library name for containing assembly; własność dopuszcza wartości NULL i jest tylko do odczytu.
        /// </summary>
        public string Library => AssemblyInfo?.LibraryName;

        /// <summary>
        ///     Module name without library prefix
        /// </summary>
        public string Name
        {
            get => _name;
            private set
            {
                value = (value ?? string.Empty).Trim();
                value = value.Replace("\\", "/");
                if (value == _name) return;
                _name = value;
                UpdateIncludePathExpression();
            }
        }

        /// <summary>
        ///     defined const or variables to include before module name
        /// </summary>
        public string[] OptionalIncludePathPrefix { get; set; }

        /// <summary>
        ///     rozszerzenie nazwy pliku
        /// </summary>
        public string Extension
        {
            get => _extension;
            set => _extension = (value ?? string.Empty).Trim();
        }

        /// <summary>
        ///     Expression with complete path to this module
        /// </summary>
        public IPhpValue PhpIncludePathExpression { get; set; }

        /// <summary>
        ///     Indicated that name is empty; własność jest tylko do odczytu.
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(_name);

        private string _name      = string.Empty;
        private string _extension = ".php";

        /// <summary>
        ///     Late binging module
        /// </summary>
        public const string CS2PHP_CONFIG_MODULE_NAME = "*cs2phpconfig*";
    }
}