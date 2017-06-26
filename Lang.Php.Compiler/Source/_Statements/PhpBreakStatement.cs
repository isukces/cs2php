using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpBreakStatement : IPhpStatementBase
    {
        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            writer.WriteLn("break;");
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }
    }
}
