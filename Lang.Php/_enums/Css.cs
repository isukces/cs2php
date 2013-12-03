using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [EnumRender(EnumRenderOptions.MinusLowercase)]
    public enum Css
    {
        Color,
        BackgroundColor,
        FontSize,
        FontFamily,
        Width,
        TextAlign,
        Margin,
        Padding,
        Border,
        VerticalAlign
        // border:5px solid red;
    }
}
