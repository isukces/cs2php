using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lang.Php;

namespace Lang.Php.Graph
{
    public enum ImageTypes
    {
        [RenderValue("0")]
        Unknown = 0,
        [RenderValue("1")]
        Gif = 1,
        [RenderValue("2")]
        Jpeg = 2,
        [RenderValue("3")]
        Png = 3,
        [RenderValue("4")]
        Swf = 4,
        [RenderValue("5")]
        Psd = 5,
        [RenderValue("6")]
        Bmp = 6,
        [RenderValue("7")]
        Tiff_Ii = 7,
        [RenderValue("8")]
        Tiff_Mm = 8,
        [RenderValue("9")]
        Jpc = 9,
        [RenderValue("10")]
        Jp2 = 10,
        [RenderValue("11")]
        Jpx = 11,
        [RenderValue("12")]
        Jb2 = 12,
        [RenderValue("13")]
        Swc = 13,
        [RenderValue("14")]
        Iff = 14,
        [RenderValue("15")]
        Wbmp = 15,
        [RenderValue("16")]
        Xbm = 16,
        [RenderValue("17")]
        Ico = 17,    
    }
}
