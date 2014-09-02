using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    class MysqlMethods : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
		#region Methods 

		// Public Methods 

        public int GetPriority()
        {
            return 1;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            return null;
        }

		#endregion Methods 
    }
}
