using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpArrayAccessExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="phpArray"></param>
        ///     <param name="index"></param>
        /// </summary>
        public PhpArrayAccessExpression(IPhpValue phpArray, IPhpValue index)
        {
            PhpArray = phpArray;
            Index = index;
        }


        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return PhpStatementBase.GetCodeRequests(PhpArray, Index);
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}[{1}]", PhpArray.GetPhpCode(style), Index.GetPhpCode(style));
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue PhpArray { get; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Index { get; }
    }
}