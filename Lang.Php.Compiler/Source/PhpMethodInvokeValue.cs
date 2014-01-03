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
    implement Constructor Expression
    
    property Expression IPhpValue 
    
    property ByRef bool 
    smartClassEnd
    */

    public partial class PhpMethodInvokeValue : IPhpValueBase
    {

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (expression == (object)null)
                throw new Exception("Unable to get code from empty expression");
            var ex = PhpParenthesizedExpression.Strip(expression);
            var a = expression.GetPhpCode(style);
            if (byRef)
                a = "&" + a;
            return a;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return expression == null ? new ICodeRequest[0] : expression.GetCodeRequests();
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-17 19:31
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpMethodInvokeValue
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpMethodInvokeValue()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Expression## ##ByRef##
        implement ToString Expression=##Expression##, ByRef=##ByRef##
        implement equals Expression, ByRef
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Expression"></param>
        /// </summary>
        public PhpMethodInvokeValue(IPhpValue Expression)
        {
            this.Expression = Expression;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Expression; 
        /// </summary>
        public const string PROPERTYNAME_EXPRESSION = "Expression";
        /// <summary>
        /// Nazwa własności ByRef; 
        /// </summary>
        public const string PROPERTYNAME_BYREF = "ByRef";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Expression
        {
            get
            {
                return expression;
            }
            set
            {
                expression = value;
            }
        }
        private IPhpValue expression;
        /// <summary>
        /// 
        /// </summary>
        public bool ByRef
        {
            get
            {
                return byRef;
            }
            set
            {
                byRef = value;
            }
        }
        private bool byRef;
        #endregion Properties

    }
}
