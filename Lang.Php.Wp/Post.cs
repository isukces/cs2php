using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Wp
{
    public class Post
    {
        [GlobalVariable("$post")]
        public static Post Current;

        public int ID;
    }
}
