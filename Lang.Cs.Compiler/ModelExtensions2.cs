using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Lang.Cs.Compiler
{
    public static class ModelExtensions2
    {
        public static MyInfo GetTypeInfo2(this SemanticModel model, ExpressionSyntax expression)
        {
            var typeInfo = ModelExtensions.GetTypeInfo(model, expression);
            if (typeInfo.ConvertedType == null)
                System.Diagnostics.Debug.Write("");
            var g = new MyInfo
            {
                Conversion1 = typeInfo.ConvertedType != null
                ? (Conversion?)model.ClassifyConversion(expression, typeInfo.ConvertedType)
                : null,
                Conversion2 = model.GetConversion(expression),
                TypeInfo = typeInfo
            };
            return g;

        }

        public class MyInfo
        {
            public Conversion? Conversion1 { get; set; }
            public Conversion Conversion2 { get; set; }

            public object ImplicitConversion
            {
                get
                {
                    var myConversions = new[] { Conversion1, Conversion2 }.Where(a => a.HasValue && !a.Value.IsIdentity).Distinct().ToArray();
                    return myConversions.Any() ? string.Join("=>", myConversions) : "no conversion";
                }
            }

            public TypeInfo TypeInfo { get; set; }

            public IMethodSymbol GetMethodSymbol()
            {
                var myConversions = new[] { Conversion1, Conversion2 }.Where(a => a.HasValue && a.Value.MethodSymbol != null).Select(a => a.Value).Distinct().ToArray();
                switch (myConversions.Length)
                {
                    case 0:
                        return null;
                    case 1:
                        return myConversions[0].MethodSymbol;
                    default:
                        throw new Exception("Too many method symbols");
                }
            }
        }
    }
}
