using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lang.Cs.Compiler.Visitors
{
    public class QualifiedNameVisitor : CodeVisitor<QualifiedNameVisitor.R>
    {
        public class R
        {
            public string GetNonGeneric()
            {
                if (IsGeneric)
                    throw new ArgumentException();
                return BaseName;
            }
            public string BaseName { get; set; }
            public TypeSyntax[] Generics { get; set; }
            public bool IsGeneric
            {
                get
                {
                    return Generics != null && Generics.Any();
                }
            }
        }
        protected override R VisitQualifiedName(QualifiedNameSyntax node)
        {
            var a = Visit(node.Left);
            var b = Visit(node.Right);
            if (a.IsGeneric || b.IsGeneric)
                throw new NotSupportedException();
            return new R
            {
                BaseName = a.BaseName + "." + b.BaseName
            };
        }

        protected override R VisitIdentifierName(IdentifierNameSyntax node)
        {
            return new R
            {
                BaseName = node.Identifier.ValueText
            };
        }
        protected override R VisitGenericName(GenericNameSyntax node)
        {
            var bn = node.Identifier.ValueText;
            return new R
            {
                BaseName = bn, 
                Generics = node.TypeArgumentList.Arguments.ToArray()
            };
        }
    }
}
