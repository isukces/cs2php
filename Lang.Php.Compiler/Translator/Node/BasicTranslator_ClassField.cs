using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    class BasicTranslator_ClassField : IPhpNodeTranslator<ClassFieldAccessExpression>
    {
		#region Methods 

		// Public Methods 

        public int getPriority()
        {
            return 9;
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

		#endregion Methods 

		#region Fields 

        Dictionary<string, IPhpValue> cache;

		#endregion Fields 
    }
}
