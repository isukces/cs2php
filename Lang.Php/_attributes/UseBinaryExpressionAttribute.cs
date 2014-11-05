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
    
    property Operator string 
    
    property Left string 
    
    property Right string 
    smartClassEnd
    */
    
    /// <summary>
    /// Sugeruje zastąpienie (np. własności instancyjnej) operatorem (np.===)
    /// </summary>
    public partial class UseBinaryExpressionAttribute : Attribute
    {
    
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-12 09:27
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class UseBinaryExpressionAttribute 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public UseBinaryExpressionAttribute()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Operator## ##Left## ##Right##
        implement ToString Operator=##Operator##, Left=##Left##, Right=##Right##
        implement equals Operator, Left, Right
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Operator"></param>
        /// <param name="Left"></param>
        /// <param name="Right"></param>
        /// </summary>
        public UseBinaryExpressionAttribute(string Operator, string Left, string Right)
        {
            _operator = Operator;
            left = Left;
            right = Right;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Operator; 
        /// </summary>
        public const string PROPERTYNAME_OPERATOR = "Operator";
        /// <summary>
        /// Nazwa własności Left; 
        /// </summary>
        public const string PROPERTYNAME_LEFT = "Left";
        /// <summary>
        /// Nazwa własności Right; 
        /// </summary>
        public const string PROPERTYNAME_RIGHT = "Right";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
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
        public string Left
        {
            get
            {
                return left;
            }
        }
        private string left = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string Right
        {
            get
            {
                return right;
            }
        }
        private string right = string.Empty;
        #endregion Properties

    }
}
