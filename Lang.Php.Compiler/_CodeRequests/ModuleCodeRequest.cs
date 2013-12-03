using Lang.Php.Compiler.Source;
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
    
    property ModuleName PhpCodeModuleName 
    smartClassEnd
    */
    
    public partial class ModuleCodeRequest : ICodeRequest
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 18:40
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class ModuleCodeRequest 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ModuleCodeRequest()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ModuleName##
        implement ToString ModuleName=##ModuleName##
        implement equals ModuleName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="ModuleName"></param>
        /// </summary>
        public ModuleCodeRequest(PhpCodeModuleName ModuleName)
        {
            this.ModuleName = ModuleName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności ModuleName; 
        /// </summary>
        public const string PROPERTYNAME_MODULENAME = "ModuleName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
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
