using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public interface IModuleProcessor
    {
        void BeforeEmit(PhpCodeModule module, TranslationInfo info);
    }
}
