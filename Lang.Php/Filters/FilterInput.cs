using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Filters
{
    public class FilterInput
    {
        public enum Type
        {
            [RenderValue("INPUT_GET")]
            Get,
            [RenderValue("INPUT_POST")]
            Post,
            [RenderValue("INPUT_COOKIE")]
            Cookie,
            [RenderValue("INPUT_SERVER")]
            Server,
            [RenderValue("INPUT_ENV")]
            Env
        }
        //public static string Filter(Type type, string variable_name )
        //{
        //    // mixed filter_input ( int $type , string $variable_name 
        // [, int $filter = FILTER_DEFAULT [, mixed $options ]] )
        //    // INPUT_GET, INPUT_POST, INPUT_COOKIE, INPUT_SERVER, or INPUT_ENV.
        //}

        public static bool? ValidateBoolean(Type type, string variable_name)
        {
            throw new NotImplementedException();
        }
        public static bool ValidateBoolean(Type type, string variable_name, bool defaultValue)
        {
            throw new NotImplementedException();
        }
        public static string ValidateEmail(Type type, string variable_name, EmailOptions options = null)
        {
            throw new NotImplementedException();
        }
        public static string ValidateFloat(Type type, string variable_name, bool ALLOW_THOUSAND, FloatOptions options = null)
        {
            throw new NotImplementedException();
        }
        public static string ValidateInt(Type type, string variable_name, IntFlags flags, IntOptions options = null)
        {
            throw new NotImplementedException();
        }
        public static string ValidateIp(Type type, string variable_name, IpFlags flags = 0, IpOptions options = null)
        {
            throw new NotImplementedException();
        }
        public static string ValidateRegexp(Type type, string variable_name, RegExpOptions options)
        {
            throw new NotImplementedException();
        }
        public static string ValidateUrl(Type type, string variable_name, UrlFlags flags, RegExpOptions options)
        {
            throw new NotImplementedException();
        }

    }
}
