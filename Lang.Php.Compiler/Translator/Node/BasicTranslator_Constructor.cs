using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    public class BasicTranslator_Constructor : IPhpNodeTranslator<CallConstructor>
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
            var cType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
            if (cType == typeof(List<>))
            {
                if (src.Initializers != null && src.Initializers.Any())
                    throw new NotSupportedException();
                return new PhpArrayCreateExpression();
            }
            if (cType == typeof(Dictionary<,>))
            {
                if (src.Initializers != null && src.Initializers.Any())
                    throw new NotSupportedException();
                return new PhpArrayCreateExpression();
            }
            if (cType == typeof(Stack<>))
            {
                if (src.Initializers != null && src.Initializers.Any())
                    throw new NotSupportedException();
                return new PhpArrayCreateExpression();
            }
            {
                var directCallAttribute = src.Info.GetCustomAttributes(false).OfType<DirectCallAttribute>().FirstOrDefault();
                if (directCallAttribute != null)
                {
                    var result = DotnetMethodCallTranslator.CreateExpressionFromDirectCallAttribute(
                        ctx, directCallAttribute, null, src.Arguments);
                    return result;
                }
            }
            return null;
        }

		#endregion Methods 
    }
}
