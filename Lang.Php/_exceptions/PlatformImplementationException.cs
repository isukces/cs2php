using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public class PlatformImplementationException : Exception
    {
        public PlatformImplementationException(Type t, string method, string msg)
            : base(string.Format("Platform implementation exception in {0}.{1}:\r\n{2}", t.FullName, method, msg))
        {

        }
    }
}
