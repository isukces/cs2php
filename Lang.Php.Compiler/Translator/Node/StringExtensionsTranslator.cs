using Lang.Cs.Compiler;
using System.Linq;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator.Node
{
    public class StringExtensionsTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType != typeof (StringExtension)) return null;
            if (src.MethodInfo.Name != "PhpExplodeList") return null;
            var a = src.Arguments
                .Select(ctx.TranslateValue)
                .Cast<PhpMethodInvokeValue>()
                .ToArray();
                    
                 

            var list = new PhpMethodCallExpression("list");                                 
            list.Arguments.AddRange(a.Skip(2));
            foreach (var i in list.Arguments)
                i.ByRef = false;


            var explode = new PhpMethodCallExpression("explode");
            explode.Arguments.Add(a[1]);
            explode.Arguments.Add(a[0]);

            var aa = new PhpAssignExpression(list, explode);
            return aa;


            // throw new NotSupportedException();
            //  throw new NotSupportedException();
        }

        public int GetPriority()
        {
            return 2;
        }
    }
}
