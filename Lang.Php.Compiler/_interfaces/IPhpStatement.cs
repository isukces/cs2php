// ReSharper disable once CheckNamespace
namespace Lang.Php.Compiler
{
    public interface IPhpStatement : ICodeRelated, IEmitable
    {
        StatementEmitInfo GetStatementEmitInfo(PhpEmitStyle style);
    }
}
