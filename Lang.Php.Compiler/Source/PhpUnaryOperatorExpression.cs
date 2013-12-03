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
    
    property Operand IPhpValue 
    
    property Operator string 
    smartClassEnd
    */
    
    public partial class PhpUnaryOperatorExpression : IPhpValueBase 
    {
 
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}{1}", _operator, operand.GetPhpCode(style));
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return operand == null ? new ICodeRequest[0] : operand.GetCodeRequests();
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-06 18:43
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpUnaryOperatorExpression 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpUnaryOperatorExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Operand## ##Operator##
        implement ToString Operand=##Operand##, Operator=##Operator##
        implement equals Operand, Operator
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Operand"></param>
        /// <param name="Operator"></param>
        /// </summary>
        public PhpUnaryOperatorExpression(IPhpValue Operand, string Operator)
        {
            this.Operand = Operand;
            this.Operator = Operator;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Operand; 
        /// </summary>
        public const string PROPERTYNAME_OPERAND = "Operand";
        /// <summary>
        /// Nazwa własności Operator; 
        /// </summary>
        public const string PROPERTYNAME_OPERATOR = "Operator";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Operand
        {
            get
            {
                return operand;
            }
            set
            {
                operand = value;
            }
        }
        private IPhpValue operand;
        /// <summary>
        /// 
        /// </summary>
        public string Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _operator = value;
            }
        }
        private string _operator = string.Empty;
        #endregion Properties

    }
}
