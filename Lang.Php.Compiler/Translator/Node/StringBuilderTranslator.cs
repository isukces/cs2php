using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    public class StringBuilderTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>,
        IPhpNodeTranslator<CallConstructor>,
        IPhpNodeTranslator<CsharpInstancePropertyAccessExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType == typeof(StringBuilder))
            {
                var fn = src.MethodInfo.ToString();
                if (fn == "System.Text.StringBuilder Append(System.String)")
                {
                    var sb = ctx.TranslateValue(src.TargetObject);
                    var arg = ctx.TranslateValue(src.Arguments[0].MyValue);
                    return new PhpAssignExpression(sb, arg, ".");
                }
                if (fn == "System.Text.StringBuilder AppendLine(System.String)")
                {
                    var sb = ctx.TranslateValue(src.TargetObject);
                    var arg = ctx.TranslateValue(src.Arguments[0].MyValue);
                    var eol = new PhpDefinedConstExpression("PHP_EOL", null);
                    var arg_eol = PhpBinaryOperatorExpression.ConcatStrings(arg, eol);
                    return new PhpAssignExpression(sb, arg_eol, ".");
                }
                if (fn == "System.String ToString()")
                    return ctx.TranslateValue(src.TargetObject);
                Console.WriteLine(fn);
                throw new NotSupportedException();
            }
            return null;
        }

        public int getPriority()
        {
            return 1;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CallConstructor src)
        {
            if (src.Info.DeclaringType == typeof(StringBuilder))
            {
                return new PhpConstValue("");
            }
            return null;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpInstancePropertyAccessExpression src)
        {
            if (src.Member.DeclaringType == typeof(StringBuilder))
            {
                var n = src.Member.Name;
                if (n == "Length")
                {
                    var sb = ctx.TranslateValue(src.TargetObject);
                    var m = new PhpMethodCallExpression("mb_strlen", sb);
                    return m;
                }
                throw new NotSupportedException();
            }
            return null;
        }
    }
}
