using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lang.Php.Compiler.Translator.Node
{
    public class GraphTranslator : IPhpNodeTranslator<ClassFieldAccessExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, ClassFieldAccessExpression src)
        {
            if (src.Member.DeclaringType == typeof(Lang.Php.Graph.Font))
            {
                var name = src.Member.Name;
                if (name =="Font1" || name =="Font2" || name =="Font3" || name =="Font4" || name =="Font5" ) {
                    var size = int.Parse(name.Substring(4));
                    return new PhpConstValue(size);
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
