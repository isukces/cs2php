using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Php;
using Lang.Php.Graph;
using Lang.Php.Filters;


namespace E04_Graphics
{
    [Page("image")]
    class DynamicImage : PhpDummy
    {
        #region Static Methods

        // Public Methods 

        public static void PhpMain()
        {
            int compression = FilterInput.ValidateInt(FilterInput.Type.Get, "compression", IntFlags.AllowHex, new IntOptions() { Default = 90 }).Value;
            if (Script.Get.ContainsKey("format") && Script.Get["format"] == "jpg")
                CreateImage(true, compression);
            else
                CreateImage(false, compression);
        }
        // Private Methods 

        private static void CreateImage(bool jpg, int compression)
        {
            if (compression < 0)
                compression = 0;
            else if (compression > 100)
                compression = 100;
            if (jpg)
                header(Image.HEADER_CONTENT_TYPE_JPG);
            else
                header(Image.HEADER_CONTENT_TYPE_PNG);

            var myImage = Image.CreateTrueColor(WIDTH, HEIGHT);
            if (Image.IsValid(myImage))
            {
                Color color = myImage.ColorAllocate(255, 0, 0); // first call is background
                for (int i = 0; i < WIDTH; i++)
                {
                    color = myImage.ColorAllocate(i, 255 - i, 0);
                    myImage.DrawLine(0, HEIGHT - 1, i, 0, color);
                    color = myImage.ColorAllocate(i, 0, 255 - i);
                    myImage.DrawLine(WIDTH - 1, 0, WIDTH - 1 - i, HEIGHT - 1, color);
                }
                color = myImage.ColorAllocate(0, 0, 255);
                myImage.DrawString(Font.Font4, 10, 30, "Hello", color);
                color = myImage.ColorAllocate(255, 255, 255);
                myImage.DrawString(Font.Font4, 10, 50, "C# to PHP is fantastic", color);
                if (jpg)
                    myImage.Jpeg(null, compression);
                else
                    myImage.Png();
                myImage.Destroy();
            }
        }

        #endregion Static Methods

        #region Fields

        public const int HEIGHT = 100;
        public const int WIDTH = 256;

        #endregion Fields
    }
}
