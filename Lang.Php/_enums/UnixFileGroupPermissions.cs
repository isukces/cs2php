using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php 
{
    [Flags]
    public enum UnixFileGroupPermissions
    {
        Read = 1,
        Write = 2,
        Exec = 4,
        All = 7
    }
   
}
