using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public class ResponseHeader
    {
        #region Static Methods

        // Public Methods 

        public static void ContentType(string type, bool replace = false)
        {
            throw new MockMethodException();
        }

        // public static void CacheControl()
        public static void Expires(DateTime dt)
        {
            throw new MockMethodException();
        }

        public static void Head(string key, string value)
        {

        }

        public static void LastModified(DateTime dt)
        {
            throw new MockMethodException();
        }

        public static void Pragma(HttpPragma pragma)
        {
            throw new MockMethodException();
        }

        #endregion Static Methods

        // header("Content-Transfer-Encoding: binary");
        //header("Cache-Control: public, must-revalidate, max-age=0");
        //   header("Pragma: public");
        //   var now = DateTime.Now;
        //   header("Expires: " + now.AddDays(2.5).AddHours(2).PhpFormat(DateTimeFormats.HttpHeader));
        //   header("Last-Modified: " + now.PhpFormat(DateTimeFormats.HttpHeader));
        //   header("Content-Type: application/force-download");
        //   header("Content-Type: application/octet-stream", false);
        //   header("Content-Type: application/download", false);
        //   header("Content-Type: application/pdf", false);
    }
}
