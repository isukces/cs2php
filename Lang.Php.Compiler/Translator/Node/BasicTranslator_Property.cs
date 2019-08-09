using System;
using System.Collections.Generic;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator.Node
{
    internal class BasicTranslator_Property : IPhpNodeTranslator<CsharpInstancePropertyAccessExpression>
    {
        // Public Methods 
        public int GetPriority()
        {
            return 100;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpInstancePropertyAccessExpression src)
        {
            _ctx = ctx;
            _src = src;
            var t = TranslatorBase.GetGenericTypeDefinition(src.Member.DeclaringType);
            if (t == typeof(KeyValuePair<,>))
                return _KeyValuePair();
            if (t == typeof(Nullable<>))
                return _Nullable();
            if (t == typeof(Array))
                return _Array();
            return null;
        }
        // Private Methods 

        private IPhpValue _Array()
        {
            var s = _src.Member.Name;
            if (s == "Length")
            {
                var targetObject = _ctx.TranslateValue(_src.TargetObject);
                var a = new PhpMethodCallExpression("count", targetObject);
                return a;
            }
            throw new NotSupportedException();
        }

        private IPhpValue _KeyValuePair()
        {
            var s = _src.Member.Name;
            if (s == "Key" || s == "Value")
            {
                var targetObject = _ctx.TranslateValue(_src.TargetObject);
                if (targetObject is PhpVariableExpression)
                {
                    var to2 = targetObject as PhpVariableExpression;
                    var a = new PhpVariableExpression(to2.VariableName + "@" + _src.Member.Name, to2.VariableKind);
                    return a;
                }
                return null;
            }
            throw new NotSupportedException();
        }

        private IPhpValue _Nullable()
        {
            var s = _src.Member.Name;
            switch (s)
            {
                case "Value":
                {
                    var targetObject = _ctx.TranslateValue(_src.TargetObject);
                    return targetObject;
                }
                case "HasValue":
                {
                    var targetObject = _ctx.TranslateValue(_src.TargetObject);
                    var isNull = new PhpMethodCallExpression("is_null", targetObject);
                    var notNull = new PhpUnaryOperatorExpression(isNull, "!");
                    return notNull;
                }
            }
            throw new NotSupportedException();
        }

        private IExternalTranslationContext _ctx;
        private CsharpInstancePropertyAccessExpression _src;
    }
}