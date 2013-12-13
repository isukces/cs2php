using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lang.Php
{

    [Skip]
    public class PhpDummy
    {

        [DirectCall("imagecreate")]
        public static Graph.Image imagecreate(int width, int height)
        {
            return Graph.Image.Create(width, height);
        }

        [DirectCall("getimagesize")]
        public static Graph.ImageInfo GetImageSize(string filename)
        {
            // array getimagesize ( string $filename [, array &$imageinfo ] )
            throw new NotImplementedException();
        }
      


        #region Static Methods

        // Public Methods 

        [DirectCall("_htmlspecialchars_")]
        public static string _htmlspecialchars_(string _string)
        {
            return System.Web.HttpUtility.HtmlEncode(_string);
        }

        [DirectCall("_urlencode_")]
        public static string _urlencode_(string x)
        {
            throw new NotImplementedException();

        }

        [DirectCall("array")]
        public static T[] array<T>(params T[] i)
        {
            return i;
        }

        [DirectCall("array_merge")]
        public static Dictionary<TKey, TValue> array_merge<TKey, TValue>(params Dictionary<TKey, TValue>[] arrays)
        {
            Dictionary<TKey, TValue> r = new Dictionary<TKey, TValue>();
            foreach (var i in arrays.Where(u => u != null))
            {
                foreach (var j in i)
                    r[j.Key] = j.Value;
            }
            return r;
            // array array_merge ( array $array1 [, array $... ] )
            throw new NotImplementedException();
        }

        [DirectCall("array_merge")]
        public static Dictionary<TKey, TValue> array_merge2<TKey, TValue>(Dictionary<TKey, TValue> _array1, Dictionary<TKey, TValue> _array2)
        {
            throw new NotImplementedException();
        }

        [DirectCall("array_reverse")]
        public static Dictionary<TKey, TValue> array_reverse<TKey, TValue>(Dictionary<TKey, TValue> array)
        {
            return array;
        }

        [DirectCall("base64_decode")]
        public static string base64_decode(string data)
        {
            throw new NotSupportedException();
        }

        [DirectCall("base64_encode")]
        public static string base64_encode(string data)
        {
            throw new NotSupportedException();
        }

        [DirectCall("bindec")]
        public static int bindec(string binary_string)
        {
            throw new NotSupportedException();
        }

        [DirectCall("class_exists")]
        public static bool class_exists(string name)
        {
            return true;
        }

        [DirectCall("date")]
        public static string date(string format, int? timestamp = null)
        {
            throw new NotImplementedException();
        }

        [DirectCall("decbin")]
        public static string decbin(int number)
        {
            throw new NotSupportedException();
        }

        [DirectCall("die")]
        public static void die()
        {

        }

        [DirectCall("dirname")]
        public static string dirname(string path)
        {
            return PhpFiles.Dirname(path);
        }

        //[DirectCall("echo")]
        //public static void echo(object x)
        //{
        //    if (x == null)
        //        return;
        //    Response.GetRuntime().Echo(x.ToString());
        //}
        [DirectCall("echo")]
        public static void echo(string x)
        {
            if (x == null)
                return;
            Response.GetRuntime().Echo(x.ToString());
        }

        [DirectCall("empty")]
        public static bool empty(object x)
        {
            if (x is string)
                return string.IsNullOrEmpty(x as string);
            return x == null;
        }

        /// <summary>
        ///   array explode ( string $delimiter , string $string [, int $limit ] )
        /// </summary>
        /// <param name="delimiter"></param>
        /// <param name="_string"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Since("4.0.1")]
        [DirectCall("explode", "0,1")]
        public static string[] explode(string delimiter, string _string)
        {
            if (delimiter.Length == 1)
                return _string.Split(delimiter[0]);
            throw new MockMethodException();
        }

        [Since("4.0.1")]
        [DirectCall("explode", "0,1,2")]
        public static string[] explode(string delimiter, string _string, int limit)
        {
            throw new MockMethodException();
        }

        [DirectCall("file_exists")]
        public static bool file_exists(string x)
        {
            return System.IO.File.Exists(x);
        }

        [DirectCall("file_get_contents")]
        public static Falsable<string> file_get_contents(string filename, bool use_include_path = false)
        {
            // string file_get_contents ( string $filename [, bool $use_include_path = false [, resource $context [, int $offset = -1 [, int $maxlen ]]]] )
            filename = filename.Replace("\\", "/");
            if (!File.Exists(filename))
                filename = Path.Combine(Script.Server.ContextDocumentRoot.Replace("\\", "/"), filename);
            if (!File.Exists(filename))
                return Falsable<string>.False;
            try
            {
                var a = File.ReadAllBytes(filename);
                return System.Text.Encoding.UTF8.GetString(a);
            }
            catch
            {
                return Falsable<string>.False;
            }
        }

        [DirectCall("file_put_contents")]
        public static Falsable<int> file_put_contents(string filename, string data)
        {
            throw new NotImplementedException();
            // int file_put_contents ( string $filename , mixed $data [, int $flags = 0 [, resource $context ]] )
        }

        [DirectCall("file_put_contents")]
        public static Falsable<int> file_put_contents(string filename, string data, FilePutContentsFlags flags)
        {
            throw new NotImplementedException();
            // int file_put_contents ( string $filename , mixed $data [, int $flags = 0 [, resource $context ]] )
        }

        /// <summary>
        /// int filesize ( string $filename )
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [DirectCall("filesize")]
        public static int filesize(string filename)
        {
            return PhpFiles.FileSize(filename);
        }

        [DirectCall("floor")]
        public static int floor(decimal value)
        {
            return (int)Math.Floor(value);
        }

        [DirectCall("function_exists")]
        public static bool function_exists(string name)
        {
            return true;
        }

        [DirectCall("get_class")]
        public static string get_class(object x)
        {
            throw new MockMethodException();
        }

        [DirectCall("get_magic_quotes_gpc")]
        public static bool get_magic_quotes_gpc()
        {
            throw new MockMethodException();
        }

        [DirectCall("getrandmax")]
        public static int getrandmax()
        {
            throw new NotImplementedException();
        }

        [DirectCall("gettype")]
        public static PhpTypes gettype(object x)
        {
            throw new MockMethodException();
        }

        [DirectCall("gmdate")]
        public static string gmdate(string format)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// void header ( string $string [, bool $replace = true [, int $http_response_code ]] )
        /// </summary>
        /// <param name="_string"></param>
        [DirectCall("header")]
        public static void header(string _string)
        {
            _string = (_string ?? "");
            int i = _string.IndexOf(':');
            if (i < 0)
                return; // just ignore;
            var key = _string.Substring(0, i);
            var value = _string.Substring(i + 1).TrimStart(); ;
            Response.GetRuntime().SetHeader(key, value);
        }

        [DirectCall("header")]
        public static void header(string _string, bool replace)
        {
            throw new NotImplementedException();
        }

        [DirectCall("htmlentities")]
        public static string htmlentities(string _string)
        {
            return System.Web.HttpUtility.HtmlEncode(_string);
        }

        [DirectCall("htmlspecialchars")]
        public static string htmlspecialchars(string _string)
        {
            //  string htmlspecialchars ( string $string [, int $flags = ENT_COMPAT | ENT_HTML401 [, string $encoding = 'UTF-8' [, bool $double_encode = true ]]] )
            return System.Web.HttpUtility.HtmlEncode(_string);
        }

        [Obsolete("nie skończone")]
        [DirectCall("htmlspecialchars")]
        public static string htmlspecialchars(string _string, HtmlEntitiesFlags flags)
        {
            //  string htmlspecialchars ( string $string [, int $flags = ENT_COMPAT | ENT_HTML401 [, string $encoding = 'UTF-8' [, bool $double_encode = true ]]] )
            throw new NotImplementedException();
        }

        [DirectCall("htmlspecialchars")]
        public static string htmlspecialchars(string _string, HtmlEntitiesFlags flags = HtmlEntitiesFlags.COMPAT | HtmlEntitiesFlags.HTML401, Charsets encoding = Charsets.UTF_8)
        {
            //  string htmlspecialchars ( string $string [, int $flags = ENT_COMPAT | ENT_HTML401 [, string $encoding = 'UTF-8' [, bool $double_encode = true ]]] )
            return System.Web.HttpUtility.HtmlEncode(_string);
        }

        [DirectCall("iconv")]
        public static string iconv(Charsets in_charset, Charsets out_charset, string str)
        {
            //string iconv ( string $in_charset , string $out_charset , string $str )
            throw new NotImplementedException();
        }

        [DirectCall("include")]
        public static void include(string x)
        {
            // if (Script.Server)
            return;
            throw new MockMethodException();
        }

        [DirectCall("include_once")]
        public static void include_once(string filename)
        {

        }

        [DirectCall("ini_get")]
        public static Falsable<string> ini_get(string name)
        {
            string a;
            if (PhpIni.TryGetValue(name, out a))
                return a;
            return Falsable<string>.False;
        }

        [DirectCall("intval")]
        public static int intval(object i, int @base = 10)
        {
            throw new NotImplementedException();
        }

        [DirectCall("is_array")]
        public static bool is_array(object o)
        {
            throw new NotImplementedException();
        }

        [DirectCall("is_null")]
        public static bool is_null(object x)
        {
            return x == null;
        }

        [DirectCall("is_object")]
        public static bool is_object(object o)
        {
            throw new NotImplementedException();
        }

        [DirectCall("is_string")]
        public static bool is_string(object x)
        {
            return x is string;
        }

        [DirectCall("isset")]
        public static bool isset(object x)
        {
            return x != null;
        }

        [DirectCall("json_decode")]
        public static object json_decode(string json, bool assoc = false, int depth = 512, int options = 0)
        {
            // mixed json_decode ( string $json [, bool $assoc = false [, int $depth = 512 [, int $options = 0 ]]] )
            throw new MockMethodException();
        }

        [DirectCall("json_encode")]
        public static string json_encode(object value, int options = 0)
        {
            // string json_encode ( mixed $value [, int $options = 0 [, int $depth = 512 ]] )
            throw new MockMethodException();
        }

        [DirectCall("ksort")]
        [Obsolete("Not finished")]
        public static bool ksort<TKey, TValue>(Dictionary<TKey, TValue> _array)
        {
            // bool ksort ( array &$array [, int $sort_flags = SORT_REGULAR ] )
            throw new NotImplementedException();
        }

        [DirectCall("mail")]
        public static bool mail(string to, string subject, string message, string additional_headers = null, string additional_parameters = null)
        {
            throw new NotImplementedException();
        }

        [DirectCall("max")]
        public static decimal max(decimal a, decimal b)
        {
            return Math.Max(a, b);
        }

        [DirectCall("mb_strpos")]
        public static Falsable<int> mb_strpos(string x, string value)
        {
            // // $pos = strpos($mystring, $findme);
            var y = x.IndexOf(value);
            if (y < 0)
                return Falsable<int>.False;
            return y;
        }

        [DirectCall("md5")]
        public static string md5(string str, bool raw_output = false)
        {
            throw new NotSupportedException();
        }

        [DirectCall("microtime")]
        [Obsolete("not finished")]
        public static string microtime(bool get_as_float = false)
        {
            throw new NotImplementedException();
        }

        [DirectCall("mktime")]
        public static int mktime()
        {
            return (int)DateTime.Now.ToUniversalTime().Subtract((new DateTime(1970, 1, 1)).ToUniversalTime()).TotalSeconds;
        }

        [DirectCall("mt_srand")]
        public static void mt_srand(int seed = 0)
        {

        }

        [DirectCall("number_format")]
        public static string number_format(decimal number, int decimals = 0, string dec_point = ".", string thousands_sep = ",")
        {
            throw new NotImplementedException();
        }

        [DirectCall("number_format")]
        public static string number_format(double number, int decimals = 0, string dec_point = ".", string thousands_sep = ",")
        {
            throw new NotImplementedException();
        }

        [DirectCall("ob_get_clean")]
        public static string ob_get_clean()
        {
            throw new NotImplementedException();
        }

        [DirectCall("ob_start")]
        public static void ob_start()
        {
            // bool ob_start ([ callable $output_callback = NULL [, int $chunk_size = 0 [, int $flags = PHP_OUTPUT_HANDLER_STDFLAGS ]]] )
        }

        [DirectCall("ob_start")]
        public static void ob_start(Func<string, string> callback)
        {
            // bool ob_start ([ callable $output_callback = NULL [, int $chunk_size = 0 [, int $flags = PHP_OUTPUT_HANDLER_STDFLAGS ]]] )
        }

        [DirectCall("pack")]
        public static string pack(string format, params object[] args)
        {
            // string pack ( string $format [, mixed $args [, mixed $... ]] )
            throw new NotSupportedException();
        }

        [DirectCall("phpversion")]
        public static string phpversion()
        {
            throw new NotImplementedException();
        }

        [DirectCall("preg_match")]
        public static int preg_match(string pattern, string subject)
        {
            var delimiter = pattern[0];
            var i = pattern.LastIndexOf(delimiter);
            var _pattern = pattern.Substring(1, i - 1);
            var _options = pattern.Substring(i + 1);
            if (_options != "")
                throw new NotImplementedException();
            Regex re = new Regex(_pattern);
            if (re.IsMatch(subject))
                return 1;
            return 0;
            // string pattern , string subject [, array &$matches [, int $flags = 0 [, int $offset = 0 ]]] 
        }

        [DirectCall("preg_match")]
        public static int preg_match(string pattern, string subject, out Dictionary<object, string> matches)
        {
            throw new NotImplementedException();
            // string pattern , string subject [, array &$matches [, int $flags = 0 [, int $offset = 0 ]]] 
        }

        [DirectCall("preg_replace")]
        public static string preg_replace(string pattern, string replacement, string subject, int limit = -1)
        {
            throw new NotImplementedException();
        }

        [DirectCall("rand")]
        public static int rand()
        {
            throw new NotImplementedException();
        }

        [DirectCall("rand")]
        public static int rand(int min, int max)
        {
            throw new NotImplementedException();
        }

        [DirectCall("readfile")]
        public static Falsable<int> readfile(string filename, bool use_include_path = false)
        {
            return Lang.Php.PhpFiles.ReadFile(filename, use_include_path);
        }

        [DirectCall("require_once")]
        public static void require_once(string filename)
        {

        }

        [DirectCall("round")]
        public static int round(double x)
        {
            return (int)Math.Round(x);
        }

        [DirectCall("round")]
        public static int round(decimal x, int places)
        {
            throw new NotImplementedException();
            return (int)Math.Round(x);
        }

        [DirectCall("round")]
        public static int round(double x, int precision, RoundMode mode = RoundMode.Up)
        {
            throw new NotImplementedException();
            return (int)Math.Round(x);
        }

        [DirectCall("serialize")]
        public static string serialize(object data)
        {
            throw new NotImplementedException();
        }

        [DirectCall("set_time_limit")]
        public static void set_time_limit(int value)
        {

        }

        [DirectCall("sha1")]
        public static string sha1(string str)
        {
            throw new NotImplementedException();
        }

        [DirectCall("srand")]
        public static void srand(int seed = 0)
        {
            throw new NotImplementedException();
        }

        [DirectCall("str_ireplace")]
        public static string str_ireplace(string search, string replace, string subject)
        {
            throw new NotImplementedException();
        }

        [DirectCall("str_replace")]
        public static string str_replace(string search, string replace, string subject)
        {
            throw new NotImplementedException();
            //   mixed str_replace ( mixed $search , mixed $replace , mixed $subject [, int &$count ] )

        }

        [DirectCall("strtolower")]
        public static string strtolower(string x)
        {
            return x.ToLower();
        }

        [DirectCall("substr")]
        public static string substr(string _string, int start, int length = -1)
        {
            throw new NotImplementedException();
            // string substr ( string $string , int $start [, int $length ] )
        }

        [DirectCall("uniqid")]
        public static string uniqid(string prefix = "", bool more_entropy = false)
        {
            throw new NotSupportedException();
        }

        [DirectCall("unpack")]
        public static Dictionary<string, object> unpack(string format, string data)
        {
            throw new NotSupportedException();
        }

        [DirectCall("urlencode")]
        public static string urlencode(string x)
        {
            throw new NotImplementedException();

        }

        [DirectCall("var_dump")]
        public static void var_dump(object x)
        {

        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        [DirectCall("preg_replace_callback")]
        public T preg_replace_callback<T>(string pattern, Func<string[], T> callback, string subject)
        {
            throw new NotSupportedException();
            // mixed preg_replace_callback ( mixed $pattern , callable $callback , mixed $subject [, int $limit = -1 [, int &$count ]] )
        }

        [DirectCall("stripslashes")]
        public string stripslashes(string x)
        {
            throw new MockMethodException();

        }

        [DirectCall("strlen")]
        public int strlen(string x)
        {
            return x.Length;
        }

        #endregion Methods

        #region Fields

        [AsDefinedConst]
        public const string __FILE__ = "????";
        [AsDefinedConst]
        public const string PHP_EOL = "\r\n";

        #endregion Fields
    }
}
