using System;

namespace Lang.Php
{
    [Flags]
    public enum HtmlEntitiesFlags
    {
        /// <summary>
        /// Will convert double-quotes and leave single-quotes alone.
        /// </summary>
        [RenderValue("ENT_COMPAT")]
        COMPAT = 1,
        /// <summary>
        /// Will convert both double and single quotes.
        /// </summary>
        [RenderValue("ENT_QUOTES")]
        QUOTES = 2,
        /// <summary>
        /// Will leave both double and single quotes unconverted.
        /// </summary>
        [RenderValue("ENT_NOQUOTES")]
        NOQUOTES = 4,
        /// <summary>
        /// Silently discard invalid code unit sequences instead of returning an empty string. 
        /// Using this flag is discouraged as it » may have security implications.
        /// </summary>
        [RenderValue("ENT_IGNORE")]
        IGNORE = 8,
        /// <summary>
        /// Replace invalid code unit sequences with a Unicode Replacement Character U+FFFD (UTF-8) or &#FFFD; 
        /// (otherwise) instead of returning an empty string.
        /// </summary>
        [RenderValue("ENT_SUBSTITUTE")]
        SUBSTITUTE = 16,
        /// <summary>
        /// Replace invalid code points for the given document type with a Unicode Replacement Character U+FFFD (UTF-8) or &#FFFD; 
        /// (otherwise) instead of leaving them as is. 
        /// This may be useful, for instance, to ensure the well-formedness of XML documents with embedded external content.
        /// </summary>
        [RenderValue("ENT_DISALLOWED")]
        DISALLOWED = 32,
        /// <summary>
        /// Handle code as HTML 4.01.
        /// </summary>
        [RenderValue("ENT_HTML401")]
        HTML401 = 64,
        /// <summary>
        /// Handle code as XML 1.
        /// </summary>
        [RenderValue("ENT_XML1")]
        XML1 = 128,
        /// <summary>
        /// Handle code as XHTML.
        /// </summary>
        [RenderValue("ENT_XHTML")]
        _XHTML = 256,
        /// <summry>
        /// Handle code as HTML 5. 
        /// </summary>
        [RenderValue("ENT_HTML5")]
        ENT_HTML5 = 512
    }
}
