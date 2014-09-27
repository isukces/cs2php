using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lang.Cs.Compiler.Visitors
{
    public class IValueVisitor : CodeVisitor<IValue>
    {
        #region Constructors

        public IValueVisitor(CompileState ts)
        {
            if (ts == null)
                throw new ArgumentNullException("ts");
            state = ts;
            context = ts.Context;
        }

        #endregion Constructors

        #region Static Methods

        // Private Methods 

        private static QualifiedNameVisitor.R _Name(NameSyntax node)
        {
            return new QualifiedNameVisitor().Visit(node);
        }

        static IValue Simplify(IValue src)
        {
            return ExpressionConverterVisitor.Visit(src);
        }

        #endregion Static Methods

        #region Methods

        // Protected Methods 

        protected FunctionDeclarationParameter _VisitParameter(SyntaxNode node)
        {
            LangVisitor lv = new LangVisitor(state);
            return lv.Visit(node) as FunctionDeclarationParameter;
        }

        protected IValue internalVisit_BinaryExpressionSyntax(BinaryExpressionSyntax node, string _operator)
        {
#if ROSLYN
            //FAQ 4
            MethodInfo operatorMethod = null;
            var expressionTypeInfo = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node);
            var roslynResultType = state.Context.Roslyn_ResolveType(expressionTypeInfo.Type);
            var symbolInfo = ModelExtensions.GetSymbolInfo(state.Context.RoslynModel, node);
            if (symbolInfo.Symbol != null)
            {
                var symbol = symbolInfo.Symbol;
                if (symbol is IMethodSymbol)
                {
                    var methodSymbol = symbol as IMethodSymbol;
                    if (methodSymbol.MethodKind != MethodKind.BuiltinOperator)
                    {
                        var mi = state.Context.Roslyn_ResolveMethod(methodSymbol);
                        if (mi is MethodInfo)
                            operatorMethod = mi as MethodInfo;
                        else
                            throw new NotSupportedException("Jest jakiś symbol");
                    }
                }
                else
                    throw new NotSupportedException("Jest jakiś symbol");
            }
#endif

            var l = Visit(node.Left);
            var r = Visit(node.Right);
            if (l is UnknownIdentifierValue || r is UnknownIdentifierValue)
                throw new Exception("Invalid arguments");
            //var typeLeft = l.ValueType;
            //var typeRight = r.ValueType;

            var tmp = new BinaryOperatorExpression(l, r, _operator, roslynResultType, operatorMethod);
            return Simplify(tmp);
        }

        protected override IValue VisitAddAssignmentExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_AssignWithPrefix(node, "+");
        }
        protected override IValue VisitSubtractAssignmentExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_AssignWithPrefix(node, "-");
        }
        protected override IValue VisitAddExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "+"); // +++ 
        }

        protected override IValue VisitArgument(ArgumentSyntax node)
        {
            var v = Visit(node.Expression);
            //TypeInfo eti = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node.Expression);
            var eti2 = state.Context.RoslynModel.GetTypeInfo2(node.Expression);
            // eti.Type.
            var eti = eti2.TypeInfo;
            if (!Equals(eti.Type, eti.ConvertedType))
                Debug.WriteLine("{0} => {1}, {2}", eti.Type, eti.ConvertedType, eti2.ImplicitConversion);
            //if (eti.Type.ToString().Contains("Date"))
            //    System.Diagnostics.Debug.WriteLine("{0} => {1}, {2}", eti.Type, eti.ConvertedType, eti.ImplicitConversion);
            var g = node.RefOrOutKeyword.ValueText;
            var functionArgument = new FunctionArgument(g, v);
            return Simplify(functionArgument);

        }

        protected override IValue VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            var roslynType = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node);
            var type = state.Context.Roslyn_ResolveType(roslynType.Type);
            if (!type.IsArray || type.GetArrayRank() != 1)
                throw new NotSupportedException();
            IValue[] initializers = null;
            if (node.Initializer != null)
                initializers = node.Initializer.Expressions.Select(Visit).ToArray();
            var b = new ArrayCreateExpression(type, initializers);
            return Simplify(b);
        }

        protected override IValue VisitArrayInitializerExpression(InitializerExpressionSyntax node)
        {
            var b = ModelExtensions.GetSymbolInfo(context.RoslynModel, node);
            var a = context.RoslynModel.GetTypeInfo2(node);
            throw new NotSupportedException();
        }

        protected override IValue VisitArrayType(ArrayTypeSyntax node)
        {
            var info = ModelExtensions.GetTypeInfo(context.RoslynModel, node);
            var t = context.Roslyn_ResolveType(info.Type);
            return new TypeValue(t);
        }

        protected override IValue VisitAsExpression(BinaryExpressionSyntax node)
        {
            var symbolInfo = ModelExtensions.GetSymbolInfo(state.Context.RoslynModel, node);
            if (symbolInfo.Symbol != null)
                throw new NotSupportedException();

            var l = Visit(node.Left);
            var r = Visit(node.Right);
            Type t;
            if (r is TypeValue)
                t = (r as TypeValue).DotnetType;
            else
                throw new NotSupportedException();
            var o = new BinaryOperatorExpression(l, new TypeValue(t), "as", t, null);
            return Simplify(o);
        }

        //        protected override IValue VisitAssignExpression(BinaryExpressionSyntax node)
        //        {
        //            var symbolInfo = state.Context.RoslynModel.GetSymbolInfo(node);
        //            if (symbolInfo.Symbol != null)
        //                throw new NotSupportedException();
        //            var ti = state.Context.RoslynModel.GetTypeInfo(node);
        //            if (!ti.ImplicitConversion.IsIdentity)
        //                throw new NotSupportedException();
        //            if (ti.ImplicitConversion.Method != null)
        //                throw new NotSupportedException();
        //            var left = Visit(node.Left);
        //            var rigth = Visit(node.Right);
        //            var a = new CsharpAssignExpression(left, rigth, "");
        //            return Simplify(a);
        //        }
        protected override IValue VisitBitwiseAndExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "&");
        }

        protected override IValue VisitBitwiseOrExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "|");
        }

        protected override IValue VisitCaseSwitchLabel(SwitchLabelSyntax node)
        {
            var value = Visit(node.Value);
            return new CsharpSwichLabel(value, false);
        }

        protected override IValue VisitCastExpression(CastExpressionSyntax node)
        {
            var e = Visit(node.Expression);
            var tt = ModelExtensions.GetTypeInfo(context.RoslynModel, node);
            var t = context.Roslyn_ResolveType(tt.Type);
            // var t1 = context.Roslyn_ResolveType(node.Type);
            return new CastExpression(e, t);
        }

        protected override IValue VisitCharacterLiteralExpression(LiteralExpressionSyntax node)
        {
            var t = node.Token.ValueText.Trim();
            return new ConstValue(t[0]);
        }

        protected override IValue VisitCollectionInitializerExpression(InitializerExpressionSyntax node)
        {
            var aa = node.Expressions.AsEnumerable().Select(i => Visit(i)).Cast<IValueTable_PseudoValue>().ToArray();
            var g = new IValueTable2_PseudoValue(aa);
            return g;
            //            throw new NotImplementedException();
        }

        protected override IValue VisitComplexElementInitializerExpression(InitializerExpressionSyntax node)
        {
            IValue[] aa = node.Expressions.AsEnumerable().Select(i => Visit(i)).ToArray();
            var a = new IValueTable_PseudoValue(aa);
            return a;
        }

        protected override IValue VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            var aaaa = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node);
            var resultType = state.Context.Roslyn_ResolveType(aaaa.Type);
            var condition = Visit(node.Condition);
            var whenTrue = Visit(node.WhenTrue);
            var whenFalse = Visit(node.WhenFalse);
            //Type resultType;
            //if (whenTrue.ValueType == whenFalse.ValueType)
            //    resultType = whenTrue.ValueType;
            //else
            //    throw new NotSupportedException();
            var res = new ConditionalExpression(condition, whenTrue, whenFalse, resultType);
            return Simplify(res);
        }

        protected override IValue VisitDefaultSwitchLabel(SwitchLabelSyntax node)
        {
            return new CsharpSwichLabel(null, true);
        }

        protected override IValue VisitDivideExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "/");
            /*
                       var symbolInfo = ModelExtensions.GetSymbolInfo(state.Context.RoslynModel, node);
                       var type = GetResultTypeForBinaryExpression(symbolInfo);

                       var left = Visit(node.Left);
                       var right = Visit(node.Right);
                       var typeLeft = left.ValueType;
                       var typeRight = right.ValueType;
                       // var operators = MethodUtils.GetOperators("op_Div", typeLeft, typeRight);

                       if (type == null)
                       {
           //                if (typeLeft == typeof (double) || typeRight == typeof (double))
           //                    type = typeof (double);
           //                else if (typeLeft == typeof (int) && typeRight == typeof (int))
           //                    type = typeof (int);
           //                else if (typeLeft == typeof (decimal) && typeRight == typeof (int))
           //                    type = typeof (decimal);
           //                else if (typeLeft == typeof (decimal) && typeRight == typeof (decimal))
           //                    type = typeof (decimal);
           //                else
                               throw new NotSupportedException();
                       }
                       return new BinaryOperatorExpression(left, right, "/", type, null);
             */
        }

        protected override IValue VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            var expression = Visit(node.Expression);
            var aa = _internalVisitArgumentList(node.ArgumentList);
            Type elementType = TypesUtil.GetElementType(expression.ValueType);
            var g = new ElementAccessExpression(expression, aa, elementType);
            return Simplify(g);
        }

        protected override IValue VisitEqualsExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "==");
        }

        protected override IValue VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            // var a = context.RoslynModel.GetTypeInfo(node.va);
            return Visit(node.Value);
        }

        protected override IValue VisitFalseLiteralExpression(LiteralExpressionSyntax node)
        {
            return new ConstValue(false);
        }

        protected override IValue VisitGenericName(GenericNameSyntax node)
        {
            //var info = ModelExtensions.GetSymbolInfo(context.RoslynModel, node);
            //var info2 = ModelExtensions.AnalyzeDataFlow(context.RoslynModel, node);
            //var info3 = context.RoslynModel.GetDeclaredSymbol(node);
            // var mg = ModelExtensions.GetMemberGroup(context.RoslynModel, node);


            var identifier = InternalVisitTextIdentifier(node.Identifier.ValueText.Trim());
            var genericTypes = node.TypeArgumentList.Arguments.Select(Visit).ToArray();
            var gt1 = genericTypes.OfType<TypeValue>().ToArray();
            // Debug.Assert(genericTypes.Length == gt1.Length);
            if (identifier is UnknownIdentifierValue)
            {
                var xx = identifier as UnknownIdentifierValue;
                // Debug.Assert(xx.OptionalGenericTypes.Length == 0);
                {
                    var tu = context.MatchTypes(xx.Identifier, genericTypes.Length);
                    if (tu.Length > 1)
                        throw new Exception("Nie wiem co wybrać");
                    if (tu.Length == 1)
                    {
                        var dotn = tu[0];
                        if (dotn.IsGenericType)
                            dotn = dotn.MakeGenericType(gt1.Select(i => i.DotnetType).ToArray());
                        var g = new TypeValue(dotn);
                        return Simplify(g);
                    }
                }
                var result = new UnknownIdentifierValue(xx.Identifier, genericTypes);
                return Simplify(result);
            }
            if (gt1.Length <= 0) return base.VisitGenericName(node);
            if (!(identifier is TypeValue))
                throw new NotSupportedException();
            var a = node.Identifier.ValueText.Trim();
            var b = context.MatchTypes(a, gt1.Length);
            if (b.Length != 1)
                throw new NotSupportedException();
            var t = b.First();
            Debug.Assert(t.IsGenericType);
            var tt = t.MakeGenericType(gt1.Select(i => i.DotnetType).ToArray());
            return new TypeValue(tt);
        }

        protected override IValue VisitGreaterThanExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, ">");
        }

        protected override IValue VisitGreaterThanOrEqualExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, ">=");
        }

        protected override IValue VisitIdentifierName(IdentifierNameSyntax node)
        {
            // var tt = context.Roslyn_GetNamedTypeSymbols(null);

            var sInfo = ModelExtensions.GetSymbolInfo(context.RoslynModel, node);
            var tInfo2 = context.RoslynModel.GetTypeInfo2(node);
            var tInfo = tInfo2.TypeInfo;
            //var sInfo = sInfo2.ty
            Type type = tInfo.Type == null ? null : context.Roslyn_ResolveType(tInfo.Type);
            switch (sInfo.Symbol.Kind)
            {
                case SymbolKind.Local:
                    //var symbolLocal = info.Symbol as CSharp.SourceLocalSymbol;
                    IValue tmp = new LocalVariableExpression(sInfo.Symbol.Name, new LangType(type));
                    tmp = ImplicitConvert(tmp, tInfo2);
                    return Simplify(tmp);
                case SymbolKind.NamedType:
                    // var a = sInfo.Symbol.ToDisplayString();
                    var b = sInfo.Symbol.OriginalDefinition as INamedTypeSymbol;
                    if (b != null)
                    {
                        var c = context.Roslyn_ResolveType(b);
                        return new TypeValue(c);
                    }
                    throw new NotSupportedException();
                case SymbolKind.Parameter:
#warning 'Formalnie to nie jest zmienna lokalna !!';
                    tmp = new LocalVariableExpression(sInfo.Symbol.Name, new LangType(type));
                    return Simplify(tmp);
                case SymbolKind.Field:
                    var ax = context.Roslyn_ResolveField(sInfo.Symbol as IFieldSymbol);
                    if (ax.IsStatic)
                    {
                        // throw new NotSupportedException();

                        var fieldSymbol = sInfo.Symbol as IFieldSymbol;
                        tmp = new ClassFieldAccessExpression(ax, fieldSymbol != null && fieldSymbol.IsConst);
                        return Simplify(tmp);
                    }
                    tmp = new InstanceFieldAccessExpression(ax, new ThisExpression(state.CurrentType)); // niczego nie wiem o kontekście -> domyślam się, że this
                    return Simplify(tmp);
                case SymbolKind.Method:
                    {
                        var methodBaseInfo = context.Roslyn_ResolveMethod(sInfo.Symbol as IMethodSymbol);
                        var methodInfo = methodBaseInfo as MethodInfo;
                        if (methodInfo == null)
                            throw new NotSupportedException();
                        var result = new MethodExpression(methodInfo);
                        return Simplify(result);
                    }
                case SymbolKind.Property:
                    {
                        var pi = context.Roslyn_ResolveProperty(sInfo.Symbol as IPropertySymbol);
                        var targetObject = new ThisExpression(state.CurrentType);
                        var result = new CsharpInstancePropertyAccessExpression(pi, targetObject);
                        return result;

                    }

                default:
                    throw new NotSupportedException();
            }
            //throw new NotSupportedException();
            //var m = info.Symbol;
            //var info2 = context.RoslynModel.AnalyzeDataFlow(node);
            ////var info3 = context.RoslynModel.GetDeclaredSymbol(node);
            //var mg = context.RoslynModel.GetMemberGroup(node);

            //string identifier = node.Identifier.ValueText.Trim();
            // return InternalVisitTextIdentifier(identifier);
        }

        protected override IValue VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            // var si = ModelExtensions.GetSymbolInfo(context.RoslynModel, node);
            var nl = _internalVisitArgumentList(node.ArgumentList);
            IValue expression;

            Func<IValue> getRealExpression = () =>
            {
                //if (node.Expression is StaticMemberAccessExpression)
                //{
                //    MemberAccessException aaaa = node.Expression as StaticMemberAccessExpression;
                //    var si3 = context.RoslynModel.GetSymbolInfo(aaaa);
                //    var si2 = context.RoslynModel.GetTypeInfo(aaaa.);
                //}
                expression = Visit(node.Expression);
                if (expression is InstanceMemberAccessExpression)
                    return (expression as InstanceMemberAccessExpression).Expression;
                if (expression is StaticMemberAccessExpression)
                    return null;
                if (expression is UnknownIdentifierValue)
                    return new ThisExpression(state.CurrentType);
                if (expression is LocalVariableExpression)
                    return expression;
                if (expression is MethodExpression)
                    return new ThisExpression(state.CurrentType);
                throw new NotSupportedException();
            };

            var info = ModelExtensions.GetSymbolInfo(context.RoslynModel, node);
            var symbol = info.Symbol as IMethodSymbol;
            if (symbol == null)
                throw new NotImplementedException();
            var mi1 = context.Roslyn_ResolveMethod(symbol);
            if (mi1 == null)
                throw new Exception(string.Format("method not found: {0}", symbol.Name));
            if (symbol.MethodKind == MethodKind.DelegateInvoke)
            {
                expression = Visit(node.Expression);
                var tmp1 = _Make_DotnetMethodCall(mi1, expression, nl, null);
                return tmp1;
            }
            {
                IValue tmp;
                if (mi1.IsStatic)
                {
                    if ((mi1 as MethodInfo).IsExtensionMethod())
                    {
                        var realExpression = getRealExpression();
                        var ggg = nl.ToList();
                        ggg.Insert(0, new FunctionArgument("", realExpression));
                        tmp = _Make_DotnetMethodCall(mi1, realExpression, nl, null);
                    }
                    else
                    {
                        //if (getRealExpression() != null)
                        //    throw new NotSupportedException();
                        tmp = _Make_DotnetMethodCall(mi1, null, nl, null);
                    }
                }
                else
                {
                    var realExpression = getRealExpression();
                    if (realExpression == null)
                        throw new NotSupportedException();
                    tmp = _Make_DotnetMethodCall(mi1, realExpression, nl, null);
                }
                return Simplify(tmp);
            }
        }

        protected override IValue VisitLessThanExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "<");
        }

        protected override IValue VisitLessThanOrEqualExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "<=");
        }

        protected override IValue VisitLogicalAndExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "&&");
        }

        protected override IValue VisitLogicalNotExpression(PrefixUnaryExpressionSyntax node)
        {
            var operand = Visit(node.Operand);
            Debug.Assert(node.OperatorToken.CSharpKind() == SyntaxKind.ExclamationToken);
            Type t;
            if (operand.ValueType == typeof(bool))
                t = operand.ValueType;
            else
                throw new NotSupportedException();
            var a = new UnaryOperatorExpression(operand, "!", t);
            return Simplify(a);
        }

        protected override IValue VisitLogicalOrExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "||");
        }

        protected override IValue VisitModuloExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "%");
        }

        protected override IValue VisitMultiplyExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "*");
        }

        protected override IValue VisitNotEqualsExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "!=");
            //var l = Visit(node.Left);
            //var r = Visit(node.Right);
            //return new BinaryOperatorExpression(l, r, "!=", typeof(bool), null);
        }

        protected override IValue VisitNull()
        {
            return null;
        }

        protected override IValue VisitNullableType(NullableTypeSyntax node)
        {
            var ti = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node);
            var tt = state.Context.Roslyn_ResolveType(ti.Type);
            var r = new TypeValue(tt);
            return r;
        }

        protected override IValue VisitNullLiteralExpression(LiteralExpressionSyntax node)
        {
            return new ConstValue(null);
        }

        protected override IValue VisitNumericLiteralExpression(LiteralExpressionSyntax node)
        {
            var typeInfo = context.RoslynModel.GetTypeInfo2(node);
            IValue value = new ConstValue(node.Token.Value);
            value = ImplicitConvert(value, typeInfo);
            return value;
        }

        protected override IValue VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            var nodeInfo = ModelExtensions.GetSymbolInfo(Model, node);
            // var t = model.GetTypeInfo(node);
            var methodSymbol = nodeInfo.Symbol as IMethodSymbol;

            var constructorInfo = context.Roslyn_ResolveMethod(methodSymbol) as ConstructorInfo;
            if (constructorInfo == null)
                throw new NotSupportedException();
            var argList = _internalVisitArgumentList(node.ArgumentList);
            IValue[] initializer = null;

            {
                if (node.Initializer != null)
                {
                    var _initializer = Visit(node.Initializer);
                    if (_initializer == null)
                        throw new ArgumentNullException("_initializer");
                    if (_initializer is IValueTable2_PseudoValue)
                        initializer = (_initializer as IValueTable2_PseudoValue).Items.OfType<IValue>().ToArray();
                    else if (_initializer is IValueTable_PseudoValue)
                        initializer = (_initializer as IValueTable_PseudoValue).Items;
                    else
                        throw new NotSupportedException();
                }
            }
            if (initializer == null)
                initializer = new IValue[0];
            var co = new CallConstructor(constructorInfo, argList, initializer);
            return co;
        }

        protected override IValue VisitObjectInitializerExpression(InitializerExpressionSyntax node)
        {
            IValue[] items = node.Expressions
                .AsEnumerable()
                .Select(Visit)
                .Cast<CsharpAssignExpression>()
                .ToArray();
            var bb = new IValueTable_PseudoValue(items);
            return bb;
        }

        protected override IValue VisitParameter(ParameterSyntax node)
        {
            // LangVisitor lv = new LangVisitor(state);
            // var g = lv.Visit(node);
            // if (!(g is FunctionDeclarationParameter))
            //// return g;
            throw new NotSupportedException();
            //return g as FunctionDeclarationParameter;
        }

        protected override IValue VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            var e = Visit(node.Expression);
            return new ParenthesizedExpression(e);
        }

        protected override IValue VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            var info = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node);
            var ct = state.Context.Roslyn_ResolveType(info.ConvertedType);
            if (!ct.BaseType.IsMulticastDelegate())
                throw new NotImplementedException();
            var lv = new LangVisitor(state);
            var pl = lv.rVisit(node.ParameterList);
            var body = lv.Visit(node.Body);
            if (!(body is IStatement))
            {
                var iv = body as IValue;
                if (iv != null)
                    body = new ReturnStatement(iv);

                if (!(body is IStatement))
                    throw new Exception("body is not IStatement");
            }

            var h = new LambdaExpression(ct, pl, body as IStatement);
            return Simplify(h);
            // throw new NotSupportedException();
        }

        protected override IValue VisitPostDecrementExpression(PostfixUnaryExpressionSyntax node)
        {
            var a = Visit(node.Operand);
            return new IncrementDecrementExpression(a, false, false);
        }

        protected override IValue VisitPostIncrementExpression(PostfixUnaryExpressionSyntax node)
        {
            var a = Visit(node.Operand);
            return new IncrementDecrementExpression(a, true, false);
        }

        protected override IValue VisitPredefinedType(PredefinedTypeSyntax node)
        {
            var rt = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node);
            // var t = TypesUtil.PRIMITIVE_TYPES[node.Keyword.ValueText.Trim()];
            var t = state.Context.Roslyn_ResolveType(rt.Type);
            return new TypeValue(t);
        }

        protected override IValue VisitQualifiedName(QualifiedNameSyntax node)
        {
            var ti = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node);
            var dot = state.Context.Roslyn_ResolveType(ti.Type);
            return new TypeValue(dot);
        }

        protected override IValue VisitSimpleAssignmentExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_AssignWithPrefix(node, ""); // +++ 
        }

        protected override IValue VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
        {
            FunctionDeclarationParameter a = _VisitParameter(node.Parameter);
            var tmp = context.Arguments.ToArray();
            context.Arguments.Add(a);
            var bodyExpression = Visit(node.Body);
            context.Arguments = tmp.ToList();
            return new SimpleLambdaExpression(a, bodyExpression);
        }

        protected override IValue VisitSimpleMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            // var typeInfo = state.Context.RoslynModel.GetTypeInfo(node);
            var typeInfo2 = state.Context.RoslynModel.GetTypeInfo(node.Expression);
            var tt = state.Context.RoslynModel.GetSymbolInfo(node);

            MemberInfo member = null;

            switch (tt.Symbol.Kind)
            {
                case SymbolKind.Field:
                    var fieldSymbol = tt.Symbol as IFieldSymbol;
                    var fieldInfo = context.Roslyn_ResolveField(fieldSymbol);
                    if (fieldInfo.IsStatic)
                        return Simplify(new ClassFieldAccessExpression(fieldInfo, true));
                    var visitedExpression = Visit(node.Expression);
                    if (visitedExpression == null) throw new ArgumentNullException("visitedExpression");
                    return Simplify(new InstanceFieldAccessExpression(fieldInfo, visitedExpression));
                case SymbolKind.Method:
                    var sss = tt.Symbol as IMethodSymbol;
                    var methodInfo = context.Roslyn_ResolveMethod(sss);
                    member = methodInfo;

                    break;
                case SymbolKind.Property:
                    var uuu = tt.Symbol as IPropertySymbol;
                    var propertyInfo = context.Roslyn_ResolveProperty(uuu);
                    member = propertyInfo;
                    break;

            }
            IValue expression;
            var fullName = _Name(node.Name);


            if (typeInfo2.Type != null)
            {
                ISymbol[] gg;
                if (fullName.IsGeneric)
                {
                    var arity = fullName.Generics.Length;
                    ISymbol[] gg1 = typeInfo2.Type.GetMembers()
                        .AsEnumerable()
                        .OfType<IMethodSymbol>()
                        .Where(i => i.Name == fullName.BaseName && i.Arity == arity)
                        .ToArray();
                    gg = gg1;
                }
                else
                    gg = typeInfo2.Type.GetMembers(fullName.GetNonGeneric()).ToArray();
                if (gg.Length == 1)
                {
                    ISymbol ggg = gg.Single();
                    switch (ggg.Kind)
                    {
                        case SymbolKind.Property:
                            var propertySymbol = ggg as IPropertySymbol;
                            if (propertySymbol == null)
                                throw new Exception("propertySymbol is null");
                            var pi = context.Roslyn_ResolveProperty(propertySymbol);
                            if (propertySymbol.IsStatic)
                            {
                                var tmp = new ClassPropertyAccessExpression(pi);
                                return Simplify(tmp);
                            }
                            else
                            {
                                expression = Visit(node.Expression);
                                var tmp = new CsharpInstancePropertyAccessExpression(pi, expression);
                                return Simplify(tmp);
                            }
                    }
                }
            }
            // throw new NotSupportedException();
            expression = Visit(node.Expression);
            if (expression is UnknownIdentifierValue)
            {
                if (fullName.IsGeneric)
                    throw new NotSupportedException();
                var name = fullName.BaseName;

                var a = ((expression as UnknownIdentifierValue)).Identifier + "." + name;
                var t = context.MatchTypes(a, 0);
                if (t.Length == 1)
                    return new TypeValue(t[0]);
                return new UnknownIdentifierValue(a, new IValue[0]);
            }
            if (expression is TypeValue)
            {
                if (fullName.IsGeneric)
                    throw new NotSupportedException();
                var name = fullName.BaseName;
                var tmp = new StaticMemberAccessExpression(name, new LangType((expression as TypeValue).DotnetType));
                return Simplify(tmp);
            }
            if (expression is LocalVariableExpression || expression is ConstValue
                || expression is ClassFieldAccessExpression || expression is InstanceFieldAccessExpression
                || expression is ElementAccessExpression || expression is ArgumentExpression
                || expression is CsharpMethodCallExpression || expression is CsharpInstancePropertyAccessExpression
                || expression is ClassPropertyAccessExpression || expression is ParenthesizedExpression)
            {
                // if (fullName.IsGeneric)
                //     throw new NotSupportedException();
                var name = fullName.BaseName;
                if (member == null)
                    throw new NotSupportedException();
                var tmp = new InstanceMemberAccessExpression(name, expression, member);
                return Simplify(tmp);
            }



            // ReSharper disable once InvertIf
            if (expression is ThisExpression)
            {
                if (fullName.IsGeneric)
                    throw new NotSupportedException();
                var name = fullName.BaseName;
                if (member == null)
                    throw new NotSupportedException();
                var tmp = new InstanceMemberAccessExpression(name, expression, member);
                return Simplify(tmp);
            }

            throw new NotImplementedException("*** " + expression.GetType().FullName);
        }

        protected override IValue VisitStringLiteralExpression(LiteralExpressionSyntax node)
        {
            IValue value = new ConstValue(node.Token.Value);
            value = ImplicitConvert(value, context.RoslynModel.GetTypeInfo2(node));
            return value;
        }

        protected override IValue VisitSubtractExpression(BinaryExpressionSyntax node)
        {
            return internalVisit_BinaryExpressionSyntax(node, "-");
        }

        protected override IValue VisitThisExpression(ThisExpressionSyntax node)
        {
            Debug.Assert((object)state.CurrentType != null);
            return new ThisExpression(state.CurrentType);
        }

        protected override IValue VisitTrueLiteralExpression(LiteralExpressionSyntax node)
        {
            return new ConstValue(true);
        }

        protected override IValue VisitTypeOfExpression(TypeOfExpressionSyntax node)
        {
            var g = ModelExtensions.GetTypeInfo(context.RoslynModel, node.Type);
            if (g.Type == null)
                throw new NotSupportedException();
            Type t = context.Roslyn_ResolveType(g.Type);
            return new TypeOfExpression(t);
        }
        // Private Methods 

        FunctionArgument[] _internalVisitArgumentList(BaseArgumentListSyntax list)
        {
            var aa = list.Arguments.Select(i => Visit(i) as FunctionArgument).ToArray();
#if DEBUG
            Debug.Assert(aa.Where(i => i == null).Count() == 0);
#endif
            return aa;
        }

        private IValue _Make_DotnetMethodCall(MethodBase mi, IValue targetObject, FunctionArgument[] functionArguments, Type[] genericTypes)
        {
            if (mi == null)
                throw new ArgumentNullException("mi");
            IValue result;
            bool isDelegate = mi.DeclaringType.IsMulticastDelegate();

            if (mi.IsGenericMethod)
            {
                var genericArguments = mi.GetGenericArguments();
                if (!mi.IsGenericMethodDefinition && (genericTypes == null || genericTypes.Length != genericArguments.Length))
                    genericTypes = genericArguments;
                if (genericTypes == null || genericTypes.Length != genericArguments.Length)
                    throw new NotSupportedException("Brak automatycznego rozpoznawania typów generycznych (na razie)");
                // var pa = mi.GetParameters();
            }
            else
                genericTypes = new Type[0];
            if (mi is MethodInfo)
            {
                var mii = mi as MethodInfo;
                if (mii.IsExtensionMethod())
                {
                    var list = functionArguments.ToList();
                    if (targetObject == null)
                        throw new NotSupportedException();
                    list.Insert(0, new FunctionArgument("", targetObject));
                    result = new CsharpMethodCallExpression(mii, null, list.ToArray(), genericTypes, isDelegate);
                }
                else
                {
#warning 'poprawić z typami generycznymi !!!!!'
                    result = new CsharpMethodCallExpression(mii, targetObject, functionArguments, genericTypes, isDelegate);
                }
            }
            else if (mi is ConstructorInfo)
            {
                // ReSharper disable once UnusedVariable
                var mii = mi as ConstructorInfo;
                throw new NotSupportedException();
            }
            else
                throw new NotSupportedException();
            result = Simplify(result);
            return result;
        }

        Type Gt(TypeSyntax s)
        {
            var identifierNameSyntax = s as IdentifierNameSyntax;
            if (identifierNameSyntax == null)
                throw new NotSupportedException();
            var name = _Name(identifierNameSyntax);
            if (name.IsGeneric)
                throw new NotSupportedException();
            var matchTypes = context.MatchTypes(name.BaseName, 0);
            if (matchTypes.Length == 1)
                return matchTypes[0];
            throw new NotSupportedException();
        }

        private IValue ImplicitConvert(IValue v, ModelExtensions2.MyInfo info)
        {
            var methodSymbol = info.GetMethodSymbol();
            if (methodSymbol == null)
                return v;
            var m = context.Roslyn_ResolveMethod(methodSymbol);
            var a = new CsharpMethodCallExpression(m as MethodInfo, null,
                new[] { new FunctionArgument("", v) },
                null, false
                );
            return a;

        }

        private IValue internalVisit_AssignWithPrefix(BinaryExpressionSyntax node, string _operator)
        {
            var symbolInfo = ModelExtensions.GetSymbolInfo(state.Context.RoslynModel, node);
            // var typex = GetResultTypeForBinaryExpression(symbolInfo);
            //TypeInfo ti = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node);

            //TypeInfo eti = ModelExtensions.GetTypeInfo(state.Context.RoslynModel, node.Expression);
            var eti2 = state.Context.RoslynModel.GetTypeInfo2(node);

            if (!eti2.Conversion1.HasValue || !eti2.Conversion1.Value.IsIdentity)
                throw new NotSupportedException();
            //if (symbolInfo.Symbol.IsImplicitlyDeclared)
            //    throw new Exception();
            var l = Visit(node.Left);
            var r = Visit(node.Right);
            var a = new CsharpAssignExpression(l, r, _operator);
            return Simplify(a);
        }

        /// <summary>
        /// Checks symbol taken from BinaryExpressionSyntax.
        /// </summary>
        /// <remarks>This methd probably is temporary solution because Roslyn is still under development and we cannot predict what will be in the future</remarks>
        /// <param name="symbolInfo"></param>
        private Type GetResultTypeForBinaryExpression(SymbolInfo symbolInfo)
        {
            if (symbolInfo.Symbol == null) return null;
            var symbol = symbolInfo.Symbol;
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    var methodSymbol = symbol as IMethodSymbol;
                    if (methodSymbol == null)
                        throw new Exception("expected symbol type is IMethodSymbol");
                    switch (methodSymbol.MethodKind)
                    {
                        case MethodKind.BuiltinOperator:
                            return state.Context.Roslyn_ResolveType(methodSymbol.ReturnType);
                        default:
                            throw new NotSupportedException();
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        //private IValue internalVisit_BoolBinaryExpression(BinaryExpressionSyntax node, string _operator)
        //{
        //    var symbolInfo = state.Context.RoslynModel.GetSymbolInfo(node);
        //    if (symbolInfo.Symbol != null)
        //        throw new NotSupportedException();

        //    var tmp = internalVisit_BinaryExpressionSyntax(node, _operator);
        //    var l = Visit(node.Left);
        //    var r = Visit(node.Right);
        //    var g = new BinaryOperatorExpression(l, r, _operator, typeof(bool), null);
        //    return g;
        //}
        private IValue InternalVisitTextIdentifier(string identifier)
        {
            var t = context.MatchTypes(identifier, 0);
            if (t.Length > 2)
                throw new NotSupportedException();
            if (t.Length == 1)
                return new TypeValue(t.First());

            var lv = context.Arguments.Where(i => i.Name == identifier).ToArray();
            if (lv.Length == 1)
                return new ArgumentExpression(lv.First().Name, lv.First().Type);

            var lv1 = context.LocalVariables.Where(i => i.Name == identifier).ToArray();
            if (lv1.Length == 1)
                return new LocalVariableExpression(lv1.First().Name, lv1.First().Type);

            if ((object)state.CurrentType != null)
            {
                var fi = TypesUtil.ListFields(state.CurrentType, identifier);
                if (fi.Length == 1)
                {
                    var tmp = fi[0];
                    if (tmp.IsStatic)
                        throw new NotSupportedException();
                    var a = new InstanceFieldAccessExpression(fi[0], new ThisExpression(state.CurrentType));
                    return Simplify(a);
                }

            }
            {
                var aaaa = state.Context.MatchTypes(identifier, 0);
                if (aaaa.Length == 1)
                    return new TypeValue(aaaa[0]);
            }
            return new UnknownIdentifierValue(identifier, new IValue[0]);
        }

        #endregion Methods

        #region Fields

        private CompileState state;

        #endregion Fields

        #region Properties

        private SemanticModel Model
        {
            get
            {
                return context.RoslynModel;
            }
        }

        #endregion Properties




        protected override IValue VisitUnaryMinusExpression(PrefixUnaryExpressionSyntax node)
        {
            var operand = Visit(node.Operand);
            // Debug.Assert(node.OperatorToken.Kind == SyntaxKind.MinusToken);
            Type t;
            if (TypesUtil.IsNumberType(operand.ValueType))
                t = operand.ValueType;
            else
                throw new NotSupportedException();
            var a = new UnaryOperatorExpression(operand, "-", t);
            return Simplify(a);
        }

        /*
              
        */
        /*
                protected override IValue VisitSubtractAssignExpression(BinaryExpressionSyntax node)
                {
                    return internalVisit_AssignWithPrefix(node, "-");
                }
        */
    }
}
