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

        public static object FromEnum(object enumValue)
        {
            Type DeclaringType = enumValue.GetType();
            if (DeclaringType == typeof(UnixFilePermissions))
                return enumValue;

            {
                var bb = DeclaringType.GetCustomAttributes(true);
                var _enumRender = DeclaringType.GetCustomAttributes(true).OfType<EnumRenderAttribute>().FirstOrDefault();
                if (_enumRender != null)
                {
                    switch (_enumRender.Option)
                    {
                        case EnumRenderOptions.TheSameString:
                            return enumValue.ToString();
                        case EnumRenderOptions.UnderscoreLowercase:
                            return ConvertX(enumValue.ToString(), "_").ToLower();
                        case EnumRenderOptions.MinusLowercase:
                            return ConvertX(enumValue.ToString(), "-").ToLower();
                        case EnumRenderOptions.Numbers:
                            enumValue = (int)enumValue;
                            return enumValue;
                        case EnumRenderOptions.UnderscoreUppercase:
                            return ConvertX(enumValue.ToString(), "_").ToUpper();
                        default:
                            throw new NotSupportedException();
                    }
                }
            }
            {
                //field
                var fi = DeclaringType.GetFields().Where(i => i.Name == enumValue.ToString()).FirstOrDefault();
                if (fi != null)
                {
                    var aa = fi.GetCustomAttributes(true);
                    var _rv = fi.GetCustomAttributes(true).OfType<RenderValueAttribute>().FirstOrDefault();
                    if (_rv != null)
                    {
                        string str;
                        if (TryGetPhpStringValue(_rv.Name, out str))
                            return _rv.Name;
                        if (!_rv.Name.StartsWith("$"))
                            return _rv.Name; // zakładam że to jakaś stała
                        throw new NotSupportedException();
                    }
                    if (aa.Length > 0)
                        throw new NotSupportedException();
                }
            }
            return EscapeSingleQuote(enumValue.ToString());
        }

        /// <summary>
        /// Zamienia wartość .NET na wartość tekstową PHP
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPhpString(object value)
        {
            if (value == null) return "";
            if (value is string) return value as string;
            if (value is int) return value.ToString();
            var t = value.GetType();
            if (t.IsEnum)
                return FromEnum(value).ToString();
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

        private static string EscapeSingleQuote(string x)
        {
            return "'" + x.Replace("\\", "\\\\").Replace("'", "\\'") + "'";
        }

        #endregion Static Methods
    }
}
