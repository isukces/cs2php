using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Runtime
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor PhpValue, Kind
    implement Constructor PhpValue, SourceValue, Kind
    implement ToString ##PhpValue##
    
    property PhpValue string Value on PHP side
    
    property SourceValue object 
    
    property Kind Kinds 
    smartClassEnd
    */

    public partial class PhpCodeValue
    {
        #region Static Methods

        // Public Methods 

        public static PhpCodeValue FromBool(bool v)
        {
            var txt = v ? "true" : "false";
            return new PhpCodeValue(txt, v, Kinds.Bool);
        }

        public static PhpCodeValue FromDouble(double v)
        {
            var txt = v.ToString(System.Globalization.CultureInfo.InvariantCulture);
            return new PhpCodeValue(txt, v, Kinds.Double);
        }

        public static PhpCodeValue FromInt(int v, bool octal = false)
        {
            var txt = octal ? PhpValues.Dec2Oct(v) : v.ToString();
            return new PhpCodeValue(txt, v, octal ? Kinds.OctalInt : Kinds.Int);
        }

        public static PhpCodeValue FromString(string txt)
        {
            return new PhpCodeValue(PhpValues.PhpStringEmit(txt, true), txt, Kinds.StringConstant);
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public bool TryGetPhpString(out string txt)
        {
            switch (kind)
            {
                case Kinds.StringConstant:
                    if (PhpValues.TryGetPhpStringValue(this.phpValue, out txt))
                        return true;
                    throw new NotSupportedException();
                case Kinds.Bool:
                    txt = (bool)sourceValue ? "1" : "";
                    return true;
                case Kinds.Int:
                case Kinds.OctalInt:
                    txt = ((int)sourceValue).ToString();
                    return true;
                case Kinds.Double:
                    txt = phpValue;
                    return true;
                case Kinds.DefinedConst:
                    txt = "";
                    return false;
                case Kinds.Null:
                    txt = "";
                    return true;
                default:
                    throw new NotSupportedException();

            }
        }

        #endregion Methods

        #region Enums

        public enum Kinds
        {
            Other, // ie. flags
            StringConstant,
            Int,
            OctalInt,
            DefinedConst,
            Bool,
            Double,
            Null
        }

        #endregion Enums
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-26 18:53
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Runtime
{
    public partial class PhpCodeValue
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpCodeValue()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##PhpValue## ##SourceValue## ##Kind##
        implement ToString PhpValue=##PhpValue##, SourceValue=##SourceValue##, Kind=##Kind##
        implement equals PhpValue, SourceValue, Kind
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="PhpValue">Value on PHP side</param>
        /// <param name="Kind"></param>
        /// </summary>
        public PhpCodeValue(string PhpValue, Kinds Kind)
        {
            this.PhpValue = PhpValue;
            this.Kind = Kind;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="PhpValue">Value on PHP side</param>
        /// <param name="SourceValue"></param>
        /// <param name="Kind"></param>
        /// </summary>
        public PhpCodeValue(string PhpValue, object SourceValue, Kinds Kind)
        {
            this.PhpValue = PhpValue;
            this.SourceValue = SourceValue;
            this.Kind = Kind;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności PhpValue; Value on PHP side
        /// </summary>
        public const string PROPERTYNAME_PHPVALUE = "PhpValue";
        /// <summary>
        /// Nazwa własności SourceValue; 
        /// </summary>
        public const string PROPERTYNAME_SOURCEVALUE = "SourceValue";
        /// <summary>
        /// Nazwa własności Kind; 
        /// </summary>
        public const string PROPERTYNAME_KIND = "Kind";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", phpValue);
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// Value on PHP side
        /// </summary>
        public string PhpValue
        {
            get
            {
                return phpValue;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                phpValue = value;
            }
        }
        private string phpValue = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public object SourceValue
        {
            get
            {
                return sourceValue;
            }
            set
            {
                sourceValue = value;
            }
        }
        private object sourceValue;
        /// <summary>
        /// 
        /// </summary>
        public Kinds Kind
        {
            get
            {
                return kind;
            }
            set
            {
                kind = value;
            }
        }
        private Kinds kind;
        #endregion Properties

    }
}
