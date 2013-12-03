using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{
    public abstract class IPhpValueBase : PhpSourceBase, IPhpValue, ICodeRelated
    {
        protected IPhpValue StripBracketsAndSimplify(IPhpValue v, IPhpExpressionSimplifier s)
        {
            while (v is PhpParenthesizedExpression)
                v = (v as PhpParenthesizedExpression).Expression;
            v = s.Simplify(v);
            return v;
        }

        protected IPhpValue SimplifyForFieldAcces(IPhpValue src, IPhpExpressionSimplifier s)
        {
            src = s.Simplify(src);
            if (src is PhpParenthesizedExpression)
            {
                var inside = (src as PhpParenthesizedExpression).Expression;
                if (inside is PhpVariableExpression)
                    return inside;
                if (inside is PhpMethodCallExpression)
                {
                    if ((inside as PhpMethodCallExpression).IsConstructorCall)
                        return src;
                    return inside;
                }
                if (inside is PhpBinaryOperatorExpression || inside is PhpConditionalExpression)
                    return src;
                throw new NotSupportedException();
                return src;
            }
            return src;
        }


        public virtual IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            return this;
        }

        public abstract string GetPhpCode(PhpEmitStyle style);
        public override string ToString()
        {
            return GetPhpCode(null);
        }

        public abstract IEnumerable<ICodeRequest> GetCodeRequests();
    }
}
