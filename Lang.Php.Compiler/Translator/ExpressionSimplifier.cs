using System.Web;
using Lang.Php.Compiler.Source;
using Lang.Php.Runtime;
using System;
using System.Linq;

namespace Lang.Php.Compiler.Translator
{
    public class ExpressionSimplifier : PhpBaseVisitor<IPhpValue>, IPhpExpressionSimplifier
    {
        protected override IPhpValue VisitPhpIncrementDecrementExpression(PhpIncrementDecrementExpression node)
        {
            return node.Simplify(this);
        }
        public ExpressionSimplifier(OptimizeOptions op)
        {
            this.op = op;
        }
        OptimizeOptions op;

        #region Methods

        // Protected Methods 

        protected override IPhpValue VisitPhpArrayAccessExpression(PhpArrayAccessExpression node)
        {
            return node;
        }

        protected override IPhpValue VisitPhpArrayCreateExpression(PhpArrayCreateExpression node)
        {
            return node;
        }

        protected override IPhpValue VisitPhpAssignExpression(PhpAssignExpression node)
        {
            return node.Simplify(this);
        }

        public static IPhpValue[] ExplodeConcats(IPhpValue x, string op)
        {
            if (x is PhpMethodInvokeValue)
                x = (x as PhpMethodInvokeValue).Expression;
            if (x is PhpBinaryOperatorExpression)
            {
                var y = x as PhpBinaryOperatorExpression;
                if (y.Operator == op)
                    return ExplodeConcats(y.Left, op).Union(ExplodeConcats(y.Right, op)).ToArray();
            }
            return new IPhpValue[] { x };
        }

        static bool SameCode(IPhpValue a, IPhpValue b)
        {
            var codeA = a == null ? "" : a.GetPhpCode(null);
            var codeB = a == null ? "" : b.GetPhpCode(null);
            return codeA == codeB;
        }

        static IPhpValue ReturnSubst(IPhpValue old, IPhpValue @new)
        {
            if (SameCode(old, @new))
                return old;
            return @new;
        }



        protected override IPhpValue VisitPhpBinaryOperatorExpression(PhpBinaryOperatorExpression node)
        {
            switch (node.Operator)
            {
                case ".":
                    {
                        var _left = Simplify(node.Left);
                        var _right = Simplify(node.Right);
                        var n = new PhpBinaryOperatorExpression(node.Operator, _left, _right);
                        var c = ExplodeConcats(n, ".").ToList();



                        for (var i = 1; i < c.Count; i++)
                        {
                            var L = c[i - 1];
                            var R = c[i];
                            if (L is PhpConstValue && R is PhpConstValue)
                            {
                                var LValue = (L as PhpConstValue).Value;
                                var RValue = (R as PhpConstValue).Value;
                                if (LValue is string && RValue is string)
                                {
                                    c[i - 1] = new PhpConstValue((string)LValue + (string)RValue);
                                    c.RemoveAt(i);
                                    i--;
                                    continue;
                                }
                                var LCode = PhpValues.ToPhpCodeValue(LValue);
                                var RCode = PhpValues.ToPhpCodeValue(RValue);
                                string left, right;
                                if (LCode.TryGetPhpString(out left) && RCode.TryGetPhpString(out right))
                                {
                                    c[i - 1] = new PhpConstValue(left + right);
                                    c.RemoveAt(i);
                                    i--;
                                    continue;
                                }

                                var msg = string.Format("left={0}, right={1} '{2}+{3}'", LValue, RValue, LValue == null ? null : LValue.GetType().FullName, RValue == null ? null : RValue.GetType().FullName);

#if DEBUG
                                throw new NotImplementedException(msg);
#else
                        Console.WriteLine(msg);
                        Console.WriteLine(L.GetPhpCode(null));
                        Console.WriteLine(R.GetPhpCode(null));
                        continue;
#endif
                            }
                        }
                        var result = c[0];
                        if (c.Count > 1)
                            foreach (var x2 in c.Skip(1))
                                result = new PhpBinaryOperatorExpression(".", result, x2);
                        return ReturnSubst(node, result);
                    }
                case "|":
                    {
                        //                    var aLeft = node.Left as PhpConstValue;
                        //                    var aRight = node.Right as PhpConstValue;
                        //                    if (aLeft != null && aRight != null && aLeft.Value!=null && aRight.Value!=null)
                        //                    {
                        //                        var typeLeft = aLeft.Value.GetType();
                        //                        var typeRight = aRight.Value.GetType();
                        //                        if (typeLeft.IsEnum && typeLeft == typeRight)
                        //                        {
                        //                            var leftValue = (int)aLeft.Value;
                        //                            var rightValue = (int)aRight.Value;
                        //                            var c = leftValue | rightValue;
                        //                            var dd = PhpCodeValue.FromInt(c, true);
                        //                            var d = new PhpConstValue(c, false);
                        //                            return d;
                        //                        }
                        //                    }
                    }
                    break;
            }
            return node;
        }

        protected override IPhpValue VisitPhpClassFieldAccessExpression(PhpClassFieldAccessExpression node)
        {
            return node;
        }

        protected override IPhpValue VisitPhpConstValue(PhpConstValue node)
        {
            return node;
        }

        protected override IPhpValue VisitPhpDefinedConstExpression(PhpDefinedConstExpression node)
        {
            return node;
        }

        protected override IPhpValue VisitPhpFreeExpression(PhpFreeExpression node)
        {
            return node;
        }

        protected override IPhpValue VisitPhpInstanceFieldAccessExpression(PhpInstanceFieldAccessExpression node)
        {
            return node;
        }

        protected override IPhpValue VisitPhpMethodCallExpression(PhpMethodCallExpression node)
        {
            if (node.Name == "_urlencode_" || node.Name == "_htmlspecialchars_")
            {
                if (node.Arguments[0].Expression is PhpConstValue)
                {
                    var cv = (node.Arguments[0].Expression as PhpConstValue).Value;
                    if (cv == null)
                        return Simplify(node.Arguments[0].Expression);
                    if (cv is int)
                        cv = cv.ToString();
                    else if (cv is string)
                    {
                        if (node.Name == "_urlencode_")
                            cv = HttpUtility.UrlEncode(cv as string);
                        else
                            cv = HttpUtility.HtmlEncode(cv as string);
                    }
                    else
                        throw new NotSupportedException();
                    return Simplify(new PhpConstValue(cv));
                }
            }
            {
                var list1 = node.Arguments.Select(Simplify).Cast<PhpMethodInvokeValue>().ToList();
                var to = node.TargetObject == null ? null : Simplify(node.TargetObject);
                if (PhpSourceBase.EqualCode_List(list1, node.Arguments) && PhpSourceBase.EqualCode(to, node.TargetObject))
                    return node;
                var xx = new PhpMethodCallExpression(node.Name)
                {
                    Arguments = list1,
                    DontIncludeClass = node.DontIncludeClass,
                    TargetObject = to
                };
                xx.SetClassName(node.ClassName, node.TranslationInfo);
                return xx;
            }
            return node;
        }


        IPhpValue strip(IPhpValue v)
        {
            while (v is PhpParenthesizedExpression)
                v = (v as PhpParenthesizedExpression).Expression;
            v = Simplify(v);
            return v;
        }
        protected override IPhpValue VisitPhpMethodInvokeValue(PhpMethodInvokeValue node)
        {
            var nv = strip(node.Expression);

            if (PhpSourceBase.EqualCode(nv, node.Expression))
                return node;
            return new PhpMethodInvokeValue(nv) { ByRef = node.ByRef };
        }

        protected override IPhpValue VisitPhpUnaryOperatorExpression(PhpUnaryOperatorExpression node)
        {
            if (node.Operator == "!" && node.Operand is PhpBinaryOperatorExpression)
            {
                var bin = node.Operand as PhpBinaryOperatorExpression;
                if (bin.Operator == "!==")
                {
                    var bin2 = new PhpBinaryOperatorExpression("===", bin.Left, bin.Right);
                    return bin2;
                }
                var be = new PhpParenthesizedExpression(node.Operand);
                node = new PhpUnaryOperatorExpression(be, node.Operator);
                return node;
            }
            if (node.Operator == "!" && node.Operand is PhpUnaryOperatorExpression)
            {
                var bin = node.Operand as PhpUnaryOperatorExpression;
                if (bin.Operator == "!")
                    return Simplify(bin.Operand);
            }
            return node;
        }
        protected override IPhpValue VisitPhpElementAccessExpression(PhpElementAccessExpression node)
        {
            return node.Simplify(this);
        }
        protected override IPhpValue VisitPhpConditionalExpression(PhpConditionalExpression node)
        {
            return node.Simplify(this);

        }

        protected override IPhpValue VisitPhpParenthesizedExpression(PhpParenthesizedExpression node)
        {
            // zdejmowanie wielokrotnych nawiasów
            if (node.Expression is PhpParenthesizedExpression)
                return node.Expression;
            if (node.Expression is PhpMethodCallExpression)
            {
                var t = node.Expression as PhpMethodCallExpression;
                if (!t.IsConstructorCall)
                    return node.Expression;
            }
            return node;
        }
        protected override IPhpValue VisitPhpVariableExpression(PhpVariableExpression node)
        {
            return node;
        }
        // Private Methods 

        public IPhpValue Simplify(IPhpValue src)
        {
            if (src == null)
                return null;
            if (src is PhpSourceBase)
                return Visit(src as PhpSourceBase);
            return src;
        }

        protected override IPhpValue VisitPhpPropertyAccessExpression(PhpPropertyAccessExpression node)
        {
            return node.Simplify(this);
        }
        protected override IPhpValue VisitPhpThisExpression(PhpThisExpression node)
        {
            return node;
        }

        #endregion Methods

    }
}
