using Lang.Php;
using Lang.Php.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                int value = int.Parse(code);
                return new PhpConstValue(value);
            }
            throw new NotImplementedException("Only integer values are supported. Sorry.");
        }

        public static string PhpStringEmit(string txt, PhpEmitStyle style)
        {
            if (txt == null)
                return "null";
            if (txt.Length == 0)
                return "''";
            {
                var a1 = _InvisibleCharsCount(txt);
                if (a1 > 0)
                    return EscapeDoubleQuote(txt);
                //if (txt.IndexOf("'")>0)
                //    return EscapeDoubleQuote(txt);
                var a2 = _SingleQuoteEscapedCharsCount(txt);
                var a3 = _DoubleQuoteEscapedCharsCount(txt);
                if (a2 <= a3)
                    return EscapeSingleQuote(txt);
                return EscapeDoubleQuote(txt);
            }

            // http://www.php.net/manual/en/language.types.string.php#language.types.string.syntax.single

            List<string> items = new List<string>();
            while (txt.Length > 0)
            {
                var i = txt.IndexOf("\r\n");
                //i = -1;
                if (i < 0)
                {
                    items.Add(EscapeSingleQuote(txt));
                    break;
                }
                else
                {
                    if (i > 0)
                        items.Add(EscapeSingleQuote(txt.Substring(0, i)));
                    items.Add("PHP_EOL");
                    txt = txt.Substring(i + 2);
                }
            }
            string join = style == null || style.Compression == EmitStyleCompression.Beauty ? " . " : ".";
            return string.Join(join, items);
        }
        // Private Methods 

        private static int _DoubleQuoteEscapedCharsCount(string x)
        {
            int i = 0;
            foreach (var c in x)
            {
                if (c == '\\' || c == '"' || c == '\n' || c == '\r' || c == '\t' || c == '\v' || c == _ESCc || c == '\f' || c == '$')
                    i++;
            }
            return i;
        }

        private static int _InvisibleCharsCount(string x)
        {
            return x.Where(i => i < ' ').Count();
        }

        private static int _SingleQuoteEscapedCharsCount(string x)
        {
            int i = 0;
            foreach (var c in x)
                if (c == '\\' || c == '\'')
                    i++;
            return i;
        }

        static string Dec2Oct(int a)
        {
            string o = "";
            while (a != 0)
            {
                var b = a % 8;
                o = b.ToString() + o;
                a = a / 8;
            }
            return "0" + o;
        }

        private static string EscapeDoubleQuote(string x)
        {
            if (x == null)
                return "null";
            if (x.Length == 0)
                return "\"\"";
            /*
             * \n 	linefeed (LF or 0x0A (10) in ASCII)
\r 	carriage return (CR or 0x0D (13) in ASCII)
\t 	horizontal tab (HT or 0x09 (9) in ASCII)
\v 	vertical tab (VT or 0x0B (11) in ASCII) (since PHP 5.2.5)
\e 	escape (ESC or 0x1B (27) in ASCII) (since PHP 5.4.0)
\f 	form feed (FF or 0x0C (12) in ASCII) (since PHP 5.2.5)
\\ 	backslash
\$ 	dollar sign
\" 	double-quote
\[0-7]{1,3} 	the sequence of characters matching the regular expression is a character in octal notation
\x[0-9A-Fa-f]{1,2} 	the sequence of characters matching the regular expression is a character in hexadecimal notation 
             */
            x = x.Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t")
                .Replace("\v", "\\v")
                .Replace(_ESC, "\\e")
                .Replace("\f", "\\f")
                .Replace("$", "\\$");
            return "\"" + x + "\"";
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
            if (_value == null)
                return "null";
            var tt = _value.GetType();
            if (_value is string)
                return PhpStringEmit(_value as string, style);
            if (_value is double)
                return ((double)_value).ToString(System.Globalization.CultureInfo.InvariantCulture);
            if (_value is bool)
                return ((bool)_value) ? "true" : "false";
            if (Value is UnixFilePermissions)
            {
                var g = (int)(UnixFilePermissions)_value;
                return Dec2Oct(g);
            }
            if (Value is UnixFileGroupPermissions)
            {
                var g = (int)(UnixFileGroupPermissions)_value;
                return Dec2Oct(g);
            }
            if (tt.IsEnum)
            {
                var a = Enum.GetValues(tt);
                var n = Enum.GetNames(tt);
                var f = tt.GetFields(BindingFlags.Static | BindingFlags.Public);
                var txt = _value.ToString().Split('|').Select(i => i.Trim()).ToArray();
                var dict = f.ToDictionary(i => i, i => i.GetValue(null));
                for (int i = 0; i < txt.Length; i++)
                {
                    var fieldInfo = dict.Where(ii => ii.Value.ToString() == txt[i]).Select(ii => ii.Key).FirstOrDefault();
                    if (fieldInfo == null) continue;
                    var fieldValue = fieldInfo.GetValue(null);
                    var tmp = PhpValues.FromEnum(fieldValue);
                    if (tmp is string)
                    {
                        //string str;
                        //if (PhpValues.TryGetPhpStringValue(tmp as string, out str))
                        //    tmp = str;
                        //else
                        //    throw new NotSupportedException();
                        txt[i] = tmp as string;
                    }
                    else
                    {
                        throw new NotSupportedException();
                        var tmp1 = new PhpConstValue(tmp);
                        string phpValue = tmp1.GetPhpCode(style);
                        //var at = fieldInfo.GetCustomAttributes<RenderValueAttribute>(true).FirstOrDefault();
                        //if (at == null) continue;
                        txt[i] = phpValue;
                    }
                }
                var b = style == null || style.Compression == EmitStyleCompression.Beauty;
                return string.Join(b ? " | " : "|", txt);
            }
            return _value.ToString();
        }

        #endregion Methods

        #region Static Fields

        static readonly string _ESC = ((char)27).ToString();

        #endregion Static Fields

        #region Fields

        const char _ESCc = ((char)27);

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
