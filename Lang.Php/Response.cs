using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    public class Response
    {
        public static Runtime.HttpResponse RuntimeResponse;
        internal static Runtime.HttpResponse GetRuntime()
        {
            if (RuntimeResponse == null)
                throw new Exception("Runtime response is not set, unable to simpulate PHP envirnoment");
            return RuntimeResponse;
        }

        [Obsolete("nie skończone")]
        [DirectCall("htmlspecialchars")]
        public static string HtmlSpecialChars(string _string, HtmlEntitiesFlags flags)
        {
            //  string htmlspecialchars ( string $string [, int $flags = ENT_COMPAT | ENT_HTML401 [, string $encoding = 'UTF-8' [, bool $double_encode = true ]]] )
            throw new NotImplementedException();
        }

        [Obsolete("nie skończone")]
        [DirectCall("htmlspecialchars")]
        public static string HtmlSpecialChars(string _string)
        {
            //  string htmlspecialchars ( string $string [, int $flags = ENT_COMPAT | ENT_HTML401 [, string $encoding = 'UTF-8' [, bool $double_encode = true ]]] )
            throw new NotImplementedException();
        }

        [DirectCall("echo")]
        public static void Echo(string x)
        {
            throw new NotImplementedException();
        }

        [DirectCall("urlencode")]
        public static string UrlEncode(string x)
        {
            throw new NotImplementedException();

        }
        [DirectCall("urldecode ")]
        public static string UrlDecode(string x)
        {
            throw new NotImplementedException();
        }

        public static void SetContentType(string ct, Charsets charset)
        {
        }

        /// <summary>
        /// string htmlentities ( string $string [, int $flags = ENT_COMPAT | ENT_HTML401 [, string $encoding = 'UTF-8' [, bool $double_encode = true ]]] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("htmlentities")]
        public static string HtmlEntities(string _string)
        {
            throw new NotImplementedException();
        }

        [DirectCall("htmlentities")]
        public static string HtmlEntities(string _string, HtmlEntitiesFlags flags)
        {
            throw new NotImplementedException();
        }
        [DirectCall("htmlentities")]
        public static string HtmlEntities(string _string, HtmlEntitiesFlags flags, Charsets encoding)
        {
            throw new NotImplementedException();
        }
        [DirectCall("htmlentities")]
        public static string HtmlEntities(string _string, HtmlEntitiesFlags flags, Charsets encoding, bool doubleEncode)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// void header ( string $string [, bool $replace = true [, int $http_response_code ]] )
        /// </summary>
        /// <param name="_string"></param>
        [DirectCall("header")]
        public static void Header(string _string)
        {
            throw new NotImplementedException();
        }
        [DirectCall("header")]
        public static void Header(string _string, bool replace)
        {
            throw new NotImplementedException();
        }
        [DirectCall("header")]
        public static void Header(string _string, bool replace, int httpResponseCode)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// bool ob_start ([ callable $output_callback [, int $chunk_size = 0 [, bool $erase = true ]]] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("ob_start")]
        public static bool ObStart()
        {
            throw new NotImplementedException();
        }
        [DirectCall("ob_start")]
        public static bool ObStart(OutputCallbackDelegate1 outputCallback)
        {
            throw new NotImplementedException();
        }
        [DirectCall("ob_start")]
        public static bool ObStart(OutputCallbackDelegate1 outputCallback, int chunkSize)
        {
            throw new NotImplementedException();
        }
        [DirectCall("ob_start")]
        public static bool ObStart(OutputCallbackDelegate1 outputCallback, int chunkSize, bool erase)
        {
            throw new NotImplementedException();
        }


        [DirectCall("ob_start")]
        public static bool ObStart(OutputCallbackDelegate2 outputCallback)
        {
            throw new NotImplementedException();
        }
        [DirectCall("ob_start")]
        public static bool ObStart(OutputCallbackDelegate2 outputCallback, int chunkSize)
        {
            throw new NotImplementedException();
        }
        [DirectCall("ob_start")]
        public static bool ObStart(OutputCallbackDelegate2 outputCallback, int chunkSize, bool erase)
        {
            throw new NotImplementedException();
        }


    }
}
