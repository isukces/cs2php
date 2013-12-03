using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php 
{
    [Skip]
    public static class DateTimeExtension
    {
        [DirectCall("date_format ")]
        public static string PhpFormat(this DateTime x, DateTimeFormats format)
        {
            throw new NotSupportedException();
        }
        [DirectCall("date_format ")]
        public static string PhpFormat(this DateTime x, string format)
        {
            throw new NotSupportedException();
        }
    }
}
