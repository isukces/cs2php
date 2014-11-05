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
    
    property Expression IPhpValue 
    	read only
    smartClassEnd
    */

    public partial class PhpParenthesizedExpression : IPhpValueBase
    {
		#region Static Methods 

		// Public Methods 

        public static IPhpValue Strip(IPhpValue x)
        {
            if (x is PhpParenthesizedExpression)
                return Strip((x as PhpParenthesizedExpression).expression);
            return x;
        }

		#endregion Static Methods 

		#region Methods 

		// Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return expression == null ? new ICodeRequest[0] : expression.GetCodeRequests();
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("({0})", expression.GetPhpCode(style));
        }

		#endregion Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-06 18:01
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpParenthesizedExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpParenthesizedExpression()
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
        public PhpParenthesizedExpression(IPhpValue Expression)
        {
            expression = Expression;
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
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Expression
        {
            get
            {
                return expression;
            }
        }
        private IPhpValue expression;
        #endregion Properties
    }
}
