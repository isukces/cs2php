namespace Lang.Php.Compiler
{
    public interface IEmitable
    {
        void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style);
    }
}
