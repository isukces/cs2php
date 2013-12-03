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
    
    property DefinedConstName string 
    smartClassEnd
    */
    
    public partial class AsDefinedConst:Attribute
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 09:24
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class AsDefinedConst 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AsDefinedConst()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##DefinedConstName##
        implement ToString DefinedConstName=##DefinedConstName##
        implement equals DefinedConstName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AsDefinedConst()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="DefinedConstName"></param>
        /// </summary>
        public AsDefinedConst(string DefinedConstName)
        {
            this.definedConstName = DefinedConstName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności DefinedConstName; 
        /// </summary>
        public const string PROPERTYNAME_DEFINEDCONSTNAME = "DefinedConstName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string DefinedConstName
        {
            get
            {
                return definedConstName;
            }
        }
        private string definedConstName = string.Empty;
        #endregion Properties

    }
}
