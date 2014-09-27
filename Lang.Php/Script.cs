using System;
using System.Collections.Generic;

namespace Lang.Php
{
    [BuiltIn]
    public class Script
    {
        //[ScriptName("PHP_EOL")]
        public const string PHP_EOL = "\r\n";

        public static Dictionary<string, UploadFileInfo> Files;
        [GlobalVariable("_GET")]
        public static Dictionary<string, object> Get;
        [GlobalVariable("_POST")]
        public static Dictionary<string, object> Post;
        [GlobalVariable("_SERVER")]
        public static _Server Server;
        [GlobalVariable("_SESSION")]
        public static Dictionary<string, object> Session;

      

        /// <summary>
        /// This function checks to ensure that the file designated by filename is a valid upload file (meaning that it was uploaded via PHP's HTTP POST upload mechanism). If the file is valid, it will be moved to the filename given by destination. 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [DirectCall("move_uploaded_file")]
        public static bool MoveUploadedFile(string filename, string destination)
        {
            throw new NotImplementedException();
        }

        [DirectCall("get_magic_quotes_gpc")]
        public static bool get_magic_quotes_gpc()
        {
            throw new NotImplementedException();
        }

        [DirectCall("stripslashes")]
        public static string StripSlashes(string x)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// string gmdate ( string $format [, int $timestamp = time() ] )
        /// </summary>
        /// <param name="format"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        [DirectCall("gmdate")]
        public static string GmDate(string format, int timestamp)
        {
            throw new NotImplementedException();
        }

        [DirectCall("gmdate")]
        public static string GmDate(string format )
        {
            throw new NotImplementedException();
        }

    }
}
