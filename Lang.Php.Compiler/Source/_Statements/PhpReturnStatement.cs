using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property ReturnValue IPhpValue 
    	read only
    smartClassEnd
    */

    public partial class PhpReturnStatement : IPhpStatementBase
    {
        #region Methods

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            if (_returnValue == null)
                writer.WriteLn("return;");
            else
                writer.WriteLnF("return {0};", _returnValue.GetPhpCode(style));

        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return GetCodeRequests(_returnValue);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            if (_returnValue == null)
                return this;
            var newReturnValue = s.Simplify(_returnValue);
            return _returnValue == newReturnValue ? this : new PhpReturnStatement(newReturnValue);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-08 14:17
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpReturnStatement
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpReturnStatement()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ReturnValue##
        implement ToString ReturnValue=##ReturnValue##
        implement equals ReturnValue
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="returnValue"></param>
        /// </summary>
        public PhpReturnStatement(IPhpValue returnValue)
        {
            _returnValue = returnValue;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności ReturnValue; 
        /// </summary>
        public const string PropertyNameReturnValue = "ReturnValue";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue ReturnValue
        {
            get
            {
                return _returnValue;
            }
        }
        private IPhpValue _returnValue;
        #endregion Properties

    }
}
