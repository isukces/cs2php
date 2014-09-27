using System;

namespace Lang.Php
{

    /*
    smartClass
    template attribute
    option NoAdditionalFile
    implement Constructor ModuleShortName
    
    property ModuleShortName string Module short name i.e "hello-page" or "mynamespace/hello-class"
    
    property IncludePathPrefix string[] 
    smartClassEnd
    */
    
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public partial class ModuleAttribute : Attribute
    {
		#region Constructors 

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="moduleShortName">Module short name i.e "hello-page" or "mynamespace/hello-class"</param>
        /// <param name="includePathPrefix"></param>
        /// </summary>
        public ModuleAttribute(string moduleShortName, params string[] includePathPrefix)
        {
            _moduleShortName = moduleShortName;
            _includePathPrefix = includePathPrefix;
        }

		#endregion Constructors 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-27 10:51
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="moduleShortName">Module short name i.e "hello-page" or "mynamespace/hello-class"</param>
        /// </summary>
        public ModuleAttribute(string moduleShortName)
        {
            _moduleShortName = moduleShortName;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Module short name i.e "hello-page" or "mynamespace/hello-class"; własność jest tylko do odczytu.
        /// </summary>
        public string ModuleShortName
        {
            get
            {
                return _moduleShortName;
            }
        }
        private readonly string _moduleShortName = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string[] IncludePathPrefix
        {
            get
            {
                return _includePathPrefix;
            }
        }
        private readonly string[] _includePathPrefix;
        #endregion Properties

    }
}
