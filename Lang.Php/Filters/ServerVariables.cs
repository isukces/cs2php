using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Filters
{
    public enum ServerVariables
    {
        /// <summary>
        /// The filename of the currently executing script, relative to the document root. For instance, $_SERVER['PHP_SELF'] in a script at the address http://example.com/test.php/foo.bar would be /test.php/foo.bar. The __FILE__ constant contains the full path and filename of the current (i.e. included) file. If PHP is running as a command-line processor this variable contains the script name since PHP 4.3.0. Previously it was not available. 
        /// </summary>
        [RenderValue("'PHP_SELF'")]
        PhpSelf,

        /// <summary>
        /// What revision of the CGI specification the server is using, i.e. 'CGI/1.1'. 
        /// </summary>
        [RenderValue("'GATEWAY_INTERFACE'")]
        GatewayInterface,

        /// <summary>
        /// The IP address of the server under which the current script is executing.
        /// </summary>
        [RenderValue("'SERVER_ADDR'")]
        ServerAddress,

        /// <summary>
        /// The name of the server host under which the current script is executing. If the script is running on a virtual host, this will be the value defined for that virtual host. 
        /// </summary>
        [RenderValue("'SERVER_NAME'")]
        ServerName,

        /// <summary>
        /// Server identification string, given in the headers when responding to requests. 
        /// </summary>
        [RenderValue("'SERVER_SOFTWARE'")]
        ServerSoftware,


        /// <summary>
        /// Name and revision of the information protocol via which the page was requested, i.e. 'HTTP/1.0', 
        /// </summary>
        [RenderValue("'SERVER_PROTOCOL'")]
        ServerProtocol,

        /// <summary>
        /// Which request method was used to access the page, i.e. 'GET', 'HEAD', 'POST', 'PUT'. 
        /// PHP script is terminated after sending headers (it means after producing any output without output buffering) if the request method was HEAD. 
        /// </summary>
        [RenderValue("'REQUEST_METHOD'")]
        RequestMethod,

        /// <summary>
        /// The timestamp of the start of the request. Available since PHP 5.1.0. 
        /// </summary>
        [RenderValue("'REQUEST_TIME'")]
        RequestTime,

        /// <summary>
        /// The timestamp of the start of the request, with microsecond precision. Available since PHP 5.4.0. 
        /// </summary>
        [RenderValue("'REQUEST_TIME_FLOAT'")]
        RequestTimeFloat,


        /// <summary>
        /// The query string, if any, via which the page was accessed. 
        /// </summary>
        [RenderValue("'QUERY_STRING'")]
        QueryString,

        /// <summary>
        /// The document root directory under which the current script is executing, as defined in the server's configuration file. 
        /// </summary>
        [RenderValue("'DOCUMENT_ROOT'")]
        DocumentRoot,

        /// <summary>
        /// Contents of the Accept: header from the current request, if there is one. 
        /// </summary>
        [RenderValue("'HTTP_ACCEPT'")]
        HttpAccept,

        /// <summary>
        /// Contents of the Accept-Charset: header from the current request, if there is one. Example: 'iso-8859-1,*,utf-8'. 
        /// </summary>
        [RenderValue("'HTTP_ACCEPT_CHARSET'")]
        HttpAcceptCharset,

        /// <summary>
        /// Contents of the Accept-Encoding: header from the current request, if there is one. Example: 'gzip'. 
        /// </summary>
        [RenderValue("'HTTP_ACCEPT_ENCODING'")]
        HttpAcceptEncoding,

        /// <summary>
        /// Contents of the Accept-Language: header from the current request, if there is one. Example: 'en'. 
        /// </summary>
        [RenderValue("'HTTP_ACCEPT_LANGUAGE'")]
        HttpAcceptLanguage,


        /// <summary>
        /// Contents of the Connection: header from the current request, if there is one. Example: 'Keep-Alive'. 
        /// </summary>
        [RenderValue("'HTTP_CONNECTION'")]
        HttpConnection,

        /// <summary>
        /// Contents of the Host: header from the current request, if there is one. s
        /// </summary>
        [RenderValue("'HTTP_HOST'")]
        HttpHost,

        /// <summary>
        /// The address of the page (if any) which referred the user agent to the current page. This is set by the user agent. Not all user agents will set this, and some provide the ability to modify HTTP_REFERER as a feature. In short, it cannot really be trusted. 
        /// </summary>
        [RenderValue("'HTTP_REFERER'")]
        HttpReferer,

        /// <summary>
        /// Contents of the User-Agent: header from the current request, if there is one. This is a string denoting the user agent being which is accessing the page. A typical example is: Mozilla/4.5 [en] (X11, U, Linux 2.2.9 i586). 
        /// Among other things, you can use this value with get_browser() to tailor your page's output to the capabilities of the user agent. 
        /// </summary>
        [RenderValue("'HTTP_USER_AGENT'")]
        HttpUserAgent,

        /// <summary>
        /// Set to a non-empty value if the script was queried through the HTTPS protocol.
        /// Note: Note that when using ISAPI with IIS, the value will be off if the request was not made through the HTTPS protocol. 
        /// </summary>
        [RenderValue("'HTTPS'")]
        Https,

        /// <summary>
        /// The IP address from which the user is viewing the current page. 
        /// </summary>
        [RenderValue("'REMOTE_ADDR'")]
        RemoteAddress,

        /// <summary>
        /// The Host name from which the user is viewing the current page. The reverse dns lookup is based off the REMOTE_ADDR of the user.
        /// Note: Your web server must be configured to create this variable. 
        /// For example in Apache you'll need HostnameLookups 
        /// On inside httpd.conf for it to exist. See also gethostbyaddr(). 
        /// </summary>
        [RenderValue("'REMOTE_HOST'")]
        RemoteHost,

        /// <summary>
        /// The port being used on the user's machine to communicate with the web server. 
        /// </summary>
        [RenderValue("'REMOTE_PORT'")]
        RemotePort,

        /// <summary>
        /// The authenticated user. 
        /// </summary>
        [RenderValue("'REMOTE_USER'")]
        RemoteUser,

        /// <summary>
        /// The authenticated user if the request is internally redirected.
        /// </summary>
        [RenderValue("'REDIRECT_REMOTE_USER'")]
        RedirectRemoteUser,

        /// <summary>
        ///  The absolute pathname of the currently executing script.
        ///  Note:
        ///  If a script is executed with the CLI, as a relative path, 
        ///  such as file.php or ../file.php, $_SERVER['SCRIPT_FILENAME'] will contain the 
        ///  relative path specified by the user. 
        /// </summary>
        [RenderValue("'SCRIPT_FILENAME'")]
        ScriptFilename,

        /// <summary>
        /// The value given to the SERVER_ADMIN (for Apache) directive in the web server configuration file. If the script is running on a virtual host, this will be the value defined for that virtual host. 
        /// </summary>
        [RenderValue("'SERVER_ADMIN'")]
        ServerAdmin,

        /// <summary>
        /// The port on the server machine being used by the web server for communication. For default setups, this will be '80', using SSL, for instance, will change this to whatever your defined secure HTTP port is.
        /// Note: Under the Apache 2, you must set UseCanonicalName = On, as well as UseCanonicalPhysicalPort = On in order to get the physical (real) port, otherwise, this value can be spoofed and it may or may not return the physical port value. It is not safe to rely on this value in security-dependent contexts. 
        /// </summary>
        [RenderValue("'SERVER_PORT'")]
        ServerPort,


        /// <summary>
        /// String containing the server version and virtual host name which are added to server-generated pages, if enabled. 
        /// </summary>
        [RenderValue("'SERVER_SIGNATURE'")]
        ServerSignature,

        /// <summary>
        /// Filesystem- (not document root-) based path to the current script, after the server has done any virtual-to-real mapping.
        /// Note: As of PHP 4.3.2, PATH_TRANSLATED is no longer set implicitly under the Apache 2 SAPI in contrast to the situation in Apache 1, where it's set to the same value as the SCRIPT_FILENAME server variable when it's not populated by Apache. This change was made to comply with the CGI specification that PATH_TRANSLATED should only exist if PATH_INFO is defined. Apache 2 users may use AcceptPathInfo = On inside httpd.conf to define PATH_INFO. 
        /// </summary>
        [RenderValue("'PATH_TRANSLATED'")]
        PathTranslated,

        /// <summary>
        /// Contains the current script's path. This is useful for pages which need to point to themselves. 
        /// The __FILE__ constant contains the full path and filename of the current (i.e. included) file.
        /// </summary>
        [RenderValue("'SCRIPT_NAME'")]
        RenderValue,

        /// <summary>
        /// The URI which was given in order to access this page, for instance, '/index.html'. 
        /// </summary>
        [RenderValue("'REQUEST_URI'")]
        RequestUri,

        /// <summary>
        /// When doing Digest HTTP authentication this variable is set to the 'Authorization' header sent by the client (which you should then use to make the appropriate validation). 
        /// </summary>
        [RenderValue("'PHP_AUTH_DIGEST'")]
        PhpAuthDigest,

        /// <summary>
        /// When doing HTTP authentication this variable is set to the username provided by the user. 
        /// </summary>
        [RenderValue("'PHP_AUTH_USER'")]
        PhpAuthUser,

        /// <summary>
        /// When doing HTTP authentication this variable is set to the password provided by the user. 
        /// </summary>
        [RenderValue("'PHP_AUTH_PW'")]
        PhpAuthPassword,


        /// <summary>
        /// When doing HTTP authenticated this variable is set to the authentication type. 
        /// </summary>
        [RenderValue("'AUTH_TYPE'")]
        AuthType,


        /// <summary>
        /// Contains any client-provided pathname information trailing the actual script filename but preceding the query string, if available. For instance, 
        /// if the current script was accessed via the URL http://www.example.com/php/path_info.php/some/stuff?foo=bar, then $_SERVER['PATH_INFO'] would contain /some/stuff. 
        /// </summary>
        [RenderValue("'PATH_INFO'")]
        PathInfo,

        /// <summary>
        /// Original version of 'PATH_INFO' before processed by PHP. 
        /// </summary>
        [RenderValue("'ORIG_PATH_INFO'")]
        OriginalPathInfo,


        [RenderValue("'HTTP_CLIENT_IP'")]
        HttpClientIp,

        [RenderValue("'HTTP_X_FORWARDED_FOR'")]
        HttpXForwarderFor,
    }
}
