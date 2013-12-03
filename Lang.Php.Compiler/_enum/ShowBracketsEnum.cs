using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler 
{
    public enum  ShowBracketsEnum
    {
        Always,
        IfManyItems,
        IfManyItems_OR_IfStatement,
        Never
    }
}
