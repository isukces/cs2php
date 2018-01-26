using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpMethodInvokeValue : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="Expression"></param>
        /// </summary>
        public PhpMethodInvokeValue(IPhpValue Expression)
        {
            this.Expression = Expression;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return Expression == null ? new ICodeRequest[0] : Expression.GetCodeRequests();
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (Expression == null)
                throw new Exception("Unable to get code from empty expression");
            var ex = PhpParenthesizedExpression.Strip(Expression);
            var a  = Expression.GetPhpCode(style);
            if (ByRef)
                a = "&" + a;
            return a;
        }

        /// <summary>
        /// </summary>
        public IPhpValue Expression { get; set; }

        /// <summary>
        /// </summary>
        public bool ByRef { get; set; }

        /// <summary>
        ///     Nazwa własności Expression;
        /// </summary>
        public const string PROPERTYNAME_EXPRESSION = "Expression";

        /// <summary>
        ///     Nazwa własności ByRef;
        /// </summary>
        public const string PROPERTYNAME_BYREF = "ByRef";
    }
}