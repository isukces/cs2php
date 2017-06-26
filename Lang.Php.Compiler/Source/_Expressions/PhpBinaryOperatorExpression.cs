using System;
using System.Collections.Generic;
using Lang.Php.Compiler.Translator;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor  Operator,   Left,   Right
    
    property Left IPhpValue 
    	read only
    	OnChange if(value==null) throw new ArgumentNullException("Left");
    
    property Right IPhpValue 
    	read only
    	OnChange if(value==null) throw new ArgumentNullException("Right");
    
    property Operator string 
    	read only
    smartClassEnd
    */

    public partial class PhpBinaryOperatorExpression : IPhpValueBase
    {

        public override string GetPhpCode(PhpEmitStyle style)
        {
            if (style == null || style.Compression == EmitStyleCompression.Beauty)
                return string.Format("{0} {1} {2}", left.GetPhpCode(style), _operator, right.GetPhpCode(style));
            return string.Format("{0}{1}{2}", left.GetPhpCode(style), _operator, right.GetPhpCode(style));
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return IPhpStatementBase.GetCodeRequests(left, right);
        }
   

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IPhpValue ConcatStrings(params IPhpValue[] items)
        {
            if (items == null) return null;
            IPhpValue result = null;
            foreach (var i in items)
            {
                if (result == null)
                    result = i;
                else
                    result = new PhpBinaryOperatorExpression(".", result, i);
            }
            if (result != null)
            {
                var simplifier = new ExpressionSimplifier(new OptimizeOptions());
                result = simplifier.Simplify(result);
            }
            return result;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-25 22:21
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpBinaryOperatorExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpBinaryOperatorExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Left## ##Right## ##Operator##
        implement ToString Left=##Left##, Right=##Right##, Operator=##Operator##
        implement equals Left, Right, Operator
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Left"></param>
        /// <param name="Right"></param>
        /// <param name="Operator"></param>
        /// </summary>
        public PhpBinaryOperatorExpression(string Operator, IPhpValue Left, IPhpValue Right)
        {
            left = Left;
            right = Right;
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            _operator = Operator;
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
        /// Nazwa własności Operator; 
        /// </summary>
        public const string PROPERTYNAME_OPERATOR = "Operator";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Left
        {
            get
            {
                return left;
            }
        }
        private IPhpValue left;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Right
        {
            get
            {
                return right;
            }
        }
        private IPhpValue right;
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
