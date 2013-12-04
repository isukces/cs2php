using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Wp
{
    public enum Priority
    {
        [RenderValue("'high'")]
        High,
        [RenderValue("'core'")]
        Core,
        [RenderValue("'default'")]
        Default,
        [RenderValue("'low'")]
        Low
    }
}
