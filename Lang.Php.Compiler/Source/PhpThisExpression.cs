using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{
    public class PhpThisExpression : IPhpValueBase
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
