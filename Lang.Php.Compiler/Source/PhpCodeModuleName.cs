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
    implement Equals Library, Name
    
    property AssemblyInfo AssemblyTranslationInfo 
    
    property Library string Library name for containing assembly
    	read only assemblyInfo == null ? null : assemblyInfo.LibraryName
    
    property Name string Module name without library prefix
    	preprocess value = value.Replace("\\", "/");
    
    property Extension string rozszerzenie nazwy pliku
    	init ".php"
    
    property EmitPath string Full file name for module or directory name for assembly
    
    property IncludePath IPhpValue Expression with complete path to this module
    smartClassEnd
    */
    
    public partial class PhpCodeModuleName : PhpSourceBase
    {
        #region Constructors
        public PhpCodeModuleName(Type type, AssemblyTranslationInfo assemblyInfo, ClassTranslationInfo declaringTypeInfo)
        {
            if (assemblyInfo == null)
                throw new ArgumentNullException("assemblyInfo");
            if (type == null)
                throw new ArgumentNullException("type");
            if (type.DeclaringType != null && declaringTypeInfo==null)
                throw new ArgumentNullException("declaringTypeInfo");

            Name = type.FullName.Replace(".", "_").Replace("+", "__");
            this.assemblyInfo = assemblyInfo;

            {
                // take module name from parent, this can be overrided if nested class is decorated with attributes
                if (declaringTypeInfo != null)
                    Name = declaringTypeInfo.ModuleName.Name;
                var ats = type.GetCustomAttributes(false);

                #region ModuleAttribute
                {
                    ModuleAttribute _module = ats.OfType<ModuleAttribute>().FirstOrDefault();
                    if (_module != null)
                        Name = _module.ModuleShortName;
                }
                #endregion
                #region PageAttribute
                {
                    var _page = ats.OfType<PageAttribute>().FirstOrDefault();
                    if (_page != null)
                        Name = _page.ModuleShortName;
                }
                #endregion
            }



            #region Include Path
            {
                List<IPhpValue> ip = new List<IPhpValue>();
                #region Take include path from assembly - obsolete ??
                {
                    var tmp = assemblyInfo.IncludePathConstOrVarName;
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        if (tmp.StartsWith("$"))
                            ip.Add(new PhpVariableExpression(tmp, PhpVariableKind.Global));
                        else
                        {
                            throw new NotSupportedException();
                            PhpCodeModuleName IDontKnowHowToFindModuleName = null;
                            ip.Add(new PhpDefinedConstExpression(tmp, IDontKnowHowToFindModuleName));
                        }
                    }
                }
                #endregion
                #region RootPathAttribute
                {
                    if (!string.IsNullOrEmpty(assemblyInfo.RootPath))
                        ip.Add(new PhpConstValue(assemblyInfo.RootPath));
                }
                #endregion
            }
            #endregion
        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 


        // Private Methods 

        private static string[] Split(string name)
        {
            var p1 = ("/" + name).Split('/').Select(a => a.Trim()).Where(a => !string.IsNullOrEmpty(a)).ToArray();
            return p1;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void MakeEmitPath(string basePath)
        {
            var p = System.IO.Path.Combine(basePath, Name.Replace("/", "\\") + extension);
            this.EmitPath = p;
        }

        public IPhpValue MakeIncludePath(PhpCodeModuleName relatedTo)
        {
            if (relatedTo.Library == Library)
                return ProcessPath(name + extension, relatedTo.name + extension);
            else
            {
                var aaaa = "/" + name + extension;
                if (includePath == null)
                {
                    if (!string.IsNullOrEmpty(emitPath))
                        return new PhpConstValue(emitPath);
                    // throw new NotSupportedException("Unable to include " + library);
                    return new PhpConstValue("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                }
                if (includePath is PhpConstValue)
                {
                    aaaa = ((includePath as PhpConstValue).Value ?? "").ToString() + aaaa;
                    return ProcessPath(aaaa, relatedTo.emitPath);
                    // return new PhpConstValue(aaaa, true);
                }
                var a = new PhpConstValue(aaaa, true);
                var g = new PhpBinaryOperatorExpression(".", includePath, a);
                return g;
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

        private IPhpValue ProcessPath(string name, string relatedTo)
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
            return new PhpConstValue(g);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-06 10:37
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
        implement ToString ##AssemblyInfo## ##Library## ##Name## ##Extension## ##EmitPath## ##IncludePath##
        implement ToString AssemblyInfo=##AssemblyInfo##, Library=##Library##, Name=##Name##, Extension=##Extension##, EmitPath=##EmitPath##, IncludePath=##IncludePath##
        implement equals AssemblyInfo, Library, Name, Extension, EmitPath, IncludePath
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
        /// Nazwa własności Extension; rozszerzenie nazwy pliku
        /// </summary>
        public const string PROPERTYNAME_EXTENSION = "Extension";
        /// <summary>
        /// Nazwa własności EmitPath; Full file name for module or directory name for assembly
        /// </summary>
        public const string PROPERTYNAME_EMITPATH = "EmitPath";
        /// <summary>
        /// Nazwa własności IncludePath; Expression with complete path to this module
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
            _hash_ = _hash_ * 31 + Library.GetHashCode();
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
        /// 
        /// </summary>
        public AssemblyTranslationInfo AssemblyInfo
        {
            get
            {
                return assemblyInfo;
            }
            set
            {
                assemblyInfo = value;
            }
        }
        private AssemblyTranslationInfo assemblyInfo;
        /// <summary>
        /// Library name for containing assembly; własność jest tylko do odczytu.
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
                name = value;
            }
        }
        private string name = string.Empty;
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
        /// Full file name for module or directory name for assembly
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
        /// Expression with complete path to this module
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
