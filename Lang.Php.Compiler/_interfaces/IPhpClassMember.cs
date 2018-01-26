namespace Lang.Php.Compiler
{
    public interface IPhpClassMember
    {
        Visibility Visibility { get; }
        bool IsStatic { get; }
    }
}
