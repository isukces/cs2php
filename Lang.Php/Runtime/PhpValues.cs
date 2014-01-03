using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Lang.Php.Runtime
{
    public class PhpValues
    {
        #region Static Methods

        // Public Methods 

        public static string ConvertX(string strToConvert, string separator)
        {
            var sb = new StringBuilder(strToConvert.Length * 2);
            foreach (var c in strToConvert)
            {
                if (c >= 'A' && c <= 'Z' && sb.Length > 0)
                    sb.Append(separator);
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static PhpCodeValue EnumToPhpCode(object enumValue, bool beauty)
        {
            var DeclaringType = enumValue.GetType();
            if (!DeclaringType.IsEnum)
                throw new ArgumentException("value is not enum");
            {
                var _enumRender = DeclaringType.GetCustomAttributes(true).OfType<EnumRenderAttribute>().FirstOrDefault();
                if (_enumRender != null)
                {
                    if (_enumRender.Option == EnumRenderOptions.OctalNumbers)
                        return PhpCodeValue.FromInt((int)enumValue, true);
                    if (_enumRender.Option == EnumRenderOptions.OctalNumbers)
                        return PhpCodeValue.FromInt((int)enumValue, false);
                }
            }
            // var values = Enum.GetValues(DeclaringType);
            // var names = Enum.GetNames(DeclaringType);
            var fields = DeclaringType.GetFields(BindingFlags.Static | BindingFlags.Public);
            string[] txt = enumValue.ToString().Split('|').Select(i => i.Trim()).ToArray();
            if (txt.Length == 1)
                return _SingleEnumValueToPhpCode(enumValue); // not a flag value
            var dict = fields.ToDictionary(i => i, i => i.GetValue(null));
            for (int i = 0; i < txt.Length; i++)
            {
                var fieldInfo = dict.Where(ii => ii.Value.ToString() == txt[i]).Select(ii => ii.Key).FirstOrDefault();
                if (fieldInfo == null) continue;
                var fieldValue = fieldInfo.GetValue(null);
                PhpCodeValue tmp = _SingleEnumValueToPhpCode(fieldValue);
                txt[i] = tmp.PhpValue;
            }
            return new PhpCodeValue(string.Join(beauty ? " | " : "|", txt), PhpCodeValue.Kinds.Other);
        }


        private static int _SingleQuoteEscapedCharsCount(string x)
        {
            int i = 0;
            foreach (var c in x)
                if (c == '\\' || c == '\'')
                    i++;
            return i;
        }


        public static string PhpStringEmit(string txt, bool beauty)
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
            string join = beauty ? " . " : ".";
            return string.Join(join, items);
        }

        /// <summary>
        /// Zamienia wartość .NET na wartość tekstową PHP
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static PhpCodeValue ToPhpCodeValue(object value, bool beauty = true)
        {
            if (value == null) 
                return new PhpCodeValue("", PhpCodeValue.Kinds.Null);
            var type = value.GetType();
            if (type.IsEnum)
                return _SingleEnumValueToPhpCode(value);
            if (value is bool) return PhpCodeValue.FromBool((bool)value);
            if (value is string) return PhpCodeValue.FromString(value as string);
            if (value is int) return PhpCodeValue.FromInt((int)value);
            if (value is double) return PhpCodeValue.FromDouble((double)value);
            throw new NotSupportedException();
        }

        public static bool TryGetPhpStringValue(string a, out string x)
        {
            const string bs = "\\";
            x = null;
            if (a == null) return false;
            a = a.Trim();
            if (a.Length < 2)
                return false;
            if (a.StartsWith("'") && a.EndsWith("'"))
            {
                a = a.Substring(1, a.Length - 2);
                if (a.Length == 0)
                {
                    x = "";
                    return true;
                }
                x = a
                    .Replace(bs + "'", "")
                    .Replace(bs + bs, "");
                if (a.IndexOf(bs) > 0 || a.IndexOf("'") > 0)
                    return false;
                x = a
                    .Replace(bs + "'", "'")
                    .Replace(bs + bs, bs);

                return true;

            }
            x = null;
            return false;
        }
        // Private Methods 

        private static int _DoubleQuoteEscapedCharsCount(string x)
        {
            int i = 0;
            foreach (var c in x)
            {
                if (c == '\\' || c == '"' || c == '\n' || c == '\r' || c == '\t' || c == '\v' || c == _ESCchar || c == '\f' || c == '$')
                    i++;
            }
            return i;
        }
        const char _ESCchar = (char)27;
        private static int _InvisibleCharsCount(string x)
        {
            return x.Where(i => i < ' ').Count();
        }

        static PhpCodeValue _SingleEnumValueToPhpCode(object enumValue)
        {
            Type DeclaringType = enumValue.GetType();
            {
                var customAttributes = DeclaringType.GetCustomAttributes(true);
                #region EnumRenderAttribute
                var _enumRender = DeclaringType.GetCustomAttributes(true).OfType<EnumRenderAttribute>().FirstOrDefault();
                if (_enumRender != null)
                {
                    string v;
                    switch (_enumRender.Option)
                    {
                        case EnumRenderOptions.TheSameString:
                            v = enumValue.ToString();
                            break;
                        case EnumRenderOptions.UnderscoreLowercase:
                            v = ConvertX(enumValue.ToString(), "_").ToLower();
                            break;
                        case EnumRenderOptions.MinusLowercase:
                            v = ConvertX(enumValue.ToString(), "-").ToLower();
                            break;
                        case EnumRenderOptions.Numbers:
                            return PhpCodeValue.FromInt((int)enumValue, false);
                        case EnumRenderOptions.OctalNumbers:
                            return PhpCodeValue.FromInt((int)enumValue, true);
                        case EnumRenderOptions.UnderscoreUppercase:
                            v = ConvertX(enumValue.ToString(), "_").ToUpper();
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                    if (!_enumRender.DefinedConst)
                        v = EscapeSingleQuote(v);
                    return new PhpCodeValue(v, _enumRender.DefinedConst ? PhpCodeValue.Kinds.DefinedConst : PhpCodeValue.Kinds.StringConstant);

                }
                #endregion
            }
            {
                //field
                var fi = DeclaringType.GetFields().Where(i => i.Name == enumValue.ToString()).FirstOrDefault();
                if (fi != null)
                {
                    var customAttributes = fi.GetCustomAttributes(true);
                    var _rv = fi.GetCustomAttributes(true).OfType<RenderValueAttribute>().FirstOrDefault();
                    if (_rv != null)
                    {
                        string str;
                        if (TryGetPhpStringValue(_rv.Name, out str))
                            return PhpCodeValue.FromString(str);
                        if (!_rv.Name.StartsWith("$"))
                            return new PhpCodeValue(_rv.Name, _rv.Name, PhpCodeValue.Kinds.DefinedConst);
                        throw new NotSupportedException();
                    }
                    if (customAttributes.Length > 0)
                        throw new NotSupportedException();
                }
            }
            return new PhpCodeValue(EscapeSingleQuote(enumValue.ToString()), enumValue, PhpCodeValue.Kinds.Other);
        }

        public static string Dec2Oct(int number)
        {
            string o = "";
            while (number != 0)
            {
                var b = number % 8;
                o = b.ToString() + o;
                number = number / 8;
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

        #region Static Fields

        static readonly string _ESC = ((char)27).ToString();

        #endregion Static Fields
    }
}
