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
    
    property Condition IPhpValue 
    
    property WhenTrue IPhpValue 
    
    property WhenFalse IPhpValue 
    smartClassEnd
    */

    public partial class PhpConditionalExpression : IPhpValueBase
    {
        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            var _condition = SimplifyForFieldAcces(Condition, s);
            var whenTrue = SimplifyForFieldAcces(WhenTrue, s);
            var whenFalse = SimplifyForFieldAcces(WhenFalse, s);

            var newNode = new PhpConditionalExpression(_condition, whenTrue, whenFalse);
            if (PhpSourceBase.EqualCode(this, newNode))
                return this;
            return newNode;
        }
        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (style == null || style.Compression == EmitStyleCompression.Beauty)
                return string.Format("{0} ? {1} : {2}", condition.GetPhpCode(style), whenTrue.GetPhpCode(style), whenFalse.GetPhpCode(style));
            else
                return string.Format("{0}?{1}:{2}", condition.GetPhpCode(style), whenTrue.GetPhpCode(style), whenFalse.GetPhpCode(style));
        }
        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return IPhpStatementBase.XXX(condition, whenTrue, whenFalse);
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-08 09:14
// File generated automatically ver 2013-07-10 08:43
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
        /// <param name="Condition"></param>
        /// <param name="WhenTrue"></param>
        /// <param name="WhenFalse"></param>
        /// </summary>
        public PhpConditionalExpression(IPhpValue Condition, IPhpValue WhenTrue, IPhpValue WhenFalse)
        {
            this.Condition = Condition;
            this.WhenTrue = WhenTrue;
            this.WhenFalse = WhenFalse;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Condition; 
        /// </summary>
        public const string PROPERTYNAME_CONDITION = "Condition";
        /// <summary>
        /// Nazwa własności WhenTrue; 
        /// </summary>
        public const string PROPERTYNAME_WHENTRUE = "WhenTrue";
        /// <summary>
        /// Nazwa własności WhenFalse; 
        /// </summary>
        public const string PROPERTYNAME_WHENFALSE = "WhenFalse";
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
                return condition;
            }
            set
            {
                condition = value;
            }
        }
        private IPhpValue condition;
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue WhenTrue
        {
            get
            {
                return whenTrue;
            }
            set
            {
                whenTrue = value;
            }
        }
        private IPhpValue whenTrue;
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue WhenFalse
        {
            get
            {
                return whenFalse;
            }
            set
            {
                whenFalse = value;
            }
        }
        private IPhpValue whenFalse;
        #endregion Properties

    }
}
