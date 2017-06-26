using System;

namespace Lang.Php.Compiler 
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property VariableName string nazwa zmiennej globalnej
    
    property IsArgument bool czy zmienna jest deklarowana jako argument funkcji
    
    property ChangeNameAction Action<string> akcja, która musi być wykonana przy zmianie nazwy zmiennej
    smartClassEnd
    */
    
    public partial class LocalVariableRequest : ICodeRequest
    {
        public override string ToString()
        {
            return string.Format("LocalVariableRequest {0}", variableName);
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-12 12:01
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class LocalVariableRequest 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public LocalVariableRequest()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##VariableName## ##IsArgument## ##ChangeNameAction##
        implement ToString VariableName=##VariableName##, IsArgument=##IsArgument##, ChangeNameAction=##ChangeNameAction##
        implement equals VariableName, IsArgument, ChangeNameAction
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="VariableName">nazwa zmiennej globalnej</param>
        /// <param name="IsArgument">czy zmienna jest deklarowana jako argument funkcji</param>
        /// <param name="ChangeNameAction">akcja, która musi być wykonana przy zmianie nazwy zmiennej</param>
        /// </summary>
        public LocalVariableRequest(string VariableName, bool IsArgument, Action<string> ChangeNameAction)
        {
            this.VariableName = VariableName;
            this.IsArgument = IsArgument;
            this.ChangeNameAction = ChangeNameAction;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności VariableName; nazwa zmiennej globalnej
        /// </summary>
        public const string PROPERTYNAME_VARIABLENAME = "VariableName";
        /// <summary>
        /// Nazwa własności IsArgument; czy zmienna jest deklarowana jako argument funkcji
        /// </summary>
        public const string PROPERTYNAME_ISARGUMENT = "IsArgument";
        /// <summary>
        /// Nazwa własności ChangeNameAction; akcja, która musi być wykonana przy zmianie nazwy zmiennej
        /// </summary>
        public const string PROPERTYNAME_CHANGENAMEACTION = "ChangeNameAction";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// nazwa zmiennej globalnej
        /// </summary>
        public string VariableName
        {
            get
            {
                return variableName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                variableName = value;
            }
        }
        private string variableName = string.Empty;
        /// <summary>
        /// czy zmienna jest deklarowana jako argument funkcji
        /// </summary>
        public bool IsArgument
        {
            get
            {
                return isArgument;
            }
            set
            {
                isArgument = value;
            }
        }
        private bool isArgument;
        /// <summary>
        /// akcja, która musi być wykonana przy zmianie nazwy zmiennej
        /// </summary>
        public Action<string> ChangeNameAction
        {
            get
            {
                return changeNameAction;
            }
            set
            {
                changeNameAction = value;
            }
        }
        private Action<string> changeNameAction;
        #endregion Properties

    }
}
