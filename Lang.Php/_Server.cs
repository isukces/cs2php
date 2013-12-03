using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    [AsArray]
    public class _Server
    {
        /// <summary>
        /// The filename of the currently executing script, relative to the document root. For instance, $_SERVER['PHP_SELF'] in a script at the address http://example.com/test.php/foo.bar would be /test.php/foo.bar. The __FILE__ constant contains the full path and filename of the current (i.e. included) file. If PHP is running as a command-line processor this variable contains the script name since PHP 4.3.0. Previously it was not available. 
        /// </summary>
        [ScriptName("PHP_SELF")]
        public string PhpSelf;

        /// <summary>
        /// What revision of the CGI specification the server is using; i.e. 'CGI/1.1'. 
        /// </summary>
        [ScriptName("GATEWAY_INTERFACE")]
        public string GatewayInterface;

        /// <summary>
        /// The IP address of the server under which the current script is executing.
        /// </summary>
        [ScriptName("SERVER_ADDR")]
        public string ServerAddress;

        /// <summary>
        /// The name of the server host under which the current script is executing. If the script is running on a virtual host, this will be the value defined for that virtual host. 
        /// </summary>
        [ScriptName("SERVER_NAME")]
        public string ServerName;

        /// <summary>
        /// Server identification string, given in the headers when responding to requests. 
        /// </summary>
        [ScriptName("SERVER_SOFTWARE")]
        public string ServerSoftware;


        /// <summary>
        /// Name and revision of the information protocol via which the page was requested; i.e. 'HTTP/1.0'; 
        /// </summary>
        [ScriptName("SERVER_PROTOCOL")]
        public string ServerProtocol;

        /// <summary>
        /// Which request method was used to access the page; i.e. 'GET', 'HEAD', 'POST', 'PUT'. 
        /// PHP script is terminated after sending headers (it means after producing any output without output buffering) if the request method was HEAD. 
        /// </summary>
        [ScriptName("REQUEST_METHOD")]
        public string RequestMethod;

        /// <summary>
        /// The timestamp of the start of the request. Available since PHP 5.1.0. 
        /// </summary>
        [ScriptName("REQUEST_TIME")]
        public int RequestTime;

        /// <summary>
        /// The timestamp of the start of the request, with microsecond precision. Available since PHP 5.4.0. 
        /// </summary>
        [ScriptName("REQUEST_TIME_FLOAT")]
        public double RequestTimeFloat;


        /// <summary>
        /// The query string, if any, via which the page was accessed. 
        /// </summary>
        [ScriptName("QUERY_STRING")]
        public string QueryString;

        /// <summary>
        /// The document root directory under which the current script is executing, as defined in the server's configuration file. 
        /// </summary>
        [ScriptName("DOCUMENT_ROOT")]
        public string DocumentRoot;

        /// <summary>
        /// Contents of the Accept: header from the current request, if there is one. 
        /// </summary>
        [ScriptName("HTTP_ACCEPT")]
        public string HttpAccept;

        /// <summary>
        /// Contents of the Accept-Charset: header from the current request, if there is one. Example: 'iso-8859-1,*,utf-8'. 
        /// </summary>
        [ScriptName("HTTP_ACCEPT_CHARSET")]
        public string HttpAcceptCharset;

        /// <summary>
        /// Contents of the Accept-Encoding: header from the current request, if there is one. Example: 'gzip'. 
        /// </summary>
        [ScriptName("HTTP_ACCEPT_ENCODING")]
        public string HttpAcceptEncoding;

        /// <summary>
        /// Contents of the Accept-Language: header from the current request, if there is one. Example: 'en'. 
        /// </summary>
        [ScriptName("HTTP_ACCEPT_LANGUAGE")]
        public string HttpAcceptLanguage;


        /// <summary>
        /// Contents of the Connection: header from the current request, if there is one. Example: 'Keep-Alive'. 
        /// </summary>
        [ScriptName("HTTP_CONNECTION")]
        public string HttpConnection;

        /// <summary>
        /// Contents of the Host: header from the current request, if there is one. s
        /// </summary>
        [ScriptName("HTTP_HOST")]
        public string HttpHost;


        /// <summary>
        /// ??
        /// </summary>
        [ScriptName("CONTEXT_PREFIX")]
        public string ContextPrefix;

        /// <summary>
        /// ??
        /// </summary>
        [ScriptName("CONTEXT_DOCUMENT_ROOT")]
        public string ContextDocumentRoot;



        /// <summary>
        /// The address of the page (if any) which referred the user agent to the current page. This is set by the user agent. Not all user agents will set this, and some provide the ability to modify HTTP_REFERER as a feature. In short, it cannot really be trusted. 
        /// </summary>
        [ScriptName("HTTP_REFERER")]
        public string HttpReferer;

        /// <summary>
        /// Contents of the User-Agent: header from the current request, if there is one. This is a string denoting the user agent being which is accessing the page. A typical example is: Mozilla/4.5 [en] (X11; U; Linux 2.2.9 i586). 
        /// Among other things, you can use this value with get_browser() to tailor your page's output to the capabilities of the user agent. 
        /// </summary>
        [ScriptName("HTTP_USER_AGENT")]
        public string HttpUserAgent;

        /// <summary>
        /// Set to a non-empty value if the script was queried through the HTTPS protocol.
        /// Note: Note that when using ISAPI with IIS, the value will be off if the request was not made through the HTTPS protocol. 
        /// </summary>
        [ScriptName("HTTPS")]
        public string Https;

        /// <summary>
        /// The IP address from which the user is viewing the current page. 
        /// </summary>
        [ScriptName("REMOTE_ADDR")]
        public string RemoteAddress;

        /// <summary>
        /// The Host name from which the user is viewing the current page. The reverse dns lookup is based off the REMOTE_ADDR of the user.
        /// Note: Your web server must be configured to create this variable. 
        /// For example in Apache you'll need HostnameLookups 
        /// On inside httpd.conf for it to exist. See also gethostbyaddr(). 
        /// </summary>
        [ScriptName("REMOTE_HOST")]
        public string RemoteHost;

        /// <summary>
        /// The port being used on the user's machine to communicate with the web server. 
        /// </summary>
        [ScriptName("REMOTE_PORT")]
        public string RemotePort;

        /// <summary>
        /// The authenticated user. 
        /// </summary>
        [ScriptName("REMOTE_USER")]
        public string RemoteUser;

        /// <summary>
        /// The authenticated user if the request is internally redirected.
        /// </summary>
        [ScriptName("REDIRECT_REMOTE_USER")]
        public string RedirectRemoteUser;

        /// <summary>
        ///  The absolute pathname of the currently executing script.
        ///  Note:
        ///  If a script is executed with the CLI, as a relative path, 
        ///  such as file.php or ../file.php, $_SERVER['SCRIPT_FILENAME'] will contain the 
        ///  relative path specified by the user. 
        /// </summary>
        [ScriptName("SCRIPT_FILENAME")]
        public string ScriptFilename;

        /// <summary>
        /// The value given to the SERVER_ADMIN (for Apache) directive in the web server configuration file. If the script is running on a virtual host, this will be the value defined for that virtual host. 
        /// </summary>
        [ScriptName("SERVER_ADMIN")]
        public string ServerAdmin;

        /// <summary>
        /// The port on the server machine being used by the web server for communication. For default setups, this will be '80'; using SSL, for instance, will change this to whatever your defined secure HTTP port is.
        /// Note: Under the Apache 2, you must set UseCanonicalName = On, as well as UseCanonicalPhysicalPort = On in order to get the physical (real) port, otherwise, this value can be spoofed and it may or may not return the physical port value. It is not safe to rely on this value in security-dependent contexts. 
        /// </summary>
        [ScriptName("SERVER_PORT")]
        public string ServerPort;


        /// <summary>
        /// String containing the server version and virtual host name which are added to server-generated pages, if enabled. 
        /// </summary>
        [ScriptName("SERVER_SIGNATURE")]
        public string ServerSignature;

        /// <summary>
        /// Filesystem- (not document root-) based path to the current script, after the server has done any virtual-to-real mapping.
        /// Note: As of PHP 4.3.2, PATH_TRANSLATED is no longer set implicitly under the Apache 2 SAPI in contrast to the situation in Apache 1, where it's set to the same value as the SCRIPT_FILENAME server variable when it's not populated by Apache. This change was made to comply with the CGI specification that PATH_TRANSLATED should only exist if PATH_INFO is defined. Apache 2 users may use AcceptPathInfo = On inside httpd.conf to define PATH_INFO. 
        /// </summary>
        [ScriptName("PATH_TRANSLATED")]
        public string PathTranslated;

        /// <summary>
        /// Contains the current script's path. This is useful for pages which need to point to themselves. 
        /// The __FILE__ constant contains the full path and filename of the current (i.e. included) file.
        /// </summary>
        [ScriptName("SCRIPT_NAME")]
        public string ScriptName;

        /// <summary>
        /// The URI which was given in order to access this page; for instance, '/index.html'. 
        /// </summary>
        [ScriptName("REQUEST_URI")]
        public string RequestUri;

        /// <summary>
        /// When doing Digest HTTP authentication this variable is set to the 'Authorization' header sent by the client (which you should then use to make the appropriate validation). 
        /// </summary>
        [ScriptName("PHP_AUTH_DIGEST")]
        public string PhpAuthDigest;

        /// <summary>
        /// When doing HTTP authentication this variable is set to the username provided by the user. 
        /// </summary>
        [ScriptName("PHP_AUTH_USER")]
        public string PhpAuthUser;

        /// <summary>
        /// When doing HTTP authentication this variable is set to the password provided by the user. 
        /// </summary>
        [ScriptName("PHP_AUTH_PW")]
        public string PhpAuthPassword;


        /// <summary>
        /// When doing HTTP authenticated this variable is set to the authentication type. 
        /// </summary>
        [ScriptName("AUTH_TYPE")]
        public string AuthType;


        /// <summary>
        /// Contains any client-provided pathname information trailing the actual script filename but preceding the query string, if available. For instance, 
        /// if the current script was accessed via the URL http://www.example.com/php/path_info.php/some/stuff?foo=bar, then $_SERVER['PATH_INFO'] would contain /some/stuff. 
        /// </summary>
        [ScriptName("PATH_INFO")]
        public string PathInfo;

        /// <summary>
        /// Original version of 'PATH_INFO' before processed by PHP. 
        /// </summary>
        [ScriptName("ORIG_PATH_INFO")]
        public string OriginalPathInfo;


        [ScriptName("HTTP_CLIENT_IP")]
        public string HttpClientIp;

        [ScriptName("HTTP_X_FORWARDED_FOR")]
        public string HttpXForwarderFor;
    }
}
