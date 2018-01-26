#define _UNUSED
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Lang.Cs.Compiler.Visitors
{
    public class LangVisitor : CodeVisitor<object>
    {
        public LangVisitor(CompileState ts)
        {
            if (ts == null)
                ts = new CompileState();
            state = ts;
            context = ts.Context;
        }

        // Private Methods 

        private static QualifiedNameVisitor.R _Name(NameSyntax namr)
        {
            return new QualifiedNameVisitor().Visit(namr);
        }

        // Public Methods 

        public LangType _ResolveLangType(SyntaxNode name)
        {
            //if (name is ExpressionSyntax)
            //{
            //    var info = context.RoslynModel.GetSymbolInfo(name as ExpressionSyntax);
            //    var symbol = info.Symbol as TypeSymbol;
            //    if (symbol == null)
            //        throw new NotSupportedException();
            //}
            var a = _ResolveTypeX(name);
            return new LangType(a);
        }

        public FunctionDeclarationParameter[] rVisit(ParameterListSyntax s)
        {
            IParameterSymbol[] symbols = (from i in s.Parameters
                                          let symbol = ModelExtensions.GetDeclaredSymbol(context.RoslynModel, i) as IParameterSymbol
                                          select symbol
                         ).ToArray();
            if (symbols.Length == 0)
                return new FunctionDeclarationParameter[0];
            if (symbols.Any(i => i == null))
                throw new Exception("Jakieś nule");
            return rVisit(symbols);
        }
        // Protected Methods 

        protected override object VisitAccessorList(AccessorListSyntax node)
        {
            throw new NotImplementedException();
        }

#if UNUSED
        protected override object VisitAddAssignExpression(BinaryExpressionSyntax node)
        {
            var g = _VisitExpression(node);
            return g;
        }
#endif
        protected override object VisitAddExpression(BinaryExpressionSyntax node)
        {
            throw new Exception("Why am I here???");
        }

        [Obsolete]
        protected override object VisitArgument(ArgumentSyntax node)
        {
            throw new Exception("Why am I here???");
            var a = _VisitExpression(node);
            return _OptimizeIValue(a);
        }

        [Obsolete]
        protected override object VisitArgumentList(ArgumentListSyntax node)
        {
            throw new Exception("Why am I here???");
            return node.Arguments.Select(i => Visit(i) as FunctionArgument).ToArray();
        }
        protected override object VisitSimpleAssignmentExpression(BinaryExpressionSyntax node)
        {
            Console.WriteLine("aa");
            return base.VisitSimpleAssignmentExpression(node);
        }



        // override visitf
        protected override object VisitBlock(BlockSyntax node)
        {
            return context.DoWithLocalVariables(null,
                () =>
                {
                    var a = node.Statements.Where(u => !(u is EmptyStatementSyntax)).Select(i => Visit(i) as IStatement).ToArray();
                    return new CodeBlock(a);
                });
        }
        protected override object VisitSwitchStatement(SwitchStatementSyntax node)
        {
            var expression = _VisitExpression(node.Expression);
            List<CsharpSwichSection> sections = new List<CsharpSwichSection>(node.Sections.Count);
            foreach (var csSection in node.Sections)
            {
                CsharpSwichLabel[] labels = csSection.Labels.Select(a => _VisitExpression(a)).Cast<CsharpSwichLabel>().ToArray();
                IStatement[] statements = csSection.Statements.Select(a => Visit(a)).Cast<IStatement>().ToArray();
                CsharpSwichSection section = new CsharpSwichSection(labels, statements);
                sections.Add(section);
            }
            return new CsharpSwitchStatement(expression, sections.ToArray());
        }
        protected override object VisitBreakStatement(BreakStatementSyntax node)
        {
            return new BreakStatement();
        }

        protected override object VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            var aaa = ModelExtensions.GetDeclaredSymbol(context.RoslynModel, node);
            var name = _Name(node.Identifier);
            context.ClassNames.Push(name);
            try
            {
                var fulName = state.Context.CurrentNamespace + "." + name;
                state.CurrentType = context.KnownTypes.Where(i => i.FullName == fulName).FirstOrDefault();
                try
                {
                    var statements1 = Visit(node.Members);
                    var wrong = statements1.Where(i => !(i is IClassMember)).ToArray();
                    if (wrong.Any())
                        throw new Exception(wrong[0].GetType().FullName);
                    return new InterfaceDeclaration(name, statements1.Cast<IClassMember>().ToArray());
                }
                finally
                {
                    state.CurrentType = null;
                }
            }
            finally
            {
                context.ClassNames.Pop();

            }
        }

        protected override object VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            // MemberDeclarationSyntax
            var aaa = ModelExtensions.GetDeclaredSymbol(context.RoslynModel, node);
            // var literalInfo = model.GetTypeInfo(helloWorldString);

            //if (context.ClassNames.Count != 0)
            //    throw new NotSupportedException("Nested class");
            var name = _Name(node.Identifier);
            context.ClassNames.Push(name);
            try
            {
                var fulName = state.Context.CurrentNamespace + "." + name;
                state.CurrentType = context.KnownTypes.Where(i => i.FullName == fulName).FirstOrDefault();
                try
                {
                    var statements1 = Visit(node.Members);
                    var wrong = statements1.Where(i => !(i is IClassMember)).ToArray();
                    if (wrong.Any())
                        throw new Exception(wrong[0].GetType().FullName);
                    return new ClassDeclaration(name, statements1.Cast<IClassMember>().ToArray());
                }
                finally
                {
                    state.CurrentType = null;
                }
            }
            finally
            {
                context.ClassNames.Pop();

            }
        }

        protected override object VisitCompilationUnit(CompilationUnitSyntax node)
        {
            var usingList = node.Usings.Select(i => Visit(i)).OfType<ImportNamespace>().ToArray();
            var tmp = context.ImportedNamespaces;
            context.ImportedNamespaces = usingList.ToList();
            try
            {
                var code = Visit(node.Members).OfType<NamespaceDeclaration>().ToArray();
                var a = new CompilationUnit(usingList, code);
                return a;
            }
            finally
            {
                context.ImportedNamespaces = tmp;
            }
        }

        protected override object VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            // FunctionDeclarationParameter[] Parameters;
            // throw new NotSupportedException();

            var methodSymbol = (IMethodSymbol)ModelExtensions.GetDeclaredSymbol(context.RoslynModel, node);


            var mi = context.Roslyn_ResolveMethod(methodSymbol) as ConstructorInfo;
            if (mi == null)
                throw new NotSupportedException();

#warning 'Zarejestrować nazwy parametrów'
            var body = Visit(node.Body) as IStatement;
            var con = new ConstructorDeclaration(mi, body);
            return con;

            //var name = _Name(node.Identifier);
            //var mod = VisitModifiers(node.Modifiers);
            //// var rt = _ResolveLangType(node.ReturnType);
            ////var p1 = Visit(node.ParameterList);
            //var Parameters = rVisit(node.ParameterList); // Visit(node.ParameterList) as FunctionDeclarationParameter[];


            //var old = context.Arguments.ToArray();
            //context.Arguments.AddRange(Parameters);


            //var body = Visit(node.Body) as IStatement;
            //context.Arguments = old.ToList();

            //return new MethodDeclaration();
            //    name,
            //    mod,
            //    null,
            //    Parameters,
            //    body);
            //throw new NotImplementedException();

            //return base.VisitConstructorDeclaration(node);
        }

        protected override object VisitContinueStatement(ContinueStatementSyntax node)
        {
            return new ContinueStatement();
        }

        protected override object VisitElseClause(ElseClauseSyntax node)
        {
            return Visit(node.Statement);
            return base.VisitElseClause(node);
        }

        protected override object VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            var name = _Name(node.Identifier);
            var ed = new EnumDeclaration(name);
            return ed;
        }

        protected override object VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            return Visit(node.Value);
        }

        protected override object VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            var e = _VisitExpression(node.Expression);
            if (e is IStatement)
                return e;
            //return new Lang.Cs.Compiler.state
            throw new NotImplementedException();
        }

        protected override object VisitFieldDeclaration(FieldDeclarationSyntax node)
        {

            var mod = VisitModifiers(node.Modifiers);

            var type1 = _VisitExpression(node.Declaration.Type);
            Debug.Assert(type1 is TypeValue);
            var type = type1 as TypeValue;
            var variables = node.Declaration.Variables.Select(i => Visit(i)).OfType<VariableDeclarator>().ToArray();
            return new FieldDeclaration(new LangType(type.DotnetType), variables, mod);
        }

        protected override object VisitForEachStatement(ForEachStatementSyntax node)
        {
            var collection = _VisitExpression(node.Expression);
#if ROSLYN
            var info1 = ModelExtensions.GetDeclaredSymbol(context.RoslynModel, node);
            var info = info1 as ILocalSymbol;
            if (info == null)
                throw new ArgumentNullException("info");
            Type rosType = context.Roslyn_ResolveType(info.Type);
            var loopVariableType = new LangType(rosType);
            string loopVariableName = info.Name;
#else
            var type = _VisitExpression(node.Type);   
            if (type is UnknownIdentifierValue)
            {
                var xx = type as UnknownIdentifierValue;
                if (xx.Identifier == "var")
                {
                    var et = TypesUtil.GetEnumerateItemType(collection.ValueType);
                    type = new TypeValue(et);
                }
                else throw new NotSupportedException();
            }
            Debug.Assert(type is TypeValue);
            Debug.Assert((type as TypeValue).DotnetType == rosType);
            var loopVariableType = new LangType((type as TypeValue).DotnetType);
              var loopVariableName = _Name(node.Identifier);
#endif

            VariableDeclaration ps = new VariableDeclaration(
                 loopVariableType,
                new VariableDeclarator[] {
                    new VariableDeclarator(loopVariableName, null, null)
                    }

                );
            return context.DoWithLocalVariables(ps,
                () =>
                {
                    var stat = Visit(node.Statement);
                    Debug.Assert(stat is IStatement);
                    var g = new ForEachStatement(loopVariableType, loopVariableName, collection, stat as IStatement);
                    return g;
                });



        }

        protected override object VisitForStatement(ForStatementSyntax node)
        {
            var declaration1 = Visit(node.Declaration);
            var declaration = declaration1 as VariableDeclaration;
            Debug.Assert(declaration != null);


            return state.Context.DoWithLocalVariables(declaration,
                () =>
                {
                    IValue condition = _VisitExpression(node.Condition);
                    object[] incrementors1 = node.Incrementors.Select(i => Visit(i)).ToArray();
                    IStatement[] incrementors = incrementors1.OfType<IStatement>().ToArray();
                    Debug.Assert(incrementors1.Length == incrementors.Length);
                    var statement1 = Visit(node.Statement);
                    Debug.Assert(statement1 is IStatement);
                    return new ForStatement(declaration, condition, statement1 as IStatement, incrementors);

                });
            throw new NotSupportedException();
        }

        protected override object VisitGetAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            return InternalVisitAccessorDeclaration(node);
        }

        protected override object VisitIdentifierName(IdentifierNameSyntax node)
        {
            return _Name(node.Identifier);
        }

        protected override object VisitIfStatement(IfStatementSyntax node)
        {
            var condition = _VisitExpression(node.Condition);
            var s1 = node.Statement == null ? null : Visit(node.Statement);
            var s2 = node.Else == null ? null : Visit(node.Else);
            Debug.Assert(s1 == null || s1 is IStatement);
            Debug.Assert(s2 == null || s2 is IStatement);
            var a = new IfStatement(condition, s1 as IStatement, s2 as IStatement);
            return a;
        }

        protected override object VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            return new ValueVisitor(state).Visit(node);
        }

        protected override object VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            var isConst = node.IsConst;
            var isFixed = node.Modifiers.Any(SyntaxKind.FixedKeyword);
            var v = Visit(node.Declaration) as VariableDeclaration;
            foreach (var i in v.Declarators)
            {
                context.LocalVariables.Add(new NameType(i.Name, v.Type));
            }
            return new LocalDeclarationStatement(isConst, isFixed, v);
        }

#if UNUSED
        protected override object VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var name = _Name(node.Name).GetNonGeneric();
            var expression = _VisitExpression(node.Expression);
            IValue TMP = new MemberAccessExpression(name, expression);
            return _OptimizeIValue(TMP);
        }
#endif

        protected override object VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // FunctionDeclarationParameter[] Parameters;
            var doc = DeclarationItemDescription.Parse(node);
            var headerInfo = context.GetMethodMethodHeaderInfo(node);


#warning 'Zarejestrować nazwy parametrów'
            var body = Visit(node.Body) as IStatement;


            return new MethodDeclaration(headerInfo.MethodInfo, body);
#if OLD
            Parameters = rVisit(methodSymbol.Parameters.AsEnumerable());

            //var rrt = methodSymbol.ReturnType;
            //var aaaa = context.RoslynCompilation.GetTypeByMetadataName(rrt.Name);
            //var fullTypeName = methodSymbol.ReturnType.ToDisplayString();
            //// LangType rt = new LangType();
            //var hhh = methodSymbol.Parameters.ToArray();

            var name = _Name(node.Identifier);
            var mod = VisitModifiers(node.Modifiers);
            var rt = _ResolveLangType(node.ReturnType);


            //var p1 = Visit(node.ParameterList);
            //   var Parameters = Visit(node.ParameterList) as FunctionDeclarationParameter[];


            var old = context.Arguments.ToArray();
            context.Arguments.AddRange(Parameters);


       

            context.Arguments = old.ToList();

            return new MethodDeclaration(
                name,
                mod,
                rt,
                Parameters,
                body);
            throw new NotImplementedException();
#endif
        }
#if UNUSED
        protected override object VisitMultiplyAssignExpression(BinaryExpressionSyntax node)
        {
            var g = _VisitExpression(node);
            return g;
        }
#endif

        protected override object VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var name = _Name(node.Name).GetNonGeneric();
            var tmp = context.CurrentNamespace;
            context.CurrentNamespace = name;
            try
            {
                var statements = Visit(node.Members).Cast<INamespaceMember>().ToArray();
                return new NamespaceDeclaration(name, statements);
            }
            finally
            {
                context.CurrentNamespace = tmp;
            }
        }

        protected override object VisitNull()
        {
            return new ConstValue(null);
        }

        protected override object VisitNumericLiteralExpression(LiteralExpressionSyntax node)
        {
            var a = _Name(node.Token);
            return new ConstValue(node.Token.Value);
        }

        protected override object VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            throw new Exception("Nie powinno tutaj być");
            //var type = _ResolveLangType(node.Type);
            //var arg = Visit(node.ArgumentList) as FunctionArgument[];
            //return new CallConstructor(type, arg);
        }

        protected override object VisitParameter(ParameterSyntax node)
        {
            // throw new Exception("Roslyn should do this");
            var name = _Name(node.Identifier);
            Modifiers mod = VisitModifiers(node.Modifiers);
            LangType t = node.Type == null ? null : _ResolveLangType(node.Type);
            var _default = node.Default == null ? null : _VisitExpression(node.Default);
            return new FunctionDeclarationParameter(name, mod, t, _default as IValue);

        }

        protected override object VisitParameterList(ParameterListSyntax node)
        {
            // throw new Exception("Roslyn should do this");
            var aaa = node.Parameters.Select(i => Visit(i)).Cast<FunctionDeclarationParameter>().ToArray();
            return aaa;
        }

        protected override object VisitPostIncrementExpression(PostfixUnaryExpressionSyntax node)
        {
            var a = _VisitExpression(node);
            Debug.Assert(a is IStatement);
            return a;
        }

        protected override object VisitPredefinedType(PredefinedTypeSyntax node)
        {
            var name = _Name(node.Keyword);
            var type = _ResolveLangType(node);
            return type;
        }

        protected override object VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var info = ModelExtensions.GetSymbolInfo(context.RoslynModel, node.Type);
            var symbol = info.Symbol as ITypeSymbol;
            if (symbol == null)
                throw new NotSupportedException();
            var tt = context.Roslyn_ResolveType(symbol);

            var propertyName = _Name(node.Identifier);
            var mod = VisitModifiers(node.Modifiers);
            var accessors = node.AccessorList.Accessors.Select(i => Visit(i) as CsharpPropertyDeclarationAccessor).ToArray();
            var type = _ResolveLangType(node.Type);

            var doc = DeclarationItemDescription.Parse(node);
            var declaration = new CsharpPropertyDeclaration(propertyName, type, accessors, mod, doc);
            return declaration;

        }

        protected override object VisitReturnStatement(ReturnStatementSyntax node)
        {
            IValue v = node.Expression == null ? null : _VisitExpression(node.Expression);
            return new ReturnStatement(v);
        }

        protected override object VisitSetAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            return InternalVisitAccessorDeclaration(node);
        }

        protected override object VisitStringLiteralExpression(LiteralExpressionSyntax node)
        {
            return new ConstValue(_Name(node.Token));
        }

        protected override object VisitThrowStatement(ThrowStatementSyntax node)
        {
            return base.VisitThrowStatement(node);
        }

        protected override object VisitUsingDirective(UsingDirectiveSyntax node)
        {
            var name = _Name(node.Name).GetNonGeneric();
            var alias = node.Alias == null ? "" : _Name(node.Alias.Name).GetNonGeneric();
            return new ImportNamespace(name, alias);

        }

        protected override object VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            LangType t = _ResolveLangType(node.Type);
#if ROSLYN
            //if (t == null || t.DotnetType == null)
            //    throw new Exception("Roslyn powinien tozwiązać ten typ");
#endif
            var vars = node.Variables.Select(Visit).OfType<VariableDeclarator>().ToArray();
            if ((object)t.DotnetType == null)
            {
                if (vars.Any())
                    t = new LangType(vars[0].Value.ValueType);
                // Console.ForegroundColor
                Console.WriteLine("Brak typu dla {0} => {1}", node.ToString(), t);
            }
            return new VariableDeclaration(t, vars);
        }

        protected override object VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            var si = (ModelExtensions.GetDeclaredSymbol(state.Context.RoslynModel, node));
            // var ti = state.Context.RoslynModel.GetTypeInfo(node);
            FieldInfo fi = null;
            if (si is IFieldSymbol)
            {
                fi = state.Context.Roslyn_ResolveField(si as IFieldSymbol);
            }
            var name = _Name(node.Identifier);
            var value = _VisitExpression(node.Initializer); // Visit(node.Initializer) as IValue;


            // FAQ 2
            // TypeSymbol type = ((LocalSymbol)state.Context.RoslynModel.GetDeclaredSymbol(node)).Type;
            return new VariableDeclarator(name, value, fi);
        }

        protected override object VisitWhileStatement(WhileStatementSyntax node)
        {
            var condition = _VisitExpression(node.Condition);
            var s1 = node.Statement == null ? null : Visit(node.Statement);

            Debug.Assert(s1 == null || s1 is IStatement);

            var a = new WhileStatement(condition, s1 as IStatement);
            return a;
        }
        // Private Methods 

        static string _Name(SyntaxToken a)
        {
            return a.ValueText;
        }

        static IValue _OptimizeIValue(IValue n)
        {
            n = ExpressionConverterVisitor.Visit(n);
            return n;
        }

        private string _ResolveType(SyntaxNode name)
        {
            var a = _ResolveTypeX(name);
            return (object)a == null ? "var" : a.FullName;
        }

        private Type _ResolveTypeX(SyntaxNode name)
        {
            // We can do this directly 
            TypeVisitor v = new TypeVisitor(state);
            return v.Visit(name);
        }

        IValue _VisitExpression(SyntaxNode n)
        {
            ValueVisitor vv = new ValueVisitor(state);
            var tmp = vv.Visit(n);
            return tmp;
        }


        private object InternalVisitAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            var name = _Name(node.Keyword);
            IStatement code = null;
            if (node.Body != null)
            {
                var tmp = Visit(node.Body);
                if (tmp == null)
                    throw new NotSupportedException();
                code = tmp as IStatement;
                if (code == null)
                    throw new NotSupportedException();
            }
            return new CsharpPropertyDeclarationAccessor(name, null, code);
        }

        FunctionDeclarationParameter[] rVisit(IEnumerable<IParameterSymbol> s)
        {
            return s.Select(i => rVisit(i)).ToArray();
        }

        static Modifiers rVisit(RefKind refKind)
        {
            switch (refKind)
            {
                case RefKind.Out:
                    return new Modifiers(new[] { "out" });
                case RefKind.Ref:
                    return new Modifiers(new[] { "ref" });
                default:
                    return new Modifiers(new string[0]);
            }
        }

        FunctionDeclarationParameter rVisit(IParameterSymbol s)
        {
            string name = s.Name;
            Modifiers m = rVisit(s.RefKind);
            var type = context.Roslyn_ResolveType(s.Type);
            IValue initial = null;
            if (s.HasExplicitDefaultValue)
                initial = new ConstValue(s.ExplicitDefaultValue);

            var a = new FunctionDeclarationParameter(name, m, new LangType(type), initial);
            return a;
        }

        protected override object VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            var doc = DeclarationItemDescription.Parse(node);
            var methodSymbol = (IMethodSymbol)ModelExtensions.GetDeclaredSymbol(context.RoslynModel, node);


            var mi = context.Roslyn_ResolveMethod(methodSymbol) as MethodInfo;
            if (mi == null)
                throw new NotSupportedException();

#warning 'Zarejestrować nazwy parametrów'
            var body = Visit(node.Body) as IStatement;


            return new MethodDeclaration(mi, body);

        }
        protected override object VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            var doc = DeclarationItemDescription.Parse(node);
            var methodSymbol = (IMethodSymbol)ModelExtensions.GetDeclaredSymbol(context.RoslynModel, node);


            var mi = context.Roslyn_ResolveMethod(methodSymbol) as MethodInfo;
            if (mi == null)
                throw new NotSupportedException();

#warning 'Zarejestrować nazwy parametrów'
            var body = Visit(node.Body) as IStatement;


            return new MethodDeclaration(mi, body);
        }

        

        object[] Visit(IEnumerable<MemberDeclarationSyntax> i)
        {
            List<object> res = new List<object>();
            foreach (var ii in i)
            {
                try
                {
                    var j = Visit(ii);
                    res.Add(j);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("While processing {0}/{1}\r\n{2}", state.CurrentType, ii, e), e);
                }
            }
            return res.ToArray();
        }

        static Modifiers VisitModifiers(SyntaxTokenList mod)
        {
            //var isStatic = mod.Where(u => u.ValueText == "static").Any();
            //var isPublic = mod.Where(u => u.ValueText == "public").Any();
            //var isRef = mod.Where(u => u.ValueText == "ref").Any();
            return new Modifiers(mod.Select(i => i.ValueText).ToArray());
        }

        private CompileState state;
    }
}
