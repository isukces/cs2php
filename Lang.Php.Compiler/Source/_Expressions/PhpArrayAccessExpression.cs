using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property PhpArray IPhpValue 
    	read only
    
    property Index IPhpValue 
    	read only
    smartClassEnd
    */
    
    public partial class PhpArrayAccessExpression : IPhpValueBase
    {
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}[{1}]", _phpArray.GetPhpCode(style), _index.GetPhpCode(style));
        }


        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return IPhpStatementBase.GetCodeRequests(_phpArray, _index);
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-08 14:10
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpArrayAccessExpression 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpArrayAccessExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##PhpArray## ##Index##
        implement ToString PhpArray=##PhpArray##, Index=##Index##
        implement equals PhpArray, Index
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="phpArray"></param>
        /// <param name="index"></param>
        /// </summary>
        public PhpArrayAccessExpression(IPhpValue phpArray, IPhpValue index)
        {
            _phpArray = phpArray;
            _index = index;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności PhpArray; 
        /// </summary>
        public const string PropertyNamePhpArray = "PhpArray";
        /// <summary>
        /// Nazwa własności Index; 
        /// </summary>
        public const string PropertyNameIndex = "Index";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue PhpArray
        {
            get
            {
                return _phpArray;
            }
        }
        private IPhpValue _phpArray;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue Index
        {
            get
            {
                return _index;
            }
        }
        private IPhpValue _index;
        #endregion Properties

    }
}
