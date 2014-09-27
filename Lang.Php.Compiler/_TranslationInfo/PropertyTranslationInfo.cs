using System;
using System.Linq;
using System.Reflection;
using Lang.Cs.Compiler.Sandbox;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property FieldScriptName string 
    
    property GetSetByMethod bool 
    
    property SetMethodName string 
    
    property GetMethodName string 
    
    property IsStatic bool 
    smartClassEnd
    */

    public partial class PropertyTranslationInfo
    {
        #region Static Methods

        // Public Methods 

        public static PropertyTranslationInfo FromPropertyInfo(PropertyInfo propInfo)
        {
            var result = new PropertyTranslationInfo();
            var gm = propInfo.GetGetMethod();
            var sm = propInfo.GetSetMethod();
            if (gm == null && sm == null)
                throw new Exception(string.Format("Unable to get getter or setter for {0}", propInfo));
            result._isStatic = (gm != null && gm.IsStatic)
                               || (sm != null && sm.IsStatic);
            var autoImplemented = IsAutoProperty(propInfo, result._isStatic);
            if (autoImplemented)
                result.FieldScriptName = propInfo.Name; // just field with the same name 
            else
            {
                var name = propInfo.Name;
                result.FieldScriptName = name.Substring(0, 1).ToLower() + name.Substring(1); // lowecase field name
            }
            {
                var at = propInfo.GetCustomAttribute<ScriptNameAttribute>(false);
                if (at != null)
                    result.FieldScriptName = at.Name;// preserve case
            }
            if (autoImplemented)
            {
                result._getSetByMethod = false;
            }
            else
            {
                result._getSetByMethod = true;
                if (propInfo.CanRead)
                    result.GetMethodName = propInfo.GetGetMethod().Name;
                if (propInfo.CanWrite)
                    result.SetMethodName = propInfo.GetSetMethod().Name;
            }
            return result;
        }

        /// <summary>
        /// Taken from http://stackoverflow.com/questions/2210309/how-to-find-out-if-a-property-is-an-auto-implemented-property-with-reflection
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool IsAutoProperty(PropertyInfo prop, bool isStatic)
        {
            var d = isStatic
                ? BindingFlags.NonPublic | BindingFlags.Static
                : BindingFlags.NonPublic | BindingFlags.Instance;
            return prop.DeclaringType.GetFields(d)
                                     .Any(f => f.Name.Contains("<" + prop.Name + ">"));
        }

        #endregion Static Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-03 17:57
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class PropertyTranslationInfo
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PropertyTranslationInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##FieldScriptName## ##GetSetByMethod## ##SetMethodName## ##GetMethodName## ##IsStatic##
        implement ToString FieldScriptName=##FieldScriptName##, GetSetByMethod=##GetSetByMethod##, SetMethodName=##SetMethodName##, GetMethodName=##GetMethodName##, IsStatic=##IsStatic##
        implement equals FieldScriptName, GetSetByMethod, SetMethodName, GetMethodName, IsStatic
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności FieldScriptName; 
        /// </summary>
        public const string PropertyNameFieldScriptName = "FieldScriptName";
        /// <summary>
        /// Nazwa własności GetSetByMethod; 
        /// </summary>
        public const string PropertyNameGetSetByMethod = "GetSetByMethod";
        /// <summary>
        /// Nazwa własności SetMethodName; 
        /// </summary>
        public const string PropertyNameSetMethodName = "SetMethodName";
        /// <summary>
        /// Nazwa własności GetMethodName; 
        /// </summary>
        public const string PropertyNameGetMethodName = "GetMethodName";
        /// <summary>
        /// Nazwa własności IsStatic; 
        /// </summary>
        public const string PropertyNameIsStatic = "IsStatic";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string FieldScriptName
        {
            get
            {
                return _fieldScriptName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _fieldScriptName = value;
            }
        }
        private string _fieldScriptName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool GetSetByMethod
        {
            get
            {
                return _getSetByMethod;
            }
            set
            {
                _getSetByMethod = value;
            }
        }
        private bool _getSetByMethod;
        /// <summary>
        /// 
        /// </summary>
        public string SetMethodName
        {
            get
            {
                return _setMethodName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _setMethodName = value;
            }
        }
        private string _setMethodName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string GetMethodName
        {
            get
            {
                return _getMethodName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _getMethodName = value;
            }
        }
        private string _getMethodName = string.Empty;
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
        #endregion Properties

    }
}
