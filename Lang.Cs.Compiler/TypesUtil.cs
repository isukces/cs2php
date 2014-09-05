using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Lang.Cs.Compiler.Sandbox;

namespace Lang.Cs.Compiler
{

    public static class TypesUtil
    {
        public static Type GetEnumerateItemType(Type srcType)
        {
            //var dic = new Dictionary<int, string>();
            //foreach (KeyValuePair<int, string> g in dic)
            //{

            //}
            if (srcType == typeof(System.Collections.IEnumerable))
                return typeof(object);
            if (srcType.IsArray)
                return srcType.GetElementType();
            if (srcType.IsGenericType)
            {
                var generic = srcType.GetGenericTypeDefinition();
                var definitionTypes = srcType.GetGenericArguments();
                if (generic == typeof(Dictionary<,>))
                    return typeof(KeyValuePair<,>).MakeGenericType(definitionTypes);
                if (generic == typeof(List<>))
                    return definitionTypes[0];
                if (generic == typeof(IEnumerable<>))
                    return definitionTypes[0];
                throw new NotSupportedException();
            }
            throw new NotSupportedException(); // np. lista
        }
        [Obsolete]
        public static Type GetElementType(Type srcType)
        {

            if (srcType == typeof(string))
                return typeof(char);
            if (srcType.IsArray)
                return srcType.GetElementType();
            if (!srcType.IsGenericType) throw new NotSupportedException(); // np. lista
            var generic = srcType.GetGenericTypeDefinition();
            var definitionTypes = srcType.GetGenericArguments();
            if (generic == typeof(Dictionary<,>))
                return definitionTypes[1];
            if (generic == typeof(List<>))
                return definitionTypes[0];
            throw new NotSupportedException();

            //return srcType.GetElementTypeForLinq();
        }
        public static bool IsNumberType(Type t)
        {
            return t == typeof(Double)
                || t == typeof(Single)
                || t == typeof(Decimal)
                || t == typeof(Int64)
                || t == typeof(Int32)
                || t == typeof(Byte)
                || t == typeof(UInt64)
                || t == typeof(UInt32)
                || t == typeof(UInt16);
        }
        public static string CsStringToString(string o)
        {
            if (o == null)
                return "null";
            const string q = "\"";
            o = o.Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\\r")
                .Replace("\n", "\\\n")
                .Replace("\t", "\\\t");
            return q + o + q;
        }
        public static string CSValueToString(object o)
        {
            if (o == null)
                return "null";
            if (o is string)
                return CsStringToString(o as string);
            if (o is bool)
                return o.ToString().ToLower();
            return o.ToString();
        }

        public static bool IsAssignableFrom(Type required, Type given, bool useImplicitOperator)
        {
            if (required.IsAssignableFrom(given)) return true;
            if (required.IsArray && given.IsArray)
            {
                if (required.GetArrayRank() == given.GetArrayRank())
                {
                    if (IsAssignableFrom(required.GetElementType(), given.GetElementType(), useImplicitOperator))
                        return true;
                }
            }
            if (useImplicitOperator)
            {
                if (required == typeof(double) && given == typeof(int))
                    return true;
                var ops = MethodUtils.GetOperators("op_Implicit", new Type[] { required, given });
                foreach (var i in ops)
                {
                    var ps = i.GetParameters();
                    if (ps.Length == 1)
                    {
                        var a1 = i.ReturnType;
                        var b1 = ps[0].ParameterType;
                        if (required.IsAssignableFrom(a1))
                            if (b1.IsAssignableFrom(given))
                                return true;
                    }
                }
                return false; // ten return można pominąć, ale jest pomocny dla debugowania
            }
            return false;
        }

        public static FieldInfo[] ListFields(Type t, string identifier)
        {
            var fi = new List<FieldInfo>();
            var all = true;
            while ((object)t != null)
            {
                var g = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Where(a => a.Name == identifier).ToArray();
                fi.AddRange(all ? g : g.Where(i => !i.IsPrivate));
                all = false;
                t = t.BaseType;
            }
            return fi.ToArray();
        }



        public static MethodInfo[] GetExtensionMethods(Type searchType, string methodName, Type firsArg)
        {
            var extensionMethods = (
                     from method in searchType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                     where method.Name == methodName && method.IsExtensionMethod()
                     let parameters = method.GetParameters()
                     where parameters.Length > 0
                     let fa1 = parameters[0]
                     where fa1.ParameterType.IsAssignableFrom(firsArg)
                     select method).ToArray();
            return extensionMethods;
        }

        static TypesUtil()
        {
            PRIMITIVE_TYPES = new Dictionary<string, Type>()
            {
                {"void",   typeof(void)},         
                {"bool",    typeof(bool)},       
                {"object",   typeof(object)},       
                {"string",   typeof(string)},         
                {"char",   typeof(char)},         
                {"double",   typeof(double)},         
                {"int",   typeof(int)},         
                {"long",  typeof(long)},
                {"decimal",  typeof(decimal)}
            };
            PRIMITIVE_TYPES_REV = PRIMITIVE_TYPES.ToDictionary(a => a.Value, a => a.Key);
        }

        [Obsolete("to jest zależne od domeny i załadowanych assembly")]
        public static readonly Dictionary<string, Type> PRIMITIVE_TYPES;
        public static readonly Dictionary<Type, string> PRIMITIVE_TYPES_REV;


        public static bool IsMulticastDelegate(this Type type)
        {
            return type == typeof(MulticastDelegate);
        }

        public static string TypeToString(Type t)
        {
            if (t.IsArray)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("{0}[", TypeToString(t.GetElementType()));
                var q = t.GetArrayRank();
                for (int i = 01; i < q; i++)
                    builder.Append(",");
                builder.Append("]");
                return builder.ToString();
            }
            string o;
            return PRIMITIVE_TYPES_REV.TryGetValue(t, out o) ? o : t.ToString();
        }

        public static Type ResultType(MemberInfo mi)
        {
            if (mi is MethodInfo)
                return (mi as MethodInfo).ReturnType;
            if (mi is FieldInfo)
                return (mi as FieldInfo).FieldType;
            if (mi is PropertyInfo)
                return (mi as PropertyInfo).PropertyType;
            throw new NotImplementedException();
        }

        public static Type GetMemberType(LangType t, string name, bool isstatic)
        {
            if (t == null || (object)t.DotnetType == null)
                throw new Exception("");
            var bf = isstatic ? BindingFlags.Static : BindingFlags.Instance;
            var a = t.DotnetType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | bf).Where(i => i.Name == name).ToArray();
            if (a.Length == 1)
                return ResultType(a[0]);
            throw new NotSupportedException();
        }

        public static Type GetMemberType(IValue expression, string name)
        {
            if (expression is LangType)
                return GetMemberType(expression as LangType, name, true);
            throw new NotSupportedException();
        }
    }
}
