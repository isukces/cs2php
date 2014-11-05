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
    implement Constructor ReplacedType
    
    property ReplacedType Type 
    smartClassEnd
    */

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public partial class ReplaceAttribute : Attribute
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-04 08:14
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class ReplaceAttribute
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ReplaceAttribute()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ReplacedType##
        implement ToString ReplacedType=##ReplacedType##
        implement equals ReplacedType
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="ReplacedType"></param>
        /// </summary>
        public ReplaceAttribute(Type ReplacedType)
        {
            replacedType = ReplacedType;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności ReplacedType; 
        /// </summary>
        public const string PROPERTYNAME_REPLACEDTYPE = "ReplacedType";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public Type ReplacedType
        {
            get
            {
                return replacedType;
            }
        }
        private Type replacedType;
        #endregion Properties

    }
}
