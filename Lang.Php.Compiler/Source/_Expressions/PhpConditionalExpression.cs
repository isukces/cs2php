using System.Collections.Generic;
using System.Diagnostics;

namespace Lang.Php.Compiler.Source
{
    public class PhpConditionalExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="condition"></param>
        ///     <param name="whenTrue"></param>
        ///     <param name="whenFalse"></param>
        /// </summary>
        public PhpConditionalExpression(IPhpValue condition, IPhpValue whenTrue, IPhpValue whenFalse)
        {
            Condition = condition;
            WhenTrue  = whenTrue;
            WhenFalse = whenFalse;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return PhpStatementBase.GetCodeRequests(Condition, WhenTrue, WhenFalse);
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            var form = style == null || style.Compression == EmitStyleCompression.Beauty
                ? "{0} ? {1} : {2}"
                : "{0}?{1}:{2}";
            return string.Format(form, Condition.GetPhpCode(style), WhenTrue.GetPhpCode(style),
                WhenFalse.GetPhpCode(style));
        }

        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            Debug.Assert(Condition != null, "Condition != null");
            var condition = SimplifyForFieldAcces(Condition, s);
            var whenTrue  = SimplifyForFieldAcces(WhenTrue,  s);
            var whenFalse = SimplifyForFieldAcces(WhenFalse, s);
            var newNode   = new PhpConditionalExpression(condition, whenTrue, whenFalse);
            return EqualCode(this, newNode) ? this : newNode;
        }

        /// <summary>
        /// </summary>
        public IPhpValue Condition { get; set; }

        /// <summary>
        /// </summary>
        public IPhpValue WhenTrue { get; set; }

        /// <summary>
        /// </summary>
        public IPhpValue WhenFalse { get; set; }
    }
}