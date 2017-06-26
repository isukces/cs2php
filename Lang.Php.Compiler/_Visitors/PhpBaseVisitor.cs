using Lang.Php.Compiler.Source;
using System;

namespace Lang.Php.Compiler.Translator
{
    public class PhpBaseVisitor<T>
    {
	    // public LangParseContext context = new LangParseContext();
        public bool ThrowNotImplementedException = true;

        public virtual T Visit(PhpSourceBase node)
        {
			if (node == null)
				return VisitNull();
            switch (node.Kind)
            {
                case PhpSourceItems.PhpCodeBlock:
                    return VisitPhpCodeBlock(node as PhpCodeBlock);
                case PhpSourceItems.PhpArrayCreateExpression:
                    return VisitPhpArrayCreateExpression(node as PhpArrayCreateExpression);
                case PhpSourceItems.PhpDefinedConstExpression:
                    return VisitPhpDefinedConstExpression(node as PhpDefinedConstExpression);
                case PhpSourceItems.PhpFreeExpression:
                    return VisitPhpFreeExpression(node as PhpFreeExpression);
                case PhpSourceItems.PhpBreakStatement:
                    return VisitPhpBreakStatement(node as PhpBreakStatement);
                case PhpSourceItems.PhpCodeModuleName:
                    return VisitPhpCodeModuleName(node as PhpCodeModuleName);
                case PhpSourceItems.PhpArrayAccessExpression:
                    return VisitPhpArrayAccessExpression(node as PhpArrayAccessExpression);
                case PhpSourceItems.PhpAssignExpression:
                    return VisitPhpAssignExpression(node as PhpAssignExpression);
                case PhpSourceItems.PhpClassFieldAccessExpression:
                    return VisitPhpClassFieldAccessExpression(node as PhpClassFieldAccessExpression);
                case PhpSourceItems.PhpBinaryOperatorExpression:
                    return VisitPhpBinaryOperatorExpression(node as PhpBinaryOperatorExpression);
                case PhpSourceItems.PhpConditionalExpression:
                    return VisitPhpConditionalExpression(node as PhpConditionalExpression);
                case PhpSourceItems.PhpConstValue:
                    return VisitPhpConstValue(node as PhpConstValue);
                case PhpSourceItems.PhpElementAccessExpression:
                    return VisitPhpElementAccessExpression(node as PhpElementAccessExpression);
                case PhpSourceItems.PhpContinueStatement:
                    return VisitPhpContinueStatement(node as PhpContinueStatement);
                case PhpSourceItems.PhpExpressionStatement:
                    return VisitPhpExpressionStatement(node as PhpExpressionStatement);
                case PhpSourceItems.PhpForEachStatement:
                    return VisitPhpForEachStatement(node as PhpForEachStatement);
                case PhpSourceItems.PhpForStatement:
                    return VisitPhpForStatement(node as PhpForStatement);
                case PhpSourceItems.PhpIfStatement:
                    return VisitPhpIfStatement(node as PhpIfStatement);
                case PhpSourceItems.PhpIncrementDecrementExpression:
                    return VisitPhpIncrementDecrementExpression(node as PhpIncrementDecrementExpression);
                case PhpSourceItems.PhpInstanceFieldAccessExpression:
                    return VisitPhpInstanceFieldAccessExpression(node as PhpInstanceFieldAccessExpression);
                case PhpSourceItems.PhpPropertyAccessExpression:
                    return VisitPhpPropertyAccessExpression(node as PhpPropertyAccessExpression);
                case PhpSourceItems.PhpMethodInvokeValue:
                    return VisitPhpMethodInvokeValue(node as PhpMethodInvokeValue);
                case PhpSourceItems.PhpMethodCallExpression:
                    return VisitPhpMethodCallExpression(node as PhpMethodCallExpression);
                case PhpSourceItems.PhpParenthesizedExpression:
                    return VisitPhpParenthesizedExpression(node as PhpParenthesizedExpression);
                case PhpSourceItems.PhpReturnStatement:
                    return VisitPhpReturnStatement(node as PhpReturnStatement);
                case PhpSourceItems.PhpThisExpression:
                    return VisitPhpThisExpression(node as PhpThisExpression);
                case PhpSourceItems.PhpUnaryOperatorExpression:
                    return VisitPhpUnaryOperatorExpression(node as PhpUnaryOperatorExpression);
                case PhpSourceItems.PhpVariableExpression:
                    return VisitPhpVariableExpression(node as PhpVariableExpression);
                case PhpSourceItems.PhpWhileStatement:
                    return VisitPhpWhileStatement(node as PhpWhileStatement);
                case PhpSourceItems.PhpSwitchStatement:
                    return VisitPhpSwitchStatement(node as PhpSwitchStatement);
                default: throw new NotSupportedException(node.Kind.ToString() + "," + node.GetType().Name);
            }
        }

		protected virtual T VisitNull()
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported", "VisitNull"));
            return default(T);
        }

            
        protected virtual T VisitPhpCodeBlock(PhpCodeBlock node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpCodeBlock", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpArrayCreateExpression(PhpArrayCreateExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpArrayCreateExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpDefinedConstExpression(PhpDefinedConstExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpDefinedConstExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpFreeExpression(PhpFreeExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpFreeExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpBreakStatement(PhpBreakStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpBreakStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpCodeModuleName(PhpCodeModuleName node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpCodeModuleName", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpArrayAccessExpression(PhpArrayAccessExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpArrayAccessExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpAssignExpression(PhpAssignExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpAssignExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpClassFieldAccessExpression(PhpClassFieldAccessExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpClassFieldAccessExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpBinaryOperatorExpression(PhpBinaryOperatorExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpBinaryOperatorExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpConditionalExpression(PhpConditionalExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpConditionalExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpConstValue(PhpConstValue node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpConstValue", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpElementAccessExpression(PhpElementAccessExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpElementAccessExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpContinueStatement(PhpContinueStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpContinueStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpExpressionStatement(PhpExpressionStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpExpressionStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpForEachStatement(PhpForEachStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpForEachStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpForStatement(PhpForStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpForStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpIfStatement(PhpIfStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpIfStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpIncrementDecrementExpression(PhpIncrementDecrementExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpIncrementDecrementExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpInstanceFieldAccessExpression(PhpInstanceFieldAccessExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpInstanceFieldAccessExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpPropertyAccessExpression(PhpPropertyAccessExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpPropertyAccessExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpMethodInvokeValue(PhpMethodInvokeValue node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpMethodInvokeValue", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpMethodCallExpression(PhpMethodCallExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpMethodCallExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpParenthesizedExpression(PhpParenthesizedExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpParenthesizedExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpReturnStatement(PhpReturnStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpReturnStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpThisExpression(PhpThisExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpThisExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpUnaryOperatorExpression(PhpUnaryOperatorExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpUnaryOperatorExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpVariableExpression(PhpVariableExpression node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpVariableExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpWhileStatement(PhpWhileStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpWhileStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPhpSwitchStatement(PhpSwitchStatement node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPhpSwitchStatement", GetType().FullName));
            return default(T);
        }
		public static PhpSourceItems GetKind(PhpSourceBase x) {
			if (x == null) throw new ArgumentNullException();           
			if (x.GetType() == typeof(PhpCodeBlock)) return PhpSourceItems.PhpCodeBlock;
			if (x.GetType() == typeof(PhpArrayCreateExpression)) return PhpSourceItems.PhpArrayCreateExpression;
			if (x.GetType() == typeof(PhpDefinedConstExpression)) return PhpSourceItems.PhpDefinedConstExpression;
			if (x.GetType() == typeof(PhpFreeExpression)) return PhpSourceItems.PhpFreeExpression;
			if (x.GetType() == typeof(PhpBreakStatement)) return PhpSourceItems.PhpBreakStatement;
			if (x.GetType() == typeof(PhpCodeModuleName)) return PhpSourceItems.PhpCodeModuleName;
			if (x.GetType() == typeof(PhpArrayAccessExpression)) return PhpSourceItems.PhpArrayAccessExpression;
			if (x.GetType() == typeof(PhpAssignExpression)) return PhpSourceItems.PhpAssignExpression;
			if (x.GetType() == typeof(PhpClassFieldAccessExpression)) return PhpSourceItems.PhpClassFieldAccessExpression;
			if (x.GetType() == typeof(PhpBinaryOperatorExpression)) return PhpSourceItems.PhpBinaryOperatorExpression;
			if (x.GetType() == typeof(PhpConditionalExpression)) return PhpSourceItems.PhpConditionalExpression;
			if (x.GetType() == typeof(PhpConstValue)) return PhpSourceItems.PhpConstValue;
			if (x.GetType() == typeof(PhpElementAccessExpression)) return PhpSourceItems.PhpElementAccessExpression;
			if (x.GetType() == typeof(PhpContinueStatement)) return PhpSourceItems.PhpContinueStatement;
			if (x.GetType() == typeof(PhpExpressionStatement)) return PhpSourceItems.PhpExpressionStatement;
			if (x.GetType() == typeof(PhpForEachStatement)) return PhpSourceItems.PhpForEachStatement;
			if (x.GetType() == typeof(PhpForStatement)) return PhpSourceItems.PhpForStatement;
			if (x.GetType() == typeof(PhpIfStatement)) return PhpSourceItems.PhpIfStatement;
			if (x.GetType() == typeof(PhpIncrementDecrementExpression)) return PhpSourceItems.PhpIncrementDecrementExpression;
			if (x.GetType() == typeof(PhpInstanceFieldAccessExpression)) return PhpSourceItems.PhpInstanceFieldAccessExpression;
			if (x.GetType() == typeof(PhpPropertyAccessExpression)) return PhpSourceItems.PhpPropertyAccessExpression;
			if (x.GetType() == typeof(PhpMethodInvokeValue)) return PhpSourceItems.PhpMethodInvokeValue;
			if (x.GetType() == typeof(PhpMethodCallExpression)) return PhpSourceItems.PhpMethodCallExpression;
			if (x.GetType() == typeof(PhpParenthesizedExpression)) return PhpSourceItems.PhpParenthesizedExpression;
			if (x.GetType() == typeof(PhpReturnStatement)) return PhpSourceItems.PhpReturnStatement;
			if (x.GetType() == typeof(PhpThisExpression)) return PhpSourceItems.PhpThisExpression;
			if (x.GetType() == typeof(PhpUnaryOperatorExpression)) return PhpSourceItems.PhpUnaryOperatorExpression;
			if (x.GetType() == typeof(PhpVariableExpression)) return PhpSourceItems.PhpVariableExpression;
			if (x.GetType() == typeof(PhpWhileStatement)) return PhpSourceItems.PhpWhileStatement;
			if (x.GetType() == typeof(PhpSwitchStatement)) return PhpSourceItems.PhpSwitchStatement;
            throw new NotSupportedException(x.GetType().FullName);
		}
    }
	public enum PhpSourceItems
	{
		PhpCodeBlock,
PhpArrayCreateExpression,
PhpDefinedConstExpression,
PhpFreeExpression,
PhpBreakStatement,
PhpCodeModuleName,
PhpArrayAccessExpression,
PhpAssignExpression,
PhpClassFieldAccessExpression,
PhpBinaryOperatorExpression,
PhpConditionalExpression,
PhpConstValue,
PhpElementAccessExpression,
PhpContinueStatement,
PhpExpressionStatement,
PhpForEachStatement,
PhpForStatement,
PhpIfStatement,
PhpIncrementDecrementExpression,
PhpInstanceFieldAccessExpression,
PhpPropertyAccessExpression,
PhpMethodInvokeValue,
PhpMethodCallExpression,
PhpParenthesizedExpression,
PhpReturnStatement,
PhpThisExpression,
PhpUnaryOperatorExpression,
PhpVariableExpression,
PhpWhileStatement,
PhpSwitchStatement,
	}
}
