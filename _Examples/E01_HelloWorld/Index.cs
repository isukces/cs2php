using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lang.Php;

namespace Lang.Php.Examples.HelloWorld
{

    [Page("index")]
    public class Index : PhpDummy
    {
        public static void PhpMain()
        {
            echo("Hello world!");
        }
    }
}
