using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    class BasicTranslator : IPhpNodeTranslator<ClassFieldAccessExpression>,
        IPhpNodeTranslator<CallConstructor>
    {
        #region Methods

        // Public Methods 

        public int getPriority()
        {
            return 999;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, ClassFieldAccessExpression src)
        {
            var s = TranslatorBase.GetCompareName(src.Member);
            if (cache == null)
                cache = new Dictionary<string, IPhpValue>()
                {                    
                    { "System.String::Empty",  new PhpConstValue("")},
                    { "System.Math::PI",  new PhpDefinedConstExpression("M_PI", null)},
                    { "System.Math::E",  new PhpDefinedConstExpression("M_E", null)},
                    { "System.Int32::MaxValue",  new PhpConstValue(int.MaxValue) },
                };

            // Math.E
            IPhpValue o;
            if (cache.TryGetValue(s, out o))
                return o;
            return null;
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

        #region Fields

        Dictionary<string, IPhpValue> cache;

        #endregion Fields
    }
}
