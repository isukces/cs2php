using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator
{
    public class PregTranslator : IPhpNodeTranslator<ClassPropertyAccessExpression>,
        IPhpNodeTranslator<MethodExpression>
       

    {
        // ReSharper disable once NoParameterNullCheckForPublicFunctions
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, ClassPropertyAccessExpression src)
        {
            if (src.Member.DeclaringType != typeof(PhpDummy)) return null;
            if (src.Member.Name.StartsWith("Is"))
            {
                return null;
            }
            else
            {
                return null;
                return new PhpConstValue("1");
            }
                //throw new NotSupportedException(string.Format("Do not use PregMatchResult class properties. For testing purposes use Is{0}", src.Member.Name));
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, MethodExpression src)
        {
            return null;
        }

        public int GetPriority()
        {
            return -100;
        }
    }
}
