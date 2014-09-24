using System;
using System.Reflection;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator.Node
{

    // ReSharper disable once InconsistentNaming
    public class BasicTranslator_ClassPropertyAccess :
         IPhpNodeTranslator<ClassPropertyAccessExpression>
    {
        #region Methods

        // Public Methods 

        public int GetPriority()
        {
            return 999;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, ClassPropertyAccessExpression src)
        {
            var da = src.Member.GetCustomAttribute(typeof(DirectCallAttribute), true) as DirectCallAttribute;
            if (da == null) return null;
            if (da.MapArray != null && da.MapArray.Length != 0)
                throw new NotSupportedException();
            switch (da.CallType)
            {
                case MethodCallStyles.Procedural:
                    return new PhpMethodCallExpression(da.Name);
                default:
                    throw new NotSupportedException();
            }
        }

        #endregion Methods
    }
}
