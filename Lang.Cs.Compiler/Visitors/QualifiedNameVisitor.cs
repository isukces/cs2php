using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected override QualifiedNameVisitor.R VisitQualifiedName(Roslyn.Compilers.CSharp.QualifiedNameSyntax node)
        {
            var a = Visit(node.Left);
            var b = Visit(node.Right);
            if (a.IsGeneric || b.IsGeneric)
                throw new NotSupportedException();
            return new R() { BaseName = a.BaseName + "." + b.BaseName };
        }

        protected override QualifiedNameVisitor.R VisitIdentifierName(Roslyn.Compilers.CSharp.IdentifierNameSyntax node)
        {
            return new R() { BaseName = node.Identifier.ValueText };
        }
        protected override QualifiedNameVisitor.R VisitGenericName(Roslyn.Compilers.CSharp.GenericNameSyntax node)
        {
            var bn = node.Identifier.ValueText;
            return new R() { BaseName = bn, Generics = node.TypeArgumentList.Arguments.ToArray() };
        }
    }
}
