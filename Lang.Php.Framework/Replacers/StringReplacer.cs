using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Framework.Replacers
{

    /// <summary>
    /// Klasa zawiera lustrzane metody dla klasy System.String
    /// </summary>
    [Replace(typeof(string))]
    internal class StringReplacer
    {
        #region Methods


        [DirectCall("empty")]
        static bool IsNullOrEmpty(string value)
        {
            throw new MockMethodException();
        }

        // Public Methods 

        [DirectCall("mb_substr", "this,0,1")]
        string Substring(int startIndex, int length)
        {
            throw new MockMethodException();

        }


        [DirectCall("mb_substr", "this,0")]
        string Substring(int startIndex)
        {
            throw new MockMethodException();

        }

 
        // Private Methods 

        [DirectCall("mb_strpos", "this,0")]
        int IndexOf(string value)
        {
            throw new MockMethodException();
        }

        #endregion Methods

        [DirectCall("mb_strtolower", "this")]
        string ToLower()
        {
            throw new MockMethodException();
        }
        [DirectCall("trim", "this")]
        string Trim()
        {
            throw new MockMethodException();
        }

        [UseOperator("+")]
        public static string Concat(string str0, string str1)
        {
            return str0 + str1;
        }

        #region Properties

        [DirectCall("mb_strlen", "this")]
        int Length { get; set; }

        #endregion Properties
    }
}
