using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php 
{

    /*
    smartClass
    template attribute
    option NoAdditionalFile
    implement Constructor
    implement Constructor *
    
    property GlobalVariableName string 
    smartClassEnd
    */
    
    public partial class GlobalVariableAttribute : Attribute
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 22:09
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class GlobalVariableAttribute 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AsGlobalVariableAttribute()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##GlobalVariableName##
        implement ToString GlobalVariableName=##GlobalVariableName##
        implement equals GlobalVariableName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public GlobalVariableAttribute()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="GlobalVariableName"></param>
        /// </summary>
        public GlobalVariableAttribute(string GlobalVariableName)
        {
            globalVariableName = GlobalVariableName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności GlobalVariableName; 
        /// </summary>
        public const string PROPERTYNAME_GLOBALVARIABLENAME = "GlobalVariableName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string GlobalVariableName
        {
            get
            {
                return globalVariableName;
            }
        }
        private string globalVariableName = string.Empty;
        #endregion Properties

    }
}
