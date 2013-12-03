using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Framework.Replacers
{
    [Replace(typeof(int))]
    class IntReplacer
    {
        [DirectCall("", "0")]
        public static int Parse(string s)
        {
            return int.Parse(s);
        }

        [DirectCall("","this")]
        public override string ToString()
        {
            throw new MockMethodException();
        }
    }
}
