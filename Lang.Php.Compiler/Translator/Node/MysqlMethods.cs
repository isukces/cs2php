using Lang.Cs.Compiler;

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
