using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Wp
{
    [EnumRender(EnumRenderOptions.UnderscoreLowercase)]
    public enum PostTypes
    {
        Post,
        Page,
        Sashboard,
        Link,
        Attachment,
        CustomPostType
    }
}
