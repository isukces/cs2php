using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpIncrementDecrementExpression : PhpValueBase
    {
        // Public Methods 

        public override string GetPhpCode(PhpEmitStyle style)
        {
            var o = Increment ? "++" : "--";
            return string.Format("{0}{1}{2}",
                Pre ? o : "",
                Operand.GetPhpCode(style),
                Pre ? "" : o);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return PhpStatementBase.GetCodeRequests(Operand);
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="operand">,</param>
        /// <param name="increment"></param>
        /// <param name="pre"></param>
        /// </summary>
        public PhpIncrementDecrementExpression(IPhpValue operand, bool increment, bool pre)
        {
            Operand = operand;
            Increment = increment;
            Pre = pre;
        }


        /// <summary>
        /// ,
        /// </summary>
        public IPhpValue Operand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Increment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Pre { get; set; }
    }
}
