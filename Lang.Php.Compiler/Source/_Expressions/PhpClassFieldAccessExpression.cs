using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    
    property ClassName PhpQualifiedName 
    	read only
    
    property FieldName string 
    	preprocess while (value.StartsWith("$")) value = value.Substring(1);
    
    property IsConst bool 
    
    property ClassTi ClassTranslationInfo 
    	read only
    smartClassEnd
    */
    
    public partial class PhpClassFieldAccessExpression : IPhpValueBase
    {
        public override string GetPhpCode(PhpEmitStyle style)
        {
            return string.Format("{0}::{1}{2}",
                _className.NameForEmit(style),
                _isConst ? "" : "$",
                _fieldName);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (_className.EmitName != PhpQualifiedName.ClassnameSelf)
                yield return new ClassCodeRequest(_className);
        }

        public void SetClassName(PhpQualifiedName phpClassName, ClassTranslationInfo classTi )
        {
            _className = phpClassName;
            _classTi = classTi;
            
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-26 23:46
// File generated automatically ver 2014-09-01 19:00
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
        implement ToString ##ClassName## ##FieldName## ##IsConst## ##ClassTi##
        implement ToString ClassName=##ClassName##, FieldName=##FieldName##, IsConst=##IsConst##, ClassTi=##ClassTi##
        implement equals ClassName, FieldName, IsConst, ClassTi
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności ClassName; 
        /// </summary>
        public const string PropertyNameClassName = "ClassName";
        /// <summary>
        /// Nazwa własności FieldName; 
        /// </summary>
        public const string PropertyNameFieldName = "FieldName";
        /// <summary>
        /// Nazwa własności IsConst; 
        /// </summary>
        public const string PropertyNameIsConst = "IsConst";
        /// <summary>
        /// Nazwa własności ClassTi; 
        /// </summary>
        public const string PropertyNameClassTi = "ClassTi";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName ClassName
        {
            get
            {
                return _className;
            }
        }
        private PhpQualifiedName _className;
        /// <summary>
        /// 
        /// </summary>
        public string FieldName
        {
            get
            {
                return _fieldName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                while (value.StartsWith("$")) value = value.Substring(1);
                _fieldName = value;
            }
        }
        private string _fieldName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool IsConst
        {
            get
            {
                return _isConst;
            }
            set
            {
                _isConst = value;
            }
        }
        private bool _isConst;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public ClassTranslationInfo ClassTi
        {
            get
            {
                return _classTi;
            }
        }
        private ClassTranslationInfo _classTi;
        #endregion Properties

    }
}
