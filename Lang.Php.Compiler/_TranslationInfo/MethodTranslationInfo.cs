using System;
using System.Reflection;

namespace Lang.Php.Compiler
{
    public class MethodTranslationInfo
    {
        public static MethodTranslationInfo FromMethodInfo(MethodBase methodInfo,
            ClassTranslationInfo classTranslationInfo)
        {
            var result = new MethodTranslationInfo
            {
                ScriptName = methodInfo.Name,
                ClassTi = classTranslationInfo
            };
            var scriptNameAttribute = methodInfo.GetCustomAttribute<ScriptNameAttribute>();
            if (scriptNameAttribute != null)
                result.ScriptName = scriptNameAttribute.Name.Trim();
            if (string.IsNullOrEmpty(result.ScriptName))
                throw new Exception("Method name is empty");
            return result;
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string ScriptName { get; private set; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public ClassTranslationInfo ClassTi { get; private set; }
    }
}