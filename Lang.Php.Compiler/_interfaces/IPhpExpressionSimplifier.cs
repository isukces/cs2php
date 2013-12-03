using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler 
{
    public interface IPhpExpressionSimplifier
    {
        IPhpValue Simplify(IPhpValue src);
    }
}
