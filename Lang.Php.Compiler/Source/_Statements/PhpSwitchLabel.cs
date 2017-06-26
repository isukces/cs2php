using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor
    implement Constructor Value
    
    property Value IPhpValue 
    
    property IsDefault bool 
    smartClassEnd
    */

    public partial class PhpSwitchLabel : ICodeRelated
    {
        #region Methods

        // Public Methods 

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (_value != null)
                return _value.GetCodeRequests();
            return new ICodeRequest[0];
        }

        public PhpSwitchLabel Simplify(IPhpSimplifier s, out bool wasChanged)
        {
            wasChanged = false;
            if (isDefault)
                return this;
            var e1 = s.Simplify(Value);
            wasChanged = !PhpSourceBase.EqualCode(e1, Value);
            if (wasChanged)
                return new PhpSwitchLabel(e1);
            return this;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-23 11:45
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpSwitchLabel
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpSwitchLabel()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Value## ##IsDefault##
        implement ToString Value=##Value##, IsDefault=##IsDefault##
        implement equals Value, IsDefault
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpSwitchLabel()
        {
        }

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Value"></param>
        /// </summary>
        public PhpSwitchLabel(IPhpValue Value)
        {
            this.Value = Value;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Value; 
        /// </summary>
        public const string PROPERTYNAME_VALUE = "Value";
        /// <summary>
        /// Nazwa własności IsDefault; 
        /// </summary>
        public const string PROPERTYNAME_ISDEFAULT = "IsDefault";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        private IPhpValue _value;
        /// <summary>
        /// 
        /// </summary>
        public bool IsDefault
        {
            get
            {
                return isDefault;
            }
            set
            {
                isDefault = value;
            }
        }
        private bool isDefault;
        #endregion Properties
    }
}
