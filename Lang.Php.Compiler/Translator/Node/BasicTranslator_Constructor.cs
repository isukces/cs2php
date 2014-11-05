using System.Web.Configuration;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lang.Php.Compiler.Translator.Node
{
    public class BasicTranslatorForConstructor : IPhpNodeTranslator<CallConstructor>
    {
        #region Methods

        // Public Methods 

        public int GetPriority()
        {
            return 9;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CallConstructor src)
        {
            var type = src.Info.DeclaringType;
            if (type == null)
                throw new Exception(string.Format("Strange. {0} has empty DeclaringType", src.Info));
            var cType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
            if (cType == typeof(List<>))
            {
                if (src.Initializers != null && src.Initializers.Any())
                    throw new NotSupportedException();
                return new PhpArrayCreateExpression();
            }
            if (cType == typeof(Dictionary<,>))
            {
                var a = new PhpArrayCreateExpression();
                if (src.Initializers == null || src.Initializers.Length == 0) return a;
                var b = new List<IPhpValue>();
                foreach (var i in src.Initializers)
                {
                    var ii = i as IValueTable_PseudoValue;
                    if (ii != null)
                    {
                        if (ii.Items == null || ii.Items.Length != 2)
                            throw new NotSupportedException();
                        var l = ctx.TranslateValue(ii.Items[0]);
                        var r = ctx.TranslateValue(ii.Items[1]);
                        b.Add(new PhpAssignExpression(l, r));
                    }
                    else
                        throw new NotSupportedException();
                }
                a.Initializers = b.ToArray();
                return a;
            }
            if (cType == typeof(Stack<>))
            {
                if (src.Initializers != null && src.Initializers.Any())
                    throw new NotSupportedException();
                return new PhpArrayCreateExpression();
            }
            {
                var directCallAttribute = src.Info.GetCustomAttributes<DirectCallAttribute>().FirstOrDefault();
                if (directCallAttribute == null) return null;
                var result = DotnetMethodCallTranslator.CreateExpressionFromDirectCallAttribute(
                    ctx, directCallAttribute, null, src.Arguments, src.Info);
                return result;
            }
        }

        #endregion Methods
    }
}
