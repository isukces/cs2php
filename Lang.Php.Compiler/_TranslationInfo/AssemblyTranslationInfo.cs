using System.Reflection;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Assembly Assembly 
    	read only
    
    property LibraryName string 
    	read only
    
    property IncludePathConstOrVarName string nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
    	read only
    
    property RootPath string 
    	read only
    
    property PhpPackageSourceUri string 
    	read only
    
    property PhpPackagePathStrip string 
    	read only
    
    property PhpIncludePathExpression IPhpValue 
    	read only
    
    property ConfigModuleName string 
    	init "cs2php"
    	access public private private
    
    property DefaultTimezone Timezones? 
    	read only
    smartClassEnd
    */

    public partial class AssemblyTranslationInfo
    {
        #region Static Methods



        // Public Methods 

        public static AssemblyTranslationInfo FromAssembly(Assembly assembly, TranslationInfo translationInfo)
        {
            if (assembly == null)
                return null;
            var ati = new AssemblyTranslationInfo();
            {
                ati._assembly = assembly;

                var moduleIncludeConst = assembly.GetCustomAttribute<ModuleIncludeConstAttribute>();
                if (moduleIncludeConst != null)
                {
                    ati._includePathConstOrVarName = (moduleIncludeConst.ConstOrVarName ?? "").Trim();
                    if (ati._includePathConstOrVarName.StartsWith("$"))
                    {

                    }
                    else
                    {
                        ati._includePathConstOrVarName = "\\" + ati._includePathConstOrVarName.TrimStart('\\');
                    }
                }
                ati._rootPath = GetRootPath(assembly);

                var phpPackageSource = assembly.GetCustomAttribute<PhpPackageSourceAttribute>();
                if (phpPackageSource != null)
                {
                    ati._phpPackageSourceUri = phpPackageSource.SourceUri;
                    ati._phpPackagePathStrip = phpPackageSource.StripArchivePath;
                }
                {
                    var configModule = assembly.GetCustomAttribute<ConfigModuleAttribute>();
                    if (configModule != null)
                        ati.ConfigModuleName = configModule.Name;
                }
                {
                    var defaultTimezone = assembly.GetCustomAttribute<DefaultTimezoneAttribute>();
                    if (defaultTimezone != null)
                        ati._defaultTimezone = defaultTimezone.Timezone;
                }
            }
            ati._libraryName = LibNameFromAssembly(assembly);
            ati._phpIncludePathExpression = GetDefaultIncludePath(ati, translationInfo);
            return ati;
        }

        public static string GetRootPath(Assembly assembly)
        {
            var rootPathAttribute = assembly.GetCustomAttribute<RootPathAttribute>();
            if (rootPathAttribute == null)
                return null;
            string result = (rootPathAttribute.Path ?? "").Replace("\\", "/").TrimEnd('/').TrimStart('/') + "/";
            while (result.Contains("//"))
                result = result.Replace("//", "/");
            return result;
        }

        private static string LibNameFromAssembly(Assembly a)
        {
            var tmp = a.ManifestModule.ScopeName.ToLower();
            if (tmp.EndsWith(".dll"))
                tmp = tmp.Substring(0, tmp.Length - 4);
            return tmp;
        }
        // Private Methods 
        public override string ToString()
        {
            return _libraryName;
        }
        static IPhpValue GetDefaultIncludePath(AssemblyTranslationInfo ati, TranslationInfo translationInfo)
        {
            var pathElements = new List<IPhpValue>();
            #region Take include path variable or const
            if (!string.IsNullOrEmpty(ati.IncludePathConstOrVarName))
            {
                if (ati.IncludePathConstOrVarName.StartsWith("$"))
                    pathElements.Add(new PhpVariableExpression(ati.IncludePathConstOrVarName, PhpVariableKind.Global));
                else
                {
                    var tmp = ati.IncludePathConstOrVarName;
                    if (!tmp.StartsWith("\\")) // defined const is in global namespace ALWAYS
                        tmp = "\\" + tmp;

                    KnownConstInfo info;
                    if (translationInfo != null && translationInfo.KnownConstsValues.TryGetValue(tmp, out info) && info.UseFixedValue)
                        pathElements.Add(new PhpConstValue(info.Value));
                    else
                        pathElements.Add(new PhpDefinedConstExpression(tmp, PhpCodeModuleName.Cs2PhpConfigModuleName));
                }
            }
            #endregion

            //#region RootPathAttribute
            //{
            //    if (!string.IsNullOrEmpty(ati.RootPath) && ati.RootPath != "/")
            //        pathElements.Add(new PhpConstValue(ati.RootPath));
            //}
            //#endregion
            IPhpValue result = PhpBinaryOperatorExpression.ConcatStrings(pathElements.ToArray());
            return result;
        }

        #endregion Static Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-07 12:40
// File generated automatically ver 2014-09-01 19:00
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
        implement ToString ##Assembly## ##LibraryName## ##IncludePathConstOrVarName## ##RootPath## ##PhpPackageSourceUri## ##PhpPackagePathStrip## ##PhpIncludePathExpression## ##ConfigModuleName## ##DefaultTimezone##
        implement ToString Assembly=##Assembly##, LibraryName=##LibraryName##, IncludePathConstOrVarName=##IncludePathConstOrVarName##, RootPath=##RootPath##, PhpPackageSourceUri=##PhpPackageSourceUri##, PhpPackagePathStrip=##PhpPackagePathStrip##, PhpIncludePathExpression=##PhpIncludePathExpression##, ConfigModuleName=##ConfigModuleName##, DefaultTimezone=##DefaultTimezone##
        implement equals Assembly, LibraryName, IncludePathConstOrVarName, RootPath, PhpPackageSourceUri, PhpPackagePathStrip, PhpIncludePathExpression, ConfigModuleName, DefaultTimezone
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Assembly; 
        /// </summary>
        public const string PropertyNameAssembly = "Assembly";
        /// <summary>
        /// Nazwa własności LibraryName; 
        /// </summary>
        public const string PropertyNameLibraryName = "LibraryName";
        /// <summary>
        /// Nazwa własności IncludePathConstOrVarName; nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
        /// </summary>
        public const string PropertyNameIncludePathConstOrVarName = "IncludePathConstOrVarName";
        /// <summary>
        /// Nazwa własności RootPath; 
        /// </summary>
        public const string PropertyNameRootPath = "RootPath";
        /// <summary>
        /// Nazwa własności PhpPackageSourceUri; 
        /// </summary>
        public const string PropertyNamePhpPackageSourceUri = "PhpPackageSourceUri";
        /// <summary>
        /// Nazwa własności PhpPackagePathStrip; 
        /// </summary>
        public const string PropertyNamePhpPackagePathStrip = "PhpPackagePathStrip";
        /// <summary>
        /// Nazwa własności PhpIncludePathExpression; 
        /// </summary>
        public const string PropertyNamePhpIncludePathExpression = "PhpIncludePathExpression";
        /// <summary>
        /// Nazwa własności ConfigModuleName; 
        /// </summary>
        public const string PropertyNameConfigModuleName = "ConfigModuleName";
        /// <summary>
        /// Nazwa własności DefaultTimezone; 
        /// </summary>
        public const string PropertyNameDefaultTimezone = "DefaultTimezone";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public Assembly Assembly
        {
            get
            {
                return _assembly;
            }
        }
        private Assembly _assembly;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string LibraryName
        {
            get
            {
                return _libraryName;
            }
        }
        private string _libraryName = string.Empty;
        /// <summary>
        /// nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki; własność jest tylko do odczytu.
        /// </summary>
        public string IncludePathConstOrVarName
        {
            get
            {
                return _includePathConstOrVarName;
            }
        }
        private string _includePathConstOrVarName = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string RootPath
        {
            get
            {
                return _rootPath;
            }
        }
        private string _rootPath = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string PhpPackageSourceUri
        {
            get
            {
                return _phpPackageSourceUri;
            }
        }
        private string _phpPackageSourceUri = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string PhpPackagePathStrip
        {
            get
            {
                return _phpPackagePathStrip;
            }
        }
        private string _phpPackagePathStrip = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue PhpIncludePathExpression
        {
            get
            {
                return _phpIncludePathExpression;
            }
        }
        private IPhpValue _phpIncludePathExpression;
        /// <summary>
        /// 
        /// </summary>
        public string ConfigModuleName
        {
            get
            {
                return _configModuleName;
            }
            private set
            {
                value = (value ?? String.Empty).Trim();
                _configModuleName = value;
            }
        }
        private string _configModuleName = "cs2php";
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public Timezones? DefaultTimezone
        {
            get
            {
                return _defaultTimezone;
            }
        }
        private Timezones? _defaultTimezone;
        #endregion Properties

    }
}
