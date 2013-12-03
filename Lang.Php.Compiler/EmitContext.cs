using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property LibraryDictionaries Dictionary<string, string> Mapuje nazwy bibliotek na nazwy katalogów, gdzie mają być umieszczane pliki
    	init #
    smartClassEnd
    */
    
    public partial class EmitContext
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-10 15:42
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class EmitContext 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public EmitContext()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##LibraryDictionaries##
        implement ToString LibraryDictionaries=##LibraryDictionaries##
        implement equals LibraryDictionaries
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności LibraryDictionaries; Mapuje nazwy bibliotek na nazwy katalogów, gdzie mają być umieszczane pliki
        /// </summary>
        public const string PROPERTYNAME_LIBRARYDICTIONARIES = "LibraryDictionaries";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Mapuje nazwy bibliotek na nazwy katalogów, gdzie mają być umieszczane pliki
        /// </summary>
        public Dictionary<string, string> LibraryDictionaries
        {
            get
            {
                return libraryDictionaries;
            }
            set
            {
                libraryDictionaries = value;
            }
        }
        private Dictionary<string, string> libraryDictionaries = new Dictionary<string, string>();
        #endregion Properties

    }
}
