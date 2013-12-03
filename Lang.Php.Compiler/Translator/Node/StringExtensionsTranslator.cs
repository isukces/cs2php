using Lang.Cs.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;
using Lang.Php;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator.Node
{
    public class StringExtensionsTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType == typeof(StringExtension))
            {
                if (src.MethodInfo.Name == "PhpExplodeList")
                {                    
                    var a = src.Arguments
                        .Select(i => ctx.TranslateValue(i))
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


                    throw new NotSupportedException();

                }
              //  throw new NotSupportedException();
            }
            return null;


        }

        public int getPriority()
        {
            return 2;
        }
    }
}
