using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    public class ResponseHeaderTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType == typeof(ResponseHeader))
            {
                var fn = src.MethodInfo.ToString();
                if (fn == "Void Expires(System.DateTime)")
                    return mkHeader(ctx, "Expires", _PhpFormat(src.Arguments[0]));
                if (fn == "Void LastModified(System.DateTime)")
                    return mkHeader(ctx, "Last-Modified", _PhpFormat(src.Arguments[0]));
                if (fn == "Void ContentType(System.String, Boolean)")
                {
                    if (src.Arguments.Length == 2)
                        return mkHeader(ctx, "Last-Modified", src.Arguments[0], src.Arguments[1]);
                    return mkHeader(ctx, "Last-Modified", src.Arguments[0]);
                }
                throw new NotImplementedException();
            }
            return null;
        }

        private static CsharpMethodCallExpression _PhpFormat(FunctionArgument a1)
        {
            var a2 = new FunctionArgument("", new ConstValue(DateTimeFormats.HttpHeader));
            var m = typeof(DateTimeExtension)
                .GetMethods(System.Reflection.BindingFlags.Static | BindingFlags.Public)
                .Where(i => i.ToString() == "System.String PhpFormat(System.DateTime, Lang.Php.DateTimeFormats)")
                .Single();
            var dmc = new CsharpMethodCallExpression(m, null, new FunctionArgument[] { a1, a2 }, null, false);
            return dmc;
        }

        public IPhpValue mkHeader(IExternalTranslationContext ctx, string key, IValue v, IValue replace = null)
        {
            if (v is FunctionArgument)
                v = (v as FunctionArgument).MyValue;
            var a1 = new PhpConstValue(key + ": ");
            var a2 = ctx.TranslateValue(v);
            var concat = new PhpBinaryOperatorExpression(".", a1, a2);
            PhpMethodCallExpression phpm;
            if (replace != null)
            {
                var a3 = ctx.TranslateValue(replace);
                phpm = new PhpMethodCallExpression("header", concat, a3);
            }
            else
                phpm = new PhpMethodCallExpression("header", concat);
            return phpm;
        }

        public int getPriority()
        {
            return 1;
        }
    }
}
