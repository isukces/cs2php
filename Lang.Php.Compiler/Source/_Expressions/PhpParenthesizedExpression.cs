using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpParenthesizedExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="Expression"></param>
        /// </summary>
        public PhpParenthesizedExpression(IPhpValue Expression)
        {
            this.Expression = Expression;
        }
        // Public Methods 

        public static IPhpValue Strip(IPhpValue x)
        {
            if (x is PhpParenthesizedExpression)
                return Strip((x as PhpParenthesizedExpression).Expression);
            return x;
        }

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return Expression == null ? new ICodeRequest[0] : Expression.GetCodeRequests();
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("({0})", Expression.GetPhpCode(style));
        }


        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Expression { get; }
    }
}