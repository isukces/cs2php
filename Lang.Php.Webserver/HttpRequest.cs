using System;
using System.Collections.Generic;
using System.IO;

namespace Lang.Php.Webserver
{
    public class HttpRequest
    {
        // Public Methods 

        public static HttpRequest Parse(string txt)
        {
            var r = new HttpRequest();

            var ln = 0;

            void Pl(string line)
            {
                if (ln++ == 0)
                    r.ParseFirstLine(line);
                else
                {
                    if (string.IsNullOrEmpty(line))
                        return;
                    var i1 = line.IndexOf(":");
                    if (i1 < 0)
                        throw new NotSupportedException();
                    r.Head[line.Substring(0, i1)] = line.Substring(i1 + 1).TrimStart();
                }
            }

            while (!string.IsNullOrEmpty(txt))
            {
                var idx = txt.IndexOf("\r\n", StringComparison.Ordinal);
                if (idx < 0)
                {
                    Pl(txt);
                    break;
                }
                Pl(txt.Substring(0, idx));
                txt = txt.Substring(idx + 2);
            }
            r.Update();
            return r;
        }

        private static string ReadPart(ref string text, string separator)
        {
            var idx = text.IndexOf(separator, StringComparison.Ordinal);
            if (idx < 0)
            {
                var t = text;
                text = "";
                return t;
            }
            else
            {
                var t = text.Substring(0, idx);
                text = text.Substring(idx + separator.Length);
                if (separator == " ")
                    text = text.TrimStart();
                return t;
            }
        }

        public void Update()
        {
            var i = Script.LastIndexOf("/");
            if (i < 0)
                Server.ContextPrefix = "/";
            else
                Server.ContextPrefix = Script.Substring(0, i + 1);

            if (Server.DocumentRoot != null)
                Server.ContextDocumentRoot = Path.Combine(
                            Server.DocumentRoot.Replace("/", "\\"),
                            Server.ContextPrefix.Substring(1).Replace("/", "\\"))
                        .Replace("\\", "/")
                    ;
        }

        private void ParseFirstLine(string line)
        {
            // GET / HTTP/1.1
            _method = ReadPart(ref line, " ").ToLower();
            if (string.IsNullOrEmpty(line))
                throw new NotSupportedException();
            _requestUri = ReadPart(ref line, " ");

            {
                // parsowanie uri
                var txt = _requestUri;
                Script = ReadPart(ref txt, "?");
                if (!string.IsNullOrEmpty(txt))
                {
                    var get = ReadPart(ref txt, "#");
                    var getItems = get.Split('&');
                    foreach (var getItem in getItems)
                    {
                        var keyValue = getItem.Split('=');
                        if (keyValue.Length < 2) continue;
                        Get[keyValue[0]] = keyValue[1];
                    }
                }
            }
        }

        /*
array (size=41)
  'REDIRECT_STATUS' => string '200' (length=3)
  'HTTP_HOST' => string 'localhost' (length=9)
  'HTTP_CONNECTION' => string 'keep-alive' (length=10)
  'CONTENT_LENGTH' => string '44' (length=2)
  'HTTP_CACHE_CONTROL' => string 'max-age=0' (length=9)
  'HTTP_ACCEPT' => string 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,* /*;q=0.8' (length=74)
  'HTTP_ORIGIN' => string 'http://localhost' (length=16)
  'HTTP_USER_AGENT' => string 'Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36' (length=108)
  'CONTENT_TYPE' => string 'application/x-www-form-urlencoded' (length=33)
  'HTTP_REFERER' => string 'http://localhost/cs2php/html/magic/' (length=35)
  'HTTP_ACCEPT_ENCODING' => string 'gzip,deflate,sdch' (length=17)
  'HTTP_ACCEPT_LANGUAGE' => string 'pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4' (length=35)
  'PATH' => string 'C:\Program Files (x86)\NVIDIA Corporation\PhysX\Common;C:\WINDOWS\system32;C:\WINDOWS;C:\WINDOWS\System32\Wbem;C:\WINDOWS\System32\WindowsPowerShell\v1.0\;C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\;C:\Program Files\Microsoft SQL Server\110\Tools\Binn\;C:\Program Files (x86)\GtkSharp\2.12\bin;C:\Program Files (x86)\Google\Google Apps Sync\;C:\Program Files (x86)\Google\Google Apps Migration\;C:\Program Files (x86)\Microsoft SDKs\TypeScript\;C:\Program Files (x86)\Microsoft SQL Server\11'... (length=979)
  'SystemRoot' => string 'C:\WINDOWS' (length=10)
  'COMSPEC' => string 'C:\WINDOWS\system32\cmd.exe' (length=27)
  'PATHEXT' => string '.COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.JSE;.WSF;.WSH;.MSC' (length=53)
  'WINDIR' => string 'C:\WINDOWS' (length=10)
  'SERVER_SIGNATURE' => string '' (length=0)
  'SERVER_SOFTWARE' => string 'Apache/2.4.4 (Win64) PHP/5.4.12' (length=31)
  'SERVER_NAME' => string 'localhost' (length=9)
  'SERVER_ADDR' => string '::1' (length=3)
  'SERVER_PORT' => string '80' (length=2)
  'REMOTE_ADDR' => string '::1' (length=3)
  'DOCUMENT_ROOT' => string 'E:/wamp/www' (length=11)
  'REQUEST_SCHEME' => string 'http' (length=4)
  'CONTEXT_PREFIX' => string '/cs2php/' (length=8)
  'CONTEXT_DOCUMENT_ROOT' => string 'E:/temp/#SharpLinguaFranca/phpCompiled/' (length=39)
  'SERVER_ADMIN' => string 'admin@example.com' (length=17)
  'SCRIPT_FILENAME' => string 'E:/temp/#SharpLinguaFranca/phpCompiled/index.php' (length=48)
  'REMOTE_PORT' => string '11984' (length=5)
  'REDIRECT_QUERY_STRING' => string 'document=magic' (length=14)
  'REDIRECT_URL' => string '/cs2php/html/magic/' (length=19)
  'GATEWAY_INTERFACE' => string 'CGI/1.1' (length=7)
  'SERVER_PROTOCOL' => string 'HTTP/1.1' (length=8)
  'REQUEST_METHOD' => string 'POST' (length=4)
  'QUERY_STRING' => string 'document=magic' (length=14)
  'REQUEST_URI' => string '/cs2php/html/magic/' (length=19)
  'SCRIPT_NAME' => string '/cs2php/index.php' (length=17)
  'PHP_SELF' => string '/cs2php/index.php' (length=17)
  'REQUEST_TIME_FLOAT' => float 1385333088.592
  'REQUEST_TIME' => int 1385333088 */


        /// <summary>
        /// </summary>
        public string Method
        {
            get { return _method; }
            set { _method = value?.Trim() ?? string.Empty; }
        }

        /// <summary>
        /// </summary>
        public Dictionary<string, object> Head { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// </summary>
        public Dictionary<string, object> Get { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// </summary>
        public Dictionary<string, object> Post { get; set; } = new Dictionary<string, object>();

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string Script { get; private set; } = string.Empty;

        /// <summary>
        /// </summary>
        public _Server Server { get; set; } = new _Server();

        /// <summary>
        /// </summary>
        public string RequestUri
        {
            get { return _requestUri; }
            set { _requestUri = value?.Trim() ?? string.Empty; }
        }

        private string _method = string.Empty;
        private string _requestUri = string.Empty;

        public const string
            Example = @"GET / HTTP/1.1
Host: localhost:11000
Connection: keep-alive
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36
Accept-Encoding: gzip,deflate,sdch
Accept-Language: pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4
";
    }

    /*
GET / HTTP/1.1
Host: localhost:11000
Connection: keep-alive
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,* /*;q=0.8
User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36
Accept-Encoding: gzip,deflate,sdch
Accept-Language: pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4
*/
}