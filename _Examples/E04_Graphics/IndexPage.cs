using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Php;
using Lang.Php.Graph;

namespace E04_Graphics
{
    [Page("index")]
    public class IndexPage : PhpDummy
    {
        public static void PhpMain()
        {
           
            header("Content-Type: image/png");
            var myImage = Image.Create(200, 100);
            if (Image.IsValid(myImage))
            {
                Color color = myImage.ColorAllocate(0, 0, 0); // first call is background
                color = myImage.ColorAllocate(255, 0, 0);
                myImage.DrawString(Font.Font2, 10, 10, "Hello", color);
                myImage.Png();
                //myImage.Jpeg();
                //myImage.Jpeg(null);
                //myImage.Jpeg(null, 40);
                //myImage.Destroy();
            }
        }
    }
}
