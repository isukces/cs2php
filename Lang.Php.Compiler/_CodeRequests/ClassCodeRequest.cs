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
    implement ToString ClassCodeRequest ##PhpClassName##
    
    property ClassName PhpClassName 
    smartClassEnd
    */
    
    public partial class ClassCodeRequest : ICodeRequest
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-20 22:12
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
        /// <param name="ClassName"></param>
        /// </summary>
        public ClassCodeRequest(PhpQualifiedName ClassName)
        {
            this.ClassName = ClassName;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności ClassName; 
        /// </summary>
        public const string PROPERTYNAME_CLASSNAME = "ClassName";
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
                return className;
            }
            set
            {
                className = value;
            }
        }
        private PhpQualifiedName className;
        #endregion Properties

    }
}
