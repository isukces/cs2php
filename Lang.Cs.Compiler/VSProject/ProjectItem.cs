using System;

namespace Lang.Cs.Compiler.VSProject
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor name, BuildAction
    
    property Name string 
    
    property SubItems ProjectItem[] 
    
    property BuildAction BuildActions 
    smartClassEnd
    */
    
    public partial class ProjectItem
    {
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-01-04 15:46
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
        implement ToString ##Name## ##SubItems## ##BuildAction##
        implement ToString Name=##Name##, SubItems=##SubItems##, BuildAction=##BuildAction##
        implement equals Name, SubItems, BuildAction
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name"></param>
        /// <param name="BuildAction"></param>
        /// </summary>
        public ProjectItem(string Name, BuildActions BuildAction)
        {
            this.Name = Name;
            this.BuildAction = BuildAction;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Name; 
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności SubItems; 
        /// </summary>
        public const string PROPERTYNAME_SUBITEMS = "SubItems";
        /// <summary>
        /// Nazwa własności BuildAction; 
        /// </summary>
        public const string PROPERTYNAME_BUILDACTION = "BuildAction";
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
        /// <summary>
        /// 
        /// </summary>
        public BuildActions BuildAction
        {
            get
            {
                return buildAction;
            }
            set
            {
                buildAction = value;
            }
        }
        private BuildActions buildAction;
        #endregion Properties

    }
}
