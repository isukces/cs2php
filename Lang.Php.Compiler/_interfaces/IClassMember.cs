namespace Lang.Php.Compiler
{
    public interface IClassMember
    {
        Visibility Visibility { get; }
        bool IsStatic { get; }
    }
}
