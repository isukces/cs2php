using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpFreeExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="expression"></param>
        /// </summary>
        public PhpFreeExpression(string expression)
        {
            Expression = expression;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return _expression;
        }

        /// <summary>
        /// </summary>
        public string Expression
        {
            get => _expression;
            set => _expression = (value ?? string.Empty).Trim();
        }

        private string _expression = string.Empty;
    }
}