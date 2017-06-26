using System;

namespace Lang.Php.Filters
{
    [Flags]
    public enum IntFlags
    {
        /// <summary>
        /// Regards inputs starting with a zero (0) as octal numbers. 
        /// This only allows the succeeding digits to be 0-7.
        /// </summary>
        [RenderValue("FILTER_FLAG_ALLOW_OCTAL")]
        AllowOctal,

        /// <summary>
        /// Regards inputs starting with 0x or 0X as hexadecimal numbers. 
        /// This only allows succeeding characters to be a-fA-F0-9.
        /// </summary>
        [RenderValue("FILTER_FLAG_ALLOW_HEX")]
        AllowHex
    }
}
