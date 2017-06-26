using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpContinueStatement : IPhpStatementBase
    {
		#region Methods 

		// Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var s = style == null ? EmitStyleCompression.Beauty : style.Compression;
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
