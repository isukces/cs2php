using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Examples.BasicFeaturesExample
{
    [Page("swich-case")]
    class SwichCaseTest : PhpDummy
    {
        public static void PhpMain()
        {
            int a = 2 * 3;
            switch (a)
            {
                case 1:
                case 2:
                case 3:
                    echo("Something wrong");
                    break;
                case 6:
                    echo("Everything is OK.");
                    break;
                default:
                    echo("Something really wrong");
                    break;

            }
        }
    }
}
