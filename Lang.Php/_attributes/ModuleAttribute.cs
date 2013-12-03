using System;

namespace Lang.Php
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property ModuleShortName string Module short name i.e "hello-page" or "mynamespace/hello-class"
    smartClassEnd
    */
    
    public partial class ModuleAttribute : Attribute
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 18:48
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
        implement ToString ##ModuleShortName##
        implement ToString ModuleShortName=##ModuleShortName##
        implement equals ModuleShortName
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
        #endregion Properties

    }
}
