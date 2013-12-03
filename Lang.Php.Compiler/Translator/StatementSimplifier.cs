using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator
{
    public class StatementSimplifier : PhpBaseVisitor<IPhpStatement>, IPhpSimplifier, IPhpExpressionSimplifier
    {
        #region Constructors

        public StatementSimplifier(OptimizeOptions op)
        {
            this.op = op;
        }

        #endregion Constructors

        #region Methods

        // Protected Methods 

        PhpMethodCallExpression getPHPNativeMethodCall(IPhpStatement s, string name)
        {
            if (s is PhpExpressionStatement)
            {
                var v = s as PhpExpressionStatement;
                if (v.Expression is PhpMethodCallExpression)
                {
                    var m = v.Expression as PhpMethodCallExpression;
                    if (m.Name == name && m.TargetObject == null && string.IsNullOrEmpty(m.ClassName))
                        return m;
                }
            }
            return null;
        }

        protected override IPhpStatement VisitPhpCodeBlock(PhpCodeBlock node)
        {



            PhpCodeBlock newNode = new PhpCodeBlock();
            foreach (var i in node.GetPlain())
            {
                newNode.Statements.Add(Simplify(i));
            }

            #region Łączenie kolejnych echo
            if (op.JoinEchoStatements)
            {
                //newNode.Statements.Clear();
                {
                    for (int i = 1; i < newNode.Statements.Count; i++)
                    {
                        var e1 = getPHPNativeMethodCall(newNode.Statements[i - 1], "echo");
                        if (e1 == null) continue;
                        var e2 = getPHPNativeMethodCall(newNode.Statements[i], "echo");
                        if (e2 == null) continue;
                        IPhpValue e = new PhpBinaryOperatorExpression(".", e1.Arguments[0].Expression, e2.Arguments[0].Expression);
                        e = Simplify(e);
                        IPhpValue _echo_ = new PhpMethodCallExpression("echo", e);
                        newNode.Statements[i - 1] = new PhpExpressionStatement(_echo_);
                        newNode.Statements.RemoveAt(i);
                        i--;
                    }
                }
            }
            #endregion
            if (PhpSourceBase.EqualCode_List(node.Statements, newNode.Statements))
                return node;
            return newNode;
        }


        protected override IPhpStatement VisitPhpExpressionStatement(PhpExpressionStatement node)
        {
            var newExpression = Simplify(node.Expression);
            if (newExpression == node.Expression)
                return node;
            return new PhpExpressionStatement(newExpression);
        }

        protected override IPhpStatement VisitPhpIfStatement(PhpIfStatement node)
        {
            return node.Simplify(this);
        }

        protected override IPhpStatement VisitPhpReturnStatement(PhpReturnStatement node)
        {
            return node.Simplify(this);
        }
        // Private Methods 

        protected override IPhpStatement VisitPhpWhileStatement(PhpWhileStatement node)
        {
            return node.Simplify(this);
        }
        protected override IPhpStatement VisitPhpForEachStatement(PhpForEachStatement node)
        {
            return node.Simplify(this);
        }
        protected override IPhpStatement VisitPhpContinueStatement(PhpContinueStatement node)
        {
            return node.Simplify(this);
        }
        protected override IPhpStatement VisitPhpForStatement(PhpForStatement node)
        {
            return node.Simplify(this);
        }
        public IPhpStatement Simplify(IPhpStatement x)
        {
            if (x == null)
                return null;
            if (!(x is PhpSourceBase))
                throw new Exception(x.GetType().FullName);
            return Visit(x as PhpSourceBase);
        }
        protected override IPhpStatement VisitPhpBreakStatement(PhpBreakStatement node)
        {
            return node;
        }

        public IPhpValue Simplify(IPhpValue x)
        {
            var a = new ExpressionSimplifier(op);
            return a.Visit(x as PhpSourceBase);
        }

        #endregion Methods

        #region Fields

        OptimizeOptions op;

        #endregion Fields

    }
}
