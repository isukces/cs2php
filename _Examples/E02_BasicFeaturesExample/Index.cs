using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Php;
namespace Lang.Php.Examples.BasicFeaturesExample
{

    [Page("index")]
    class Index : PhpDummy
    {
        [ScriptName("h")]
        public static void Header(string title)
        {
            Response.Echo(Doctypes.XhtmlTransitional);
            echo(PHP_EOL);
            Html.EchoTagOpen(Tags.Html);
            echo(PHP_EOL);
            Html.EchoTagOpen(Tags.Head);
            echo(PHP_EOL);
            {
                // put here your html tags
                Html.EchoTagBound(Tags.Title, htmlentities(title));
                echo(PHP_EOL);
            }
            Html.EchoTagClose(Tags.Head);
            echo(PHP_EOL);
            Html.EchoTagOpen(Tags.Body);
            echo(PHP_EOL);
        }
        [ScriptName("f")]
        public static void Footer()
        {
            Html.EchoTagClose(Tags.Body);
            Html.EchoTagClose(Tags.Html);
        }
        public static void PhpMain()
        {
            Header("Welcome to CS2PHP");
            echo("<p>You can use echo just like in PHP</p>");
            Html.EchoTagBound(Tags.P, "Or use some helpers.");

            MultiplicationTable.ShowMultiplicationTable(10, 12);
            Footer();
        }

     
    }
}
