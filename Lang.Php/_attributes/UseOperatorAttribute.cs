using System;

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


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-11-05 18:35
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="_operator">operator, którym zostanie zastąpiony sugerowany</param>
        /// </summary>
        public UseOperatorAttribute(string _operator)
        {
            this._operator = _operator;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="_operator">operator, którym zostanie zastąpiony sugerowany</param>
        /// <param name="map"></param>
        /// </summary>
        public UseOperatorAttribute(string _operator, string map)
        {
            this._operator = _operator;
            _map = map;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Operator; operator, którym zostanie zastąpiony sugerowany
        /// </summary>
        public const string PropertyNameOperator = "Operator";
        /// <summary>
        /// Nazwa własności Map; 
        /// </summary>
        public const string PropertyNameMap = "Map";
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
                return _map;
            }
        }
        private string _map = string.Empty;
        #endregion Properties

    }
}
