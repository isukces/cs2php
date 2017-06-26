using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property FieldName string 
    	read only
    
    property TargetObject IPhpValue 
    	read only
    
    property IncludeModule PhpCodeModuleName 
    smartClassEnd
    */
    
    public partial class PhpInstanceFieldAccessExpression : IPhpValueBase
    {
        #region Methods

        // Public Methods 

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}->{1}", targetObject.GetPhpCode(style), fieldName);
        }

        public override string ToString()
        {
            return GetPhpCode(new PhpEmitStyle());
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a =  IPhpStatementBase.GetCodeRequests(targetObject).ToList();
            if (IncludeModule != null)
                a.Add(new ModuleCodeRequest(includeModule, string.Format("instance field {0}",this)));
            return a;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-19 23:02
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpInstanceFieldAccessExpression 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpInstanceFieldAccessExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##FieldName## ##TargetObject## ##IncludeModule##
        implement ToString FieldName=##FieldName##, TargetObject=##TargetObject##, IncludeModule=##IncludeModule##
        implement equals FieldName, TargetObject, IncludeModule
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="FieldName"></param>
        /// <param name="TargetObject"></param>
        /// <param name="IncludeModule"></param>
        /// </summary>
        public PhpInstanceFieldAccessExpression(string FieldName, IPhpValue TargetObject, PhpCodeModuleName IncludeModule)
        {
            fieldName = FieldName;
            targetObject = TargetObject;
            this.IncludeModule = IncludeModule;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności FieldName; 
        /// </summary>
        public const string PROPERTYNAME_FIELDNAME = "FieldName";
        /// <summary>
        /// Nazwa własności TargetObject; 
        /// </summary>
        public const string PROPERTYNAME_TARGETOBJECT = "TargetObject";
        /// <summary>
        /// Nazwa własności IncludeModule; 
        /// </summary>
        public const string PROPERTYNAME_INCLUDEMODULE = "IncludeModule";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string FieldName
        {
            get
            {
                return fieldName;
            }
        }
        private string fieldName = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue TargetObject
        {
            get
            {
                return targetObject;
            }
        }
        private IPhpValue targetObject;
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeModuleName IncludeModule
        {
            get
            {
                return includeModule;
            }
            set
            {
                includeModule = value;
            }
        }
        private PhpCodeModuleName includeModule;
        #endregion Properties

    }
}
