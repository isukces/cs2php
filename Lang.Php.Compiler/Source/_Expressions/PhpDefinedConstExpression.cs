using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property DefinedConstName string 
    	read only
    
    property ModuleName PhpCodeModuleName 
    	read only
    smartClassEnd
    */

    public partial class PhpDefinedConstExpression : IPhpValueBase
    {
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return _definedConstName;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (_moduleName != null)
                yield return new ModuleCodeRequest(_moduleName, "defined const " + DefinedConstName);
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-27 14:13
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="definedConstName"></param>
        /// <param name="moduleName"></param>
        /// </summary>
        public PhpDefinedConstExpression(string definedConstName, PhpCodeModuleName moduleName)
        {
            if (definedConstName == "PHP_EOL" && moduleName != null)
                throw new Exception("PHP_EOL is built in");
            _definedConstName = definedConstName;
            _moduleName = moduleName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności DefinedConstName; 
        /// </summary>
        public const string PropertyNameDefinedConstName = "DefinedConstName";
        /// <summary>
        /// Nazwa własności ModuleName; 
        /// </summary>
        public const string PropertyNameModuleName = "ModuleName";
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
                return _definedConstName;
            }
        }
        private string _definedConstName = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public PhpCodeModuleName ModuleName
        {
            get
            {
                return _moduleName;
            }
        }
        private PhpCodeModuleName _moduleName;
        #endregion Properties

    }
}
