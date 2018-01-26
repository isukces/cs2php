using System;
using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{

    public class PhpAssignExpression : PhpValueBase
    {
        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = PhpStatementBase.GetCodeRequests(Left, Right).ToArray();
            return a;
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (style == null || style.Compression == EmitStyleCompression.Beauty)
                return string.Format("{0} {1}= {2}", Left.GetPhpCode(style), _optionalOperator, Right.GetPhpCode(style));
            return string.Format("{0}{1}={2}", Left.GetPhpCode(style), _optionalOperator, Right.GetPhpCode(style));
        }

        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            var right = StripBracketsAndSimplify(Right, s);
            var left = s.Simplify(Left);
            if (left is PhpPropertyAccessExpression)
            {
                var e = left as PhpPropertyAccessExpression;
                var a = e.MakeSetValueExpression(right);
                if (a is PhpAssignExpression && (a as PhpAssignExpression).Left is PhpPropertyAccessExpression)
                    if (EqualCode(a, this))
                        return this;
                return a;

            }
            if (EqualCode(left, Left) && EqualCode(right, Right))
                return this;
            return new PhpAssignExpression(left, right, _optionalOperator);
        }


        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="optionalOperator"></param>
        /// </summary>
        public PhpAssignExpression(IPhpValue left, IPhpValue right, string optionalOperator)
        {
            Left = left;
            Right = right;
            OptionalOperator = optionalOperator;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// </summary>
        public PhpAssignExpression(IPhpValue left, IPhpValue right)
        {
            Left = left;
            Right = right;
        }


        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Left { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Right { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OptionalOperator
        {
            get => _optionalOperator;
            set => _optionalOperator = (value ?? string.Empty).Trim();
        }
        private string _optionalOperator = string.Empty;
    }
}
