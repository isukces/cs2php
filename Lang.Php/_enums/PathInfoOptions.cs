using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Flags]
    public enum PathInfoOptions
    {
        [RenderValue("PATHINFO_DIRNAME")]
        Dirname = 1,
        [RenderValue("PATHINFO_BASENAME")]
        Basename = 2,
        [RenderValue("PATHINFO_EXTENSION")]
        Extension = 4,
        [RenderValue("PATHINFO_FILENAME")]
        Filename = 8,

        [RenderValue("PATHINFO_DIRNAME | PATHINFO_BASENAME | PATHINFO_EXTENSION | PATHINFO_FILENAME")]
        All = 1+2+4+8
    }
}
