using Lang.Cs.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public interface IPhpNodeTranslator<T> where T : IValue
    {

        IPhpValue TranslateToPhp(IExternalTranslationContext ctx, T src);
        int GetPriority();
    }
}
