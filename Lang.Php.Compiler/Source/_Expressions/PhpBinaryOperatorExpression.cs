using System;
using System.Collections.Generic;
using Lang.Php.Compiler.Translator;

namespace Lang.Php.Compiler.Source
{
    public class PhpBinaryOperatorExpression : PhpValueBase
    {

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (style == null || style.Compression == EmitStyleCompression.Beauty)
                return string.Format("{0} {1} {2}", Left.GetPhpCode(style), Operator, Right.GetPhpCode(style));
            return string.Format("{0}{1}{2}", Left.GetPhpCode(style), Operator, Right.GetPhpCode(style));
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return PhpStatementBase.GetCodeRequests(Left, Right);
        }
   

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IPhpValue ConcatStrings(params IPhpValue[] items)
        {
            if (items == null) return null;
            IPhpValue result = null;
            foreach (var i in items)
            {
                if (result == null)
                    result = i;
                else
                    result = new PhpBinaryOperatorExpression(".", result, i);
            }
            if (result != null)
            {
                var simplifier = new ExpressionSimplifier(new OptimizeOptions());
                result = simplifier.Simplify(result);
            }
            return result;
        }


        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="Operator"></param>
        /// </summary>
        public PhpBinaryOperatorExpression(string Operator, IPhpValue left, IPhpValue right)
        {
            if (Left == null) throw new ArgumentNullException(nameof(left));
            if (Right == null) throw new ArgumentNullException(nameof(right));
            Left = left;
            Right = right;
            this.Operator = Operator;
        }

        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Left { get; }

        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Right { get; }

        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string Operator { get; } = string.Empty;
    }
}
