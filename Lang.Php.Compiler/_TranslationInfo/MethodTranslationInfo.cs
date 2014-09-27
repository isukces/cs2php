using System;
using System.Reflection;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property ScriptName string 
    	access public public private
    
    property ClassTi ClassTranslationInfo 
    	read only
    smartClassEnd
    */

    public partial class MethodTranslationInfo
    {

        public static MethodTranslationInfo FromMethodInfo(MethodBase methodInfo, ClassTranslationInfo classTranslationInfo)
        {
            var result = new MethodTranslationInfo
            {
                _scriptName = methodInfo.Name,
                _classTi = classTranslationInfo
            };
            var scriptNameAttribute = methodInfo.GetCustomAttribute<ScriptNameAttribute>();
            if (scriptNameAttribute != null)
                result._scriptName = scriptNameAttribute.Name.Trim();
            if (string.IsNullOrEmpty(result._scriptName))
                throw new Exception("Method name is empty");
            return result;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-26 19:25
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
        implement ToString ##ScriptName## ##ClassTi##
        implement ToString ScriptName=##ScriptName##, ClassTi=##ClassTi##
        implement equals ScriptName, ClassTi
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności ScriptName; 
        /// </summary>
        public const string PropertyNameScriptName = "ScriptName";
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
        public string ScriptName
        {
            get
            {
                return _scriptName;
            }
        }
        private string _scriptName = string.Empty;
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
