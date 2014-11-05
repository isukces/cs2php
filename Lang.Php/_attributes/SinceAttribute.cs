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
    implement Constructor *
    
    property Version string Wersja PHP
    smartClassEnd
    */
    
    public partial class SinceAttribute:Attribute
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-04 12:09
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class SinceAttribute 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public SinceAttribute()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Version##
        implement ToString Version=##Version##
        implement equals Version
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Version">Wersja PHP</param>
        /// </summary>
        public SinceAttribute(string Version)
        {
            version = Version;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Version; Wersja PHP
        /// </summary>
        public const string PROPERTYNAME_VERSION = "Version";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Wersja PHP; własność jest tylko do odczytu.
        /// </summary>
        public string Version
        {
            get
            {
                return version;
            }
        }
        private string version = string.Empty;
        #endregion Properties

    }
}
