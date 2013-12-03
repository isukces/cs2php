using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property TranslationInfo PropertyTranslationInfo 
    
    property TargetObject IPhpValue 
    smartClassEnd
    */

    public partial class PhpPropertyAccessExpression : IPhpValueBase
    {
		#region Methods 

		// Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (translationInfo == null)
                throw new ArgumentNullException("translationInfo");
            if (translationInfo.IsStatic)
                throw new NotSupportedException();
            // jeśli nic nie podano to zakładam odczyt własności
            var a = MakeGetValueExpression();
            return a.GetPhpCode(style);
        }

        public IPhpValue MakeGetValueExpression()
        {
            if (translationInfo == null)
                throw new ArgumentNullException("translationInfo");
            if (translationInfo.IsStatic)
                throw new NotSupportedException();
            if (translationInfo.GetSetByMethod)
            {
                var a = new PhpMethodCallExpression(translationInfo.GetMethodName);
                a.TargetObject = targetObject;
                return a;
            }
            else
            {
                var a = new PhpInstanceFieldAccessExpression(translationInfo.FieldScriptName, targetObject, null);
                return a;
            }

        }

        public IPhpValue MakeSetValueExpression(IPhpValue v)
        {
            if (translationInfo == null)
                throw new ArgumentNullException("translationInfo");
            if (translationInfo.IsStatic)
                throw new NotSupportedException();

            if (translationInfo.GetSetByMethod)
            {
                var a = new PhpMethodCallExpression(translationInfo.SetMethodName);
                a.Arguments.Add(new PhpMethodInvokeValue(v));
                a.TargetObject = targetObject;
                return a;
            }
            else
            {
                var a = new PhpInstanceFieldAccessExpression(translationInfo.FieldScriptName, targetObject, null);
                var b = new PhpAssignExpression(a, v);
                return b;
            }

        }

        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            var to = SimplifyForFieldAcces(targetObject, s);
            if (EqualCode(targetObject, to))
                return this;

            return new PhpPropertyAccessExpression(translationInfo, to);

        }

		#endregion Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-26 09:16
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpPropertyAccessExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpPropertyAccessExpression()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##TranslationInfo## ##TargetObject##
        implement ToString TranslationInfo=##TranslationInfo##, TargetObject=##TargetObject##
        implement equals TranslationInfo, TargetObject
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="TranslationInfo"></param>
        /// <param name="TargetObject"></param>
        /// </summary>
        public PhpPropertyAccessExpression(PropertyTranslationInfo TranslationInfo, IPhpValue TargetObject)
        {
            this.TranslationInfo = TranslationInfo;
            this.TargetObject = TargetObject;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności TranslationInfo; 
        /// </summary>
        public const string PROPERTYNAME_TRANSLATIONINFO = "TranslationInfo";
        /// <summary>
        /// Nazwa własności TargetObject; 
        /// </summary>
        public const string PROPERTYNAME_TARGETOBJECT = "TargetObject";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public PropertyTranslationInfo TranslationInfo
        {
            get
            {
                return translationInfo;
            }
            set
            {
                translationInfo = value;
            }
        }
        private PropertyTranslationInfo translationInfo;
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue TargetObject
        {
            get
            {
                return targetObject;
            }
            set
            {
                targetObject = value;
            }
        }
        private IPhpValue targetObject;
        #endregion Properties
    }
}
