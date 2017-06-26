using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TypeInfo = Microsoft.CodeAnalysis.TypeInfo;

namespace Lang.Cs.Compiler.Visitors
{
    /// <summary>
    /// Provides information about method header
    /// </summary>
    class MethodHeaderInfo
    {
        #region Constructors

        public MethodHeaderInfo()
        {
            TypesToPassAsArguments = new List<A>();
        }

        #endregion Constructors

        #region Static Methods

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
                MethodInfo = methodInfo,
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

        private static TypeParameterConstraintInfo ConvertTypeParameterConstraints(TypeParameterConstraintClauseSyntax x, LangParseContext context)
        {
            TypeInfo info = context.RoslynModel.GetTypeInfo(x.Name);
            if (info.Type == null)
                throw new NullReferenceException("info.Type");
            Type ti = context.Roslyn_ResolveType(info.Type);
            return new TypeParameterConstraintInfo
            {
                Type = ti,
                Constr = x.Constraints.Select(a => Convert2(a, context)).ToArray()
            };
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void PassTypeName(Type type)
        {
            var tmp = TypesToPassAsArguments.FirstOrDefault(a => a.Type == type);
            if (tmp != null)
                return;
            TypesToPassAsArguments.Add(new A
            {
                Type=type,
                ArgumentName = "generictype" + TypesToPassAsArguments.Count
            });
        }

        #endregion Methods

        #region Enums

        internal enum ConstraintKind
        {
            ParameterlessConstructor
        }

        #endregion Enums

        #region Properties

        public TypeParameterConstraintInfo[] TypeParameterConstraints { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public List<A> TypesToPassAsArguments { get; private set; }

        #endregion Properties

        #region Nested Classes


        internal class A
        {
            public Type Type { get; set; }
            public string ArgumentName { get; set; }
        }
        internal class ConstraintInfo
        {
            #region Properties

            public ConstraintKind Kind { get; set; }

            #endregion Properties
        }
        internal class TypeParameterConstraintInfo
        {
            #region Properties

            public Type Type { get; set; }

            public ConstraintInfo[] Constr { get; set; }

            public bool RequiresParameterlessConstructor
            {
                get
                {
                    return Constr.Any(a => a.Kind == ConstraintKind.ParameterlessConstructor);
                }
            }

            #endregion Properties
        }
        #endregion Nested Classes
    }
}