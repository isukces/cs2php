using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Examples.BasicFeaturesExample.SomeFolder
{

    [Module("SomeFolder/ClassInSubfolder")]
    public class ClassInSubfolder
    {
        public static void Hello()
        {
            PhpDummy.echo("ClassInSubfolder hello");
        }
    }
}
