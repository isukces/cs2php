using System;
using System.Collections.Generic;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator
{
    public class PregTranslator : IPhpNodeTranslator<ClassPropertyAccessExpression>,
        IPhpNodeTranslator<MethodExpression>,
        IPhpNodeTranslator<CsharpMethodCallExpression>
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

        static PhpMethodCallExpression Make(IExternalTranslationContext ctx, CsharpMethodCallExpression src, bool addoffset)
        {
            var p = new List<IPhpValue>(src.Arguments.Length + 1)
            {
                ctx.TranslateValue(src.Arguments[0].MyValue),
                ctx.TranslateValue(src.Arguments[1].MyValue)
            };


            for (int i = 2; i < src.Arguments.Length; i++)
            {
                p.Add(ctx.TranslateValue(src.Arguments[i].MyValue));
                if (addoffset && i == 2)
                    p.Add(new PhpDefinedConstExpression("PREG_OFFSET_CAPTURE", null));

            }
            var a = new PhpMethodCallExpression("preg_match", p.ToArray());
            return a;

        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType != typeof(Preg)) return null;
            var mn = src.MethodInfo.ToString();
            Console.WriteLine(mn);
            switch (mn)
            {
                case "Lang.Php.PregMatchResult MatchWithOffset(System.String, System.String, System.Collections.Generic.Dictionary`2[System.Object,Lang.Php.PregMatchWithOffset] ByRef, Int32)":
                    return Make(ctx, src, true);

                case "Lang.Php.PregMatchResult Match(System.String, System.String, System.Collections.Generic.Dictionary`2[System.Object,System.String] ByRef, Int32)":
                // case "Lang.Php.PregMatchResult Match(System.String, System.String, Int32)":
                case "Lang.Php.PregMatchResult Match(System.String, System.String)":
                    return Make(ctx, src, false);
                default:
                    return null;
            }
        }

        public int GetPriority()
        {
            return -100;
        }
    }
}
