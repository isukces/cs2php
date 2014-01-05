using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Filters
{
    public class FilterInputServer
    {
        [Obsolete("Has bug on Debian !!! http://bugs.debian.org/cgi-bin/bugreport.cgi?bug=730094")]
        public static bool? ValidateBoolean(ServerVariables variable)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Has bug on Debian !!! http://bugs.debian.org/cgi-bin/bugreport.cgi?bug=730094")]
        public static bool ValidateBoolean(ServerVariables variable, bool defaultValue)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Has bug on Debian !!! http://bugs.debian.org/cgi-bin/bugreport.cgi?bug=730094")]
        public static string ValidateEmail(ServerVariables variable, EmailOptions options = null)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Has bug on Debian !!! http://bugs.debian.org/cgi-bin/bugreport.cgi?bug=730094")]
        public static string ValidateFloat(ServerVariables variable, bool ALLOW_THOUSAND, FloatOptions options = null)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Has bug on Debian !!! http://bugs.debian.org/cgi-bin/bugreport.cgi?bug=730094")]
        public static string ValidateInt(ServerVariables variable, IntFlags flags)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Has bug on Debian !!! http://bugs.debian.org/cgi-bin/bugreport.cgi?bug=730094")]
        public static string ValidateIp(ServerVariables variable, IpFlags flags = 0, IpOptions options = null)
        {
            throw new NotImplementedException();
        }
        [Obsolete("Has bug on Debian !!! http://bugs.debian.org/cgi-bin/bugreport.cgi?bug=730094")]
        public static string ValidateRegexp(ServerVariables variable, RegExpOptions options)
        {
            throw new NotImplementedException();
        }
        public static string ValidateUrl(ServerVariables variable, UrlFlags flags, RegExpOptions options)
        {
            throw new NotImplementedException();
        }

    }
}
