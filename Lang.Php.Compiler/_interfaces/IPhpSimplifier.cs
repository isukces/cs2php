namespace Lang.Php.Compiler 
{
    public interface IPhpSimplifier:IPhpExpressionSimplifier
    {
     
        IPhpStatement Simplify(IPhpStatement src);
    }
}
