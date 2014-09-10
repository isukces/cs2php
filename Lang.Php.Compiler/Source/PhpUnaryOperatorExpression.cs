using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property Operand IPhpValue 
    	read only
    
    property Operator string 
    	read only
    smartClassEnd
    */
    
    public partial class PhpUnaryOperatorExpression : IPhpValueBase
    {
        #region Methods

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return _operand == null ? new ICodeRequest[0] : _operand.GetCodeRequests();
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}{1}", _operator, _operand.GetPhpCode(style));
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-09 07:46
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="operand"></param>
        /// <param name="_operator"></param>
        /// </summary>
        public PhpUnaryOperatorExpression(IPhpValue operand, string _operator)
        {
            _operand = operand;
            this._operator = _operator;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Operand; 
        /// </summary>
        public const string PropertyNameOperand = "Operand";
        /// <summary>
        /// Nazwa własności Operator; 
        /// </summary>
        public const string PropertyNameOperator = "Operator";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Operand
        {
            get
            {
                return _operand;
            }
        }
        private IPhpValue _operand;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string Operator
        {
            get
            {
                return _operator;
            }
        }
        private string _operator = string.Empty;
        #endregion Properties

    }
}
