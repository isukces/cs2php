using System.Collections.Generic;
using System.Reflection;

namespace Lang.Cs.Compiler
{
#if DEBUG

    public class DesignTimeUtils
    {


   


        public static string AAA(MethodInfo mi)
        {
            var p = mi.GetParameters();
            List<object> o = new List<object>();
            o.Add(mi.IsStatic ? "static" : "instance");
            o.Add(mi.IsSpecialName ? "special" : "normal");
            o.Add(TypesUtil.TypeToString(mi.DeclaringType));
            o.Add(TypesUtil.TypeToString(mi.ReflectedType));
            o.Add(mi.Name);
            o.Add(p.Length);

            o.Add(TypesUtil.TypeToString(mi.ReturnType));
            foreach (var i in p)
            {
                o.Add(i.Name);
                o.Add(TypesUtil.TypeToString(i.ParameterType));
            }
            return string.Join("\t", o);
        }
    }

#endif
}
