using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lang.Cs.Compiler.Visitors;

namespace Lang.Cs.Compiler
{
    public class CompileState
    {
        // Public Methods 

        public static Type[] GetAllTypes(IEnumerable<Assembly> assemblies)
        {
            assemblies = assemblies.Distinct().ToArray();
            var result = new Dictionary<string, Type>();
            foreach (var assembly in assemblies)
                GetTypesX(assembly, result);
            return result.Values.ToArray();
        }
        // Private Methods 

        private static void GetTypesX(Assembly a, Dictionary<string, Type> addTo)
        {
            foreach (var type in a.GetTypes())
                GetTypesX(type, addTo);
        }

        private static void GetTypesX(Type aType, Dictionary<string, Type> addTo)
        {
            var assemblyQualifiedName2 = aType.IdString();
            if (addTo.ContainsKey(assemblyQualifiedName2))
                return;
            addTo.Add(assemblyQualifiedName2, aType);
            var tmp = aType.GetNestedTypes();
            if (tmp.Length != 0)
            {
                foreach (var qType in tmp)
                    addTo[qType.IdString()] = qType;
                foreach (var i in tmp)
                    GetTypesX(i, addTo);
            }

            var mm =
                aType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                 BindingFlags.Static);
            var mm1 = mm.Where(i => i.IsGenericMethod).ToArray();
            if (!mm1.Any()) return;

            var enumerableTypes = from i in mm1
                let types = i.GetGenericArguments()
                from type in types
                select type;
            foreach (var type in enumerableTypes)
                GetTypesX(type, addTo);
        }

        /// <summary>
        /// </summary>
        public Type CurrentType { get; set; }

        /// <summary>
        /// </summary>
        public LangParseContext Context { get; set; } = new LangParseContext();
    }
}