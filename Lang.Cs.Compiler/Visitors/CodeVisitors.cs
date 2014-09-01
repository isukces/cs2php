using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Lang.Cs.Compiler.Visitors
{
    public class CodeVisitor<T>
    {
	    public LangParseContext context = new LangParseContext();
        public bool throwNotImplementedException = true;

        public virtual T Visit(SyntaxNode node)
        {
			if (node == null)
				return VisitNull();
            switch (node.CSharpKind())
            {
                case SyntaxKind.IfDirectiveTrivia:
                      return VisitIfDirectiveTrivia(node as IfDirectiveTriviaSyntax);
                case SyntaxKind.ElifDirectiveTrivia:
                      return VisitElifDirectiveTrivia(node as ElifDirectiveTriviaSyntax);
                case SyntaxKind.ElseDirectiveTrivia:
                      return VisitElseDirectiveTrivia(node as ElseDirectiveTriviaSyntax);
                case SyntaxKind.EndIfDirectiveTrivia:
                      return VisitEndIfDirectiveTrivia(node as EndIfDirectiveTriviaSyntax);
                case SyntaxKind.RegionDirectiveTrivia:
                      return VisitRegionDirectiveTrivia(node as RegionDirectiveTriviaSyntax);
                case SyntaxKind.EndRegionDirectiveTrivia:
                      return VisitEndRegionDirectiveTrivia(node as EndRegionDirectiveTriviaSyntax);
                case SyntaxKind.DefineDirectiveTrivia:
                      return VisitDefineDirectiveTrivia(node as DefineDirectiveTriviaSyntax);
                case SyntaxKind.UndefDirectiveTrivia:
                      return VisitUndefDirectiveTrivia(node as UndefDirectiveTriviaSyntax);
                case SyntaxKind.ErrorDirectiveTrivia:
                      return VisitErrorDirectiveTrivia(node as ErrorDirectiveTriviaSyntax);
                case SyntaxKind.WarningDirectiveTrivia:
                      return VisitWarningDirectiveTrivia(node as WarningDirectiveTriviaSyntax);
                case SyntaxKind.LineDirectiveTrivia:
                      return VisitLineDirectiveTrivia(node as LineDirectiveTriviaSyntax);
                case SyntaxKind.PragmaWarningDirectiveTrivia:
                      return VisitPragmaWarningDirectiveTrivia(node as PragmaWarningDirectiveTriviaSyntax);
                case SyntaxKind.PragmaChecksumDirectiveTrivia:
                      return VisitPragmaChecksumDirectiveTrivia(node as PragmaChecksumDirectiveTriviaSyntax);
                case SyntaxKind.ReferenceDirectiveTrivia:
                      return VisitReferenceDirectiveTrivia(node as ReferenceDirectiveTriviaSyntax);
                case SyntaxKind.BadDirectiveTrivia:
                      return VisitBadDirectiveTrivia(node as BadDirectiveTriviaSyntax);
                case SyntaxKind.SkippedTokensTrivia:
                      return VisitSkippedTokensTrivia(node as SkippedTokensTriviaSyntax);
                case SyntaxKind.XmlElementStartTag:
                      return VisitXmlElementStartTag(node as XmlElementStartTagSyntax);
                case SyntaxKind.XmlElementEndTag:
                      return VisitXmlElementEndTag(node as XmlElementEndTagSyntax);
                case SyntaxKind.XmlEmptyElement:
                      return VisitXmlEmptyElement(node as XmlEmptyElementSyntax);
                case SyntaxKind.XmlName:
                      return VisitXmlName(node as XmlNameSyntax);
                case SyntaxKind.XmlPrefix:
                      return VisitXmlPrefix(node as XmlPrefixSyntax);
                case SyntaxKind.XmlText:
                      return VisitXmlText(node as XmlTextSyntax);
                case SyntaxKind.XmlCDataSection:
                      return VisitXmlCDataSection(node as XmlCDataSectionSyntax);
                case SyntaxKind.XmlComment:
                      return VisitXmlComment(node as XmlCommentSyntax);
                case SyntaxKind.XmlProcessingInstruction:
                      return VisitXmlProcessingInstruction(node as XmlProcessingInstructionSyntax);
                case SyntaxKind.IdentifierName:
                      return VisitIdentifierName(node as IdentifierNameSyntax);
                case SyntaxKind.QualifiedName:
                      return VisitQualifiedName(node as QualifiedNameSyntax);
                case SyntaxKind.TypeArgumentList:
                      return VisitTypeArgumentList(node as TypeArgumentListSyntax);
                case SyntaxKind.AliasQualifiedName:
                      return VisitAliasQualifiedName(node as AliasQualifiedNameSyntax);
                case SyntaxKind.PredefinedType:
                      return VisitPredefinedType(node as PredefinedTypeSyntax);
                case SyntaxKind.ArrayRankSpecifier:
                      return VisitArrayRankSpecifier(node as ArrayRankSpecifierSyntax);
                case SyntaxKind.NullableType:
                      return VisitNullableType(node as NullableTypeSyntax);
                case SyntaxKind.OmittedTypeArgument:
                      return VisitOmittedTypeArgument(node as OmittedTypeArgumentSyntax);
                case SyntaxKind.ParenthesizedExpression:
                      return VisitParenthesizedExpression(node as ParenthesizedExpressionSyntax);
                case SyntaxKind.ConditionalExpression:
                      return VisitConditionalExpression(node as ConditionalExpressionSyntax);
                case SyntaxKind.InvocationExpression:
                      return VisitInvocationExpression(node as InvocationExpressionSyntax);
                case SyntaxKind.ElementAccessExpression:
                      return VisitElementAccessExpression(node as ElementAccessExpressionSyntax);
                case SyntaxKind.ArgumentList:
                      return VisitArgumentList(node as ArgumentListSyntax);
                case SyntaxKind.BracketedArgumentList:
                      return VisitBracketedArgumentList(node as BracketedArgumentListSyntax);
                case SyntaxKind.NameColon:
                      return VisitNameColon(node as NameColonSyntax);
                case SyntaxKind.CastExpression:
                      return VisitCastExpression(node as CastExpressionSyntax);
                case SyntaxKind.AnonymousMethodExpression:
                      return VisitAnonymousMethodExpression(node as AnonymousMethodExpressionSyntax);
                case SyntaxKind.SimpleLambdaExpression:
                      return VisitSimpleLambdaExpression(node as SimpleLambdaExpressionSyntax);
                case SyntaxKind.ParenthesizedLambdaExpression:
                      return VisitParenthesizedLambdaExpression(node as ParenthesizedLambdaExpressionSyntax);
                case SyntaxKind.AnonymousObjectMemberDeclarator:
                      return VisitAnonymousObjectMemberDeclarator(node as AnonymousObjectMemberDeclaratorSyntax);
                case SyntaxKind.ObjectCreationExpression:
                      return VisitObjectCreationExpression(node as ObjectCreationExpressionSyntax);
                case SyntaxKind.AnonymousObjectCreationExpression:
                      return VisitAnonymousObjectCreationExpression(node as AnonymousObjectCreationExpressionSyntax);
                case SyntaxKind.ArrayCreationExpression:
                      return VisitArrayCreationExpression(node as ArrayCreationExpressionSyntax);
                case SyntaxKind.ImplicitArrayCreationExpression:
                      return VisitImplicitArrayCreationExpression(node as ImplicitArrayCreationExpressionSyntax);
                case SyntaxKind.StackAllocArrayCreationExpression:
                      return VisitStackAllocArrayCreationExpression(node as StackAllocArrayCreationExpressionSyntax);
                case SyntaxKind.OmittedArraySizeExpression:
                      return VisitOmittedArraySizeExpression(node as OmittedArraySizeExpressionSyntax);
                case SyntaxKind.SimpleMemberAccessExpression:
                      return VisitSimpleMemberAccessExpression(node as MemberAccessExpressionSyntax);
                case SyntaxKind.ThisExpression:
                      return VisitThisExpression(node as ThisExpressionSyntax);
                case SyntaxKind.BaseExpression:
                      return VisitBaseExpression(node as BaseExpressionSyntax);
                case SyntaxKind.TypeOfExpression:
                      return VisitTypeOfExpression(node as TypeOfExpressionSyntax);
                case SyntaxKind.SizeOfExpression:
                      return VisitSizeOfExpression(node as SizeOfExpressionSyntax);
                case SyntaxKind.CheckedExpression:
                      return VisitCheckedExpression(node as CheckedExpressionSyntax);
                case SyntaxKind.DefaultExpression:
                      return VisitDefaultExpression(node as DefaultExpressionSyntax);
                case SyntaxKind.MakeRefExpression:
                      return VisitMakeRefExpression(node as MakeRefExpressionSyntax);
                case SyntaxKind.RefValueExpression:
                      return VisitRefValueExpression(node as RefValueExpressionSyntax);
                case SyntaxKind.RefTypeExpression:
                      return VisitRefTypeExpression(node as RefTypeExpressionSyntax);
                case SyntaxKind.QueryExpression:
                      return VisitQueryExpression(node as QueryExpressionSyntax);
                case SyntaxKind.QueryBody:
                      return VisitQueryBody(node as QueryBodySyntax);
                case SyntaxKind.FromClause:
                      return VisitFromClause(node as FromClauseSyntax);
                case SyntaxKind.LetClause:
                      return VisitLetClause(node as LetClauseSyntax);
                case SyntaxKind.JoinClause:
                      return VisitJoinClause(node as JoinClauseSyntax);
                case SyntaxKind.JoinIntoClause:
                      return VisitJoinIntoClause(node as JoinIntoClauseSyntax);
                case SyntaxKind.WhereClause:
                      return VisitWhereClause(node as WhereClauseSyntax);
                case SyntaxKind.OrderByClause:
                      return VisitOrderByClause(node as OrderByClauseSyntax);
                case SyntaxKind.SelectClause:
                      return VisitSelectClause(node as SelectClauseSyntax);
                case SyntaxKind.GroupClause:
                      return VisitGroupClause(node as GroupClauseSyntax);
                case SyntaxKind.QueryContinuation:
                      return VisitQueryContinuation(node as QueryContinuationSyntax);
                case SyntaxKind.LocalDeclarationStatement:
                      return VisitLocalDeclarationStatement(node as LocalDeclarationStatementSyntax);
                case SyntaxKind.VariableDeclaration:
                      return VisitVariableDeclaration(node as VariableDeclarationSyntax);
                case SyntaxKind.VariableDeclarator:
                      return VisitVariableDeclarator(node as VariableDeclaratorSyntax);
                case SyntaxKind.EqualsValueClause:
                      return VisitEqualsValueClause(node as EqualsValueClauseSyntax);
                case SyntaxKind.ExpressionStatement:
                      return VisitExpressionStatement(node as ExpressionStatementSyntax);
                case SyntaxKind.EmptyStatement:
                      return VisitEmptyStatement(node as EmptyStatementSyntax);
                case SyntaxKind.LabeledStatement:
                      return VisitLabeledStatement(node as LabeledStatementSyntax);
                case SyntaxKind.GotoStatement:
                      return VisitGotoStatement(node as GotoStatementSyntax);
                case SyntaxKind.BreakStatement:
                      return VisitBreakStatement(node as BreakStatementSyntax);
                case SyntaxKind.ContinueStatement:
                      return VisitContinueStatement(node as ContinueStatementSyntax);
                case SyntaxKind.ReturnStatement:
                      return VisitReturnStatement(node as ReturnStatementSyntax);
                case SyntaxKind.ThrowStatement:
                      return VisitThrowStatement(node as ThrowStatementSyntax);
                case SyntaxKind.WhileStatement:
                      return VisitWhileStatement(node as WhileStatementSyntax);
                case SyntaxKind.DoStatement:
                      return VisitDoStatement(node as DoStatementSyntax);
                case SyntaxKind.ForStatement:
                      return VisitForStatement(node as ForStatementSyntax);
                case SyntaxKind.CheckedStatement:
                      return VisitCheckedStatement(node as CheckedStatementSyntax);
                case SyntaxKind.UnsafeStatement:
                      return VisitUnsafeStatement(node as UnsafeStatementSyntax);
                case SyntaxKind.LockStatement:
                      return VisitLockStatement(node as LockStatementSyntax);
                case SyntaxKind.IfStatement:
                      return VisitIfStatement(node as IfStatementSyntax);
                case SyntaxKind.ElseClause:
                      return VisitElseClause(node as ElseClauseSyntax);
                case SyntaxKind.SwitchStatement:
                      return VisitSwitchStatement(node as SwitchStatementSyntax);
                case SyntaxKind.SwitchSection:
                      return VisitSwitchSection(node as SwitchSectionSyntax);
                case SyntaxKind.TryStatement:
                      return VisitTryStatement(node as TryStatementSyntax);
                case SyntaxKind.CatchClause:
                      return VisitCatchClause(node as CatchClauseSyntax);
                case SyntaxKind.CatchDeclaration:
                      return VisitCatchDeclaration(node as CatchDeclarationSyntax);
                case SyntaxKind.FinallyClause:
                      return VisitFinallyClause(node as FinallyClauseSyntax);
                case SyntaxKind.CompilationUnit:
                      return VisitCompilationUnit(node as CompilationUnitSyntax);
                case SyntaxKind.GlobalStatement:
                      return VisitGlobalStatement(node as GlobalStatementSyntax);
                case SyntaxKind.NamespaceDeclaration:
                      return VisitNamespaceDeclaration(node as NamespaceDeclarationSyntax);
                case SyntaxKind.UsingDirective:
                      return VisitUsingDirective(node as UsingDirectiveSyntax);
                case SyntaxKind.ExternAliasDirective:
                      return VisitExternAliasDirective(node as ExternAliasDirectiveSyntax);
                case SyntaxKind.AttributeList:
                      return VisitAttributeList(node as AttributeListSyntax);
                case SyntaxKind.AttributeTargetSpecifier:
                      return VisitAttributeTargetSpecifier(node as AttributeTargetSpecifierSyntax);
                case SyntaxKind.AttributeArgumentList:
                      return VisitAttributeArgumentList(node as AttributeArgumentListSyntax);
                case SyntaxKind.NameEquals:
                      return VisitNameEquals(node as NameEqualsSyntax);
                case SyntaxKind.ClassDeclaration:
                      return VisitClassDeclaration(node as ClassDeclarationSyntax);
                case SyntaxKind.StructDeclaration:
                      return VisitStructDeclaration(node as StructDeclarationSyntax);
                case SyntaxKind.InterfaceDeclaration:
                      return VisitInterfaceDeclaration(node as InterfaceDeclarationSyntax);
                case SyntaxKind.EnumDeclaration:
                      return VisitEnumDeclaration(node as EnumDeclarationSyntax);
                case SyntaxKind.DelegateDeclaration:
                      return VisitDelegateDeclaration(node as DelegateDeclarationSyntax);
                case SyntaxKind.BaseList:
                      return VisitBaseList(node as BaseListSyntax);
                case SyntaxKind.ConstructorConstraint:
                      return VisitConstructorConstraint(node as ConstructorConstraintSyntax);
                case SyntaxKind.TypeConstraint:
                      return VisitTypeConstraint(node as TypeConstraintSyntax);
                case SyntaxKind.ExplicitInterfaceSpecifier:
                      return VisitExplicitInterfaceSpecifier(node as ExplicitInterfaceSpecifierSyntax);
                case SyntaxKind.EnumMemberDeclaration:
                      return VisitEnumMemberDeclaration(node as EnumMemberDeclarationSyntax);
                case SyntaxKind.FieldDeclaration:
                      return VisitFieldDeclaration(node as FieldDeclarationSyntax);
                case SyntaxKind.EventFieldDeclaration:
                      return VisitEventFieldDeclaration(node as EventFieldDeclarationSyntax);
                case SyntaxKind.MethodDeclaration:
                      return VisitMethodDeclaration(node as MethodDeclarationSyntax);
                case SyntaxKind.OperatorDeclaration:
                      return VisitOperatorDeclaration(node as OperatorDeclarationSyntax);
                case SyntaxKind.ConversionOperatorDeclaration:
                      return VisitConversionOperatorDeclaration(node as ConversionOperatorDeclarationSyntax);
                case SyntaxKind.ConstructorDeclaration:
                      return VisitConstructorDeclaration(node as ConstructorDeclarationSyntax);
                case SyntaxKind.DestructorDeclaration:
                      return VisitDestructorDeclaration(node as DestructorDeclarationSyntax);
                case SyntaxKind.PropertyDeclaration:
                      return VisitPropertyDeclaration(node as PropertyDeclarationSyntax);
                case SyntaxKind.EventDeclaration:
                      return VisitEventDeclaration(node as EventDeclarationSyntax);
                case SyntaxKind.IndexerDeclaration:
                      return VisitIndexerDeclaration(node as IndexerDeclarationSyntax);
                case SyntaxKind.AccessorList:
                      return VisitAccessorList(node as AccessorListSyntax);
                case SyntaxKind.ParameterList:
                      return VisitParameterList(node as ParameterListSyntax);
                case SyntaxKind.BracketedParameterList:
                      return VisitBracketedParameterList(node as BracketedParameterListSyntax);
                case SyntaxKind.TypeParameterList:
                      return VisitTypeParameterList(node as TypeParameterListSyntax);
                case SyntaxKind.IncompleteMember:
                      return VisitIncompleteMember(node as IncompleteMemberSyntax);
                case SyntaxKind.EqualsExpression:
                      return VisitEqualsExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.GetAccessorDeclaration:
                      return VisitGetAccessorDeclaration(node as AccessorDeclarationSyntax);
                case SyntaxKind.SetAccessorDeclaration:
                      return VisitSetAccessorDeclaration(node as AccessorDeclarationSyntax);
                case SyntaxKind.DivideExpression:
                      return VisitDivideExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.AddExpression:
                      return VisitAddExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.FalseLiteralExpression:
                      return VisitFalseLiteralExpression(node as LiteralExpressionSyntax);
                case SyntaxKind.CharacterLiteralExpression:
                      return VisitCharacterLiteralExpression(node as LiteralExpressionSyntax);
                case SyntaxKind.MultiplyExpression:
                      return VisitMultiplyExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.TrueLiteralExpression:
                      return VisitTrueLiteralExpression(node as LiteralExpressionSyntax);
                case SyntaxKind.LessThanExpression:
                      return VisitLessThanExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.PostIncrementExpression:
                      return VisitPostIncrementExpression(node as PostfixUnaryExpressionSyntax);
                case SyntaxKind.GreaterThanExpression:
                      return VisitGreaterThanExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.NullLiteralExpression:
                      return VisitNullLiteralExpression(node as LiteralExpressionSyntax);
                case SyntaxKind.LogicalAndExpression:
                      return VisitLogicalAndExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.AsExpression:
                      return VisitAsExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.LogicalNotExpression:
                      return VisitLogicalNotExpression(node as PrefixUnaryExpressionSyntax);
                case SyntaxKind.SubtractExpression:
                      return VisitSubtractExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.GreaterThanOrEqualExpression:
                      return VisitGreaterThanOrEqualExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.LessThanOrEqualExpression:
                      return VisitLessThanOrEqualExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.LogicalOrExpression:
                      return VisitLogicalOrExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.PostDecrementExpression:
                      return VisitPostDecrementExpression(node as PostfixUnaryExpressionSyntax);
                case SyntaxKind.CollectionInitializerExpression:
                      return VisitCollectionInitializerExpression(node as InitializerExpressionSyntax);
                case SyntaxKind.ComplexElementInitializerExpression:
                      return VisitComplexElementInitializerExpression(node as InitializerExpressionSyntax);
                case SyntaxKind.ObjectInitializerExpression:
                      return VisitObjectInitializerExpression(node as InitializerExpressionSyntax);
                case SyntaxKind.NotEqualsExpression:
                      return VisitNotEqualsExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.ModuloExpression:
                      return VisitModuloExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.BitwiseOrExpression:
                      return VisitBitwiseOrExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.ArrayInitializerExpression:
                      return VisitArrayInitializerExpression(node as InitializerExpressionSyntax);
                case SyntaxKind.BitwiseAndExpression:
                      return VisitBitwiseAndExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.CaseSwitchLabel:
                      return VisitCaseSwitchLabel(node as SwitchLabelSyntax);
                case SyntaxKind.DefaultSwitchLabel:
                      return VisitDefaultSwitchLabel(node as SwitchLabelSyntax);
                case SyntaxKind.StringLiteralExpression:
                      return VisitStringLiteralExpression(node as LiteralExpressionSyntax);
                case SyntaxKind.Parameter:
                      return VisitParameter(node as ParameterSyntax);
                case SyntaxKind.ArrayType:
                      return VisitArrayType(node as ArrayTypeSyntax);
                case SyntaxKind.NumericLiteralExpression:
                      return VisitNumericLiteralExpression(node as LiteralExpressionSyntax);
                case SyntaxKind.Block:
                      return VisitBlock(node as BlockSyntax);
                case SyntaxKind.Argument:
                      return VisitArgument(node as ArgumentSyntax);
                case SyntaxKind.GenericName:
                      return VisitGenericName(node as GenericNameSyntax);
                case SyntaxKind.ForEachStatement:
                      return VisitForEachStatement(node as ForEachStatementSyntax);
                default: throw new NotSupportedException(node.CSharpKind().ToString() + "," + node.GetType().Name);
            }
        }

		protected virtual T VisitNull()
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported", "VisitXmlAttribute"));
            return default(T);
        }

            
        protected virtual T VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIfDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitElifDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitElseDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEndIfDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitRegionDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEndRegionDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDefineDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitUndefDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitErrorDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitWarningDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLineDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPragmaWarningDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPragmaChecksumDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitReferenceDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBadDirectiveTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSkippedTokensTrivia", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlElementStartTag(XmlElementStartTagSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlElementStartTag", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlElementEndTag(XmlElementEndTagSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlElementEndTag", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlEmptyElement(XmlEmptyElementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlEmptyElement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlName(XmlNameSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlName", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlPrefix(XmlPrefixSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlPrefix", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlText(XmlTextSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlText", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlCDataSection(XmlCDataSectionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlCDataSection", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlComment(XmlCommentSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlComment", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlProcessingInstruction", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIdentifierName", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitQualifiedName(QualifiedNameSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitQualifiedName", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTypeArgumentList(TypeArgumentListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTypeArgumentList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAliasQualifiedName", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPredefinedType(PredefinedTypeSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPredefinedType", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArrayRankSpecifier", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNullableType(NullableTypeSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNullableType", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitOmittedTypeArgument", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitParenthesizedExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitConditionalExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitInvocationExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitElementAccessExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArgumentList(ArgumentListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArgumentList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBracketedArgumentList(BracketedArgumentListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBracketedArgumentList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNameColon(NameColonSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNameColon", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCastExpression(CastExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCastExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAnonymousMethodExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSimpleLambdaExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitParenthesizedLambdaExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAnonymousObjectMemberDeclarator", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitObjectCreationExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAnonymousObjectCreationExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArrayCreationExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitImplicitArrayCreationExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitStackAllocArrayCreationExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitOmittedArraySizeExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSimpleMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSimpleMemberAccessExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitThisExpression(ThisExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitThisExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBaseExpression(BaseExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBaseExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTypeOfExpression(TypeOfExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTypeOfExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSizeOfExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCheckedExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDefaultExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitMakeRefExpression(MakeRefExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitMakeRefExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitRefValueExpression(RefValueExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitRefValueExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitRefTypeExpression(RefTypeExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitRefTypeExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitQueryExpression(QueryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitQueryExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitQueryBody(QueryBodySyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitQueryBody", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitFromClause(FromClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitFromClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLetClause(LetClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLetClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitJoinClause(JoinClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitJoinClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitJoinIntoClause(JoinIntoClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitJoinIntoClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitWhereClause(WhereClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitWhereClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitOrderByClause(OrderByClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitOrderByClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSelectClause(SelectClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSelectClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGroupClause(GroupClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGroupClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitQueryContinuation(QueryContinuationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitQueryContinuation", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLocalDeclarationStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitVariableDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitVariableDeclarator", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEqualsValueClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitExpressionStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEmptyStatement(EmptyStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEmptyStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLabeledStatement(LabeledStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLabeledStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGotoStatement(GotoStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGotoStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBreakStatement(BreakStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBreakStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitContinueStatement(ContinueStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitContinueStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitReturnStatement(ReturnStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitReturnStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitThrowStatement(ThrowStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitThrowStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitWhileStatement(WhileStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitWhileStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDoStatement(DoStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDoStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitForStatement(ForStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitForStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCheckedStatement(CheckedStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCheckedStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitUnsafeStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLockStatement(LockStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLockStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIfStatement(IfStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIfStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitElseClause(ElseClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitElseClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSwitchStatement(SwitchStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSwitchStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSwitchSection(SwitchSectionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSwitchSection", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTryStatement(TryStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTryStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCatchClause(CatchClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCatchClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCatchDeclaration(CatchDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCatchDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitFinallyClause(FinallyClauseSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitFinallyClause", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCompilationUnit(CompilationUnitSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCompilationUnit", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGlobalStatement(GlobalStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGlobalStatement", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNamespaceDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitUsingDirective(UsingDirectiveSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitUsingDirective", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitExternAliasDirective", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAttributeList(AttributeListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAttributeList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAttributeTargetSpecifier", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAttributeArgumentList(AttributeArgumentListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAttributeArgumentList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNameEquals(NameEqualsSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNameEquals", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitClassDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitStructDeclaration(StructDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitStructDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitInterfaceDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEnumDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDelegateDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBaseList(BaseListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBaseList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitConstructorConstraint(ConstructorConstraintSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitConstructorConstraint", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTypeConstraint(TypeConstraintSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTypeConstraint", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitExplicitInterfaceSpecifier", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEnumMemberDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitFieldDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEventFieldDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitMethodDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitOperatorDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitConversionOperatorDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitConstructorDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDestructorDeclaration(DestructorDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDestructorDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPropertyDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEventDeclaration(EventDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEventDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIndexerDeclaration(IndexerDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIndexerDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAccessorList(AccessorListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAccessorList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitParameterList(ParameterListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitParameterList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBracketedParameterList(BracketedParameterListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBracketedParameterList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTypeParameterList(TypeParameterListSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTypeParameterList", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIncompleteMember(IncompleteMemberSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIncompleteMember", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEqualsExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEqualsExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGetAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGetAccessorDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSetAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSetAccessorDeclaration", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDivideExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDivideExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAddExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAddExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitFalseLiteralExpression(LiteralExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitFalseLiteralExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCharacterLiteralExpression(LiteralExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCharacterLiteralExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitMultiplyExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitMultiplyExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTrueLiteralExpression(LiteralExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTrueLiteralExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLessThanExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLessThanExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPostIncrementExpression(PostfixUnaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPostIncrementExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGreaterThanExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGreaterThanExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNullLiteralExpression(LiteralExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNullLiteralExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLogicalAndExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLogicalAndExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAsExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAsExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLogicalNotExpression(PrefixUnaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLogicalNotExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSubtractExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSubtractExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGreaterThanOrEqualExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGreaterThanOrEqualExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLessThanOrEqualExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLessThanOrEqualExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLogicalOrExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLogicalOrExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPostDecrementExpression(PostfixUnaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPostDecrementExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCollectionInitializerExpression(InitializerExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCollectionInitializerExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitComplexElementInitializerExpression(InitializerExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitComplexElementInitializerExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitObjectInitializerExpression(InitializerExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitObjectInitializerExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNotEqualsExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNotEqualsExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitModuloExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitModuloExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBitwiseOrExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBitwiseOrExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArrayInitializerExpression(InitializerExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArrayInitializerExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBitwiseAndExpression(BinaryExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBitwiseAndExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCaseSwitchLabel(SwitchLabelSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCaseSwitchLabel", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDefaultSwitchLabel(SwitchLabelSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDefaultSwitchLabel", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitStringLiteralExpression(LiteralExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitStringLiteralExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitParameter(ParameterSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitParameter", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArrayType(ArrayTypeSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArrayType", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNumericLiteralExpression(LiteralExpressionSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNumericLiteralExpression", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBlock(BlockSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBlock", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArgument(ArgumentSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArgument", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGenericName(GenericNameSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGenericName", GetType().FullName));
            return default(T);
        }

        protected virtual T VisitForEachStatement(ForEachStatementSyntax node)
        {
            if (throwNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitForEachStatement", GetType().FullName));
            return default(T);
        }
    }
}
