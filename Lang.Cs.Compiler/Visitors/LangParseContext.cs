using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Compilation = Microsoft.CodeAnalysis.Compilation;
using MethodKind = Microsoft.CodeAnalysis.MethodKind;
using SemanticModel = Microsoft.CodeAnalysis.SemanticModel;
using TypeKind = Microsoft.CodeAnalysis.TypeKind;

namespace Lang.Cs.Compiler.Visitors
{
    /*
    smartClass
    option NoAdditionalFile
    
    property CurrentNamespace string 
    
    property ImportedNamespaces List<ImportNamespace> 
    	init #
    
    property ClassNames Stack<string> 
    	init #
    
    property KnownTypes Type[] 
    
    property LocalVariables List<NameType> 
    	init #
    
    property Arguments List<FunctionDeclarationParameter> 
    	init #
    
    property RoslynCompilation Compilation 
    	OnChange ClearCache();
    
    property RoslynModel SemanticModel 
    	OnChange ClearCache();
    smartClassEnd
    */

    public partial class LangParseContext
    {
        #region Methods

        // Public Methods 

        public void DoWithLocalVariables(VariableDeclaration declaration, Action action)
        {
            var savedVariables = localVariables.ToArray();
            try
            {
                if (declaration != null)
                    foreach (var i in declaration.Declarators)
                        localVariables.Add(new NameType(i.Name, declaration.Type));
                action();
            }
            finally
            {
                localVariables = savedVariables.ToList();
            }
        }

        public T DoWithLocalVariables<T>(VariableDeclaration declaration, Func<T> action)
        {
            var savedVariables = localVariables.ToArray();
            try
            {
                if (declaration == null) 
                    return action();
                foreach (var i in declaration.Declarators)
                    localVariables.Add(new NameType(i.Name, declaration.Type));
                return action();
            }
            finally
            {
                localVariables = savedVariables.ToList();
            }
        }

        public NameType GetLocalVariableByName(string variableName)
        {
            var g = localVariables.Where(u => u.Name == variableName).ToArray();
            if (g.Length == 1)
                return g.First();
            throw new Exception("rough salamander, unable to find local variable " + variableName);
        }

        public Type[] MatchTypes(string name, int gen)
        {
            if (gen > 0)
                name += "`" + gen.ToString();

            if (TypesUtil.PRIMITIVE_TYPES.ContainsKey(name))
                return new Type[] { TypesUtil.PRIMITIVE_TYPES[name] };
            var t1 = KnownTypes.Where(i => i.FullName == name).ToArray();
            if (t1.Length == 1)
                return t1;
            if (t1.Length > 1)
                throw new Exception("pink sparrow");

            var fullNames = importedNamespaces.Select(i => i.Name + "." + name).Distinct().ToList();
            if (!string.IsNullOrEmpty(currentNamespace))
                fullNames.Add(currentNamespace + "." + name);
            var types = (from fullname in fullNames.Distinct()
                         join type in KnownTypes
                         on fullname equals type.FullName
                         select type).ToArray();

            return types;
        }

        public INamedTypeSymbol[] Roslyn_GetNamedTypeSymbols(INamespaceSymbol m)
        {
            if (m == null)
                m = roslynCompilation.GlobalNamespace;
            var a = m.GetTypeMembers().ToArray();
            var b = (from mm in m.GetNamespaceMembers()
                     from h in Roslyn_GetNamedTypeSymbols(mm)
                     select h).ToArray();
            return a.Union(b).ToArray();
        }

        public FieldInfo Roslyn_ResolveField(IFieldSymbol type)
        {
#warning 'Dorobić const'
            var f = type.IsStatic ? BindingFlags.Static : BindingFlags.Instance;
            f |= BindingFlags.Public | BindingFlags.NonPublic;


            var ct = Roslyn_ResolveType(type.ContainingType);
            var fi = ct.GetField(type.Name, f);
            return fi;
            throw new NotSupportedException();

        }

        public MethodBase Roslyn_ResolveMethod(IMethodSymbol method)
        {
            var m = Roslyn_ResolveMethodInternal(method);
            //Console.WriteLine("M  {0} => {1}", method, m);
            //Console.WriteLine("   {0}", m.DeclaringType);
            //Console.WriteLine("   {0}", m);
            return m;
        }

        public PropertyInfo Roslyn_ResolveProperty(IPropertySymbol type)
        {
            var t = type.ContainingType;
            var tt = Roslyn_ResolveType(t);
            var ttt = tt.GetProperty(type.Name);
            return ttt;
            throw new NotSupportedException();

        }

        public Type Roslyn_ResolveType(ITypeSymbol type)
        {
            Type t;
            if (_cacheRoslynResolveType.TryGetValue(type, out t))
                return t;
            t = Roslyn_ResolveType_internal(type);
            _cacheRoslynResolveType[type] = t;
            return t;
        }
        // Private Methods 

        private MethodBase _Resolve_OrdinaryMethod(IMethodSymbol method)
        {
            var reflectionFlags = method.IsStatic ? BindingFlags.Static : BindingFlags.Instance;
            reflectionFlags |= BindingFlags.Public | BindingFlags.NonPublic;

            var rt = Roslyn_ResolveType(method.ReturnType);
            var _typeArguments = method.TypeArguments.ToArray();
            if (_typeArguments == null) 
                throw new ArgumentNullException("_typeArguments");
            var typeArguments = _typeArguments.Select(Roslyn_ResolveType).ToArray();
            var rType = Roslyn_ResolveType(method.ReturnType);
            var pTypes = ConvertParameterTypes(method.Parameters.ToArray());
            var hostType = Roslyn_ResolveType(method.ContainingType);

            var AllMethods = hostType.GetMethods(reflectionFlags).Where(i => i.Name == method.Name).ToArray();
            var AllMethods1 = (from i in AllMethods
                               where cs(method, i)
                               select i
                                   ).ToArray();
            if (typeArguments.Length == 0)
            {
                if (AllMethods.Length == 1)
                    return AllMethods[0];
                {
                    var a = AllMethods
                            .Where(i =>
                                i.Name == method.Name
                                && !i.IsGenericMethodDefinition
                                && CompareArrayTypes(i.GetParameters(), pTypes)).ToArray();
                    if (a.Length > 0)
                    {
                        // throw new Exception(string.Format("Unable to find method {1}.{0}", method.Name,  hostType.FullName));
                        if (a.Length == 1)
                            return a.First();
                        if (a.Length == 0)
                            throw new Exception(string.Format("Unable to find method {1}.{0}", method.Name, hostType.FullName));
                        a = a.Where(i => i.ReturnType == rType).ToArray();
                        if (a.Length == 1)
                            return a.First();
                    }
                }
                MethodInfo m = null;
                try
                {
                    m = hostType.GetMethod(method.Name, pTypes);
                }
                catch (System.Reflection.AmbiguousMatchException)
                {
                    m = null;
                }
                if (m != null)
                    return m;
                throw new Exception();
            }
            else
            {
                Type[] pTypes1 = pTypes;
                if (!hostType.IsGenericType)
                {
                    //metody generyczne w niegenerycznej klasie
                    pTypes1 = (from i in pTypes
                               select i.IsGenericType && !i.IsGenericTypeDefinition
                               ? i.GetGenericTypeDefinition()
                               : i
                                   ).ToArray();
                    if (rType.IsGenericType && !rType.IsGenericTypeDefinition)
                        rType = rType.GetGenericTypeDefinition();
                }
                var arity = typeArguments.Length;
                var am = hostType.GetMethods(reflectionFlags);
                var am1 = (from i in am
                           where
                           i.Name == method.Name
                           && i.IsGenericMethodDefinition
                           && i.GetGenericArguments().Length == arity
                           let aa = i.MakeGenericMethod(typeArguments)
                           // where aa.ReturnType == rt
                           select new
                           {
                               Method = i,
                               GMethod = aa,
                               Rt = aa.ReturnType
                           }
                               ).ToArray();
                MethodInfo[] a = (from i in am1 where CompareTypes(i.Rt, rt, true) select i.Method).ToArray();
                //var am1 = am.Where(i => i.Name == method.Name && i.IsGenericMethodDefinition)
                //    .Select(i => new { Method = i, GA = i.GetGenericArguments() })
                //    .ToArray();
                //MethodInfo[] a = (from i in am1
                //                  where i.GA.Length == arity
                //                  let aa = i.Method.MakeGenericMethod(typeArguments)
                //                  where aa.ReturnType == rt
                //                  select i.Method
                //                      ).ToArray();
                if (a.Length == 0)
                    throw new Exception(string.Format("Unable to find method {2}.{0} with arity {1}", method.Name, arity, hostType.FullName));
                if (a.Length == 1)
                {
                    var yy = a[0].MakeGenericMethod(typeArguments);
                    return yy;
                }
                {
                    var method2 = hostType.GetMethod(
                        method.Name, reflectionFlags,
                        null,
                        pTypes,
                        null);

                }
                var hh = method.IsDefinition;
                var ggg = method.IsGenericMethod;
                //Type[] pTypes1 = (from i in pTypes
                //                  select i.IsGenericType && !i.IsGenericTypeDefinition
                //                  ? i.GetGenericTypeDefinition()
                //                  : i
                //               ).ToArray();
                var m = hostType.GetMethod(method.Name, pTypes1);
                if (m != null)
                    return m;
                if (AllMethods1.Length == 1)
                {
                    m = AllMethods1[0];
                    return m;
                }

                throw new Exception();
            }
        }

        void ClearCache()
        {
            _roslynAllNamedTypeSymbols = null;
            _cacheRoslynResolveType.Clear();
        }

        /// <summary>
        /// it is only workaround method
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="ignoreAssemblyVersion"></param>
        /// <returns></returns>
        private static bool CompareTypes(Type a, Type b, bool ignoreAssemblyVersion)
        {
            if (a == b)
                return true;
            if (!ignoreAssemblyVersion)
                return false;
            return a.FullName == b.FullName;
        }

        private bool CompareArrayTypes(ParameterInfo[] a, Type[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                var g = a[i].ParameterType;
                var bb = b[i];
                if (g != bb)
                    return false;
            }
            return true;
        }

        private Type[] ConvertParameterTypes(IList<IParameterSymbol> parameters)
        {
            var pTypes = parameters.Select(i => Roslyn_ResolveType(i.Type)).ToArray();
            {
                for (var i = 0; i < pTypes.Length; i++)
                {
                    if (pTypes[i] == null) continue;
                    if (parameters[i].RefKind !=  RefKind.None)
                        pTypes[i] = pTypes[i].MakeByRefType();
                }
            }
            return pTypes;
        }

        private bool cs(IMethodSymbol a, MethodInfo b)
        {
            if (a.Name != b.Name)
                return false;
            var c = a.Parameters.AsEnumerable().Select(i => i.Name).ToArray();
            var d = b.GetParameters().Select(i => i.Name).ToArray();
            var cc = string.Join("\r\n", c);
            var dd = string.Join("\r\n", d);
            return cc == dd;
        }

        private Type MKK(Type rt)
        {
            if (rt.IsGenericType && rt.IsGenericTypeDefinition)
                rt = rt.GetGenericTypeDefinition();
            return rt;
        }

        private MethodBase Roslyn_ResolveMethodInternal(IMethodSymbol method)
        {
            switch (method.MethodKind)
            {
                case MethodKind.Constructor: // 1 Konstruktor
                    {
                        var hostType = Roslyn_ResolveType(method.ContainingType);
                        var pTypes = ConvertParameterTypes(method.Parameters.ToArray());
                        var ci = hostType.GetConstructor(pTypes);
                        if (ci != null)
                            return ci;
                        throw new NotSupportedException();
                    }
                case MethodKind.Conversion: // 2 A user-defined conversion.
                    goto case MethodKind.Ordinary;
                case MethodKind.DelegateInvoke: // 3 The invoke method of a delegate.
                    goto case MethodKind.Ordinary;
                case MethodKind.UserDefinedOperator:// 9 A user-defined operator
                    goto case MethodKind.Ordinary;
                case MethodKind.Ordinary: // 10 A normal method.
                    return _Resolve_OrdinaryMethod(method);
                case MethodKind.ReducedExtension: // 13 An extension method with the "this" parameter removed.
                    if (method.ReducedFrom != null)
                    {
                        var m1 = Roslyn_ResolveMethod(method.ReducedFrom);
                        return m1;
                    }
                    else
                    {
                        var m1 = Roslyn_ResolveMethod(method);
                        return m1;
                    }
            };
            throw new NotSupportedException();

            /*        //
      
        // Summary:
        //     A destructor.
        Destructor = 4,
        //
        // Summary:
        //     The implicitly-defined add method associated with an event.
        EventAdd = 5,
        //
        // Summary:
        //     The implicitly-defined remove method associated with an event.
        EventRemove = 7,
        //
        // Summary:
        //     An explicit interface implementation method. The ImplementedMethods property
        //     can be used to determine which method is being implemented.
        ExplicitInterfaceImplementation = 8,
        
        // Summary:
        //     The implicitly-defined get method associated with a property.
        PropertyGet = 11,
        //
        // Summary:
        //     The implicitly-defined set method associated with a property.
        PropertySet = 12,        
        //
        // Summary:
        //     A static constructor. The return type is always void.
        StaticConstructor = 14,*/

        }

        Type Roslyn_ResolveType_internal(ITypeSymbol type)
        {
            // var aaaa = roslynCompilation.GetSpecialType(type.SpecialType);
            switch (type.SpecialType)
            {
                case SpecialType.System_String:
                    return typeof(string);
                case SpecialType.System_Double:
                    return typeof(System.Double);
                case SpecialType.System_Decimal:
                    return typeof(System.Decimal);
                case SpecialType.System_Int16:
                    return typeof(System.Int16);
                case SpecialType.System_Int32:
                    return typeof(System.Int32);
                case SpecialType.System_Int64:
                    return typeof(System.Int64);
                case SpecialType.System_Object:
                    return typeof(System.Object);
                case SpecialType.System_Boolean:
                    return typeof(System.Boolean);
                case SpecialType.System_Char:
                    return typeof(System.Char);
                case SpecialType.System_Void:
                    return typeof(void);
                case SpecialType.System_Array:
                    return typeof(Array);
                case SpecialType.System_DateTime:
                    return typeof(DateTime);
                case SpecialType.System_Enum:
                    return typeof(Enum);
                default:
                    throw new NotSupportedException(type.ToString());


                case SpecialType.None:
                    {

                        switch (type.TypeKind)
                        {
                            case TypeKind.ArrayType:
                                {
                                    var arrayTypeSymbol = type as IArrayTypeSymbol;
                                    var elementType = Roslyn_ResolveType(arrayTypeSymbol.ElementType);
                                    var result = arrayTypeSymbol.Rank == 1
                                        ? elementType.MakeArrayType()
                                        : elementType.MakeArrayType(arrayTypeSymbol.Rank);
                                    return result;
                                }
                            case TypeKind.Error:
                                return null;
                            case TypeKind.TypeParameter:
                                {
                                    var type1 = (ITypeParameterSymbol)type;
                                    var a = knownTypes.Where(i => i.IsGenericParameter && i.DeclaringMethod != null).ToArray();
                                    var b = a.Where(i => i.DeclaringMethod.Name == type1.DeclaringMethod.Name).ToArray();

                                    MethodInfo mi;
                                    // mi.GetGenericArguments()
                                    return null;
                                }
                        }
                        //var a = type.ToDisplayString();
                        //var v = roslynCompilation.GlobalNamespace.GetNamespaceMembers().ToArray(); //.GetTypeByMetadataName(a);
                        //var v5 = v[5].GetTypeMembers();
                        if (_roslynAllNamedTypeSymbols == null)
                            _roslynAllNamedTypeSymbols = Roslyn_GetNamedTypeSymbols(null);

                        if (type is INamedTypeSymbol)
                        {
                            var b = type as INamedTypeSymbol;
                            //if (b.ContainingType != null)
                            //    throw new NotSupportedException();
                            if (b.IsGenericType)
                            {
                                var reflectionSearch = b.ToDisplayString();
                                // var reflectionSearchMd = b.OriginalDefinition.MetadataName;
                                // var reflectionSearchXx = b.ConstructUnboundGenericType();
                                var reflectionSearch2 = b.ConstructedFrom.ToDisplayString();
                                if (reflectionSearch != reflectionSearch2)
                                    reflectionSearch = reflectionSearch2;
                                reflectionSearch = reflectionSearch.Substring(0, reflectionSearch.IndexOf("<", System.StringComparison.Ordinal)) + "`" + b.Arity.ToString();
                                var reflected = KnownTypes.Single(i => i.FullName == reflectionSearch);


                                var TypeArguments = b.TypeArguments.AsEnumerable().Select(i => Roslyn_ResolveType(i)).ToArray();
                                var reflected2 = reflected.MakeGenericType(TypeArguments);
                                return reflected2;
                            }
                            else
                            {
                                if (b.ContainingType != null)
                                {
                                    var ct = Roslyn_ResolveType(b.ContainingType);
                                    var kt = KnownTypes.Where(i => i.DeclaringType == ct).ToArray();
                                    var reflectionSearch = b.ContainingType.ToDisplayString() + "+" + b.Name;
                                    var reflected = KnownTypes.Single(i => i.FullName == reflectionSearch);
                                    return reflected;
                                }
                                else
                                {
                                    var reflectionSearch = b.ToDisplayString();
                                    var reflected = KnownTypes.Single(i => i.FullName == reflectionSearch);
                                    return reflected;

                                }
                            }
                        }


                        throw new NotSupportedException(type.ToString());
                    }


            }

        }

        #endregion Methods

        #region Fields

        readonly Dictionary<ITypeSymbol, Type> _cacheRoslynResolveType = new Dictionary<ITypeSymbol, Type>();
        ITypeSymbol[] _roslynAllNamedTypeSymbols;

        #endregion Fields
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-07 11:55
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler.Visitors
{
    public partial class LangParseContext
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public LangParseContext()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##CurrentNamespace## ##ImportedNamespaces## ##ClassNames## ##KnownTypes## ##LocalVariables## ##Arguments## ##RoslynCompilation## ##RoslynModel##
        implement ToString CurrentNamespace=##CurrentNamespace##, ImportedNamespaces=##ImportedNamespaces##, ClassNames=##ClassNames##, KnownTypes=##KnownTypes##, LocalVariables=##LocalVariables##, Arguments=##Arguments##, RoslynCompilation=##RoslynCompilation##, RoslynModel=##RoslynModel##
        implement equals CurrentNamespace, ImportedNamespaces, ClassNames, KnownTypes, LocalVariables, Arguments, RoslynCompilation, RoslynModel
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności CurrentNamespace; 
        /// </summary>
        public const string PROPERTYNAME_CURRENTNAMESPACE = "CurrentNamespace";
        /// <summary>
        /// Nazwa własności ImportedNamespaces; 
        /// </summary>
        public const string PROPERTYNAME_IMPORTEDNAMESPACES = "ImportedNamespaces";
        /// <summary>
        /// Nazwa własności ClassNames; 
        /// </summary>
        public const string PROPERTYNAME_CLASSNAMES = "ClassNames";
        /// <summary>
        /// Nazwa własności KnownTypes; 
        /// </summary>
        public const string PROPERTYNAME_KNOWNTYPES = "KnownTypes";
        /// <summary>
        /// Nazwa własności LocalVariables; 
        /// </summary>
        public const string PROPERTYNAME_LOCALVARIABLES = "LocalVariables";
        /// <summary>
        /// Nazwa własności Arguments; 
        /// </summary>
        public const string PROPERTYNAME_ARGUMENTS = "Arguments";
        /// <summary>
        /// Nazwa własności RoslynCompilation; 
        /// </summary>
        public const string PROPERTYNAME_ROSLYNCOMPILATION = "RoslynCompilation";
        /// <summary>
        /// Nazwa własności RoslynModel; 
        /// </summary>
        public const string PROPERTYNAME_ROSLYNMODEL = "RoslynModel";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string CurrentNamespace
        {
            get
            {
                return currentNamespace;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                currentNamespace = value;
            }
        }
        private string currentNamespace = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<ImportNamespace> ImportedNamespaces
        {
            get
            {
                return importedNamespaces;
            }
            set
            {
                importedNamespaces = value;
            }
        }
        private List<ImportNamespace> importedNamespaces = new List<ImportNamespace>();
        /// <summary>
        /// 
        /// </summary>
        public Stack<string> ClassNames
        {
            get
            {
                return classNames;
            }
            set
            {
                classNames = value;
            }
        }
        private Stack<string> classNames = new Stack<string>();
        /// <summary>
        /// 
        /// </summary>
        public Type[] KnownTypes
        {
            get
            {
                return knownTypes;
            }
            set
            {
                knownTypes = value;
            }
        }
        private Type[] knownTypes;
        /// <summary>
        /// 
        /// </summary>
        public List<NameType> LocalVariables
        {
            get
            {
                return localVariables;
            }
            set
            {
                localVariables = value;
            }
        }
        private List<NameType> localVariables = new List<NameType>();
        /// <summary>
        /// 
        /// </summary>
        public List<FunctionDeclarationParameter> Arguments
        {
            get
            {
                return arguments;
            }
            set
            {
                arguments = value;
            }
        }
        private List<FunctionDeclarationParameter> arguments = new List<FunctionDeclarationParameter>();
        /// <summary>
        /// 
        /// </summary>
        public Compilation RoslynCompilation
        {
            get
            {
                return roslynCompilation;
            }
            set
            {
                if (value == roslynCompilation) return;
                roslynCompilation = value;
                ClearCache();
            }
        }
        private Compilation roslynCompilation;
        /// <summary>
        /// 
        /// </summary>
        public SemanticModel RoslynModel
        {
            get
            {
                return roslynModel;
            }
            set
            {
                if (value == roslynModel) return;
                roslynModel = value;
                ClearCache();
            }
        }
        private SemanticModel roslynModel;
        #endregion Properties
    }
}
