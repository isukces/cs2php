using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lang.Php;

namespace Lang.Php.Wp
{
    [EnumRender(EnumRenderOptions.UnderscoreLowercase)]
    public enum BlogInfoShows
    {
        Description,
        Name,
        TemplateDirectory,
        Url        
    }
}
