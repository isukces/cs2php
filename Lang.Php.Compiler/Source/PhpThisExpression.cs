using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpThisExpression : PhpValueBase
    {
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return "$this";
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }
    }
}
