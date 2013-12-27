using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [EnumRender(EnumRenderOptions.UnderscoreLowercase, false)]
    public enum FormMethods
    {
        Post,
        Get
    }
}
