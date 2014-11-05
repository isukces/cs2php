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
    
    property SubdirectoryName string Nazwa podkatalogu, w którym będzie umieszczone główne drzewo plików dla biblioteki
    smartClassEnd
    */
    
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public partial class ModulesSubDirAttribute : Attribute
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-14 08:47
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class ModulesSubDirAttribute 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ModulesSubDirAttribute()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##SubdirectoryName##
        implement ToString SubdirectoryName=##SubdirectoryName##
        implement equals SubdirectoryName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="SubdirectoryName">Nazwa podkatalogu, w którym będzie umieszczone główne drzewo plików dla biblioteki</param>
        /// </summary>
        public ModulesSubDirAttribute(string SubdirectoryName)
        {
            subdirectoryName = SubdirectoryName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności SubdirectoryName; Nazwa podkatalogu, w którym będzie umieszczone główne drzewo plików dla biblioteki
        /// </summary>
        public const string PROPERTYNAME_SUBDIRECTORYNAME = "SubdirectoryName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Nazwa podkatalogu, w którym będzie umieszczone główne drzewo plików dla biblioteki; własność jest tylko do odczytu.
        /// </summary>
        public string SubdirectoryName
        {
            get
            {
                return subdirectoryName;
            }
        }
        private string subdirectoryName = string.Empty;
        #endregion Properties

    }
}
