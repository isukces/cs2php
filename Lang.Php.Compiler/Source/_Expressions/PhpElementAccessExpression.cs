using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{
    public class PhpElementAccessExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="expression"></param>
        ///     <param name="arguments"></param>
        /// </summary>
        public PhpElementAccessExpression(IPhpValue expression, IPhpValue[] arguments)
        {
            Expression = expression;
            Arguments  = arguments;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = PhpStatementBase.GetCodeRequests<IPhpValue>(Arguments);
            var b = PhpStatementBase.GetCodeRequests(Expression);
            return a.Union(b).ToArray();
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            var a = Arguments.Select(u => u.GetPhpCode(style));
            return string.Format("{0}[{1}]", Expression.GetPhpCode(style), string.Join(",", a));
        }

        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            var expression = SimplifyForFieldAcces(Expression, s);
            if (Arguments == null || Arguments.Length == 0)
            {
                if (EqualCode(expression, Expression))
                    return this;
                return new PhpElementAccessExpression(expression, null);
            }

            var arguments = Arguments.Select(i => StripBracketsAndSimplify(i, s)).ToArray();
            var candidate = new PhpElementAccessExpression(expression, arguments);
            if (EqualCode(candidate, this))
                return this;
            return candidate;
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Expression { get; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue[] Arguments { get; }
    }
}