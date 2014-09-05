using System;
using System.Linq;
using System.Reflection;

namespace Lang.Php.Compiler
{
    public static class LangPhpCompilerReflectionExtension
    {
        public static T[] GetAttributes<T>(this ICustomAttributeProvider member) where T : Attribute
        {
            var attributes = member.GetCustomAttributes(true).OfType<T>().ToArray();
            if (attributes.Any())
                return attributes;
            var fn = typeof(T).FullName;
            var checkAttribute = member.GetCustomAttributes(true).FirstOrDefault(a => a.GetType().FullName == fn);
            if (checkAttribute == null)
                return new T[0];
            var loadedAssembly = checkAttribute.GetType().Assembly.Location;
            var expectedAssembly = typeof(DirectCallAttribute).Assembly.Location;
            var loadedAssemblyVersion = checkAttribute.GetType().Assembly.GetName().Version;
            var expectedAssemblyVersion = typeof(DirectCallAttribute).Assembly.GetName().Version;
            throw new Exception(string.Format("Assembly Lang.Php {0} ver {2} has been loaded instead of {1} ver {3}",
            loadedAssembly, expectedAssembly, loadedAssemblyVersion, expectedAssemblyVersion));
        }


        public static T GetAttribute<T>(this ICustomAttributeProvider member) where T : Attribute
        {
            var attributes = GetAttributes<T>(member);
            return attributes.FirstOrDefault();
        }



        public static DirectCallAttribute GetDirectCallAttribute(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");
            return GetAttribute<DirectCallAttribute>(methodInfo);
        }
    }
}
