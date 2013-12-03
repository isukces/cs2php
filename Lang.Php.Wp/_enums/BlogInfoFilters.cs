using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lang.Php;

namespace Lang.Php.Wp
{
    [EnumRender(EnumRenderOptions.UnderscoreLowercase)]
    public enum BlogInfoFilters
    {
        Display,
        Raw
    }
}
