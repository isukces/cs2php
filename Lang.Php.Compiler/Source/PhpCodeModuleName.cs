using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lang.Cs.Compiler.Sandbox;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Equals Library, Name
    
    property AssemblyInfo AssemblyTranslationInfo 
    	read only
    
    property Library string Library name for containing assembly
    	nullable
    	read only assemblyInfo == null ? null : assemblyInfo.LibraryName
    
    property Name string Module name without library prefix
    	preprocess value = value.Replace("\\", "/");
    	OnChange UpdateIncludePathExpression();
    
    property OptionalIncludePathPrefix string[] defined const or variables to include before module name
    
    property Extension string rozszerzenie nazwy pliku
    	init ".php"
    
    property PhpIncludePathExpression IPhpValue Expression with complete path to this module
    smartClassEnd
    */

    public partial class PhpCodeModuleName : PhpSourceBase
    {
        #region Constructors

        private PhpCodeModuleName()
        {

        }
        /// <summary>
        /// Creates instance of modulename not related to any .NET class (i.e. for config code)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="assemblyInfo"></param>
        public PhpCodeModuleName(string name, AssemblyTranslationInfo assemblyInfo)
        {
            if (assemblyInfo == null)
                throw new ArgumentNullException("assemblyInfo");
            if (name == null)
                throw new ArgumentNullException("name");

            this.assemblyInfo = assemblyInfo;
            this.Name = name;
        }
        public PhpCodeModuleName(Type type, AssemblyTranslationInfo assemblyInfo, ClassTranslationInfo declaringTypeInfo)
        {
            if (assemblyInfo == null)
                throw new ArgumentNullException("assemblyInfo");
            if ((object)type == null)
                throw new ArgumentNullException("type");
            if ((object)type.DeclaringType != null && declaringTypeInfo == null)
                throw new ArgumentNullException("declaringTypeInfo");

            this.assemblyInfo = assemblyInfo;

            {
                if (type.FullName == null)
                    Name = type.Name;
                else
                    Name = type.FullName.Replace(".", "_").Replace("+", "__").Replace("<", "__").Replace(">", "__");
                // take module name from parent, this can be overrided if nested class is decorated with attributes
                if (declaringTypeInfo != null)
                    Name = declaringTypeInfo.ModuleName.Name;
                var ats = type.GetCustomAttributes(false);

                #region ModuleAttribute
                {
                    ModuleAttribute _module = type.GetCustomAttribute<ModuleAttribute>();
                    if (_module != null)
                    {
                        Name = _module.ModuleShortName;
                        OptionalIncludePathPrefix = _module.IncludePathPrefix;
                    }
                }
                #endregion
                #region PageAttribute
                {
                    var _page = type.GetCustomAttribute<PageAttribute>();
                    if (_page != null)
                        Name = _page.ModuleShortName;
                }
                #endregion
            }

        }

        private void UpdateIncludePathExpression()
        {
            if (assemblyInfo == null)
            {
                PhpIncludePathExpression = null;
                return;
            }
            List<IPhpValue> pathItems = new List<IPhpValue>();
            {
                IPhpValue assemblyPath = assemblyInfo.PhpIncludePathExpression;
                if (assemblyPath != null)
                    pathItems.Add(assemblyPath);
            }
            if (optionalIncludePathPrefix != null && optionalIncludePathPrefix.Any())
                foreach (var n in optionalIncludePathPrefix)
                {
                    if (n.StartsWith("$"))
                        pathItems.Add(new PhpVariableExpression(n, PhpVariableKind.Global));
                    else
                        pathItems.Add(new PhpDefinedConstExpression(n, null));
                }
            pathItems.Add(new PhpConstValue(name + Extension));
            PhpIncludePathExpression = PhpBinaryOperatorExpression.ConcatStrings(pathItems.ToArray());
        }

        #endregion Constructors

        #region Static Methods

        // Private Methods 

        private static string[] Split(string name)
        {
            if (name.Contains("\\"))
                name = name.Replace("\\", "/");
            var p1 = ("/" + name).Split('/').Select(a => a.Trim()).Where(a => !string.IsNullOrEmpty(a)).ToArray();
            return p1;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public string MakeEmitPath(string basePath, int dupa)
        {
            var p = System.IO.Path.Combine(basePath, Name.Replace("/", "\\") + extension);
            return p;
        }

        /// <summary>
        /// Used for patch only
        /// </summary>
        public static bool IsFrameworkName(PhpCodeModuleName name)
        {
            var List = "commonlanguageruntimelibrary".Split(' ');
            return List.Contains(name.Library);
        }

        public IPhpValue MakeIncludePath(PhpCodeModuleName relatedTo)
        {
            if (relatedTo.Library == Library)
            {
                var knownPath = ProcessPath(name + extension, relatedTo.name + extension);
                //dirname(__FILE__)
                var __FILE__ = new PhpDefinedConstExpression("__FILE__", null);
                var dirname = new PhpMethodCallExpression("dirname", __FILE__);
                var path = new PhpConstValue(PathUtil.MakeUnixPath(PathUtil.UNIX_SEP + knownPath));
                var result = PhpBinaryOperatorExpression.ConcatStrings(dirname, path);
                return result;
            }
            else
            {
                string path = null;
                string pathRelTo = null;
                if (phpIncludePathExpression is PhpConstValue)
                {
                    path = (phpIncludePathExpression as PhpConstValue).Value as string;
                    if (path == null)
                        throw new NotSupportedException();
                }
                else
                    return phpIncludePathExpression; // assume expression like MPDF_LIB_PATH . 'lib/mpdf/mpdf.php'
                if (relatedTo.phpIncludePathExpression is PhpConstValue)
                {
                    pathRelTo = (relatedTo.phpIncludePathExpression as PhpConstValue).Value as string;
                    if (pathRelTo == null)
                        throw new NotSupportedException();
                }
                if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(path))
                {
                    var knownPath = ProcessPath(path, pathRelTo);
                    return new PhpConstValue(knownPath);
                }
                throw new NotSupportedException();
                //var aaaa = "/" + name + extension;
                //if (phpIncludePathExpression == null)
                //    return null;
                //if (phpIncludePathExpression is PhpConstValue)
                //{
                //    aaaa = ((phpIncludePathExpression as PhpConstValue).Value ?? "").ToString() + aaaa;
                //    return ProcessPath(aaaa, relatedTo.emitPath);
                //    // return new PhpConstValue(aaaa, true);
                //}
                //var a = new PhpConstValue(aaaa, true);
                //var g = new PhpBinaryOperatorExpression(".", phpIncludePathExpression, a);
                //return g;
            }
            throw new NotSupportedException();
        }

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}@{1}", name, Library);
        }
        // Private Methods 

        private string ProcessPath(string name, string relatedTo)
        {
            var p1 = Split(name);
            var p2 = Split(relatedTo);
            int common = 0;
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
            List<string> aa = new List<string>();
            for (int i = 0; i < p2.Length - 1; i++)
                aa.Add("..");
            aa.AddRange(p1);
            var g = string.Join("/", aa);
            //if (p1.Length == 1)
            //    return new PhpConstValue(Name + extension);
            return g;
        }

        #endregion Methods

        #region Fields

        /// <summary>
        /// Late binging module
        /// </summary>
        public const string CS2PHP_CONFIG_MODULE_NAME = "*cs2phpconfig*";

        #endregion Fields

        #region Static Properties

        public static PhpCodeModuleName Cs2PhpConfigModuleName
        {
            get
            {
                var a = new PhpCodeModuleName();
                a.Name = CS2PHP_CONFIG_MODULE_NAME;
                return a;
            }
        }

        #endregion Static Properties
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-25 13:50
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpCodeModuleName : IEquatable<PhpCodeModuleName>
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpCodeModuleName()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##AssemblyInfo## ##Library## ##Name## ##OptionalIncludePathPrefix## ##Extension## ##PhpIncludePathExpression##
        implement ToString AssemblyInfo=##AssemblyInfo##, Library=##Library##, Name=##Name##, OptionalIncludePathPrefix=##OptionalIncludePathPrefix##, Extension=##Extension##, PhpIncludePathExpression=##PhpIncludePathExpression##
        implement equals AssemblyInfo, Library, Name, OptionalIncludePathPrefix, Extension, PhpIncludePathExpression
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności AssemblyInfo; 
        /// </summary>
        public const string PROPERTYNAME_ASSEMBLYINFO = "AssemblyInfo";
        /// <summary>
        /// Nazwa własności Library; Library name for containing assembly
        /// </summary>
        public const string PROPERTYNAME_LIBRARY = "Library";
        /// <summary>
        /// Nazwa własności Name; Module name without library prefix
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności OptionalIncludePathPrefix; defined const or variables to include before module name
        /// </summary>
        public const string PROPERTYNAME_OPTIONALINCLUDEPATHPREFIX = "OptionalIncludePathPrefix";
        /// <summary>
        /// Nazwa własności Extension; rozszerzenie nazwy pliku
        /// </summary>
        public const string PROPERTYNAME_EXTENSION = "Extension";
        /// <summary>
        /// Nazwa własności PhpIncludePathExpression; Expression with complete path to this module
        /// </summary>
        public const string PROPERTYNAME_PHPINCLUDEPATHEXPRESSION = "PhpIncludePathExpression";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public bool Equals(PhpCodeModuleName other)
        {
            return other == this;
        }

        /// <summary>
        /// Sprawdza, czy wskazany obiekt jest równy bieżącemu
        /// </summary>
        /// <param name="obj">obiekt do porównania z obiektem bieżącym</param>
        /// <returns><c>true</c> jeśli wskazany obiekt jest równy bieżącemu; w przeciwnym wypadku<c>false</c></returns>
        public override bool Equals(object other)
        {
            if (!(other is PhpCodeModuleName)) return false;
            return Equals((PhpCodeModuleName)other);
        }

        /// <summary>
        /// Zwraca kod HASH obiektu
        /// </summary>
        /// <returns>kod HASH obiektu</returns>
        public override int GetHashCode()
        {
            // Good implementation suggested by Josh Bloch
            int _hash_ = 17;
            _hash_ = _hash_ * 31 + ((Library == (object)null) ? 0 : Library.GetHashCode());
            _hash_ = _hash_ * 31 + name.GetHashCode();
            return _hash_;
        }

        #endregion Methods

        #region Operators
        /// <summary>
        /// Realizuje operator ==
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są równe</returns>
        public static bool operator ==(PhpCodeModuleName left, PhpCodeModuleName right)
        {
            if (left == (object)null && right == (object)null) return true;
            if (left == (object)null || right == (object)null) return false;
            return left.Library == right.Library && left.name == right.name;
        }

        /// <summary>
        /// Realizuje operator !=
        /// </summary>
        /// <param name="left">lewa strona porównania</param>
        /// <param name="right">prawa strona porównania</param>
        /// <returns><c>true</c> jeśli obiekty są różne</returns>
        public static bool operator !=(PhpCodeModuleName left, PhpCodeModuleName right)
        {
            if (left == (object)null && right == (object)null) return false;
            if (left == (object)null || right == (object)null) return true;
            return left.Library != right.Library || left.name != right.name;
        }

        #endregion Operators

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public AssemblyTranslationInfo AssemblyInfo
        {
            get
            {
                return assemblyInfo;
            }
        }
        private AssemblyTranslationInfo assemblyInfo;
        /// <summary>
        /// Library name for containing assembly; własność dopuszcza wartości NULL i jest tylko do odczytu.
        /// </summary>
        public string Library
        {
            get
            {
                return assemblyInfo == null ? null : assemblyInfo.LibraryName;
            }
        }
        /// <summary>
        /// Module name without library prefix
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                value = value.Replace("\\", "/");
                if (value == name) return;
                name = value;
                UpdateIncludePathExpression();
            }
        }
        private string name = string.Empty;
        /// <summary>
        /// defined const or variables to include before module name
        /// </summary>
        public string[] OptionalIncludePathPrefix
        {
            get
            {
                return optionalIncludePathPrefix;
            }
            set
            {
                optionalIncludePathPrefix = value;
            }
        }
        private string[] optionalIncludePathPrefix;
        /// <summary>
        /// rozszerzenie nazwy pliku
        /// </summary>
        public string Extension
        {
            get
            {
                return extension;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                extension = value;
            }
        }
        private string extension = ".php";
        /// <summary>
        /// Expression with complete path to this module
        /// </summary>
        public IPhpValue PhpIncludePathExpression
        {
            get
            {
                return phpIncludePathExpression;
            }
            set
            {
                phpIncludePathExpression = value;
            }
        }
        private IPhpValue phpIncludePathExpression;
        #endregion Properties

    }
}
