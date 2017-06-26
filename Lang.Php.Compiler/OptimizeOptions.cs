namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property JoinEchoStatements bool 
    	init true
    smartClassEnd
    */
    
    public partial class OptimizeOptions
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-21 09:20
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class OptimizeOptions 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public OptimizeOptions()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##JoinEchoStatements##
        implement ToString JoinEchoStatements=##JoinEchoStatements##
        implement equals JoinEchoStatements
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności JoinEchoStatements; 
        /// </summary>
        public const string PROPERTYNAME_JOINECHOSTATEMENTS = "JoinEchoStatements";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool JoinEchoStatements
        {
            get
            {
                return joinEchoStatements;
            }
            set
            {
                joinEchoStatements = value;
            }
        }
        private bool joinEchoStatements = true;
        #endregion Properties

    }
}
