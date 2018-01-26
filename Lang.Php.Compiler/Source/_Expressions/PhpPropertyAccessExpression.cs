using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpPropertyAccessExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="translationInfo"></param>
        ///     <param name="targetObject"></param>
        /// </summary>
        public PhpPropertyAccessExpression(PropertyTranslationInfo translationInfo, IPhpValue targetObject)
        {
            TranslationInfo = translationInfo;
            this.TargetObject    = targetObject;
        }
        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (TranslationInfo == null)
                throw new ArgumentNullException("translationInfo");
            if (TranslationInfo.IsStatic)
                throw new NotSupportedException();
            // jeśli nic nie podano to zakładam odczyt własności
            var a = MakeGetValueExpression();
            return a.GetPhpCode(style);
        }

        public IPhpValue MakeGetValueExpression()
        {
            if (TranslationInfo == null)
                throw new ArgumentNullException("translationInfo");
            if (TranslationInfo.IsStatic)
                throw new NotSupportedException();
            if (TranslationInfo.GetSetByMethod)
            {
                var a          = new PhpMethodCallExpression(TranslationInfo.GetMethodName);
                a.TargetObject = TargetObject;
                return a;
            }
            else
            {
                var a = new PhpInstanceFieldAccessExpression(TranslationInfo.FieldScriptName, TargetObject, null);
                return a;
            }
        }

        public IPhpValue MakeSetValueExpression(IPhpValue v)
        {
            if (TranslationInfo == null)
                throw new ArgumentNullException("translationInfo");
            if (TranslationInfo.IsStatic)
                throw new NotSupportedException();

            if (TranslationInfo.GetSetByMethod)
            {
                var a = new PhpMethodCallExpression(TranslationInfo.SetMethodName);
                a.Arguments.Add(new PhpMethodInvokeValue(v));
                a.TargetObject = TargetObject;
                return a;
            }
            else
            {
                var a = new PhpInstanceFieldAccessExpression(TranslationInfo.FieldScriptName, TargetObject, null);
                var b = new PhpAssignExpression(a, v);
                return b;
            }
        }

        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            var to = SimplifyForFieldAcces(TargetObject, s);
            if (EqualCode(TargetObject, to))
                return this;

            return new PhpPropertyAccessExpression(TranslationInfo, to);
        }


        /// <summary>
        /// </summary>
        public PropertyTranslationInfo TranslationInfo { get; set; }

        /// <summary>
        /// </summary>
        public IPhpValue TargetObject { get; set; }
    }
}