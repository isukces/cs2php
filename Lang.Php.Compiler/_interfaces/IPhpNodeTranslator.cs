using Lang.Cs.Compiler;

namespace Lang.Php.Compiler
{
    public interface IPhpNodeTranslator<T> where T : IValue
    {

        IPhpValue TranslateToPhp(IExternalTranslationContext ctx, T src);
        int GetPriority();
    }
}
