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
            if (returnValue == null)
                writer.WriteLn("return;");
            else
                writer.WriteLnF("return {0};", returnValue.GetPhpCode(style));

        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return XXX(returnValue);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            if (returnValue == null)
                return this;
            var newReturnValue = s.Simplify(returnValue);
            if (returnValue == newReturnValue)
                return this;
            return new PhpReturnStatement(newReturnValue);
        }

		#endregion Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 18:08
// File generated automatically ver 2013-07-10 08:43
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
        /// <param name="ReturnValue"></param>
        /// </summary>
        public PhpReturnStatement(IPhpValue ReturnValue)
        {
            this.returnValue = ReturnValue;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności ReturnValue; 
        /// </summary>
        public const string PROPERTYNAME_RETURNVALUE = "ReturnValue";
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
                return returnValue;
            }
        }
        private IPhpValue returnValue;
        #endregion Properties
    }
}
