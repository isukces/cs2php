using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Graph
{
    [Skip]
    public class Image
    {
		#region Constructors 

        private Image()
        {

        }

		#endregion Constructors 

		#region Static Methods 

		// Public Methods 

        [DirectCall("imagecreate")]
        public static Image Create(int width, int height)
        {
            throw new NotImplementedException();
        }

        [DirectCall("imagecreatetruecolor ")]
        public static Image CreateTrueColor(int width, int height)
        {
            throw new NotImplementedException();
        }

        [UseBinaryExpression("!==", "false", "$0")]
        public static bool IsValid(Image image)
        {
            return image != null;
        }

		#endregion Static Methods 

		#region Methods 

		// Public Methods 

        [DirectCall("imagecolorallocate", "this,0,1,2")]
        public Color ColorAllocate(int red, int green, int blue)
        {
            throw new NotImplementedException();
        }

        [DirectCall("imagecolordeallocate", "this,0")]
        public Color ColorDeallocate(Color c)
        {
            throw new NotImplementedException();
        }

        [DirectCall("imagedestroy", "this")]
        public bool Destroy()
        {
            throw new NotImplementedException();
        }

        [DirectCall("imageline", "this,0,1,2,3,4")]
        public bool DrawLine(int x1, int y1, int x2, int y2, Color color)
        {
            //  bool imageline ( resource image , int x1 , int y1 , int x2 , int y2 , int color )
            throw new NotImplementedException();
        }

        [DirectCall("imagestring", "this,0,1,2,3,4")]
        public bool DrawString(Font font, int x, int y, string text, Color color)
        {
            // bool imagestring ( resource image , int font , int x , int y , string string , int color )
            throw new NotImplementedException();
        }

        [DirectCall("imagefilledrectangle", "this,0,1,2,3,4")]
        public bool FillRectangle(int x1, int y1, int x2, int y2, Color color)
        {
            // bool imagefilledrectangle ( resource $image , int $x1 , int $y1 , int $x2 , int $y2 , int $color )
            throw new NotImplementedException();
        }

        [DirectCall("imagejpeg ", "this,0,1")]
        public bool Jpeg(string filename = null, int? quality = null)
        {
            // bool imagejpeg ( resource $image [, string $filename [, int $quality ]] )
            throw new NotImplementedException();
        }

        [DirectCall("imagepng ", "this")]
        public bool Png()
        {
            throw new NotImplementedException();
        }

		#endregion Methods 

		#region Fields 
        [AsValue]
        public const string HEADER_CONTENT_TYPE_JPG = "Content-Type: image/jpg";
        [AsValue]
        public const string HEADER_CONTENT_TYPE_PNG = "Content-Type: image/png";

		#endregion Fields 
    }
}
