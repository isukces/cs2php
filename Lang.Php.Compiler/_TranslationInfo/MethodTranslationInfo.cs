using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lang.Cs.Compiler.Sandbox;

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
            var result = new MethodTranslationInfo {ScriptName = methodInfo.Name};
            {
                var attr = methodInfo.GetCustomAttribute<ScriptNameAttribute>();
                if (attr != null)
                    result._scriptName = attr.Name;
            }
            return result;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-03 18:00
// File generated automatically ver 2014-09-01 19:00
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
        public const string PropertyNameScriptName = "ScriptName";
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
                return _scriptName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _scriptName = value;
            }
        }
        private string _scriptName = string.Empty;
        #endregion Properties

    }
}
