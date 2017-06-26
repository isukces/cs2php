using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property Expression string 
    smartClassEnd
    */
    
    public partial class PhpFreeExpression : IPhpValueBase
    {
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return expression;
        }
        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return new ICodeRequest[0];
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 11:31
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpFreeExpression 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpFreeExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Expression##
        implement ToString Expression=##Expression##
        implement equals Expression
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Expression"></param>
        /// </summary>
        public PhpFreeExpression(string Expression)
        {
            this.Expression = Expression;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Expression; 
        /// </summary>
        public const string PROPERTYNAME_EXPRESSION = "Expression";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Expression
        {
            get
            {
                return expression;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                expression = value;
            }
        }
        private string expression = string.Empty;
        #endregion Properties

    }
}
