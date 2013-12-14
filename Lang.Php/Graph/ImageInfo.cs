using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Graph
{
    [AsArray]
    public class ImageInfo
    {
        /*
         array (size=7)
  0 => int 3456
  1 => int 2592
  2 => int 2
  3 => string 'width="3456" height="2592"' (length=26)
  'bits' => int 8
  'channels' => int 3
  'mime' => string 'image/jpeg' (length=10)
         */
        [ScriptName("0")]
        public int Width;
        [ScriptName("1")]
        public int Height;
        [ScriptName("2")]
        public ImageTypes ImageType;

        /// <summary>
        /// i.e. 'width="3456" height="2592"'
        /// </summary>
        [ScriptName("3")]
        public string HtmlWidthHeight;

        [ScriptName("bits")]
        public int Bits;
        [ScriptName("channels")]
        public int Channels;
        /// <summary>
        /// i.e. 'image/jpeg'
        /// </summary>
        [ScriptName("mime")]
        public string Mime;
    }
}
