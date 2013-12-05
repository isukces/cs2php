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
    
    property LibraryName string 
    
    property IncludePathConstOrVarName string nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
    
    property RootPath string 
    	preprocess value = value.Replace("/", "\\");
    	preprocess value = value.StartsWith("\\") ? value.Substring(1) : value;
    
    property PhpPackageSourceUri string 
    
    property PhpPackagePathStrip string 
    smartClassEnd
    */
    
    public partial class AssemblyTranslationInfo
    {
		#region Static Methods 

		// Public Methods 

        public static AssemblyTranslationInfo FromAssembly(Assembly assembly)
        {
            if (assembly == null)
                return null;
            AssemblyTranslationInfo a = new AssemblyTranslationInfo();
            {
                var _ModuleIncludeConst = assembly.GetCustomAttribute<ModuleIncludeConstAttribute>();
                if (_ModuleIncludeConst != null)
                    a.IncludePathConstOrVarName = _ModuleIncludeConst.ConstOrVarName;
                var _RootPath = assembly.GetCustomAttribute<RootPathAttribute>();
                if (_RootPath != null)
                    a.RootPath = _RootPath.Path;

                var _PhpPackageSource = assembly.GetCustomAttribute<PhpPackageSourceAttribute>();
                if (_PhpPackageSource != null)
                {
                    a.PhpPackageSourceUri = _PhpPackageSource.SourceUri;
                    a.PhpPackagePathStrip = _PhpPackageSource.StripArchivePath;
                }
            }
            a.LibraryName = PhpCodeModuleName.LibNameFromAssembly(assembly);
            return a;
        }

		#endregion Static Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-05 13:24
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
        implement ToString ##LibraryName## ##IncludePathConstOrVarName## ##RootPath## ##PhpPackageSourceUri## ##PhpPackagePathStrip##
        implement ToString LibraryName=##LibraryName##, IncludePathConstOrVarName=##IncludePathConstOrVarName##, RootPath=##RootPath##, PhpPackageSourceUri=##PhpPackageSourceUri##, PhpPackagePathStrip=##PhpPackagePathStrip##
        implement equals LibraryName, IncludePathConstOrVarName, RootPath, PhpPackageSourceUri, PhpPackagePathStrip
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
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
        public string LibraryName
        {
            get
            {
                return libraryName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                libraryName = value;
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
