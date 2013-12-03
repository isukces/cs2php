using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Flags]
    public enum UnixFilePermissions
    {
        OtherRead = 1,
        OtherWrite = 2,
        OtherExec = 4,

        GroupRead = 8,
        GroupWrite = 16,
        GroupExec = 32,

        OwnerRead = 64,
        OwnerWrite = 128,
        OwnerExec = 256,

        Common_770 = 256 + 128 + 64 + 32 + 16 + 8,
        Common_777 = 256 + 128 + 64 + 32 + 16 + 8 + 4 + 2 + 1

    }
}
