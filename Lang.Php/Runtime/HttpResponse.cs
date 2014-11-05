using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Runtime
{


    /*
    smartClass
    option NoAdditionalFile
    
    property Method string 
    
    property Head List<KeyValuePair<string,string>> 
    	init #
    
    property Text string 
    	read only sb.ToString();
    
    property StatusCode HttpStatusCode 
    	init 200
    smartClassEnd
    */
    
    public partial class HttpResponse
    {
        #region Methods

        // Public Methods 

        public void Echo(string x)
        {
            sb.Append(x);
        }

        public byte[] GetComplete(Encoding encoding)
        {
            var output = new StringBuilder();
            {
                //var a = Enum.GetValues(typeof(HttpStatusCode)).OfType<HttpStatusCode>();
                //var bb = a.Where(ia => code == (int)ia).ToArray();

                output.AppendFormat("HTTP/1.1 {1} {0}\r\n", statusCode, (int)statusCode );
                foreach (var h in head)
                    output.AppendFormat("{0}: {1}\r\n", h.Key, h.Value);
                output.Append("\r\n");
            }
            output.Append(Text);
            var g = output.ToString();
            var binaryOutput = encoding.GetBytes(g);
            return binaryOutput;
        }
        public static string AAA(DateTime d)
        {
            if (d.Kind == DateTimeKind.Unspecified)
                d = d.ToLocalTime();
            d = d.ToUniversalTime();
            // Sun, 17 Nov 2013 09:02:29 GMT
            return d.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture.DateTimeFormat) + " GMT";
            /*
             * tring.Format("{0:y yy yyy yyyy}", dt);  // "8 08 008 2008"   year
String.Format("{0:M MM MMM MMMM}", dt);  // "3 03 Mar March"  month
String.Format("{0:d dd ddd dddd}", dt);  // "9 09 Sun Sunday" day
String.Format("{0:h hh H HH}",     dt);  // "4 04 16 16"      hour 12/24
String.Format("{0:m mm}",          dt);  // "5 05"            minute
String.Format("{0:s ss}",          dt);  // "7 07"            second
String.Format("{0:f ff fff ffff}", dt);  // "1 12 123 1230"   sec.fraction
String.Format("{0:F FF FFF FFFF}", dt);  // "1 12 123 123"    without zeroes
String.Format("{0:t tt}",          dt);  // "P PM"            A.M. or P.M.
String.Format("{0:z zz zzz}",      dt);  // "-6 -06 -06:00"   time zone
             */
        }
        public HttpResponse()
        {
            SetHeader("Date", AAA(DateTime.Now));
            SetHeader("Server", "CS2PHP Server");
            SetHeader("Content-Type", "text/html");
            /*
             * Date: Sun, 17 Nov 2013 09:02:29 GMT
Server: Apache/2.4.4 (Win64) PHP/5.4.12
X-Powered-By: PHP/5.4.12
Content-Length: 4233
Keep-Alive: timeout=5, max=100
Connection: Keep-Alive
Content-Type: text/html
             */
        }

        public void SetHeader(string key, string value)
        {
            var newHeaderItem = new KeyValuePair<string, string>(key, value);
            for (int idx = 0; idx < head.Count; idx++)
                if (head[idx].Key == key)
                {
                    head[idx] = newHeaderItem;
                    return;
                }
            head.Add(newHeaderItem);
        }

        #endregion Methods

        #region Fields

        StringBuilder sb = new StringBuilder();

        #endregion Fields

        public const string Example = @"HTTP/1.1 200 OK
Date: Sun, 17 Nov 2013 09:02:29 GMT
Server: Apache/2.4.4 (Win64) PHP/5.4.12
X-Powered-By: PHP/5.4.12
Content-Length: 4233
Keep-Alive: timeout=5, max=100
Connection: Keep-Alive
Content-Type: text/html

<?xml version=""1.0"" encoding=""iso-8859-1""?>
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.1//EN""
	""http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd"">
<html lang=""en"" xml:lang=""en"">
<head>
	<title>WAMPSERVER Homepage</title>
	<meta http-equiv=""Content-Type"" content=""txt/html; charset=utf-8"" />
	<style type=""text/css"">
* {
	margin: 0;
	padding: 0;
}

html {
	background: #ddd;
}

body {
	margin: 1em 10%;
	padding: 1em 3em;
	font: 80%/1.4 tahoma, arial, helvetica, lucida sans, sans-serif;
	border: 1px solid #999;
	background: #eee;
	position: relative;
}

#head {
	margin-bottom: 1.8em;
	margin-top: 1.8em;
	padding-bottom: 0em;
	border-bottom: 1px solid #999;
	letter-spacing: -500em;
	text-indent: -500em;
	height: 125px;
	background: url(index.php?img=gifLogo) 0 0 no-repeat;
}

.utility {
	position: absolute;
	right: 4em;
	top: 145px;
	font-size: 0.85em;
}

.utility li {
	display: inline;
}

h2 {
	margin: 0.8em 0 0 0;
}

ul {
	list-style: none;
	margin: 0;
	padding: 0;
}

#head ul li, dl ul li, #foot li {
	list-style: none;
	display: inline;
	margin: 0;
	padding: 0 0.2em;
}

ul.aliases, ul.projects, ul.tools {
	list-style: none;
	line-height: 24px;
}

ul.aliases a, ul.projects a, ul.tools a {
	padding-left: 22px;
	background: url(index.php?img=pngFolder) 0 100% no-repeat;
}

ul.tools a {
	background: url(index.php?img=pngWrench) 0 100% no-repeat;
}

ul.aliases a {
	background: url(index.php?img=pngFolderGo) 0 100% no-repeat;
}

dl {
	margin: 0;
	padding: 0;
}

dt {
	font-weight: bold;
	text-align: right;
	width: 11em;
	clear: both;
}

dd {
	margin: -1.35em 0 0 12em;
	padding-bottom: 0.4em;
	overflow: auto;
}

dd ul li {
	float: left;
	display: block;
	width: 16.5%;
	margin: 0;
	padding: 0 0 0 20px;
	background: url(index.php?img=pngPlugin) 2px 50% no-repeat;
	line-height: 1.6;
}

a {
	color: #024378;
	font-weight: bold;
	text-decoration: none;
}

a:hover {
	color: #04569A;
	text-decoration: underline;
}

#foot {
	text-align: center;
	margin-top: 1.8em;
	border-top: 1px solid #999;
	padding-top: 1em;
	font-size: 0.85em;
}
</style>
	<link rel=""shortcut icon"" href=""index.php?img=favicon"" type=""image/ico"" />
</head>
<body>
	<div id=""head"">
		<h1><abbr title=""Windows"">W</abbr><abbr title=""Apache"">A</abbr><abbr title=""MySQL"">M</abbr><abbr title=""PHP"">P</abbr></h1>
		<ul>
			<li>PHP 5</li>
			<li>Apache 2</li>
			<li>MySQL 5</li>
		</ul>
	</div>
	<ul class=""utility"">
		<li>Version 2.4
</li>
		<li><a href=""?lang=fr"">Version Fran&ccedil;aise</a></li>
	</ul>
	<h2> Server Configuration </h2>
	<dl class=""content"">
		<dt>Apache Version :</dt>
		<dd>2.4.4
 &nbsp;</dd>
		<dt>PHP Version :</dt>
		<dd>5.4.12
 &nbsp;</dd>
		<dt>Loaded Extensions : </dt>
		<dd>
			<ul>
			<li>Core</li><li>bcmath</li><li>calendar</li><li>com_dotnet</li><li>ctype</li><li>date</li><li>ereg</li><li>filter</li><li>ftp</li><li>hash</li><li>iconv</li><li>json</li><li>mcrypt</li><li>SPL</li><li>odbc</li><li>pcre</li><li>Reflection</li><li>session</li><li>standard</li><li>mysqlnd</li><li>tokenizer</li><li>zip</li><li>zlib</li><li>libxml</li><li>dom</li><li>PDO</li><li>Phar</li><li>SimpleXML</li><li>wddx</li><li>xml</li><li>xmlreader</li><li>xmlwriter</li><li>apache2handler</li><li>gd</li><li>mbstring</li><li>mysql</li><li>mysqli</li><li>pdo_mysql</li><li>pdo_sqlite</li><li>mhash</li><li>xdebug</li>
			</ul>
		</dd>
		<dt>MySQL Version :</dt>
		<dd>5.6.12
 &nbsp;</dd>
	</dl>
	<h2>Tools</h2>
	<ul class=""tools"">
		<li><a href=""?phpinfo=1"">phpinfo()</a></li>
		<li><a href=""phpmyadmin/"">phpmyadmin</a></li>
	</ul>
	<h2>Your Projects</h2>
	<ul class=""projects"">
	</ul>
	<h2>Your Aliases</h2>
	<ul class=""aliases"">
	<li><a href=""cs2php/"">cs2php</a></li><li><a href=""phpmyadmin/"">phpmyadmin</a></li><li><a href=""sqlbuddy/"">sqlbuddy</a></li><li><a href=""webgrind/"">webgrind</a></li><li><a href=""wp/"">wp</a></li>
	</ul>
	<ul id=""foot"">
		<li><a href=""http://www.wampserver.com"">WampServer</a></li> -
        <li><a href=""http://www.wampserver.com/en/donations.php"">Donate</a></li> -
		<li><a href=""http://www.alterway.fr"">Alter Way</a></li>
	</ul>
</body>
</html>";
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-17 10:30
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Runtime
{
    public partial class HttpResponse 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public HttpResponse()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Method## ##Head## ##Text## ##StatusCode##
        implement ToString Method=##Method##, Head=##Head##, Text=##Text##, StatusCode=##StatusCode##
        implement equals Method, Head, Text, StatusCode
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Method; 
        /// </summary>
        public const string PROPERTYNAME_METHOD = "Method";
        /// <summary>
        /// Nazwa własności Head; 
        /// </summary>
        public const string PROPERTYNAME_HEAD = "Head";
        /// <summary>
        /// Nazwa własności Text; 
        /// </summary>
        public const string PROPERTYNAME_TEXT = "Text";
        /// <summary>
        /// Nazwa własności StatusCode; 
        /// </summary>
        public const string PROPERTYNAME_STATUSCODE = "StatusCode";
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
                return method;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                method = value;
            }
        }
        private string method = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<KeyValuePair<string,string>> Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
            }
        }
        private List<KeyValuePair<string,string>> head = new List<KeyValuePair<string,string>>();
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string Text
        {
            get
            {
                return sb.ToString();;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode
        {
            get
            {
                return statusCode;
            }
            set
            {
                statusCode = value;
            }
        }
        private HttpStatusCode statusCode = HttpStatusCode.OK;
        #endregion Properties

    }
}
