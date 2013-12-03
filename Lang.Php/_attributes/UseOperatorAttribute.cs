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
    implement Constructor Operator
    implement Constructor *
    
    property Operator string operator, którym zostanie zastąpiony sugerowany
    
    property Map string 
    smartClassEnd
    */
    
    [AttributeUsage(AttributeTargets.Method)]
    public partial class UseOperatorAttribute : Attribute
    {

    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-05 12:04
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class UseOperatorAttribute 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public UseOperatorAttribute()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Operator## ##Map##
        implement ToString Operator=##Operator##, Map=##Map##
        implement equals Operator, Map
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Operator">operator, którym zostanie zastąpiony sugerowany</param>
        /// </summary>
        public UseOperatorAttribute(string Operator)
        {
            this._operator = Operator;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Operator">operator, którym zostanie zastąpiony sugerowany</param>
        /// <param name="Map"></param>
        /// </summary>
        public UseOperatorAttribute(string Operator, string Map)
        {
            this._operator = Operator;
            this.map = Map;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Operator; operator, którym zostanie zastąpiony sugerowany
        /// </summary>
        public const string PROPERTYNAME_OPERATOR = "Operator";
        /// <summary>
        /// Nazwa własności Map; 
        /// </summary>
        public const string PROPERTYNAME_MAP = "Map";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// operator, którym zostanie zastąpiony sugerowany; własność jest tylko do odczytu.
        /// </summary>
        public string Operator
        {
            get
            {
                return _operator;
            }
        }
        private string _operator = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string Map
        {
            get
            {
                return map;
            }
        }
        private string map = string.Empty;
        #endregion Properties

    }
}
