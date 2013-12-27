using System;

namespace Lang.Php
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor ModuleShortName
    
    property ModuleShortName string Module short name i.e "hello-page" or "mynamespace/hello-class"
    
    property IncludePathPrefix string[] 
    smartClassEnd
    */
    
    public partial class ModuleAttribute : Attribute
    {
		#region Constructors 

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="ModuleShortName">Module short name i.e "hello-page" or "mynamespace/hello-class"</param>
        /// <param name="IncludePathPrefix"></param>
        /// </summary>
        public ModuleAttribute(string ModuleShortName, params string[] IncludePathPrefix)
        {
            this.ModuleShortName = ModuleShortName;
            this.IncludePathPrefix = IncludePathPrefix;
        }

		#endregion Constructors 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-26 10:23
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class ModuleAttribute 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ModuleAttribute()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ModuleShortName## ##IncludePathPrefix##
        implement ToString ModuleShortName=##ModuleShortName##, IncludePathPrefix=##IncludePathPrefix##
        implement equals ModuleShortName, IncludePathPrefix
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="ModuleShortName">Module short name i.e "hello-page" or "mynamespace/hello-class"</param>
        /// </summary>
        public ModuleAttribute(string ModuleShortName)
        {
            this.ModuleShortName = ModuleShortName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności ModuleShortName; Module short name i.e "hello-page" or "mynamespace/hello-class"
        /// </summary>
        public const string PROPERTYNAME_MODULESHORTNAME = "ModuleShortName";
        /// <summary>
        /// Nazwa własności IncludePathPrefix; 
        /// </summary>
        public const string PROPERTYNAME_INCLUDEPATHPREFIX = "IncludePathPrefix";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Module short name i.e "hello-page" or "mynamespace/hello-class"
        /// </summary>
        public string ModuleShortName
        {
            get
            {
                return moduleShortName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                moduleShortName = value;
            }
        }
        private string moduleShortName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string[] IncludePathPrefix
        {
            get
            {
                return includePathPrefix;
            }
            set
            {
                includePathPrefix = value;
            }
        }
        private string[] includePathPrefix;
        #endregion Properties

    }
}
