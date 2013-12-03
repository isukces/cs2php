using Lang.Cs.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Php.Compiler.Source;
using Lang.Php;

namespace Lang.Php.Compiler.Translator.Node
{
    public class PhpDirectoryEntryTranslator : IPhpNodeTranslator<CallConstructor>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CallConstructor src)
        {
            if (src.Info.DeclaringType == typeof(PhpDirectoryEntry))
            {
                var a = ctx.TranslateValue(src.Arguments[0].MyValue);
                return a;
            }
            return null;
        }

        public int getPriority()
        {
            return 1;
        }
    }
}
