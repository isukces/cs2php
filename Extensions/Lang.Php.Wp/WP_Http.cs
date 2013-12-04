using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lang.Php;
namespace Lang.Php.Wp
{
    [IgnoreNamespace]
    [Skip]
    public class WP_Http
    {
    
        public Request_Object request(string url, Dictionary<string, object> pp)
        {
            throw new NotSupportedException();
        }


        [AsArray]
        public class Request_Array
        {         
            public Response response;
            public string body;
            public Dictionary<string, string> headers;
            // public Dictionary<string, string> response;
            public Dictionary<string, string> cookies;
            public string filename;
        }
        [AsArray]
        public class Response
        {
            public int code;

        }
        [Skip]
        public class Request_Object
        {
            public object errors;

        }


    }
}
