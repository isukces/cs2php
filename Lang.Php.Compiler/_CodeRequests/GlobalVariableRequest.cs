using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property VariableName string nazwa zmiennej globalnej
    smartClassEnd
    */

    public partial class GlobalVariableRequest : ICodeRequest
    {
        public override string ToString()
        {
            return string.Format("GlobalVariableRequest {0}", variableName);
        }

    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 21:48
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class GlobalVariableRequest
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public GlobalVariableRequest()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##VariableName##
        implement ToString VariableName=##VariableName##
        implement equals VariableName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="VariableName">nazwa zmiennej globalnej</param>
        /// </summary>
        public GlobalVariableRequest(string VariableName)
        {
            this.VariableName = VariableName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności VariableName; nazwa zmiennej globalnej
        /// </summary>
        public const string PROPERTYNAME_VARIABLENAME = "VariableName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// nazwa zmiennej globalnej
        /// </summary>
        public string VariableName
        {
            get
            {
                return variableName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                variableName = value;
            }
        }
        private string variableName = string.Empty;
        #endregion Properties

    }
}
