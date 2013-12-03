using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Cs.Compiler
{
    public class MethodUtils
    {
        #region Static Methods

        // Public Methods 

        public static MethodInfo[] GetInstanceMethods(Type t)
        {
            return (from a in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    select a).ToArray();
        }

        public static MethodInfo[] GetOperators(string name, params Type[] types)
        {
            var aa = from t in types.Distinct()
                     from a in t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                     where a.IsSpecialName && a.Name == name
                     select a;
            var aaa = aa.ToArray();
            return aaa;
        }

        public static MethodInfo[] GetStaticMethods(Type t)
        {
            return (from a in t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    select a).ToArray();
        }

        public static bool IsExtensionMethod(MethodInfo method)
        {
            if (method == null)
                return false;
            return method.GetCustomAttribute<ExtensionAttribute>(true) != null;
        }

        public static MethodInfo[] ListMethods(Type type, string methodName)
        {
            var key = string.Format("{0} # {1}", type.FullName, methodName);
            MethodInfo[] r;
            if (o.TryGetValue(key, out r))
                return r;
            List<MethodInfo> fi = new List<MethodInfo>();
            List<string> names = new List<string>();
            bool includePrivate = true;
            while (type != typeof(object))
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
                    fi.Add(i);
                }
                includePrivate = false;
                type = type.BaseType;
            }
            r = fi.Distinct().ToArray();
            o[key] = r;
            return r;
        }

        public static MethodInfo Match(IEnumerable<MethodInfo> methods, Type[] argTypes)
        {
            var a = TryMatch(methods, argTypes);
            if (a != null)
                return a;
            throw new NotSupportedException();
        }

        public static MethodInfo TryMatch(IEnumerable<MethodInfo> methods, Type[] argTypes)
        {
            var strategies = Enum.GetValues(typeof(MethodMatchStrategy)).Cast<MethodMatchStrategy>().ToArray();
            foreach (var useImplicitOperator in Compiler._false_true_)
            {
                foreach (var strategy in strategies)
                {
                    var g = Match(methods, argTypes, useImplicitOperator, strategy);
                    if (g != null)
                        return g;
                }
            }
            return null;
        }
        // Private Methods 

        private static MethodInfo Match(IEnumerable<MethodInfo> methods, Type[] argTypes, bool useImplicitOperator, MethodMatchStrategy strategy)
        {

            var availableParameterCount = argTypes.Length;
            List<MethodRankInfo> candidates = new List<MethodRankInfo>();
            const int RANK_PARAMSARRAY = 0;
            const int RANK_NO_PARAMSARRAY = 0;
            const int RANK_USE_IMPLICIT_OPERATOR = 1;
            const int RANK_DONT_USE_IMPLICIT_OPERATOR = 0;

            foreach (var method in methods)
            {
                if (method.IsGenericMethod)
                    throw new NotSupportedException();
                int rank = useImplicitOperator ? RANK_USE_IMPLICIT_OPERATOR : RANK_DONT_USE_IMPLICIT_OPERATOR;



                bool isExtensionMethod = MethodUtils.IsExtensionMethod(method);
                var parameters = method.GetParameters();
                var pCountMin = parameters.Where(h => !h.HasDefaultValue).Count();
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
                    candidates.Add(new MethodRankInfo(method, rank + RANK_NO_PARAMSARRAY, optionalParameterSkippedCount));
                else if (hasParamAttribute)
                {
                    canbe = Compiler.CheckParametesType(argTypes, parameters, isExtensionMethod, true, useImplicitOperator);
                    if (canbe)
                        candidates.Add(new MethodRankInfo(method, rank + RANK_PARAMSARRAY, optionalParameterSkippedCount));
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
                        var minC = candidates.Where(i => i.Rank == min).Count();
                        if (minC == candidates.Count)
                        {
                            // wszystkie metody mają ten sam ranking -> sortuję po ilości pominiętych parametrów
                            candidates = candidates.OrderBy(i => i.NumberOfSkippedOptionalParameters).ToList();
                            min = candidates[0].NumberOfSkippedOptionalParameters;
                            minC = candidates.Where(i => i.NumberOfSkippedOptionalParameters == min).Count();
                            if (minC == 1)
                                return candidates[0].Method; // jedna metoda ma wyraźnie mniejszą ilość pominiętych parametrów od innych metod
                        }
                        return null;
                    }



            }
            return null;
        }

        #endregion Static Methods

        #region Static Fields

        static Dictionary<string, MethodInfo[]> o = new Dictionary<string, MethodInfo[]>();

        #endregion Static Fields

        #region Enums

        public enum MethodMatchStrategy
        {
            OnlyOneAccepted,
            TakeLessParameterCount
        }

        #endregion Enums

        #region Nested Classes

        public class MethodRankInfo
        {
            #region Constructors

            public MethodRankInfo(MethodInfo Method, int Rank, int NumberOfSkippedOptionalParameters)
            {
                this.Method = Method;
                this.Rank = Rank;
                this.NumberOfSkippedOptionalParameters = NumberOfSkippedOptionalParameters;
            }

            #endregion Constructors

            #region Properties

            public MethodInfo Method { get; private set; }

            public int NumberOfSkippedOptionalParameters { get; private set; }

            public int Rank { get; private set; }

            #endregion Properties
        }
        #endregion Nested Classes
    }
}
