using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property ScriptName string 
    smartClassEnd
    */

    public partial class MethodTranslationInfo
    {
        public static MethodTranslationInfo FromMethodInfo(MethodInfo methodInfo)
        {
            var result = new MethodTranslationInfo();
            result.ScriptName = methodInfo.Name;
            {
                var attr = methodInfo.GetCustomAttribute<Lang.Php.ScriptNameAttribute>();
                if (attr != null)
                {
                    result.scriptName = attr.Name;
                }
            }
            return result;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-10 17:15
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class MethodTranslationInfo
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public MethodTranslationInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ScriptName##
        implement ToString ScriptName=##ScriptName##
        implement equals ScriptName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności ScriptName; 
        /// </summary>
        public const string PROPERTYNAME_SCRIPTNAME = "ScriptName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string ScriptName
        {
            get
            {
                return scriptName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                scriptName = value;
            }
        }
        private string scriptName = string.Empty;
        #endregion Properties

    }
}
