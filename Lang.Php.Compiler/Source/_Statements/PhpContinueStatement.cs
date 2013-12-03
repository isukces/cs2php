using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{
    public class PhpContinueStatement : IPhpStatementBase
    {
		#region Methods 

		// Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            EmitStyleCompression s = style == null ? EmitStyleCompression.Beauty : style.Compression;
            if (s == EmitStyleCompression.NearCrypto)
                writer.Write("continue;");
            else
                writer.WriteLn("continue;");
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }

		#endregion Methods 
    }
}
