using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{
    public class ReflectionUtil
    {
        #region Static Methods

        // Public Methods 

        public static T GetAttribute<T>(ICustomAttributeProvider member) where T : Attribute
        {
            var attributes = GetAttributes<T>(member);
            return attributes.FirstOrDefault();
        }

        public static T[] GetAttributes<T>(ICustomAttributeProvider member) where T : Attribute
        {
            var attributes = member.GetCustomAttributes(true).OfType<T>().ToArray();
            if (attributes != null && attributes.Any())
                return attributes;
            var fn = typeof(T).FullName;
            var checkAttribute = member.GetCustomAttributes(true).Where(a => a.GetType().FullName == fn).FirstOrDefault();
            if (checkAttribute == null)
                return new T[0];
            var loadedAssembly = checkAttribute.GetType().Assembly.Location;
            var expectedAssembly = typeof(DirectCallAttribute).Assembly.Location;
            var loadedAssemblyVersion = checkAttribute.GetType().Assembly.GetName().Version;
            var expectedAssemblyVersion = typeof(DirectCallAttribute).Assembly.GetName().Version;
            throw new Exception(string.Format("Assembly Lang.Php {0} ver {2} has been loaded instead of {1} ver {3}",
                loadedAssembly, expectedAssembly, loadedAssemblyVersion, expectedAssemblyVersion));
        }

        #endregion Static Methods
    }
}
