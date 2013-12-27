using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Php;

namespace Lang.Php.Test.Code
{
    [IgnoreNamespace]
    [ScriptName("MyCodePhp")]
    class MyCode
    {
        public static void BasicMath1()
        {
            var a = 1;
            var b = 2;
            var d = (a + b) / Math.PI;
        }
    }
}
