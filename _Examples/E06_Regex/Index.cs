using System.Collections.Generic;
using System.Text.RegularExpressions;
using Lang.Php;

namespace E06_Regex
{
    [Page("index")]
    [IgnoreNamespace]
    public class Index : PhpDummy
    {
        public static void PhpMain()
        {
            Dictionary<object, string> matches;
            var a = preg_match("/hello/i", "Hello world", out matches);
            if (a.IsError)
                echo("preg_match: Error");
            else if (a.IsSuccess)
                echo("preg_match: Match");
            else
                echo("preg_match: Don't match");          
        }
    }
}
