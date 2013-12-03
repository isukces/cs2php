using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Filters
{
    [EnumRender(EnumRenderOptions.UnderscoreUppercase)]
    public enum KnownFilters
    {
        FilterValidateBoolean,
        FilterValidateEmail,
        FilterValidateFloat,
        FilterValidateInt,
        FilterValidateIp,
        FilterValidateRegexp,
        FilterValidateUrl
    }
}
