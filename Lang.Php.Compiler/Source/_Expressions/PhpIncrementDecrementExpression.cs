using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property Operand IPhpValue 
    
    property Increment bool 
    
    property Pre bool 
    smartClassEnd
    */

    public partial class PhpIncrementDecrementExpression : IPhpValueBase
    {
        #region Methods


        // Public Methods 

        public override string GetPhpCode(PhpEmitStyle style)
        {
            var o = increment ? "++" : "--";
            return string.Format("{0}{1}{2}",
                pre ? o : "",
                operand.GetPhpCode(style),
                pre ? "" : o);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return IPhpStatementBase.GetCodeRequests(operand);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-06 18:37
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpIncrementDecrementExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpIncrementDecrementExpression()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Operand## ##Increment## ##Pre##
        implement ToString Operand=##Operand##, Increment=##Increment##, Pre=##Pre##
        implement equals Operand, Increment, Pre
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Operand">,</param>
        /// <param name="Increment"></param>
        /// <param name="Pre"></param>
        /// </summary>
        public PhpIncrementDecrementExpression(IPhpValue Operand, bool Increment, bool Pre)
        {
            this.Operand = Operand;
            this.Increment = Increment;
            this.Pre = Pre;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Operand; ,
        /// </summary>
        public const string PROPERTYNAME_OPERAND = "Operand";
        /// <summary>
        /// Nazwa własności Increment; 
        /// </summary>
        public const string PROPERTYNAME_INCREMENT = "Increment";
        /// <summary>
        /// Nazwa własności Pre; 
        /// </summary>
        public const string PROPERTYNAME_PRE = "Pre";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// ,
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
        public bool Increment
        {
            get
            {
                return increment;
            }
            set
            {
                increment = value;
            }
        }
        private bool increment;
        /// <summary>
        /// 
        /// </summary>
        public bool Pre
        {
            get
            {
                return pre;
            }
            set
            {
                pre = value;
            }
        }
        private bool pre;
        #endregion Properties
    }
}
