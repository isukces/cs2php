using System;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator
{
    public class PhpStatementSimplifier : PhpBaseVisitor<IPhpStatement>, IPhpSimplifier
    {
        public PhpStatementSimplifier(OptimizeOptions op)
        {
            this.op = op;
        }

        // Protected Methods 

        private static PhpMethodCallExpression GetPhpNativeMethodCall(IPhpStatement statement, string name)
        {
            var expressionStatement = statement as PhpExpressionStatement;
            if (expressionStatement == null) return null;

            var methodCall = expressionStatement.Expression as PhpMethodCallExpression;
            if (methodCall == null) return null;
            if (methodCall.Name == name && methodCall.CallType == MethodCallStyles.Procedural)
                return methodCall;
            return null;
        }

        public IPhpStatement Simplify(IPhpStatement x)
        {
            if (x == null)
                return null;
            if (!(x is PhpSourceBase))
                throw new Exception(x.GetType().FullName);
            return Visit(x as PhpSourceBase);
        }

        public IPhpValue Simplify(IPhpValue x)
        {
            var a = new ExpressionSimplifier(op);
            return a.Visit(x as PhpSourceBase);
        }

        protected override IPhpStatement VisitPhpBreakStatement(PhpBreakStatement node)
        {
            return node;
        }

        protected override IPhpStatement VisitPhpCodeBlock(PhpCodeBlock node)
        {
            var newNode = new PhpCodeBlock();
            foreach (var i in node.GetPlain()) newNode.Statements.Add(Simplify(i));

            if (op.JoinEchoStatements)
            {
                for (var i = 1; i < newNode.Statements.Count; i++)
                {
                    var e1 = GetPhpNativeMethodCall(newNode.Statements[i - 1], "echo");
                    if (e1 == null) continue;
                    var e2 = GetPhpNativeMethodCall(newNode.Statements[i], "echo");
                    if (e2 == null) continue;

                    Func<IPhpValue, IPhpValue> AddBracketsIfNecessary = ee =>
                    {
                        if (ee is PhpParenthesizedExpression || ee is PhpConstValue ||
                            ee is PhpPropertyAccessExpression)
                            return ee;

                        if (ee is PhpBinaryOperatorExpression && ((PhpBinaryOperatorExpression)ee).Operator == ".")
                            return ee;
                        return new PhpParenthesizedExpression(ee);
                    };

                    var a1 = AddBracketsIfNecessary(e1.Arguments[0].Expression);
                    var a2 = AddBracketsIfNecessary(e2.Arguments[0].Expression);

                    IPhpValue e               = new PhpBinaryOperatorExpression(".", a1, a2);
                    e                         = Simplify(e);
                    IPhpValue echo            = new PhpMethodCallExpression("echo", e);
                    newNode.Statements[i - 1] = new PhpExpressionStatement(echo);
                    newNode.Statements.RemoveAt(i);
                    i--;
                }

                for (var i = 0; i < newNode.Statements.Count; i++)
                {
                    var a = newNode.Statements[i];
                    if (a is PhpSourceBase)
                        newNode.Statements[i] = Visit(a as PhpSourceBase);
                }
            }

            return PhpSourceBase.EqualCode_List(node.Statements, newNode.Statements) ? node : newNode;
        }

        protected override IPhpStatement VisitPhpContinueStatement(PhpContinueStatement node)
        {
            return node.Simplify(this);
        }


        protected override IPhpStatement VisitPhpExpressionStatement(PhpExpressionStatement node)
        {
            var newExpression = Simplify(node.Expression);
            return newExpression == node.Expression ? node : new PhpExpressionStatement(newExpression);
        }

        protected override IPhpStatement VisitPhpForEachStatement(PhpForEachStatement node)
        {
            return node.Simplify(this);
        }

        protected override IPhpStatement VisitPhpForStatement(PhpForStatement node)
        {
            return node.Simplify(this);
        }

        protected override IPhpStatement VisitPhpIfStatement(PhpIfStatement node)
        {
            return node.Simplify(this);
        }

        protected override IPhpStatement VisitPhpReturnStatement(PhpReturnStatement node)
        {
            return node.Simplify(this);
        }

        protected override IPhpStatement VisitPhpSwitchStatement(PhpSwitchStatement node)
        {
            return node.Simplify(this);
        }
        // Private Methods 

        protected override IPhpStatement VisitPhpWhileStatement(PhpWhileStatement node)
        {
            return node.Simplify(this);
        }

        private readonly OptimizeOptions op;
    }
}