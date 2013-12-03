using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // if (ThrowNotImplementedException)
            var types = node.TypeArgumentList.Arguments.Select(i => Visit(i)).ToArray();
            var gt = _GetType(node.Identifier, types.Length);
            var a = gt.MakeGenericType(types);
            return a;
            // throw new NotImplementedException(string.Format("Method {0} is not supported", "VisitGenericName"));
            // return default(T);
        }

        protected override Type VisitIdentifierName(IdentifierNameSyntax node)
        {
            var id = node.Identifier;

#if ROSLYN
                // wzięte z [FAQ(1)]
                var model = state.Context.RoslynModel;
                var i1 = model.GetTypeInfo(node);
                TypeSymbol type = i1.Type;
                var _netType = state.Context.Roslyn_ResolveType(type);
                //var i2 = model.GetSymbolInfo(node); // alternatywnie
                //type = (TypeSymbol)i2.Symbol;
                return _netType;
#else           
            return _GetType(id, 0);
#endif
        }

        protected override Type VisitNullableType(NullableTypeSyntax node)
        {
            var t = state.Context.RoslynModel.GetTypeInfo(node);
            var tt = state.Context.Roslyn_ResolveType(t.Type);
            return tt;
            throw new NotSupportedException();
        }

        protected override Type VisitPredefinedType(PredefinedTypeSyntax node)
        {
            return TypesUtil.PRIMITIVE_TYPES[node.Keyword.ValueText];
        }
		// Private Methods 

        private Type _GetType(SyntaxToken id, int gen)
        {
            var name = id.ValueText;
            if (name == "var")
                return null;
            var types = context.MatchTypes(name, gen);
            if (types.Length != 1)
                throw new Exception("Unable to resolve type " + name);
            return types.First();
        }

		#endregion Methods 

		#region Fields 

        CompileState state;

		#endregion Fields 
    }
}
