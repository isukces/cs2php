using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpWhileStatement : PhpStatementBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="condition"></param>
        ///     <param name="statement"></param>
        /// </summary>
        public PhpWhileStatement(IPhpValue condition, IPhpStatement statement)
        {
            Condition = condition;
            Statement = statement;
        }

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            style      = style ?? new PhpEmitStyle();
            var header = string.Format("while({0})", Condition.GetPhpCode(style));
            EmitHeaderStatement(emiter, writer, style, header, Statement);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return GetCodeRequests(Condition, Statement);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var newCondition = s.Simplify(Condition);
            var newStatement = s.Simplify(Statement);
            if (newCondition == Condition && newStatement == Statement)
                return this;
            return new PhpWhileStatement(newCondition, newStatement);
        }

        /// <summary>
        /// </summary>
        public IPhpValue Condition { get; set; }

        /// <summary>
        /// </summary>
        public IPhpStatement Statement { get; set; }
    }
}