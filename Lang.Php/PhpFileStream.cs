using System;

namespace Lang.Php
{
    [Skip]
    public partial class PhpFileStream
    {

    
        /// <summary>
        /// int fwrite ( resource $handle , string $string [, int $length ] )
        /// </summary>
        [DirectCall("fwrite", "this,0")]
        public void Write(string txt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// string fread ( resource $handle , int $length )
        /// </summary>
        [DirectCall("fread", "this,0")]
        public string Read(int length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// string fread ( resource $handle , int $length )
        /// </summary>
        [DirectCall("fclose", "this")]
        public void Close()
        {

        }

        /// <summary>
        /// resource fopen ( string $filename , string $mode [, bool $use_include_path = false [, resource $context ]] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("fopen")]
        public static PhpFileStream Open(string filename, FileStreamOpenModes mode, bool use_include_path = false)
        {
            throw new NotImplementedException();
        }

     

        public const string BINARY = "b";


    }
}
