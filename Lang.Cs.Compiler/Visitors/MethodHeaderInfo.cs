using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lang.Cs.Compiler.Visitors
{
    /// <summary>
    ///     Provides information about method header
    /// </summary>
    internal class MethodHeaderInfo
    {
        public MethodHeaderInfo()
        {
            TypesToPassAsArguments = new List<A>();
        }

        // Public Methods 

        public static MethodHeaderInfo Get(LangParseContext context, MethodDeclarationSyntax node)
        {
            var methodSymbol = (IMethodSymbol)context.RoslynModel.GetDeclaredSymbol(node);

            var methodInfo = context.Roslyn_ResolveMethod(methodSymbol) as MethodInfo;
            if (methodInfo == null)
                throw new NotSupportedException();

            var tmp = node.ConstraintClauses
                .Select(a => ConvertTypeParameterConstraints(a, context))
                .ToArray();
            return new MethodHeaderInfo
            {
                MethodInfo               = methodInfo,
                TypeParameterConstraints = tmp
            };
        }
        // Private Methods 

        private static ConstraintInfo Convert2(TypeParameterConstraintSyntax src, LangParseContext context)
        {
            if (src is ConstructorConstraintSyntax)
                return new ConstraintInfo
                {
                    Kind = ConstraintKind.ParameterlessConstructor
                };
            throw new NotSupportedException(src.GetType().ToString());
        }

        private static TypeParameterConstraintInfo ConvertTypeParameterConstraints(
            TypeParameterConstraintClauseSyntax x, LangParseContext context)
        {
            var info = context.RoslynModel.GetTypeInfo(x.Name);
            if (info.Type == null)
                throw new NullReferenceException("info.Type");
            var ti = context.Roslyn_ResolveType(info.Type);
            return new TypeParameterConstraintInfo
            {
                Type   = ti,
                Constr = x.Constraints.Select(a => Convert2(a, context)).ToArray()
            };
        }

        // Public Methods 

        public void PassTypeName(Type type)
        {
            var tmp = TypesToPassAsArguments.FirstOrDefault(a => a.Type == type);
            if (tmp != null)
                return;
            TypesToPassAsArguments.Add(new A
            {
                Type         = type,
                ArgumentName = "generictype" + TypesToPassAsArguments.Count
            });
        }

        public TypeParameterConstraintInfo[] TypeParameterConstraints { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public List<A> TypesToPassAsArguments { get; private set; }

        internal enum ConstraintKind
        {
            ParameterlessConstructor
        }


        internal class A
        {
            public Type   Type         { get; set; }
            public string ArgumentName { get; set; }
        }

        internal class ConstraintInfo
        {
            public ConstraintKind Kind { get; set; }
        }

        internal class TypeParameterConstraintInfo
        {
            public Type Type { get; set; }

            public ConstraintInfo[] Constr { get; set; }

            public bool RequiresParameterlessConstructor
            {
                get { return Constr.Any(a => a.Kind == ConstraintKind.ParameterlessConstructor); }
            }
        }
    }
}