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
            var attribute = member.GetCustomAttributes(true).OfType<T>().FirstOrDefault();
            if (attribute != null)
                return attribute;
            var fn = typeof(T).FullName;
            var checkAttribute = member.GetCustomAttributes(true).Where(a => a.GetType().FullName == fn).FirstOrDefault();
            if (checkAttribute == null)
                return null;
            var loadedAssembly = checkAttribute.GetType().Assembly.Location;
            var expectedAssembly = typeof(DirectCallAttribute).Assembly.Location;
            throw new Exception(string.Format("Assembly Lang.Php {0} was loaded instead of {1}",
                loadedAssembly, expectedAssembly));
        }

        #endregion Static Methods
    }
}
