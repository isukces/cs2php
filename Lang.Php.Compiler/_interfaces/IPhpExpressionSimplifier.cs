namespace Lang.Php.Compiler 
{
    public interface IPhpExpressionSimplifier
    {
        IPhpValue Simplify(IPhpValue src);
    }
}
