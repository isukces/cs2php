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
            result.isStatic = (propInfo.CanRead && propInfo.GetGetMethod().IsStatic)
                || (propInfo.CanWrite && propInfo.GetSetMethod().IsStatic);
            var name = propInfo.Name;
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);
            result.FieldScriptName = name.Substring(0, 1).ToLower() + name.Substring(1);
            {
                var at = propInfo.GetCustomAttribute<ScriptNameAttribute>();
                if (at != null)
                {
                    name = at.Name; // preserve case
                    result.FieldScriptName = name;
                }
            }
            var AutoImplemented = IsAutoProperty(propInfo);
            if (AutoImplemented)
            {
                result.getSetByMethod = false;
            }
            else
            {
                result.getSetByMethod = true;
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
        public static bool IsAutoProperty(PropertyInfo prop)
        {
            return prop.DeclaringType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                     .Any(f => f.Name.Contains("<" + prop.Name + ">"));
        }

		#endregion Static Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-26 09:47
// File generated automatically ver 2013-07-10 08:43
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
        public const string PROPERTYNAME_FIELDSCRIPTNAME = "FieldScriptName";
        /// <summary>
        /// Nazwa własności GetSetByMethod; 
        /// </summary>
        public const string PROPERTYNAME_GETSETBYMETHOD = "GetSetByMethod";
        /// <summary>
        /// Nazwa własności SetMethodName; 
        /// </summary>
        public const string PROPERTYNAME_SETMETHODNAME = "SetMethodName";
        /// <summary>
        /// Nazwa własności GetMethodName; 
        /// </summary>
        public const string PROPERTYNAME_GETMETHODNAME = "GetMethodName";
        /// <summary>
        /// Nazwa własności IsStatic; 
        /// </summary>
        public const string PROPERTYNAME_ISSTATIC = "IsStatic";
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
                return fieldScriptName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                fieldScriptName = value;
            }
        }
        private string fieldScriptName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool GetSetByMethod
        {
            get
            {
                return getSetByMethod;
            }
            set
            {
                getSetByMethod = value;
            }
        }
        private bool getSetByMethod;
        /// <summary>
        /// 
        /// </summary>
        public string SetMethodName
        {
            get
            {
                return setMethodName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                setMethodName = value;
            }
        }
        private string setMethodName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string GetMethodName
        {
            get
            {
                return getMethodName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                getMethodName = value;
            }
        }
        private string getMethodName = string.Empty;
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
        #endregion Properties
    }
}
