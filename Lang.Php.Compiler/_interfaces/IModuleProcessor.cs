using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{
    public interface IModuleProcessor
    {
        void BeforeEmit(PhpCodeModule module, TranslationInfo info);
    }
}
