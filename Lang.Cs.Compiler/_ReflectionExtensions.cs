using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs.Compiler
{
    public static class _ReflectionExtensions
    {
        public static string GetVisibility(bool isPublic, bool isPrivate)
        {
            if (isPublic)
                return "public";
            if (isPrivate)
                return "private";
            return "protected";
        }
        public static string GetMethodHeader(this MethodInfo method)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetVisibility(method.IsPublic, method.IsPrivate) + " ");
            sb.Append(TypesUtil.TypeToString(method.ReturnType) + " ");
            sb.Append(method.Name);
            sb.Append("(");
            bool addC = false;
            foreach (var a in method.GetParameters())
            {
                if (a.IsDefined(typeof(ParamArrayAttribute)))
                    sb.Append("params ");
                if (addC) sb.Append(", ");
                addC = true;
                if (a.IsOut)
                    sb.Append("out ");
                else if (a.IsRetval)
                    sb.AppendFormat("ref");
                sb.AppendFormat("{0} {1}", TypesUtil.TypeToString(a.ParameterType), a.Name);
                if (a.HasDefaultValue)
                {
                    sb.AppendFormat(" = {0}", TypesUtil.CSValueToString(a.DefaultValue));
                }
            }
            sb.Append(")");
            return sb.ToString();

        }
    }
}
