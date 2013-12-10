using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs.Compiler.Visitors
{
    public class CodeVisitor<T>
    {
	    public LangParseContext context = new LangParseContext();
        public bool ThrowNotImplementedException = true;

        public virtual T Visit(SyntaxNode node)
        {
			if (node == null)
				return VisitNull();
            switch (node.Kind)
            {
                case SyntaxKind.DocumentationCommentTrivia:
                      return VisitDocumentationCommentTrivia(node as DocumentationCommentTriviaSyntax);
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
                case SyntaxKind.XmlAttribute:
                      return VisitXmlAttribute(node as XmlAttributeSyntax);
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
                case SyntaxKind.MemberAccessExpression:
                      return VisitMemberAccessExpression(node as MemberAccessExpressionSyntax);
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
                case SyntaxKind.AssignExpression:
                      return VisitAssignExpression(node as BinaryExpressionSyntax);
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
                case SyntaxKind.NegateExpression:
                      return VisitNegateExpression(node as PrefixUnaryExpressionSyntax);
                case SyntaxKind.NullLiteralExpression:
                      return VisitNullLiteralExpression(node as LiteralExpressionSyntax);
                case SyntaxKind.LogicalAndExpression:
                      return VisitLogicalAndExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.AddAssignExpression:
                      return VisitAddAssignExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.AsExpression:
                      return VisitAsExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.LogicalNotExpression:
                      return VisitLogicalNotExpression(node as PrefixUnaryExpressionSyntax);
                case SyntaxKind.SubtractExpression:
                      return VisitSubtractExpression(node as BinaryExpressionSyntax);
                case SyntaxKind.SubtractAssignExpression:
                      return VisitSubtractAssignExpression(node as BinaryExpressionSyntax);
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
                      return VisitForEachStatement(node as Roslyn.Compilers.CSharp.ForEachStatementSyntax);
                default: throw new NotSupportedException(node.Kind.ToString() + "," + node.GetType().Name);
            }
        }

		protected virtual T VisitNull()
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported", "VisitXmlAttribute"));
            return default(T);
        }

            
        protected virtual T VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDocumentationCommentTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIfDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitElifDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitElseDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEndIfDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitRegionDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEndRegionDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDefineDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitUndefDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitErrorDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitWarningDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLineDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPragmaWarningDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPragmaChecksumDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitReferenceDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBadDirectiveTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSkippedTokensTrivia", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlElementStartTag(XmlElementStartTagSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlElementStartTag", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlElementEndTag(XmlElementEndTagSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlElementEndTag", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlEmptyElement(XmlEmptyElementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlEmptyElement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlAttribute(XmlAttributeSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlAttribute", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlName(XmlNameSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlName", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlPrefix(XmlPrefixSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlPrefix", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlText(XmlTextSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlText", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlCDataSection(XmlCDataSectionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlCDataSection", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlComment(XmlCommentSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlComment", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitXmlProcessingInstruction", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIdentifierName", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitQualifiedName(QualifiedNameSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitQualifiedName", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTypeArgumentList(TypeArgumentListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTypeArgumentList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAliasQualifiedName", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPredefinedType(PredefinedTypeSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPredefinedType", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArrayRankSpecifier", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNullableType(NullableTypeSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNullableType", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitOmittedTypeArgument", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitParenthesizedExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitConditionalExpression(ConditionalExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitConditionalExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitInvocationExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitElementAccessExpression(ElementAccessExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitElementAccessExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArgumentList(ArgumentListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArgumentList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBracketedArgumentList(BracketedArgumentListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBracketedArgumentList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNameColon(NameColonSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNameColon", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCastExpression(CastExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCastExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAnonymousMethodExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSimpleLambdaExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitParenthesizedLambdaExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAnonymousObjectMemberDeclarator", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitObjectCreationExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAnonymousObjectCreationExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArrayCreationExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitImplicitArrayCreationExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitStackAllocArrayCreationExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitOmittedArraySizeExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitMemberAccessExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitThisExpression(ThisExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitThisExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBaseExpression(BaseExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBaseExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTypeOfExpression(TypeOfExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTypeOfExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSizeOfExpression(SizeOfExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSizeOfExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCheckedExpression(CheckedExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCheckedExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDefaultExpression(DefaultExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDefaultExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitMakeRefExpression(MakeRefExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitMakeRefExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitRefValueExpression(RefValueExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitRefValueExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitRefTypeExpression(RefTypeExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitRefTypeExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitQueryExpression(QueryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitQueryExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitQueryBody(QueryBodySyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitQueryBody", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitFromClause(FromClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitFromClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLetClause(LetClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLetClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitJoinClause(JoinClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitJoinClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitJoinIntoClause(JoinIntoClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitJoinIntoClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitWhereClause(WhereClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitWhereClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitOrderByClause(OrderByClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitOrderByClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSelectClause(SelectClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSelectClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGroupClause(GroupClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGroupClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitQueryContinuation(QueryContinuationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitQueryContinuation", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLocalDeclarationStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitVariableDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitVariableDeclarator", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEqualsValueClause(EqualsValueClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEqualsValueClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitExpressionStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEmptyStatement(EmptyStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEmptyStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLabeledStatement(LabeledStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLabeledStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGotoStatement(GotoStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGotoStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBreakStatement(BreakStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBreakStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitContinueStatement(ContinueStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitContinueStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitReturnStatement(ReturnStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitReturnStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitThrowStatement(ThrowStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitThrowStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitWhileStatement(WhileStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitWhileStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDoStatement(DoStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDoStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitForStatement(ForStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitForStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCheckedStatement(CheckedStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCheckedStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitUnsafeStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLockStatement(LockStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLockStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIfStatement(IfStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIfStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitElseClause(ElseClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitElseClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSwitchStatement(SwitchStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSwitchStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSwitchSection(SwitchSectionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSwitchSection", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTryStatement(TryStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTryStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCatchClause(CatchClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCatchClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCatchDeclaration(CatchDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCatchDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitFinallyClause(FinallyClauseSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitFinallyClause", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCompilationUnit(CompilationUnitSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCompilationUnit", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGlobalStatement(GlobalStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGlobalStatement", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNamespaceDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitUsingDirective(UsingDirectiveSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitUsingDirective", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitExternAliasDirective", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAttributeList(AttributeListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAttributeList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAttributeTargetSpecifier", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAttributeArgumentList(AttributeArgumentListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAttributeArgumentList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNameEquals(NameEqualsSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNameEquals", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitClassDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitStructDeclaration(StructDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitStructDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitInterfaceDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEnumDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDelegateDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBaseList(BaseListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBaseList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitConstructorConstraint(ConstructorConstraintSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitConstructorConstraint", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTypeConstraint(TypeConstraintSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTypeConstraint", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitExplicitInterfaceSpecifier", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEnumMemberDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitFieldDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEventFieldDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitMethodDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitOperatorDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitConversionOperatorDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitConstructorDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDestructorDeclaration(DestructorDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDestructorDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPropertyDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEventDeclaration(EventDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEventDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIndexerDeclaration(IndexerDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIndexerDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAccessorList(AccessorListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAccessorList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitParameterList(ParameterListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitParameterList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBracketedParameterList(BracketedParameterListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBracketedParameterList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTypeParameterList(TypeParameterListSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTypeParameterList", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitIncompleteMember(IncompleteMemberSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitIncompleteMember", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitEqualsExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitEqualsExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGetAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGetAccessorDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSetAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSetAccessorDeclaration", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitDivideExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitDivideExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAssignExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAssignExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAddExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAddExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitFalseLiteralExpression(LiteralExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitFalseLiteralExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCharacterLiteralExpression(LiteralExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCharacterLiteralExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitMultiplyExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitMultiplyExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitTrueLiteralExpression(LiteralExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitTrueLiteralExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLessThanExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLessThanExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPostIncrementExpression(PostfixUnaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPostIncrementExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGreaterThanExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGreaterThanExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNegateExpression(PrefixUnaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNegateExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNullLiteralExpression(LiteralExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNullLiteralExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLogicalAndExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLogicalAndExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAddAssignExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAddAssignExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitAsExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitAsExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLogicalNotExpression(PrefixUnaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLogicalNotExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSubtractExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSubtractExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitSubtractAssignExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitSubtractAssignExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGreaterThanOrEqualExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGreaterThanOrEqualExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLessThanOrEqualExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLessThanOrEqualExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitLogicalOrExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitLogicalOrExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitPostDecrementExpression(PostfixUnaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitPostDecrementExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitCollectionInitializerExpression(InitializerExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitCollectionInitializerExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitComplexElementInitializerExpression(InitializerExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitComplexElementInitializerExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitObjectInitializerExpression(InitializerExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitObjectInitializerExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNotEqualsExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNotEqualsExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitModuloExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitModuloExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBitwiseOrExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBitwiseOrExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArrayInitializerExpression(InitializerExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArrayInitializerExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBitwiseAndExpression(BinaryExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBitwiseAndExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitStringLiteralExpression(LiteralExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitStringLiteralExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitParameter(ParameterSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitParameter", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArrayType(ArrayTypeSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArrayType", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitNumericLiteralExpression(LiteralExpressionSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitNumericLiteralExpression", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitBlock(BlockSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitBlock", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitArgument(ArgumentSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitArgument", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitGenericName(GenericNameSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitGenericName", this.GetType().FullName));
            return default(T);
        }

        protected virtual T VisitForEachStatement(Roslyn.Compilers.CSharp.ForEachStatementSyntax node)
        {
            if (ThrowNotImplementedException)
                throw new NotImplementedException(string.Format("Method {0} is not supported in class {1}", "VisitForEachStatement", this.GetType().FullName));
            return default(T);
        }
    }
}
