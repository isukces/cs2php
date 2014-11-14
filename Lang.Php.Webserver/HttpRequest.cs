using System;
using System.Collections.Generic;
using System.IO;

namespace Lang.Php.Webserver
{


    /*
    smartClass
    option NoAdditionalFile
    
    property Method string 
    
    property Head Dictionary<string,object> 
    	init #
    
    property Get Dictionary<string,object> 
    	init #
    
    property Post Dictionary<string,object> 
    	init #
    
    property Script string 
    	read only
    
    property Server _Server 
    	init #
    
    property RequestUri string 
    smartClassEnd
    */
    
    public partial class HttpRequest
    {
        private string ReadPart(ref string text, string separator)
        {
            int idx = text.IndexOf(separator);
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

        private void ParseFirstLine(string line)
        {

            // GET / HTTP/1.1
            _method = ReadPart(ref line, " ").ToLower();
            if (string.IsNullOrEmpty(line))
                throw new NotSupportedException();
            _requestUri = ReadPart(ref line, " ");

            {
                // parsowanie uri
                string txt = _requestUri;
                _script = ReadPart(ref txt, "?");
                if (!string.IsNullOrEmpty(txt))
                {
                    var get = ReadPart(ref txt, "#");
                    var getItems = get.Split('&');
                    foreach (var getItem in getItems)
                    {
                        var keyValue = getItem.Split('=');
                        if (keyValue.Length < 2) continue;
                        this._get[keyValue[0]] = keyValue[1];
                    }
                }

            }

        }

        #region Static Methods

        // Public Methods 

        public static HttpRequest Parse(string txt)
        {

            HttpRequest r = new HttpRequest();

            int ln = 0;
            Action<string> pl = (string line) =>
            {
                if (ln++ == 0)
                    r.ParseFirstLine(line);
                else
                {
                    if (string.IsNullOrEmpty(line))
                        return;
                    int i1 = line.IndexOf(":");
                    if (i1 < 0)
                        throw new NotSupportedException();
                    r._head[line.Substring(0, i1)] = line.Substring(i1 + 1).TrimStart();

                }

            };

            while (!string.IsNullOrEmpty(txt))
            {
                int idx = txt.IndexOf("\r\n");
                if (idx < 0)
                {
                    pl(txt);
                    break;
                }
                pl(txt.Substring(0, idx));
                txt = txt.Substring(idx + 2);

            }
            r.Update();
            return r;
        }

        public void Update()
        {
            var i = _script.LastIndexOf("/");
            if (i < 0)
                _server.ContextPrefix = "/";
            else
                _server.ContextPrefix = _script.Substring(0, i + 1);

            if (_server.DocumentRoot != null)
                _server.ContextDocumentRoot = Path.Combine(
                    _server.DocumentRoot.Replace("/", "\\"),
                    _server.ContextPrefix.Substring(1).Replace("/", "\\"))
                    .Replace("\\","/")
                    ;
        }

        #endregion Static Methods

        public const string
 Example = @"GET / HTTP/1.1
Host: localhost:11000
Connection: keep-alive
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36
Accept-Encoding: gzip,deflate,sdch
Accept-Language: pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4
";

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

    }
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



// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-11-14 08:52
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Webserver
{
    public partial class HttpRequest 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public HttpRequest()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Method## ##Head## ##Get## ##Post## ##Script## ##Server## ##RequestUri##
        implement ToString Method=##Method##, Head=##Head##, Get=##Get##, Post=##Post##, Script=##Script##, Server=##Server##, RequestUri=##RequestUri##
        implement equals Method, Head, Get, Post, Script, Server, RequestUri
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Method; 
        /// </summary>
        public const string PropertyNameMethod = "Method";
        /// <summary>
        /// Nazwa własności Head; 
        /// </summary>
        public const string PropertyNameHead = "Head";
        /// <summary>
        /// Nazwa własności Get; 
        /// </summary>
        public const string PropertyNameGet = "Get";
        /// <summary>
        /// Nazwa własności Post; 
        /// </summary>
        public const string PropertyNamePost = "Post";
        /// <summary>
        /// Nazwa własności Script; 
        /// </summary>
        public const string PropertyNameScript = "Script";
        /// <summary>
        /// Nazwa własności Server; 
        /// </summary>
        public const string PropertyNameServer = "Server";
        /// <summary>
        /// Nazwa własności RequestUri; 
        /// </summary>
        public const string PropertyNameRequestUri = "RequestUri";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Method
        {
            get
            {
                return _method;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _method = value;
            }
        }
        private string _method = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string,object> Head
        {
            get
            {
                return _head;
            }
            set
            {
                _head = value;
            }
        }
        private Dictionary<string,object> _head = new Dictionary<string,object>();
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string,object> Get
        {
            get
            {
                return _get;
            }
            set
            {
                _get = value;
            }
        }
        private Dictionary<string,object> _get = new Dictionary<string,object>();
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string,object> Post
        {
            get
            {
                return _post;
            }
            set
            {
                _post = value;
            }
        }
        private Dictionary<string,object> _post = new Dictionary<string,object>();
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string Script
        {
            get
            {
                return _script;
            }
        }
        private string _script = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public _Server Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }
        private _Server _server = new _Server();
        /// <summary>
        /// 
        /// </summary>
        public string RequestUri
        {
            get
            {
                return _requestUri;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _requestUri = value;
            }
        }
        private string _requestUri = string.Empty;
        #endregion Properties

    }
}
