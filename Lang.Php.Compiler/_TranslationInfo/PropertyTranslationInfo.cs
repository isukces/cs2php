using System;
using System.Linq;
using System.Reflection;

namespace Lang.Php.Compiler
{ 
    public  class PropertyTranslationInfo
    {
        // Public Methods 

        public static PropertyTranslationInfo FromPropertyInfo(PropertyInfo propInfo)
        {
            var result = new PropertyTranslationInfo();
            var gm = propInfo.GetGetMethod();
            var sm = propInfo.GetSetMethod();
            if (gm == null && sm == null)
                throw new Exception(string.Format("Unable to get getter or setter for {0}", propInfo));
            result.IsStatic = (gm != null && gm.IsStatic)
                               || (sm != null && sm.IsStatic);
            var autoImplemented = IsAutoProperty(propInfo, result.IsStatic);
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
                result.GetSetByMethod = false;
            }
            else
            {
                result.GetSetByMethod = true;
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
 
       

        /// <summary>
        /// 
        /// </summary>
        public string FieldScriptName
        {
            get => _fieldScriptName;
            set => _fieldScriptName = (value ?? String.Empty).Trim();
        }
        private string _fieldScriptName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool GetSetByMethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SetMethodName
        {
            get => _setMethodName;
            set => _setMethodName = (value ?? String.Empty).Trim();
        }
        private string _setMethodName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string GetMethodName
        {
            get => _getMethodName;
            set => _getMethodName = (value ?? String.Empty).Trim();
        }
        private string _getMethodName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool IsStatic { get; set; }
    }
}
