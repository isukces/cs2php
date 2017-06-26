using Lang.Php.Runtime;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor Value
    implement Constructor Value, UseGlue
    
    property Value Object 
    
    property UseGlue bool 
    smartClassEnd
    */

    public partial class PhpConstValue : IPhpValueBase
    {
        #region Static Methods

        // Public Methods 

        public static PhpConstValue FromPhpValue(string code)
        {
            var m = Regex.Match(code, "^(-?\\d)+$");
            if (m.Success)
            {
                var value = int.Parse(code);
                return new PhpConstValue(value);
            }
            throw new NotImplementedException("Only integer values are supported. Sorry.");
        }





        private static string EscapeSingleQuote(string x)
        {
            return "'" + x.Replace("\\", "\\\\").Replace("'", "\\'") + "'";
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            var b = style != null && style.Compression != EmitStyleCompression.Beauty;
            var a = PhpValues.ToPhpCodeValue(_value, b);
            switch (a.Kind)
            {
                case PhpCodeValue.Kinds.Null:
                    return "null";
                default:
                    return a.PhpValue;
            }
            //if (_value == null)
            //    return "null";
            //var tt = _value.GetType();
            //if (_value is string)
            //    return PhpStringEmit(_value as string, style);
            //if (_value is double)
            //    return ((double)_value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            //if (_value is bool)
            //    return ((bool)_value) ? "true" : "false";
            //if (tt.IsEnum)
            //{
            //    var tmp = PhpValues.EnumToPhpCode(_value, );
            //    return tmp.Value;

            //}
            //return _value.ToString();
        }

        #endregion Methods

        #region Static Fields



        #endregion Static Fields

        #region Fields

   

        #endregion Fields
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-13 17:35
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpConstValue
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpConstValue()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Value## ##UseGlue##
        implement ToString Value=##Value##, UseGlue=##UseGlue##
        implement equals Value, UseGlue
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Value"></param>
        /// </summary>
        public PhpConstValue(Object Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Value"></param>
        /// <param name="UseGlue"></param>
        /// </summary>
        public PhpConstValue(Object Value, bool UseGlue)
        {
            this.Value = Value;
            this.UseGlue = UseGlue;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Value; 
        /// </summary>
        public const string PROPERTYNAME_VALUE = "Value";
        /// <summary>
        /// Nazwa własności UseGlue; 
        /// </summary>
        public const string PROPERTYNAME_USEGLUE = "UseGlue";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        private Object _value;
        /// <summary>
        /// 
        /// </summary>
        public bool UseGlue
        {
            get
            {
                return useGlue;
            }
            set
            {
                useGlue = value;
            }
        }
        private bool useGlue;
        #endregion Properties
    }
}
