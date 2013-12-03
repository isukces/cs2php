using Lang.Cs.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node.Linq
{
    class EnumerableTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType == typeof(System.Linq.Enumerable))
            {
                string fn = src.MethodInfo.ToString();
                if (fn == "System.Collections.Generic.List`1[System.String] ToList[String](System.Collections.Generic.IEnumerable`1[System.String])")
                {
                    var v = ctx.TranslateValue(src.Arguments[0].MyValue);
                    return v; // po prostu argument
                }
                if (fn == "System.Linq.IOrderedEnumerable`1[System.Collections.Generic.IEnumerable`1[System.String]] OrderBy[IEnumerable`1,Func`2](System.Collections.Generic.IEnumerable`1[System.Collections.Generic.IEnumerable`1[System.String]], System.Func`2[System.Collections.Generic.IEnumerable`1[System.String],System.Func`2[System.String,System.String]])")
                {
                    var v = ctx.TranslateValue(src.Arguments[1].MyValue);
                    // var vv = new Lang.Php.ph
                    return v; // po prostu argument
                }
                throw new NotImplementedException();
            }
            return null;
        }

        public int getPriority()
        {
            return 1;
        }
    }
}
