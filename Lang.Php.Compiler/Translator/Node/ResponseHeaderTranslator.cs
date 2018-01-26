using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Linq;
using System.Reflection;

namespace Lang.Php.Compiler.Translator.Node
{
    public class ResponseHeaderTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType != typeof(ResponseHeader)) return null;
            var fn = src.MethodInfo.ToString();
            switch (fn)
            {
                case "Void Expires(System.DateTime)":
                    return MkHeader(ctx, "Expires", _PhpFormat(src.Arguments[0]));
                case "Void LastModified(System.DateTime)":
                    return MkHeader(ctx, "Last-Modified", _PhpFormat(src.Arguments[0]));
                case "Void ContentType(System.String, Boolean)":
                    return src.Arguments.Length == 2
                        ? MkHeader(ctx, "Last-Modified", src.Arguments[0], src.Arguments[1])
                        : MkHeader(ctx, "Last-Modified", src.Arguments[0]);
            }
            throw new NotImplementedException();
        }

        private static CsharpMethodCallExpression _PhpFormat(FunctionArgument functionArgument)
        {
            var a2 = new FunctionArgument("", new ConstValue(DateTimeFormats.HttpHeader), null);
            var methodInfo = typeof(DateTimeExtension)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Single(i => i.ToString() == "System.String PhpFormat(System.DateTime, Lang.Php.DateTimeFormats)");
            var dmc = new CsharpMethodCallExpression(methodInfo, null, new[] { functionArgument, a2 }, null, false);
            return dmc;
        }

        private static IPhpValue MkHeader(IExternalTranslationContext ctx, string key, IValue v, IValue replace = null)
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

        public int GetPriority()
        {
            return 1;
        }
    }
}
