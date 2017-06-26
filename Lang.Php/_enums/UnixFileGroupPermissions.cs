using System;

namespace Lang.Php 
{
    [Flags]
    [EnumRender(EnumRenderOptions.OctalNumbers, false)]
    public enum UnixFileGroupPermissions
    {
        Read = 1,
        Write = 2,
        Exec = 4,
        All = 7
    }
   
}
