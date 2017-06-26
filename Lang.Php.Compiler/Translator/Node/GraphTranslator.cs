using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using Lang.Php.Graph;


namespace Lang.Php.Compiler.Translator.Node
{
    public class GraphTranslator : IPhpNodeTranslator<ClassFieldAccessExpression>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, ClassFieldAccessExpression src)
        {
            if (src.Member.DeclaringType == typeof(Font))
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

        public int GetPriority()
        {
            return 1;
        }
    }
}
