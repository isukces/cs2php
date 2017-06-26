using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Translator.Node
{
    public class BasicTranslator_Methods : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var dt = src.MethodInfo.DeclaringType;
            if (dt.IsGenericType)
                dt = dt.GetGenericTypeDefinition();
            if (dt == typeof(Stack<>))
            {
                var fn = src.MethodInfo.ToString();
                if (fn == "System.String Peek()")
                {
                    var to = ctx.TranslateValue(src.TargetObject);
                    var cnt = new PhpMethodCallExpression("count", to);
                    var cnt_1 = new PhpBinaryOperatorExpression("-", cnt, new PhpConstValue(1));
                    var ar = new PhpArrayAccessExpression(to, cnt_1);
                    return ar;
                }
                return null;
            }
            return null;
        }

        public int GetPriority()
        {
            return 2;
        }
    }
}
