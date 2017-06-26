namespace Lang.Php
{
    public class MbPhpString
    {
        #region Methods

        // Public Methods 

        /// <summary>
        /// Returns str with all alphabetic characters converted to lowercase., <see cref="http://us1.php.net/manual/en/function.mb-strtolower.php">PHP Manual</see>
        /// </summary>
        /// <param name="str">The string being lowercased. </param>
        /// <returns></returns>
        [DirectCall("mb_strtolower")]
        public static string ToLower(string str)
        {
            return str.ToLower();
        }

        /// <summary>
        /// Returns str with all alphabetic characters converted to lowercase., <see cref="http://us1.php.net/manual/en/function.mb-strtolower.php">PHP Manual</see>
        /// </summary>
        /// <param name="str">The string being lowercased. </param>
        /// <param name="encoding">The encoding parameter is the character encoding. If it is omitted, the internal character encoding value will be used.</param>
        /// <returns></returns>
        [DirectCall("mb_strtolower")]
        public static string ToLower(string str, string encoding)
        {
            return str.ToLower();
        }



        [DirectCall("mb_strlen")]
        public static int Len(string str, string encoding)
        {
            return str.Length;
        }


        [DirectCall("mb_strlen")]
        public static int Len(string str)
        {
            return str.Length;
        }
        #endregion Methods
    }
}
