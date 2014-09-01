using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    implement ToString ClassCodeRequest ##PhpClassName##
    
    property ClassName PhpQualifiedName 
    smartClassEnd
    */
    
    public partial class ClassCodeRequest : ICodeRequest
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-01 23:22
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class ClassCodeRequest 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ClassCodeRequest()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ClassName##
        implement ToString ClassName=##ClassName##
        implement equals ClassName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="className"></param>
        /// </summary>
        public ClassCodeRequest(PhpQualifiedName className)
        {
            ClassName = className;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności ClassName; 
        /// </summary>
        public const string PropertyNameClassName = "ClassName";
        #endregion Constants

        #region Methods
        /// <summary>
        /// Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("ClassCodeRequest ##PhpClassName##");
        }

        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public PhpQualifiedName ClassName
        {
            get
            {
                return _className;
            }
            set
            {
                _className = value;
            }
        }
        private PhpQualifiedName _className;
        #endregion Properties

    }
}
