using System;
using System.Reflection;
using System.Text;

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
            var builder = new StringBuilder();
            builder.Append(GetVisibility(method.IsPublic, method.IsPrivate) + " ");
            builder.Append(TypesUtil.TypeToString(method.ReturnType) + " ");
            builder.Append(method.Name);
            builder.Append("(");
            bool addC = false;
            foreach (var a in method.GetParameters())
            {
                if (a.IsDefined(typeof(ParamArrayAttribute)))
                    builder.Append("params ");
                if (addC) builder.Append(", ");
                addC = true;
                if (a.IsOut)
                    builder.Append("out ");
                else if (a.IsRetval)
                    builder.AppendFormat("ref");
                builder.AppendFormat("{0} {1}", TypesUtil.TypeToString(a.ParameterType), a.Name);
                if (a.HasDefaultValue)
                {
                    builder.AppendFormat(" = {0}", TypesUtil.CSValueToString(a.DefaultValue));
                }
            }
            builder.Append(")");
            return builder.ToString();

        }
    }
}
