using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public class SLFSupport
    {
        public static String Concat(Object[] x)
        {
            string r = "";
            foreach (var i in x)
                r += i.ToString();
            return r;
        }
    }
}
