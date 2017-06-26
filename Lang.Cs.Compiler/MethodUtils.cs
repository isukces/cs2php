using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Lang.Cs.Compiler
{
    public static class MethodUtils
    {
        #region Static Methods 

        // Public Methods 

/*
        public static MethodInfo[] GetInstanceMethods(Type t)
        {
            return (from a in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    select a).ToArray();
        }
*/

        public static IEnumerable<MethodInfo> GetOperators(string name, params Type[] types)
        {
            var aa = from t in types.Distinct()
                from a in t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                where a.IsSpecialName && a.Name == name
                select a;
            var aaa = aa.ToArray();
            return aaa;
        }

/*
        public static MethodInfo[] GetStaticMethods(Type t)
        {
            return (from a in t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    select a).ToArray();
        }
*/


        public static bool IsExtensionMethod(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
                return false;
            return methodInfo.GetCustomAttribute<ExtensionAttribute>(true) != null;
        }
    


    #endregion Static Methods 

/*
        public static MethodInfo[] ListMethods(Type type, string methodName)
        {
            if (type == null) throw new ArgumentNullException("type");
            var key = string.Format("{0} # {1}", type.FullName, methodName);
            MethodInfo[] methods;
            if (O.TryGetValue(key, out methods))
                return methods;
            var methodInfos = new List<MethodInfo>();
            var names = new List<string>();
            var includePrivate = true;
            while (type != null && type != typeof(object))
            {
                var g = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Where(a => a.Name == methodName).ToArray();
                if (!includePrivate)
                    g = g.Where(i => !i.IsPrivate).ToArray();
                foreach (var i in g)
                {
                    // ta metoda jest konieczna jeśli definicje się przykrywają w dziedziczących klasach
                    var name = i.ToString();
                    if (names.Contains(name)) continue;
                    names.Add(name);
                    methodInfos.Add(i);
                }
                includePrivate = false;
                type = type.BaseType;
            }
            methods = methodInfos.Distinct().ToArray();
            O[key] = methods;
            return methods;
        }
*/
/*
        public static MethodInfo Match(IEnumerable<MethodInfo> methods, Type[] argTypes)
        {
            var a = TryMatch(methods, argTypes);
            if (a != null)
                return a;
            throw new NotSupportedException();
        }
*/
/*
        private static MethodInfo TryMatch(IEnumerable<MethodInfo> methods, Type[] argTypes)
        {
            var methodsArray = methods as MethodInfo[] ?? methods.ToArray();
            var strategies = Enum.GetValues(typeof(MethodMatchStrategy)).Cast<MethodMatchStrategy>().ToArray();
            var linq = from useImplicitOperator in Compiler.FalseTrueArray
                       from strategy in strategies
                       select Match(methodsArray, argTypes, useImplicitOperator, strategy);
            return linq.FirstOrDefault(methodInfo => methodInfo != null);
        }
*/
/*
        private static MethodInfo Match(IEnumerable<MethodInfo> methods, Type[] argTypes, bool useImplicitOperator, MethodMatchStrategy strategy)
        {

            var availableParameterCount = argTypes.Length;
            var candidates = new List<MethodRankInfo>();
            const int rankParamsarray = 0;
            const int rankNoParamsarray = 0;
            const int rankUseImplicitOperator = 1;
            const int rankDontUseImplicitOperator = 0;

            foreach (var method in methods)
            {
                if (method.IsGenericMethod)
                    throw new NotSupportedException();
                int rank = useImplicitOperator ? rankUseImplicitOperator : rankDontUseImplicitOperator;



                bool isExtensionMethod = IsExtensionMethod(method);
                var parameters = method.GetParameters();
                var pCountMin = parameters.Count(h => !h.HasDefaultValue);
                var pCountMax = parameters.Length;
                if (isExtensionMethod)
                {
                    pCountMin--;
                    pCountMax--;
                }


                bool hasParamAttribute = false;
                if (parameters.Any())
                {
                    hasParamAttribute = parameters.Last().IsDefined(typeof(ParamArrayAttribute));
                    if (hasParamAttribute)
                    {
                        if (pCountMin > 0) pCountMin--;
                        pCountMax = int.MaxValue;
                    }
                }
                if (availableParameterCount < pCountMin || availableParameterCount > pCountMax) continue;

                #region MyRegion
                int optionalParameterSkippedCount;
                {
                    if (hasParamAttribute)
                        optionalParameterSkippedCount = 0; // sprawdziłem ilość parametrów, więc tutaj wiem, że nie omijam żadnego
                    else
                        optionalParameterSkippedCount = parameters.Length - availableParameterCount;
                    if (isExtensionMethod)
                        optionalParameterSkippedCount--;
                }
                #endregion

                var canbe = Compiler.CheckParametesType(argTypes, parameters, isExtensionMethod, false, useImplicitOperator);
                if (canbe)
                    candidates.Add(new MethodRankInfo(method, rank + rankNoParamsarray, optionalParameterSkippedCount));
                else if (hasParamAttribute)
                {
                    canbe = Compiler.CheckParametesType(argTypes, parameters, isExtensionMethod, true, useImplicitOperator);
                    if (canbe)
                        candidates.Add(new MethodRankInfo(method, rank + rankParamsarray, optionalParameterSkippedCount));
                }
            }
            if (candidates.Count == 0)
                return null;
            if (candidates.Count == 1)
                return candidates.First().Method;
            switch (strategy)
            {
                case MethodMatchStrategy.OnlyOneAccepted:
                    return null;
                case MethodMatchStrategy.TakeLessParameterCount:
                    {
                        // sprawdzam minimalny ranking metod
                        var min = candidates.Min(i => i.Rank);
                        var minC = candidates.Count(i => i.Rank == min);
                        if (minC != candidates.Count) return null;
                        // wszystkie metody mają ten sam ranking -> sortuję po ilości pominiętych parametrów
                        candidates = candidates.OrderBy(i => i.NumberOfSkippedOptionalParameters).ToList();
                        min = candidates[0].NumberOfSkippedOptionalParameters;
                        minC = candidates.Count(i => i.NumberOfSkippedOptionalParameters == min);
                        return minC == 1 ? candidates[0].Method : null;
                    }



            }
            return null;
        }
*/
/*
        static readonly Dictionary<string, MethodInfo[]> O = new Dictionary<string, MethodInfo[]>();
*/
/*
        private enum MethodMatchStrategy
        {
            OnlyOneAccepted,
            TakeLessParameterCount
        }
*/
/*
        private class MethodRankInfo
        {

            public MethodRankInfo(MethodInfo method, int rank, int numberOfSkippedOptionalParameters)
            {
                Method = method;
                Rank = rank;
                NumberOfSkippedOptionalParameters = numberOfSkippedOptionalParameters;
            }



            public MethodInfo Method { get; private set; }

            public int NumberOfSkippedOptionalParameters { get; private set; }

            public int Rank { get; private set; }

        }
*/
    }
}
