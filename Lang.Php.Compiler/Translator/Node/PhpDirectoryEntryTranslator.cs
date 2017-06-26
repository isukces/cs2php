using Lang.Cs.Compiler;

namespace Lang.Php.Compiler.Translator.Node
{
    public class PhpDirectoryEntryTranslator : IPhpNodeTranslator<CallConstructor>
    {
        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CallConstructor src)
        {
            if (src.Info.DeclaringType == typeof(PhpDirectoryEntry))
            {
                var a = ctx.TranslateValue(src.Arguments[0].MyValue);
                return a;
            }
            return null;
        }

        public int GetPriority()
        {
            return 1;
        }
    }
}
