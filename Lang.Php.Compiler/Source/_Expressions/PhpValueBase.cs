using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Lang.Php.Compiler.Source
{
    public abstract class PhpValueBase : PhpSourceBase, IPhpValue
    {
        #region Methods

        // Public Methods 

        public abstract IEnumerable<ICodeRequest> GetCodeRequests();

        public abstract string GetPhpCode(PhpEmitStyle style);

        public virtual IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            return this;
        }

        public override string ToString()
        {
            return GetPhpCode(null);
        }
        // Protected Methods 

        protected IPhpValue SimplifyForFieldAcces(IPhpValue src, IPhpExpressionSimplifier s)
        {
            src = s.Simplify(src);
            if (!(src is PhpParenthesizedExpression)) return src;
            var inside = (src as PhpParenthesizedExpression).Expression;
            if (inside is PhpVariableExpression)
                return inside;
            if (inside is PhpMethodCallExpression)
                return (inside as PhpMethodCallExpression).IsConstructorCall ? src : inside;
            if (inside is PhpBinaryOperatorExpression || inside is PhpConditionalExpression)
                return src;
            throw new NotSupportedException();
        }

        protected IPhpValue StripBracketsAndSimplify(IPhpValue value, IPhpExpressionSimplifier s)
        {
            value = PhpParenthesizedExpression.Strip(value);
            value = s.Simplify(value);
            return value;
        }

        #endregion Methods
    }
}
