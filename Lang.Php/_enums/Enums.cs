using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [EnumRender(EnumRenderOptions.MinusLowercase, false)]
	public enum Tags
    {
	    [RenderValue("'html'")]
    Html,
    [RenderValue("'body'")]
    Body,
    [RenderValue("'head'")]
    Head,
    [RenderValue("'meta'")]
    Meta,
    [RenderValue("'script'")]
    Script,
    [RenderValue("'div'")]
    Div,
    [RenderValue("'form'")]
    Form,
    [RenderValue("'input'")]
    Input,
    [RenderValue("'a'")]
    A,
    [RenderValue("'button'")]
    Button,
    [RenderValue("'table'")]
    Table,
    [RenderValue("'tr'")]
    Tr,
    [RenderValue("'td'")]
    Td,
    [RenderValue("'th'")]
    Th,
    [RenderValue("'label'")]
    Label,
    [RenderValue("'select'")]
    Select,
    [RenderValue("'option'")]
    Option,
    [RenderValue("'textarea'")]
    Textarea,
    [RenderValue("'p'")]
    P,
    [RenderValue("'link'")]
    Link,
    [RenderValue("'br'")]
    Br,
    [RenderValue("'style'")]
    Style,
    [RenderValue("'title'")]
    Title,
    [RenderValue("'img'")]
    Img,
    [RenderValue("'b'")]
    B,
    [RenderValue("'h1'")]
    H1,
    [RenderValue("'h2'")]
    H2,
    [RenderValue("'h3'")]
    H3,
    [RenderValue("'h4'")]
    H4,
    [RenderValue("'h5'")]
    H5,
    [RenderValue("'h6'")]
    H6,
     
    }

    public enum Attr 
    {
	    [RenderValue("'method'")]
    Method,
    [RenderValue("'action'")]
    Action,
    [RenderValue("'enctype'")]
    Enctype,
    [RenderValue("'acceptCharset'")]
    AcceptCharset,
    [RenderValue("'type'")]
    Type,
    [RenderValue("'name'")]
    Name,
    [RenderValue("'value'")]
    Value,
    [RenderValue("'class'")]
    Class,
    [RenderValue("'src'")]
    Src,
    [RenderValue("'property'")]
    Property,
    [RenderValue("'content'")]
    Content,
    [RenderValue("'style'")]
    Style,
    [RenderValue("'href'")]
    Href,
    [RenderValue("'target'")]
    Target,
    [RenderValue("'id'")]
    Id,
    [RenderValue("'for'")]
    For,
    [RenderValue("'checked'")]
    Checked,
    [RenderValue("'selected'")]
    Selected,
    [RenderValue("'rel'")]
    Rel,
    [RenderValue("'alt'")]
    Alt,
    [RenderValue("'rowspan'")]
    Rowspan,
    [RenderValue("'colspan'")]
    Colspan,
    [RenderValue("'onclick'")]
    OnClick,
    [RenderValue("'xmlns'")]
    XmlNs,
    [RenderValue("'xml:lang'")]
    XmlLang,
    [RenderValue("'http-equiv'")]
    HttpEquiv,
     
    }
	public enum MimeTypes 
    {
	    [RenderValue("'text/css'")]
    TextCss,
     
    }

	

	public enum ATargets
    {
	    [RenderValue("'_blank'")]
    Blank,
     
    }

    public enum ScriptTypes
    {
	    [RenderValue("'text/javascript'")]
    TextJavascript,
     
    }
	 public enum InputTypes
    {
	    [RenderValue("'hidden'")]
    Hidden,
    [RenderValue("'text'")]
    Text,
    [RenderValue("'submit'")]
    Submit,
    [RenderValue("'button'")]
    Button,
    [RenderValue("'checkbox'")]
    Checkbox,
     
    }

	 public enum TagsSpecial
    {
	    [RenderValue("'<'")]
    Open,
    [RenderValue("'>'")]
    Close,
    [RenderValue("' />'")]
    CloseSingle,
    [RenderValue("'</'")]
    OpenClosing,
    [RenderValue("'//<![CDATA['")]
    CDataOpen,
    [RenderValue("'//]]>'")]
    CDataClose,
     
    }

	public enum Doctypes {
	    [RenderValue("'<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">'")]
    XHTML_Transitional,
	}


	public enum CssTextAlign
    {
	    [RenderValue("'left'")]
    Left,
    [RenderValue("'right'")]
    Right,
    [RenderValue("'center'")]
    Center,
     
    }
	public enum CssVerticalAlign
    {
	    [RenderValue("'top'")]
    Top,
    [RenderValue("'bottom'")]
    Bottom,
     
    }

	[EnumRender(EnumRenderOptions.MinusLowercase, false)]
	public enum CssColors
    {
	    [RenderValue("'yellow'")]
    Yellow,
    [RenderValue("'red'")]
    Red,
    [RenderValue("'black'")]
    Black,
    [RenderValue("'white'")]
    White,
    [RenderValue("'green'")]
    Green,
    [RenderValue("'blue'")]
    Blue,
     
    }
	public enum ProtocolsLong
    {
	    [RenderValue("'http://'")]
    Http,
    [RenderValue("'https://'")]
    HttpSecured,
    [RenderValue("'ftp://'")]
    Ftp,
     
    }
	public enum LinkRels
    {
	    [RenderValue("'stylesheet'")]
    Stylesheet,
     
    }
	[EnumRender(EnumRenderOptions.MinusLowercase, false)]
	public enum CssFontSizes
    {
	    XxSmall,
    XSmall,
    Small,
    Medium,
    Large,
    XLarge,
    XxLarge,
    Smaller,
    Sarger,
    Inherit,
     
    
	}
	[EnumRender(EnumRenderOptions.MinusLowercase, false)]
	public enum CssBorderStyles
    {
	    None,
    Hidden,
    Dotted,
    Dashed,
    Solid,
    Double,
    Groove,
    Ridge,
    Inset,
    Outset,
    Inherit,
     
    }

	[Flags]
	public enum FilePutContentsFlags {
	    [RenderValue("FILE_USE_INCLUDE_PATH")]
    UseIncludePath,
    [RenderValue("FILE_APPEND")]
    Append,
    [RenderValue("LOCK_EX")]
    LockEx,
    
	}

	public enum CssFontFamilies 
	{
	    [RenderValue("'arial'")]
    Arial,
    [RenderValue("'arial,verdana'")]
    Arial_verdana,
    [RenderValue("'helvetica,impact,sans-serif'")]
    Helvetica_impact_sansserif,
     	
	}
	public enum DateTimeFormats 
	{
	    [RenderValue("DateTime::ATOM")]
    ATOM,
    [RenderValue("DateTime::COOKIE")]
    COOKIE,
    [RenderValue("DateTime::ISO8601")]
    ISO8601,
    [RenderValue("DateTime::RFC822")]
    RFC822,
    [RenderValue("DateTime::RFC850")]
    RFC850,
    [RenderValue("DateTime::RFC1036")]
    RFC1036,
    [RenderValue("DateTime::RFC1123")]
    RFC1123,
    [RenderValue("DateTime::RFC2822")]
    RFC2822,
    [RenderValue("DateTime::RFC3339")]
    RFC3339,
    [RenderValue("DateTime::RSS")]
    RSS,
    [RenderValue("DateTime::W3C")]
    W3C,
    [RenderValue("'Y-m-d H:i:s'")]
    MySQL,
    [RenderValue("'D, d M Y H:i:s T'")]
    HttpHeader,
     	
	}

	public enum PhpTypes 
	{
	    [RenderValue("'boolean'")]
    Boolean,
    [RenderValue("'integer'")]
    Integer,
    [RenderValue("'double'")]
    Double,
    [RenderValue("'string'")]
    String,
    [RenderValue("'array'")]
    Array,
    [RenderValue("'object'")]
    Object,
    [RenderValue("'resource'")]
    Resource,
    [RenderValue("'NULL'")]
    Null,
    [RenderValue("'unknown type'")]
    Unknown,
     	
	}
 
}
