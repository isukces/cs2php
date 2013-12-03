using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs.Compiler.VSProject
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Name string 
    
    property SubItems ProjectItem[] 
    smartClassEnd
    */
    
    public partial class ProjectItem
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-03 09:22
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler.VSProject
{
    public partial class ProjectItem 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ProjectItem()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##SubItems##
        implement ToString Name=##Name##, SubItems=##SubItems##
        implement equals Name, SubItems
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Name; 
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności SubItems; 
        /// </summary>
        public const string PROPERTYNAME_SUBITEMS = "SubItems";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                name = value;
            }
        }
        private string name = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public ProjectItem[] SubItems
        {
            get
            {
                return subItems;
            }
            set
            {
                subItems = value;
            }
        }
        private ProjectItem[] subItems;
        #endregion Properties

    }
}
