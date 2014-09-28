using System;
using System.Collections.Generic;
using System.Text;
using Lang.Php;
namespace Lang.Php.Wp
{
    [IgnoreNamespace]
    [Skip]
    [Module("class-http", "ABSPATH", "WPINC")]
    //  include_once( ABSPATH  +  WPINC +  "/class-http + php" );
    public class WP_Http
    {

        public Request_Array request(string url, Dictionary<string, object> pp)
        {
            throw new NotSupportedException();
        }


        [AsArray]
        public class Request_Array
        {
            [ScriptName("response")]
            public Response Response;

            [ScriptName("body")]
            public string Body;

            [ScriptName("headers")]
            public Dictionary<string, string> Headers;

            [ScriptName("cookies")]
            public Dictionary<string, string> Cookies;

            [ScriptName("filename")]
            public string Filename;

            [ScriptName("errors")]
            public object[] Errors;

            public Request_Array(Response response)
            {
                this.Response = response;
            }
        }
        [AsArray]
        public class Response
        {
            [ScriptName("code")]
            public int Code;

            [ScriptName("message")] 
            public string Message;

        }
        [Skip]
        public class Request_Object
        {
            public object errors;

        }


    }
}
