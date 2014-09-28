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
            echo(Doctypes.XHTML_Transitional + PHP_EOL);
            Html.EchoTagOpen(Tags.Html);
            {
                Html.EchoTagOpen(Tags.Head);
                {
                    Html.EchoTagBound(Tags.Title, "Dynamic image");
                }
                Html.EchoTagClose(Tags.Head);
            }
            {
                Html.EchoTagOpen(Tags.Body);
                {
                    var examples = new int[] { -1, 10, 50, 100 };
                    foreach (var i in examples)
                    {
                        var type = i < 0 ? "png" : "jpg";
                        if (i < 0)
                            Html.EchoTagBound(Tags.P, "PNG");
                        else
                            Html.EchoTagBound(Tags.P, "JPG, compression " + i);
                        echo(PHP_EOL);
                        Html.EchoTagBound(Tags.P,
                            Html.TagSingle(Tags.Img,
                            Attr.Src, "image.php?format=" + type + (i > 0 ? "&compression=" + i : ""),
                            Attr.Alt, "Demo " + (i > 0 ? "jpg" : "png") + " image"
                                )
                        );
                        echo(PHP_EOL);
                    }
                }
                Html.EchoTagClose(Tags.Body);
                echo(PHP_EOL);
            }
            Html.EchoTagClose(Tags.Html);
            echo(PHP_EOL);
        }
    }
}
