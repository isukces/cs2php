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
    
    property Name string nazwa pola lub stałej
    
    property IsStatic bool 
    
    property IsConst bool 
    
    property ConstValue IPhpValue 
    
    property Visibility Visibility 
    	init Visibility.Public
    smartClassEnd
    */

    public partial class PhpClassFieldDefinition : IClassMember, ICodeRelated
    {
		#region Methods 

		// Public Methods 

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var v = visibility.ToString().ToLower();
            if (isConst)
            {
                //  const CONSTANT = 'constant value';
                writer.WriteLnF("const {0} = {1};",   name, constValue.GetPhpCode(style));
                return;
            }

            string a = string.Format("{0}{1} {2}",
                visibility.ToString().ToLower(),
                isStatic ? " static" : "",
                name
                );
            if (constValue != null)
                a += " = " + constValue.GetPhpCode(style);
            writer.WriteLn(a + ";");
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return IPhpStatementBase.GetCodeRequests(constValue);
        }

		#endregion Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-10 18:26
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpClassFieldDefinition
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpMethodFieldDefinition()
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
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności IsStatic; 
        /// </summary>
        public const string PROPERTYNAME_ISSTATIC = "IsStatic";
        /// <summary>
        /// Nazwa własności IsConst; 
        /// </summary>
        public const string PROPERTYNAME_ISCONST = "IsConst";
        /// <summary>
        /// Nazwa własności ConstValue; 
        /// </summary>
        public const string PROPERTYNAME_CONSTVALUE = "ConstValue";
        /// <summary>
        /// Nazwa własności Visibility; 
        /// </summary>
        public const string PROPERTYNAME_VISIBILITY = "Visibility";
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
        public bool IsStatic
        {
            get
            {
                return isStatic;
            }
            set
            {
                isStatic = value;
            }
        }
        private bool isStatic;
        /// <summary>
        /// 
        /// </summary>
        public bool IsConst
        {
            get
            {
                return isConst;
            }
            set
            {
                isConst = value;
            }
        }
        private bool isConst;
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue ConstValue
        {
            get
            {
                return constValue;
            }
            set
            {
                constValue = value;
            }
        }
        private IPhpValue constValue;
        /// <summary>
        /// 
        /// </summary>
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
            }
        }
        private Visibility visibility = Visibility.Public;
        #endregion Properties
    }
}
