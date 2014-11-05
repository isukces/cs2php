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
            if (da != null)
            {
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
            var pti = PropertyTranslationInfo.FromPropertyInfo(src.Member);
            //ctx.GetTranslationInfo().GetOrMakeTranslationInfo(src.Member);
            if (!pti.GetSetByMethod)
            {
                var fieldAccessExpression = new PhpClassFieldAccessExpression
                {
                    FieldName = pti.FieldScriptName
                };
                var principles = ctx.GetTranslationInfo();
                var phpClassName = principles.GetPhpType(src.Member.DeclaringType, true, principles.CurrentType);
                var classTi = principles.GetOrMakeTranslationInfo(src.Member.DeclaringType);
                fieldAccessExpression.SetClassName(phpClassName, classTi);
                return fieldAccessExpression;
            }
            throw new NotImplementedException(string.Format("Unable to translate ClassPropertyAccessExpression {0}.{1}", src.Member.DeclaringType, src.Member.Name));
        }

        #endregion Methods
    }
}
