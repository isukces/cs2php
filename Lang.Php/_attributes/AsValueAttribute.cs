using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor
    implement Constructor *
    
    property Glue bool Czy można sklejać wartości stałej z pozostałym tekstem (jeśli stała tekstowa występuje w wyrażeniu z innym tekstem)
    smartClassEnd
    */
    
    /// <summary>
    /// Atrybut określa, że stała nie będzie nigdzie deklarowana, a wszystkie jej wystąpienia zostaną zastąpione watościami
    /// <remarks>
    /// Własność glue dla przykładu 
    ///     const string CONST = "Some tekst"
    ///     x = "Mój tekst" + CONST;
    ///     jeśli glue = false
    ///     $x = 'Mój tekst' . 'Some tekst'
    ///     jeśli glue = true
    ///     $x = 'Mój tekstSome tekst'
    /// </remarks>
    /// </summary>

    public partial class AsValueAttribute : Attribute
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-13 17:09
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class AsValueAttribute 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AsValue()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Glue##
        implement ToString Glue=##Glue##
        implement equals Glue
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AsValueAttribute()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Glue">Czy można sklejać wartości stałej z pozostałym tekstem (jeśli stała tekstowa występuje w wyrażeniu z innym tekstem)</param>
        /// </summary>
        public AsValueAttribute(bool Glue)
        {
            this.Glue = Glue;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Glue; Czy można sklejać wartości stałej z pozostałym tekstem (jeśli stała tekstowa występuje w wyrażeniu z innym tekstem)
        /// </summary>
        public const string PROPERTYNAME_GLUE = "Glue";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Czy można sklejać wartości stałej z pozostałym tekstem (jeśli stała tekstowa występuje w wyrażeniu z innym tekstem)
        /// </summary>
        public bool Glue
        {
            get
            {
                return glue;
            }
            set
            {
                glue = value;
            }
        }
        private bool glue;
        #endregion Properties

    }
}
