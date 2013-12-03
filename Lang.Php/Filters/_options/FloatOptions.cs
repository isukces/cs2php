using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Filters
{
    [AsArray]
    public class FloatOptions
    {
        [ScriptName("default")]
        public double Default;
        [ScriptName("decimal")]
        public string DecimalSeparator;
    }
}
