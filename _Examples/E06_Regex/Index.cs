using System.Collections.Generic;
using Lang.Php;

namespace E06_Regex
{
    [Page("index")]
    [IgnoreNamespace]
    public class Index : PhpDummy
    {
        public static void PhpMain()
        {
            PhpStyle();
            WithOffset();
        }

        private static void WithOffset()
        {
            Dictionary<object, PregMatchWithOffset> matchWithOffsets;
            Dictionary<object, string> matches;
            var a = Preg.Match("/hello/i", "a Hello x", out matches, 2);
            var b = Preg.Match("/hello/i", "a Hello x");
            var c = Preg.MatchWithOffset("/hello/i", "ó Hello world", out matchWithOffsets);
            echo("We have " + matchWithOffsets[0].Value + " at " + matchWithOffsets[0].Offset);
        }

        private static void PhpStyle()
        {
            Dictionary<object, string> matches;
            var a = preg_match("/hello/i", "Hello world", out matches);
            if (a.IsError)
                echo("preg_match: Error");
            else if (a.IsSuccess)
                echo("preg_match: Match");
            else
                echo("preg_match: Don't match");

            var_dump(matches);
        }
    }
}
