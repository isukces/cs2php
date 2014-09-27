using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    implement ToString ##ModuleName##
    
    property ModuleName PhpCodeModuleName 
    	read only
    
    property Why string Why this Module is requested
    	read only
    smartClassEnd
    */
    
    public partial class ModuleCodeRequest : ICodeRequest
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-27 14:02
// File generated automatically ver 2014-09-01 19:00
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
        implement ToString ##ModuleName## ##Why##
        implement ToString ModuleName=##ModuleName##, Why=##Why##
        implement equals ModuleName, Why
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="moduleName"></param>
        /// <param name="why">Why this Module is requested</param>
        /// </summary>
        public ModuleCodeRequest(PhpCodeModuleName moduleName, string why)
        {
            _moduleName = moduleName;
            _why = why;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności ModuleName; 
        /// </summary>
        public const string PropertyNameModuleName = "ModuleName";
        /// <summary>
        /// Nazwa własności Why; Why this Module is requested
        /// </summary>
        public const string PropertyNameWhy = "Why";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", _moduleName);
        }

        #endregion Methods

        #region Properties
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
        /// <summary>
        /// Why this Module is requested; własność jest tylko do odczytu.
        /// </summary>
        public string Why
        {
            get
            {
                return _why;
            }
        }
        private string _why = string.Empty;
        #endregion Properties

    }
}
