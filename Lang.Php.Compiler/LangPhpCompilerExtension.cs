using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{
    public static class LangPhpCompilerExtension
    {
        public static string ExcName(this Type type)
        {
            return type == null
                ? "[EMPTY TYPE]"
                : (type.FullName ?? type.Name);
        }

        public static string ExcName(this FieldInfo fieldInfo)
        {
            return fieldInfo == null
                ? "[EMPTY FieldInfo]"
                : string.Format("{0}.{1}", fieldInfo.Name, fieldInfo.DeclaringType.ExcName());
        }

        public static string ExcName(this MethodInfo fieldInfo)
        {
            return fieldInfo == null
                ? "[EMPTY FieldInfo]"
                : string.Format("{0}.{1}", fieldInfo.Name, fieldInfo.DeclaringType.ExcName());
        }


        public static bool IsEmpty(this PhpCodeModuleName phpCodeModuleName)
        {
            return phpCodeModuleName == null || phpCodeModuleName.IsEmpty;
        }
    }
}
