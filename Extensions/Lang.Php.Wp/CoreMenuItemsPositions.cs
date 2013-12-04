using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Wp
{
    [Skip]
    public class CoreMenuItemsPositions
    {
        [AsValue]
        public const int Dashboard = 2;
        [AsValue]
        public const int Separator_Before_Posts = 4;
        [AsValue]
        public const int Posts = 5;
        [AsValue]
        public const int Media = 10;
        [AsValue]
        public const int Links = 15;
        [AsValue]
        public const int Pages = 20;
        [AsValue]
        public const int Comments = 25;
        [AsValue]
        public const int Separator_Before_Appearance = 59;
        [AsValue]
        public const int Appearance = 60;
        [AsValue]
        public const int Plugins = 65;
        [AsValue]
        public const int Users = 70;
        [AsValue]
        public const int Tools = 75;
        [AsValue]
        public const int Settings = 80;
        [AsValue]
        public const int Separator_Last = 99;
    }
}
