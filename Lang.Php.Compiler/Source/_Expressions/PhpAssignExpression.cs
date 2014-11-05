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
    implement Constructor Left, Right
    
    property Left IPhpValue 
    
    property Right IPhpValue 
    
    property OptionalOperator string 
    smartClassEnd
    */

    public partial class PhpAssignExpression : IPhpValueBase
    {
		#region Methods 

		// Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = IPhpStatementBase.GetCodeRequests(left, right).ToArray();
            return a;
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (style == null || style.Compression == EmitStyleCompression.Beauty)
                return string.Format("{0} {1}= {2}", left.GetPhpCode(style), optionalOperator, right.GetPhpCode(style));
            return string.Format("{0}{1}={2}", left.GetPhpCode(style), optionalOperator, right.GetPhpCode(style));
        }

        public override IPhpValue Simplify(IPhpExpressionSimplifier s)
        {
            var _right = StripBracketsAndSimplify(right, s);
            var _left = s.Simplify(left);
            if (_left is PhpPropertyAccessExpression)
            {
                var e = _left as PhpPropertyAccessExpression;
                var a = e.MakeSetValueExpression(_right);
                if (a is PhpAssignExpression && (a as PhpAssignExpression).left is PhpPropertyAccessExpression)
                    if (EqualCode(a, this))
                        return this;
                return a;

            }
            if (EqualCode(_left, left) && EqualCode(_right, right))
                return this;
            return new PhpAssignExpression(_left, _right, optionalOperator);
        }

		#endregion Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-23 13:04
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpAssignExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpAssignExpression()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Left## ##Right## ##OptionalOperator##
        implement ToString Left=##Left##, Right=##Right##, OptionalOperator=##OptionalOperator##
        implement equals Left, Right, OptionalOperator
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Left"></param>
        /// <param name="Right"></param>
        /// <param name="OptionalOperator"></param>
        /// </summary>
        public PhpAssignExpression(IPhpValue Left, IPhpValue Right, string OptionalOperator)
        {
            this.Left = Left;
            this.Right = Right;
            this.OptionalOperator = OptionalOperator;
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Left"></param>
        /// <param name="Right"></param>
        /// </summary>
        public PhpAssignExpression(IPhpValue Left, IPhpValue Right)
        {
            this.Left = Left;
            this.Right = Right;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Left; 
        /// </summary>
        public const string PROPERTYNAME_LEFT = "Left";
        /// <summary>
        /// Nazwa własności Right; 
        /// </summary>
        public const string PROPERTYNAME_RIGHT = "Right";
        /// <summary>
        /// Nazwa własności OptionalOperator; 
        /// </summary>
        public const string PROPERTYNAME_OPTIONALOPERATOR = "OptionalOperator";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }
        private IPhpValue left;
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }
        private IPhpValue right;
        /// <summary>
        /// 
        /// </summary>
        public string OptionalOperator
        {
            get
            {
                return optionalOperator;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                optionalOperator = value;
            }
        }
        private string optionalOperator = string.Empty;
        #endregion Properties
    }
}
