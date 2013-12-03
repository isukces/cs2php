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
    
    property DefinedConstName string 
    	read only
    
    property ModuleName PhpCodeModuleName 
    smartClassEnd
    */

    public partial class PhpDefinedConstExpression : IPhpValueBase
    {
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return definedConstName;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (moduleName != null)
                yield return new ModuleCodeRequest(moduleName);
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 19:52
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpDefinedConstExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpDefinedConstExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##DefinedConstName## ##ModuleName##
        implement ToString DefinedConstName=##DefinedConstName##, ModuleName=##ModuleName##
        implement equals DefinedConstName, ModuleName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="DefinedConstName"></param>
        /// <param name="ModuleName"></param>
        /// </summary>
        public PhpDefinedConstExpression(string DefinedConstName, PhpCodeModuleName ModuleName)
        {
            this.definedConstName = DefinedConstName;
            this.ModuleName = ModuleName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności DefinedConstName; 
        /// </summary>
        public const string PROPERTYNAME_DEFINEDCONSTNAME = "DefinedConstName";
        /// <summary>
        /// Nazwa własności ModuleName; 
        /// </summary>
        public const string PROPERTYNAME_MODULENAME = "ModuleName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string DefinedConstName
        {
            get
            {
                return definedConstName;
            }
        }
        private string definedConstName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeModuleName ModuleName
        {
            get
            {
                return moduleName;
            }
            set
            {
                moduleName = value;
            }
        }
        private PhpCodeModuleName moduleName;
        #endregion Properties

    }
}
