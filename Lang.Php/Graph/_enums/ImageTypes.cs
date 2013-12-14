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
        [RenderValue("IMG_GIF")]
        Gif = 1,
        [RenderValue("IMG_JPG")]
        Jpeg = 2,
        [RenderValue("IMAGETYPE_PNG")]
        Png = 3,
        [RenderValue("IMAGETYPE_SWF")]
        Swf = 4,
        [RenderValue("IMAGETYPE_PSD")]
        Psd = 5,
        [RenderValue("IMAGETYPE_BMP")]
        Bmp = 6,
        [RenderValue("IMAGETYPE_TIFF_II")]
        Tiff_Ii = 7,
        [RenderValue("IMAGETYPE_TIFF_MM")]
        Tiff_Mm = 8,
        [RenderValue("IMAGETYPE_JPC")]
        Jpc = 9,
        [RenderValue("IMAGETYPE_JP2")]
        Jp2 = 10,
        [RenderValue("IMAGETYPE_JPX")]
        Jpx = 11,
        [RenderValue("IMAGETYPE_JB2")]
        Jb2 = 12,
        [RenderValue("IMAGETYPE_SWC")]
        Swc = 13,
        [RenderValue("IMAGETYPE_IFF")]
        Iff = 14,
        [RenderValue("IMAGETYPE_WBMP")]
        Wbmp = 15,
        [RenderValue("IMAGETYPE_XBM")]
        Xbm = 16,
        [RenderValue("IMAGETYPE_ICO")]
        Ico = 17,    
    }
}
