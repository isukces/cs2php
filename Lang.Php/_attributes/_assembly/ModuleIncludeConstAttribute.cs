using System;

namespace Lang.Php
{
    /*
    smartClass
    template attribute
    option NoAdditionalFile
    implement Constructor *
    
    property ConstOrVarName string Module filename
      
    smartClassEnd
    */

    /// <summary>
    /// Atrybut dołączany do assemby, który wskazuje jaka stała PHP określa ścieżkę bazową dla biblioteki 
    /// może być np. MY_LIB_PATH (define) lub $MyLibPath (global var)
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public partial class ModuleIncludeConstAttribute : Attribute
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-10 15:30
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class ModuleIncludeConstAttribute
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ModuleIncludeConstAttribute()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ConstOrVarName##
        implement ToString ConstOrVarName=##ConstOrVarName##
        implement equals ConstOrVarName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="ConstOrVarName">Module filename</param>
        /// </summary>
        public ModuleIncludeConstAttribute(string ConstOrVarName)
        {
            constOrVarName = ConstOrVarName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności ConstOrVarName; Module filename
        /// </summary>
        public const string PROPERTYNAME_CONSTORVARNAME = "ConstOrVarName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Module filename; własność jest tylko do odczytu.
        /// </summary>
        public string ConstOrVarName
        {
            get
            {
                return constOrVarName;
            }
        }
        private string constOrVarName = string.Empty;
        #endregion Properties

    }
}
