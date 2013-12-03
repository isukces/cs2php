using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Filters
{

    [AsArray]
    public class EmailOptions
    {
        [ScriptName("default")]
        public string Default;
    }
}
