using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lang.Php.Runtime;

namespace Lang.Php.Compiler.Source
{
    public class PhpConstValue : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="Value"></param>
        /// </summary>
        public PhpConstValue(object Value)
        {
            this.Value = Value;
        }

        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="Value"></param>
        ///     <param name="UseGlue"></param>
        /// </summary>
        public PhpConstValue(object Value, bool UseGlue)
        {
            this.Value   = Value;
            this.UseGlue = UseGlue;
        }
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

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            var b = style != null && style.Compression != EmitStyleCompression.Beauty;
            var a = PhpValues.ToPhpCodeValue(Value, b);
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


        /// <summary>
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// </summary>
        public bool UseGlue { get; set; }
    }
}