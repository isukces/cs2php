using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public delegate string OutputCallbackDelegate1(string buffer);
    public delegate string OutputCallbackDelegate2(string buffer, int phase);
}
