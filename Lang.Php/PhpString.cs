using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Lang.Php
{

    public class PhpString
    {
		#region Static Methods 

		// Public Methods 

        /// <summary>
        /// string implode (  array $pieces )
        /// </summary>
        /// <param name="glue"></param>
        /// <param name="pieces"></param>
        /// <returns></returns>
        [DirectCall("implode")]
        [Since("4.3.0")]
        public static string Implode(string[] pieces)
        {
            return string.Join("", pieces);
        }

        /// <summary>
        /// string implode ( string $glue , array $pieces )
        /// </summary>
        /// <param name="glue"></param>
        /// <param name="pieces"></param>
        /// <returns></returns>
        [DirectCall("implode")]
        public static string Implode(string glue, string[] pieces)
        {
            return string.Join(glue, pieces);
        }

		#endregion Static Methods 

        ///// <summary>
        ///// array mb_split ( string $pattern , string $string [, int $limit = -1 ] )
        ///// </summary>
        ///// <param name="separator"></param>
        ///// <returns></returns>
        //[Since("4.2.0")]
        //[DirectCall("???")]
        //private string[] Split(params char[] separator)
        //{
        //    throw new MockMethodException();
        //}
        //[DirectCall("mb_substr", "this,0")]
        //public string Substring(int startIndex)
        //{
        //    throw new NotSupportedException();
        //}
    }
}
