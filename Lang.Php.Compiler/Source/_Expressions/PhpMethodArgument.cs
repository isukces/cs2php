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
    
    
    property Name string Nazwa argumentu
    
    property DefaultValue IPhpValue 
    smartClassEnd
    */

    public partial class PhpMethodArgument
    {
		#region Methods 

		// Public Methods 

        public string GetPhpCode(PhpEmitStyle s)
        {
            s = s ?? new PhpEmitStyle();
            string eq = s.Compression == EmitStyleCompression.Beauty ? " = " : "=";
            string d = defaultValue != null ? (eq + defaultValue.GetPhpCode(s)) : "";
            return string.Format("${0}{1}", name, d);
        }

        public override string ToString()
        {
            return GetPhpCode(null);
        }

		#endregion Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-13 08:39
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpMethodArgument
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpMethodArgument()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##DefaultValue##
        implement ToString Name=##Name##, DefaultValue=##DefaultValue##
        implement equals Name, DefaultValue
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności Name; Nazwa argumentu
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności DefaultValue; 
        /// </summary>
        public const string PROPERTYNAME_DEFAULTVALUE = "DefaultValue";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// Nazwa argumentu
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
        public IPhpValue DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }
        private IPhpValue defaultValue;
        #endregion Properties
    }
}
