using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public static class PhpIni
    {
        public const int MYSQL_DEFAULT_PORT = 3306;
        static PhpIni()
        {
            Values = new Dictionary<string, string>()
            {
                {"mysqli.default_host", "localhost" },
                {"mysqli.default_port", "3306" },
                {"mysqli.default_user", "root"},
                {"mysqli.default_pw", ""},
                {"mysqli.default_socket",""}
            };
        }
        public static Dictionary<string, string> Values
        {
            get;
            set;
        }
        public static bool TryGetValue(string name, out string value)
        {
            return Values.TryGetValue(name, out value);
        }
    }
}
