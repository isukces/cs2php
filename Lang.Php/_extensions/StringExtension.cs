using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace Lang.Php
{
    public static class StringExtension
    {
        #region Static Methods

        // Public Methods 

        /// <summary>
        ///   array explode ( string $delimiter , string $string [, int $limit ] )
        /// </summary>
        /// <param name="delimiter"></param>
        /// <param name="_string"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Since("4.0.0")]
        [DirectCall("explode", "1,0")]
        public static string[] PhpExplode(this string _string, string delimiter)
        {
            throw new MockMethodException();
        }



        public static void PhpExplodeList(this string _string, string delimiter, out string o1, out string o2)
        {
            throw new MockMethodException();
        }

        public static void PhpExplodeList(this string _string, string delimiter, out string o1, out string o2, out string o3)
        {
            throw new MockMethodException();
        }

        public static void PhpExplodeList(this string _string, string delimiter, out string o1, out string o2, out string o3, out string o4)
        {
            if (delimiter.Length != 1)
                throw new MockMethodException();
            var a = _string.Split(delimiter[0]);
            o1 = a.Length > 0 ? a[0] : null;
            o2 = a.Length > 1 ? a[1] : null;
            o3 = a.Length > 2 ? a[2] : null;
            o4 = a.Length > 3 ? a[3] : null;
        }

        public static void PhpExplodeList(this string _string, string delimiter, out string o1, out string o2, out string o3, out string o4, out string o5)
        {
            throw new MockMethodException();
        }


        /// <summary>
        ///   array explode ( string $delimiter , string $string [, int $limit ] )
        /// </summary>
        /// <param name="delimiter"></param>
        /// <param name="_string"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Since("4.0.1")]
        [DirectCall("explode", "0,1,2")]
        public static string[] PhpExplode(this string _string, string delimiter, int limit = int.MaxValue)
        {
            throw new MockMethodException();
        }

        [DirectCall("empty")]
        public static bool PhpEmpty(this string x)
        {
            return string.IsNullOrEmpty(x);
        }



        [DirectCall("mb_strpos")]
        public static Falsable<int> PhpIndexOf(this string x, string value)
        {
            // // $pos = strpos($mystring, $findme);
            var y = x.IndexOf(value);
            if (y < 0)
                return Falsable<int>.False;
            return y;
        }

        #endregion Static Methods
    }
}
