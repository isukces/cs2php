using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Wp
{
    [EnumRender(EnumRenderOptions.UnderscoreLowercase, false)]
    public enum PostTypes
    {
        Post,
        Page,
        Dashboard,
        Link,
        Attachment,
        CustomPostType
    }
}
