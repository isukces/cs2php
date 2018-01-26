using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpUnaryOperatorExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="operand"></param>
        ///     <param name="_operator"></param>
        /// </summary>
        public PhpUnaryOperatorExpression(IPhpValue operand, string _operator)
        {
            Operand  = operand;
            Operator = _operator;
        }
        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return Operand == null ? new ICodeRequest[0] : Operand.GetCodeRequests();
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}{1}", Operator, Operand.GetPhpCode(style));
        }


        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Operand { get; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string Operator { get; } = string.Empty;
    }
}