using System;
using System.Linq;
using Lang.Cs.Compiler.Sandbox;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lang.Cs.Compiler.Visitors
{
    public class TypeVisitor : CodeVisitor<Type>
    {
        #region Constructors

        public TypeVisitor(CompileState ts)
        {
            if (ts == null)
                ts = new CompileState();
            state = ts;
            context = ts.Context;
        }

        #endregion Constructors

        #region Methods

        // Protected Methods 

        protected override Type VisitArrayType(ArrayTypeSyntax node)
        {
            var ranks = node.RankSpecifiers.Select(i => i.Rank).ToArray();
            var et = Visit(node.ElementType);
            foreach (var i in ranks)
                et = et.MakeArrayType(i);
            return et;
        }

        protected override Type VisitGenericName(GenericNameSyntax node)
        {
            var symbolInfo = context.RoslynModel.GetSymbolInfo(node);
            if (!(symbolInfo.Symbol is ITypeSymbol)) 
                throw new NotSupportedException();
            var type = context.Roslyn_ResolveType(symbolInfo.Symbol as ITypeSymbol);
            return type;
        }

        protected override Type VisitIdentifierName(IdentifierNameSyntax node)
        {
            var id = node.Identifier;

#if ROSLYN
            // wzięte z [FAQ(1)]
            var model = state.Context.RoslynModel;
            var i1 = model.GetTypeInfo(node);
            ITypeSymbol type = i1.Type;
            var netType = state.Context.Roslyn_ResolveType(type);
            //var i2 = model.GetSymbolInfo(node); // alternatywnie
            //type = (TypeSymbol)i2.Symbol;
            return netType;
#else           
            return _GetType(id, 0);
#endif
        }

        protected override Type VisitNullableType(NullableTypeSyntax node)
        {
            var t = state.Context.RoslynModel.GetTypeInfo(node);
            var tt = state.Context.Roslyn_ResolveType(t.Type);
            return tt;
        }

        protected override Type VisitPredefinedType(PredefinedTypeSyntax node)
        {
            return TypesUtil.PRIMITIVE_TYPES[node.Keyword.ValueText];
        }
        // Private Methods 

        //[Obsolete]
        //private Type _GetType(SyntaxToken id, int gen)
        //{
        //    var name = id.ValueText;
        //    if (name == "var")
        //        return null;
        //    // var a = context.RoslynModel.GetTypeInfo()
        //    var types = context.MatchTypes(name, gen);
        //    if (types.Length != 1)
        //        throw new Exception("Unable to resolve type " + name);
        //    return types.First();
        //}

        #endregion Methods

        #region Fields

        CompileState state;

        #endregion Fields
    }
}
