using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public interface IClassMember
    {
        Visibility Visibility { get; }
        bool IsStatic { get; }
    }
}
