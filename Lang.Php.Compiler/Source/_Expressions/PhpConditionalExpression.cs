using System.Collections.Generic;
using System.Diagnostics;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property Condition IPhpValue 
    
    property WhenTrue IPhpValue 
    
    property WhenFalse IPhpValue 
    smartClassEnd
    */
    
    public partial class PhpConditionalExpression : IPhpValueBase
    {
        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            Debug.Assert(Condition != null, "Condition != null");
            var condition = SimplifyForFieldAcces(Condition, s);
            var whenTrue = SimplifyForFieldAcces(WhenTrue, s);
            var whenFalse = SimplifyForFieldAcces(WhenFalse, s);
            var newNode = new PhpConditionalExpression(condition, whenTrue, whenFalse);
            return EqualCode(this, newNode) ? this : newNode;
        }
        public override string GetPhpCode(PhpEmitStyle style)
        {
            string form = style == null || style.Compression == EmitStyleCompression.Beauty 
                ? "{0} ? {1} : {2}" 
                : "{0}?{1}:{2}";
            return string.Format(form, _condition.GetPhpCode(style), _whenTrue.GetPhpCode(style), _whenFalse.GetPhpCode(style));
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return IPhpStatementBase.GetCodeRequests(_condition, _whenTrue, _whenFalse);
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-08 10:04
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpConditionalExpression 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpConditionalExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Condition## ##WhenTrue## ##WhenFalse##
        implement ToString Condition=##Condition##, WhenTrue=##WhenTrue##, WhenFalse=##WhenFalse##
        implement equals Condition, WhenTrue, WhenFalse
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="condition"></param>
        /// <param name="whenTrue"></param>
        /// <param name="whenFalse"></param>
        /// </summary>
        public PhpConditionalExpression(IPhpValue condition, IPhpValue whenTrue, IPhpValue whenFalse)
        {
            Condition = condition;
            WhenTrue = whenTrue;
            WhenFalse = whenFalse;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Condition; 
        /// </summary>
        public const string PropertyNameCondition = "Condition";
        /// <summary>
        /// Nazwa własności WhenTrue; 
        /// </summary>
        public const string PropertyNameWhenTrue = "WhenTrue";
        /// <summary>
        /// Nazwa własności WhenFalse; 
        /// </summary>
        public const string PropertyNameWhenFalse = "WhenFalse";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                _condition = value;
            }
        }
        private IPhpValue _condition;
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue WhenTrue
        {
            get
            {
                return _whenTrue;
            }
            set
            {
                _whenTrue = value;
            }
        }
        private IPhpValue _whenTrue;
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue WhenFalse
        {
            get
            {
                return _whenFalse;
            }
            set
            {
                _whenFalse = value;
            }
        }
        private IPhpValue _whenFalse;
        #endregion Properties

    }
}
