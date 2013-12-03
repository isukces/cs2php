using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Library, ShortName
    implement Equals Library, ShortName
    
    property Library string 
    
    property ShortName string 
    	preprocess value = value.Replace("\\", "/");
    
    property Extension string rozszerzenie nazwy pliku
    	init ".php"
    
    property EmitPath string pełna nazwa pliku do emisji
    
    property IncludePath IPhpValue nazwa pliku do include, może być wyrażenie. Jeśli puste - to brak include
    smartClassEnd
    */

    public partial class PhpCodeModuleName : PhpSourceBase
    {

        public IPhpValue MakeIncludePath(PhpCodeModuleName n)
        {
            if (n.library == library)
            {
                var p1 = shortName.Split('/');
                var p2 = n.shortName.Split('/');
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
                if (p1.Length == 1)
                    return new PhpConstValue(shortName + extension);
                return new PhpConstValue(shortName + extension);
            }
            else
            {
                if (includePath == null)
                {

                    // throw new NotSupportedException("Unable to include " + library);
                    return new PhpConstValue("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                }
                var a = new PhpConstValue("/" + shortName + extension, true);
                var g = new PhpBinaryOperatorExpression(".", includePath, a);
                return g;
            }
            throw new NotSupportedException();
        }
        #region Constructors

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Library"></param>
        /// <param name="ShortName"></param>
        /// </summary>
        public PhpCodeModuleName(Assembly a, string ShortName)
        {

            {
                var at = a.GetCustomAttribute<Lang.Php.ModuleIncludeConstAttribute>();
                if (at != null)
                {
                    var constOrVarName = (at.ConstOrVarName ?? "").Trim();
                    if (!string.IsNullOrEmpty(constOrVarName))
                    {
                        if (constOrVarName.StartsWith("$"))
                        {
                            IncludePath = new PhpVariableExpression(constOrVarName, PhpVariableKind.Global);
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }
                    }
                }

            }

            this.Library = LibNameFromAssembly(a);
            this.ShortName = ShortName;
        }

        public static string LibNameFromAssembly(Assembly a)
        {
            var tmp = a.ManifestModule.ScopeName.ToLower();
            if (tmp.EndsWith(".dll"))
                tmp = tmp.Substring(0, tmp.Length - 4);
            return tmp;
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public void MakeEmitPath(string basePath)
        {
            var p = System.IO.Path.Combine(basePath, ShortName.Replace("/", "\\") + extension);
            this.EmitPath = p;
        }

        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}@{1}", shortName, library);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 22:34
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
        implement ToString ##Library## ##ShortName## ##Extension## ##EmitPath## ##IncludePath##
        implement ToString Library=##Library##, ShortName=##ShortName##, Extension=##Extension##, EmitPath=##EmitPath##, IncludePath=##IncludePath##
        implement equals Library, ShortName, Extension, EmitPath, IncludePath
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Library"></param>
        /// <param name="ShortName"></param>
        /// </summary>
        public PhpCodeModuleName(string Library, string ShortName)
        {
            this.Library = Library;
            this.ShortName = ShortName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Library; 
        /// </summary>
        public const string PROPERTYNAME_LIBRARY = "Library";
        /// <summary>
        /// Nazwa własności ShortName; 
        /// </summary>
        public const string PROPERTYNAME_SHORTNAME = "ShortName";
        /// <summary>
        /// Nazwa własności Extension; rozszerzenie nazwy pliku
        /// </summary>
        public const string PROPERTYNAME_EXTENSION = "Extension";
        /// <summary>
        /// Nazwa własności EmitPath; pełna nazwa pliku do emisji
        /// </summary>
        public const string PROPERTYNAME_EMITPATH = "EmitPath";
        /// <summary>
        /// Nazwa własności IncludePath; nazwa pliku do include, może być wyrażenie. Jeśli puste - to brak include
        /// </summary>
        public const string PROPERTYNAME_INCLUDEPATH = "IncludePath";
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
            _hash_ = _hash_ * 31 + library.GetHashCode();
            _hash_ = _hash_ * 31 + shortName.GetHashCode();
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
            return left.library == right.library && left.shortName == right.shortName;
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
            return left.library != right.library || left.shortName != right.shortName;
        }

        #endregion Operators

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Library
        {
            get
            {
                return library;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                library = value;
            }
        }
        private string library = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string ShortName
        {
            get
            {
                return shortName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                value = value.Replace("\\", "/");
                shortName = value;
            }
        }
        private string shortName = string.Empty;
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
        /// pełna nazwa pliku do emisji
        /// </summary>
        public string EmitPath
        {
            get
            {
                return emitPath;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                emitPath = value;
            }
        }
        private string emitPath = string.Empty;
        /// <summary>
        /// nazwa pliku do include, może być wyrażenie. Jeśli puste - to brak include
        /// </summary>
        public IPhpValue IncludePath
        {
            get
            {
                return includePath;
            }
            set
            {
                includePath = value;
            }
        }
        private IPhpValue includePath;
        #endregion Properties

    }
}
