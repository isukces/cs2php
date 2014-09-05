using System;

namespace Lang.Cs.Compiler
{
    public static class ReflectionExtension
    {
		#region Static Methods 

		// Public Methods 

        public static string IdString(this Type type)
        {
            var result = type.AssemblyQualifiedName;
            if (result != null) return result;
            if (type.DeclaringMethod == null) return result;
            if (type.DeclaringMethod.ReflectedType != null)
                return type.DeclaringMethod.ReflectedType.AssemblyQualifiedName + "/" + type.Name;
            return result;

        }

		#endregion Static Methods 
    }
}
