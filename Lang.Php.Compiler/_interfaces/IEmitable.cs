using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public interface IEmitable
    {
        void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style);
    }
}
