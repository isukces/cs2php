using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public class SLFSupport
    {
        public static System.String Concat(System.Object[] x)
        {
            string r = "";
            foreach (var i in x)
                r += i.ToString();
            return r;
        }
    }
}
