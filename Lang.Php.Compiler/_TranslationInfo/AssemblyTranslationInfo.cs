using Lang.Php.Compiler.Source;
using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Assembly Assembly 
    	access public private private
    
    property LibraryName string 
    	read only
    
    property IncludePathConstOrVarName string nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
    
    property RootPath string 
    	preprocess value = value.Replace("/", "\\");
    	preprocess value = value.StartsWith("\\") ? value.Substring(1) : value;
    	preprocess value = value.EndsWith("\\") ? value : (value + "\\");
    	preprocess value = value.Replace("\\", "\\\\");
    	preprocess value = value == "\\" ? "" : value;
    
    property PhpPackageSourceUri string 
    
    property PhpPackagePathStrip string 
    smartClassEnd
    */

    public partial class AssemblyTranslationInfo
    {
        #region Static Methods

        // Public Methods 
        public static string LibNameFromAssembly(Assembly a)
        {
            var tmp = a.ManifestModule.ScopeName.ToLower();
            if (tmp.EndsWith(".dll"))
                tmp = tmp.Substring(0, tmp.Length - 4);
            return tmp;
        }

        public static AssemblyTranslationInfo FromAssembly(Assembly assembly)
        {
            if (assembly == null)
                return null;
            AssemblyTranslationInfo ati = new AssemblyTranslationInfo();
            {
                ati.assembly = assembly;

                var _ModuleIncludeConst = assembly.GetCustomAttribute<ModuleIncludeConstAttribute>();
                if (_ModuleIncludeConst != null)
                    ati.IncludePathConstOrVarName = _ModuleIncludeConst.ConstOrVarName;
                var _RootPath = assembly.GetCustomAttribute<RootPathAttribute>();
                if (_RootPath != null)
                    ati.RootPath = _RootPath.Path;
                else
                    ati.rootPath = "/";

                var _PhpPackageSource = assembly.GetCustomAttribute<PhpPackageSourceAttribute>();
                if (_PhpPackageSource != null)
                {
                    ati.PhpPackageSourceUri = _PhpPackageSource.SourceUri;
                    ati.PhpPackagePathStrip = _PhpPackageSource.StripArchivePath;
                }
            }
            ati.libraryName = LibNameFromAssembly(assembly);
            return ati;
        }

        #endregion Static Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-06 09:09
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class AssemblyTranslationInfo
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AssemblyTranslationInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Assembly## ##LibraryName## ##IncludePathConstOrVarName## ##RootPath## ##PhpPackageSourceUri## ##PhpPackagePathStrip##
        implement ToString Assembly=##Assembly##, LibraryName=##LibraryName##, IncludePathConstOrVarName=##IncludePathConstOrVarName##, RootPath=##RootPath##, PhpPackageSourceUri=##PhpPackageSourceUri##, PhpPackagePathStrip=##PhpPackagePathStrip##
        implement equals Assembly, LibraryName, IncludePathConstOrVarName, RootPath, PhpPackageSourceUri, PhpPackagePathStrip
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Assembly; 
        /// </summary>
        public const string PROPERTYNAME_ASSEMBLY = "Assembly";
        /// <summary>
        /// Nazwa własności LibraryName; 
        /// </summary>
        public const string PROPERTYNAME_LIBRARYNAME = "LibraryName";
        /// <summary>
        /// Nazwa własności IncludePathConstOrVarName; nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
        /// </summary>
        public const string PROPERTYNAME_INCLUDEPATHCONSTORVARNAME = "IncludePathConstOrVarName";
        /// <summary>
        /// Nazwa własności RootPath; 
        /// </summary>
        public const string PROPERTYNAME_ROOTPATH = "RootPath";
        /// <summary>
        /// Nazwa własności PhpPackageSourceUri; 
        /// </summary>
        public const string PROPERTYNAME_PHPPACKAGESOURCEURI = "PhpPackageSourceUri";
        /// <summary>
        /// Nazwa własności PhpPackagePathStrip; 
        /// </summary>
        public const string PROPERTYNAME_PHPPACKAGEPATHSTRIP = "PhpPackagePathStrip";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Assembly Assembly
        {
            get
            {
                return assembly;
            }
            private set
            {
                assembly = value;
            }
        }
        private Assembly assembly;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string LibraryName
        {
            get
            {
                return libraryName;
            }
        }
        private string libraryName = string.Empty;
        /// <summary>
        /// nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
        /// </summary>
        public string IncludePathConstOrVarName
        {
            get
            {
                return includePathConstOrVarName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                includePathConstOrVarName = value;
            }
        }
        private string includePathConstOrVarName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string RootPath
        {
            get
            {
                return rootPath;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                value = value.Replace("/", "\\");
                value = value.StartsWith("\\") ? value.Substring(1) : value;
                value = value.EndsWith("\\") ? value : (value + "\\");
                value = value.Replace("\\", "\\\\");
                value = value == "\\" ? "" : value;
                rootPath = value;
            }
        }
        private string rootPath = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string PhpPackageSourceUri
        {
            get
            {
                return phpPackageSourceUri;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                phpPackageSourceUri = value;
            }
        }
        private string phpPackageSourceUri = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string PhpPackagePathStrip
        {
            get
            {
                return phpPackagePathStrip;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                phpPackagePathStrip = value;
            }
        }
        private string phpPackagePathStrip = string.Empty;
        #endregion Properties

    }
}
