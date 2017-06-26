using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property Expression IPhpValue 
    	read only
    
    property Arguments IPhpValue[] 
    	read only
    smartClassEnd
    */

    public partial class PhpElementAccessExpression : IPhpValueBase
    {
        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            var _expression = SimplifyForFieldAcces(expression, s);
            if (arguments == null || arguments.Length == 0)
            {
                if (EqualCode(_expression, expression))
                    return this;
                return new PhpElementAccessExpression(_expression, null);
            }
            var _arguments = arguments.Select(i => StripBracketsAndSimplify(i, s)).ToArray();
            var candidate = new PhpElementAccessExpression(_expression, _arguments);
            if (EqualCode(candidate, this))
                return this;
            return candidate;
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            var a = arguments.Select(u => u.GetPhpCode(style));
            return string.Format("{0}[{1}]", expression.GetPhpCode(style), string.Join(",", a));
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = IPhpStatementBase.GetCodeRequests<IPhpValue>(arguments);
            var b = IPhpStatementBase.GetCodeRequests(expression);
            return a.Union(b).ToArray();
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-06 18:25
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpElementAccessExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpElementAccessExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Expression## ##Arguments##
        implement ToString Expression=##Expression##, Arguments=##Arguments##
        implement equals Expression, Arguments
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Expression"></param>
        /// <param name="Arguments"></param>
        /// </summary>
        public PhpElementAccessExpression(IPhpValue Expression, IPhpValue[] Arguments)
        {
            expression = Expression;
            arguments = Arguments;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Expression; 
        /// </summary>
        public const string PROPERTYNAME_EXPRESSION = "Expression";
        /// <summary>
        /// Nazwa własności Arguments; 
        /// </summary>
        public const string PROPERTYNAME_ARGUMENTS = "Arguments";
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
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue[] Arguments
        {
            get
            {
                return arguments;
            }
        }
        private IPhpValue[] arguments;
        #endregion Properties

    }
}
