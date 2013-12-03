using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    
    property ClassName PhpClassName 
    
    property FieldName string 
    smartClassEnd
    */

    public partial class PhpClassFieldAccessExpression : IPhpValueBase 
    {
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}::{1}", className.NameForEmit(style), fieldName);
        }

        public  override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            yield return new ClassCodeRequest(className);
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-10 20:22
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpClassFieldAccessExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpClassFieldAccessExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ClassName## ##FieldName##
        implement ToString ClassName=##ClassName##, FieldName=##FieldName##
        implement equals ClassName, FieldName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności ClassName; 
        /// </summary>
        public const string PROPERTYNAME_CLASSNAME = "ClassName";
        /// <summary>
        /// Nazwa własności FieldName; 
        /// </summary>
        public const string PROPERTYNAME_FIELDNAME = "FieldName";
        #endregion Constants

        #region Methods
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
        /// <summary>
        /// 
        /// </summary>
        public string FieldName
        {
            get
            {
                return fieldName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                fieldName = value;
            }
        }
        private string fieldName = string.Empty;
        #endregion Properties

    }
}
