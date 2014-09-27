using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{


    /*
    smartClass
    option NoAdditionalFile
    
    property Name string nazwa pola lub stałej
    	preprocess value = PhpVariableExpression.AddDollar(value, false);
    
    property IsStatic bool 
    
    property IsConst bool 
    
    property ConstValue IPhpValue 
    
    property Visibility Visibility 
    	init Visibility.Public
    smartClassEnd
    */
    
    public partial class PhpClassFieldDefinition : IClassMember, ICodeRelated
    {
        public PhpClassFieldDefinition()
        {
            
        }
        #region Methods

        // Public Methods 
        

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            if (_isConst)
            {
                //  const CONSTANT = 'constant value';
                writer.WriteLnF("const {0} = {1};", Name, _constValue.GetPhpCode(style));
                return;
            }

            var a = string.Format("{0}{1} ${2}",
                _visibility.ToString().ToLower(),
                _isStatic ? " static" : "",
                Name
                );
            if (_constValue != null)
                a += " = " + _constValue.GetPhpCode(style);
            writer.WriteLn(a + ";");
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return IPhpStatementBase.GetCodeRequests(_constValue);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-27 12:56
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpClassFieldDefinition 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpClassFieldDefinition()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##IsStatic## ##IsConst## ##ConstValue## ##Visibility##
        implement ToString Name=##Name##, IsStatic=##IsStatic##, IsConst=##IsConst##, ConstValue=##ConstValue##, Visibility=##Visibility##
        implement equals Name, IsStatic, IsConst, ConstValue, Visibility
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Name; nazwa pola lub stałej
        /// </summary>
        public const string PropertyNameName = "Name";
        /// <summary>
        /// Nazwa własności IsStatic; 
        /// </summary>
        public const string PropertyNameIsStatic = "IsStatic";
        /// <summary>
        /// Nazwa własności IsConst; 
        /// </summary>
        public const string PropertyNameIsConst = "IsConst";
        /// <summary>
        /// Nazwa własności ConstValue; 
        /// </summary>
        public const string PropertyNameConstValue = "ConstValue";
        /// <summary>
        /// Nazwa własności Visibility; 
        /// </summary>
        public const string PropertyNameVisibility = "Visibility";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// nazwa pola lub stałej
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                value = PhpVariableExpression.AddDollar(value, false);
                _name = value;
            }
        }
        private string _name = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool IsStatic
        {
            get
            {
                return _isStatic;
            }
            set
            {
                _isStatic = value;
            }
        }
        private bool _isStatic;
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
        /// 
        /// </summary>
        public IPhpValue ConstValue
        {
            get
            {
                return _constValue;
            }
            set
            {
                _constValue = value;
            }
        }
        private IPhpValue _constValue;
        /// <summary>
        /// 
        /// </summary>
        public Visibility Visibility
        {
            get
            {
                return _visibility;
            }
            set
            {
                _visibility = value;
            }
        }
        private Visibility _visibility = Visibility.Public;
        #endregion Properties

    }
}
