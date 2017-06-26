using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lang.Php
{
    public static class Preg
    {
        public static PregMatchResult Match(string pattern, string subject)
        {
            var delimiter = pattern[0];
            var i = pattern.LastIndexOf(delimiter);
            var pattern1 = pattern.Substring(1, i - 1);
            var options = pattern.Substring(i + 1);
            if (options != "")
                throw new NotImplementedException();
            var re = new Regex(pattern1);
            return re.IsMatch(subject) ? PregMatchResult.Success : PregMatchResult.Fail;
            // string pattern , string subject [, array &$matches [, int $flags = 0 [, int $offset = 0 ]]] 
        }

        public static PregMatchResult Match(string pattern, string subject, out Dictionary<object, string> matches, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public static PregMatchResult MatchWithOffset(string pattern, string subject, out Dictionary<object, PregMatchWithOffset> matches, int offset = 0)
        {
            throw new NotImplementedException();
        }


    }
}
