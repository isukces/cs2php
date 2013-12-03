using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    public class UnixTimestamp
    {
        [DirectCall(null, "this")]
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
