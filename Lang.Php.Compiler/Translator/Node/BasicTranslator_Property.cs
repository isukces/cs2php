using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    class BasicTranslator_Property : IPhpNodeTranslator<CsharpInstancePropertyAccessExpression>
    {
        #region Methods

        // Public Methods 
        public int GetPriority()
        {
            return 100;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpInstancePropertyAccessExpression src)
        {
            this.ctx = ctx;
            this.src = src;
            var t = TranslatorBase.GetGenericTypeDefinition(src.Member.DeclaringType);
            if (t == typeof(System.Collections.Generic.KeyValuePair<,>))
                return _KeyValuePair();
            if (t == typeof(System.Nullable<>))
                return _Nullable();
            if (t == typeof(Array))
                return _Array();
            return null;
        }
        // Private Methods 

        private IPhpValue _Array()
        {
            var s = src.Member.Name;
            if (s == "Length")
            {
                var targetObject = ctx.TranslateValue(src.TargetObject);
                var a = new PhpMethodCallExpression("count", new IPhpValue[] { targetObject });
                return a;
            }
            throw new NotSupportedException();
        }

        private IPhpValue _Nullable()
        {
            var s = src.Member.Name;
            if (s == "Value")
            {
                var targetObject = ctx.TranslateValue(src.TargetObject);
                return targetObject;
            }
            if (s == "HasValue")
            {
                var targetObject = ctx.TranslateValue(src.TargetObject);
                var _isNull = new PhpMethodCallExpression("is_null", targetObject);
                var _notNull = new PhpUnaryOperatorExpression(_isNull, "!");
                return _notNull;
            }
            throw new NotSupportedException();
        }

        private IPhpValue _KeyValuePair()
        {
            var s = src.Member.Name;
            if (s == "Key" || s == "Value")
            {
                var targetObject = ctx.TranslateValue(src.TargetObject);
                if (targetObject is PhpVariableExpression)
                {
                    var to2 = targetObject as PhpVariableExpression;
                    var a = new PhpVariableExpression(to2.VariableName + "@" + src.Member.Name, to2.Kind);
                    return a;
                }
                return null;
            }
            throw new NotSupportedException();
        }

        #endregion Methods

        #region Fields

        IExternalTranslationContext ctx;
        CsharpInstancePropertyAccessExpression src;

        #endregion Fields
    }
}
