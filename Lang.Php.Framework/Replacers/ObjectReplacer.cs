using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Framework.Replacers
{

    [Replace(typeof(object))]
    class ObjectReplacer
    {
        [DirectCall("", "this")]
        public override string ToString()
        {
            throw new MockMethodException();
        }
    }
}
