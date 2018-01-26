using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lang.Cs.Compiler.Visitors
{
    /// <summary>
    ///     Helps to find generic meaning for generic method calling
    /// </summary>
    internal class GenericTypesResolver
    {
        public GenericTypesResolver(MethodBase mi, FunctionArgument[] functionArguments)
        {
            _mi                = mi;
            _functionArguments = functionArguments;
            _genericArguments  = mi.GetGenericArguments();
        }

        public Type[] Find()
        {
            _typeMappings = new Dictionary<Type, Type>();

            var methodParameters = _mi.GetParameters();
            var mappings         = MapArgumentsToMethodParameters(methodParameters, _functionArguments);
            foreach (var methodParameter in methodParameters)
                if (mappings.TryGetValue(methodParameter.Name, out var parameter))
                    Fill(methodParameter.ParameterType, parameter.MyValue.ValueType);

            var genericTypes = new Type[_genericArguments.Length];
            for (var index = 0; index < _genericArguments.Length; index++)
            {
                var genericType = _genericArguments[index];
                if (_typeMappings.TryGetValue(genericType, out var found))
                    genericTypes[index] = found;
                else
                    throw new NotSupportedException("Unable to resolve generic type " + genericType);
            }

            return genericTypes;
        }

        private void Fill(Type generic, Type nonGeneric)
        {
            if (nonGeneric.IsGenericType)
            {
                if (nonGeneric.IsGenericTypeDefinition)
                    throw new Exception("Type can't be GenericTypeDefinition");
                var nestedNonGeneric = nonGeneric.GetGenericArguments();
                var nestedGeneric    = generic.GetGenericArguments();
                if (nestedNonGeneric.Length != nestedGeneric.Length)
                    throw new NotSupportedException();
                for (var index = 0; index < nestedNonGeneric.Length; index++)
                    Fill(nestedGeneric[index], nestedNonGeneric[index]);
            }

            _typeMappings[generic] = nonGeneric;
        }

        private static IReadOnlyDictionary<string, FunctionArgument> MapArgumentsToMethodParameters(
            IReadOnlyList<ParameterInfo> methodParameters, IReadOnlyList<FunctionArgument> functionArguments)
        {
            var reqName  = false;
            var mappings = new Dictionary<string, FunctionArgument>();
            for (var index = 0; index < functionArguments.Count; index++)
            {
                var arg = functionArguments[index];
                if (string.IsNullOrEmpty(arg.ExplicitName))
                {
                    if (reqName)
                        throw new Exception("Parameter nr " + index + " requires name");
                    mappings[methodParameters[index].Name] = arg;
                }
                else
                {
                    reqName                    = true;
                    mappings[arg.ExplicitName] = arg;
                }
            }
            return mappings;
        }

        private readonly MethodBase             _mi;
        private readonly FunctionArgument[]     _functionArguments;
        private          Dictionary<Type, Type> _typeMappings;
        private readonly Type[]                 _genericArguments;
    }
}