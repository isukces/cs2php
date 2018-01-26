using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpReturnStatement : PhpStatementBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="returnValue"></param>
        /// </summary>
        public PhpReturnStatement(IPhpValue returnValue)
        {
            ReturnValue = returnValue;
        }
        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            if (ReturnValue == null)
                writer.WriteLn("return;");
            else
                writer.WriteLnF("return {0};", ReturnValue.GetPhpCode(style));
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return GetCodeRequests(ReturnValue);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            if (ReturnValue == null)
                return this;
            var newReturnValue = s.Simplify(ReturnValue);
            return ReturnValue == newReturnValue ? this : new PhpReturnStatement(newReturnValue);
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue ReturnValue { get; }
    }
}