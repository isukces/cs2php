namespace Lang.Php
{
    /// <summary>
    /// Css media types from http://www.w3.org/TR/CSS2/media.html#media-types
    /// </summary>
    [EnumRender(EnumRenderOptions.UnderscoreLowercase, false)]
    public enum CssMediaTypes
    {
        /// <summary>
        /// Suitable for all devices.
        /// </summary>
        All,

        /// <summary>
        /// Intended for braille tactile feedback devices. 
        /// </summary>
        Braille,

        /// <summary>
        /// Intended for paged braille printers.
        /// </summary>
        Embossed,

        /// <summary>
        /// Intended for handheld devices (typically small screen, limited bandwidth). 
        /// </summary>
        Handheld,

        /// <summary>
        /// Intended for paged material and for documents viewed on screen in print preview mode. Please consult the section on paged media for information about formatting issues that are specific to paged media. 
        /// </summary>
        Print,
        /// <summary>
        ///  Intended for projected presentations, for example projectors. Please consult the section on paged media for information about formatting issues that are specific to paged media. 
        /// </summary>
        Projection,

        /// <summary>
        /// Intended primarily for color computer screens. 
        /// </summary>
        Screen,
        /// <summary>
        /// Intended for speech synthesizers. Note: CSS2 had a similar media type called 'aural' for this purpose. See the appendix on aural style sheets for details. 
        /// </summary>
        Speech,
        /// <summary>
        /// Intended for media using a fixed-pitch character grid (such as teletypes, terminals, or portable devices with limited display capabilities). Authors should not use pixel units with the "tty" media type. 
        /// </summary>
        Tty,
        /// <summary>
        /// Intended for television-type devices (low resolution, color, limited-scrollability screens, sound available). 
        /// </summary>
        Tv,

    }
}
