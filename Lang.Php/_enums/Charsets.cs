using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    public enum Charsets
    {
        /// <summary>
        /// Western European, Latin-1.
        /// </summary>
        [RenderValue("'ISO-8859-1'")]
        ISO_8859_1,

        /// <summary>
        /// Latin-2
        /// </summary>
        [RenderValue("'ISO-8859-2'")]
        ISO_8859_2,

        /// <summary>
        /// Little used cyrillic charset (Latin/Cyrillic).
        /// </summary>
        [RenderValue("'ISO-8859-5'")]
        ISO_8859_5,
        /// <summary>
        /// Western European, Latin-9. Adds the Euro sign, French and Finnish letters missing in Latin-1 (ISO-8859-1).
        /// </summary>
        [RenderValue("'ISO-8859-15'")]
        ISO_8859_15,
        /// <summary>
        /// ASCII compatible multi-byte 8-bit Unicode.
        /// </summary>
        [RenderValue("'UTF-8'")]
        UTF_8,
        /// <summary>
        /// DOS-specific Cyrillic charset.
        /// </summary>
        [RenderValue("'cp866'")]
        cp866,
        /// <summary>
        /// Windows-specific Cyrillic charset.
        /// </summary>
        [RenderValue("'cp1251'")]
        cp1251,
        /// <summary>
        /// Windows specific charset for Western European.
        /// </summary>
        [RenderValue("'cp1252'")]
        cp1252,
        /// <summary>
        /// Russian.
        /// </summary>
        [RenderValue("'KOI8-R'")]
        KOI8_R,
        /// <summary>
        /// Traditional Chinese, mainly used in Taiwan.
        /// </summary>
        [RenderValue("'BIG5'")]
        BIG5,
        /// <summary>
        /// Simplified Chinese, national standard character set.
        /// </summary>
        [RenderValue("'GB2312'")]
        GB2312,
        /// <summary>
        /// Big5 with Hong Kong extensions, Traditional Chinese.
        /// </summary>
        [RenderValue("'BIG5-HKSCS'")]
        BIG5_HKSCS,
        /// <summary>
        /// Japanese
        /// </summary>
        [RenderValue("'Shift_JIS'")]
        Shift_JIS,
        /// <summary>
        /// Japanese
        /// </summary>
        [RenderValue("'EUC-JP'")]
        EUC_JP,
        /// <summary>
        /// Charset that was used by Mac OS.
        /// </summary>
        [RenderValue("'MacRoman'")]
        MacRoman,
    }
}
