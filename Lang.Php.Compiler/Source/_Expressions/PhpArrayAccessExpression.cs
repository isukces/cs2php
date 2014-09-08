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
    
    property PhpArray IPhpValue 
    
    property Index IPhpValue 
    smartClassEnd
    */
    
    public partial class PhpArrayAccessExpression : IPhpValueBase
    {
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}[{1}]", phpArray.GetPhpCode(style), index.GetPhpCode(style));
        }


        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return IPhpStatementBase.Xxx(phpArray, index);
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-12 18:17
// File generated automatically ver 2013-07-10 08:43
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
        /// <param name="PhpArray"></param>
        /// <param name="Index"></param>
        /// </summary>
        public PhpArrayAccessExpression(IPhpValue PhpArray, IPhpValue Index)
        {
            this.PhpArray = PhpArray;
            this.Index = Index;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności PhpArray; 
        /// </summary>
        public const string PROPERTYNAME_PHPARRAY = "PhpArray";
        /// <summary>
        /// Nazwa własności Index; 
        /// </summary>
        public const string PROPERTYNAME_INDEX = "Index";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue PhpArray
        {
            get
            {
                return phpArray;
            }
            set
            {
                phpArray = value;
            }
        }
        private IPhpValue phpArray;
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }
        private IPhpValue index;
        #endregion Properties

    }
}
