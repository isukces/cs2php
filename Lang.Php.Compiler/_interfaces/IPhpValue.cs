namespace Lang.Php.Compiler
{
    public interface IPhpValue : ICodeRelated
    {
        string GetPhpCode(PhpEmitStyle style);
    }
}
