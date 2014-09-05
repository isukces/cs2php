using Lang.Cs.Compiler.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lang.Cs.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property CurrentType Type 
    
    property Context LangParseContext 
    	init #
    smartClassEnd
    */
    
    public partial class CompileState
    {
		#region Static Methods 

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

        static void GetTypesX(Assembly a, Dictionary<string, Type> addTo)
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
                aType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var mm1 = mm.Where(i => i.IsGenericMethod).ToArray();
            if (!mm1.Any()) return;

            var enumerableTypes = from i in mm1
                let types = i.GetGenericArguments()
                from type in types
                select type;
            foreach (var type in enumerableTypes)
                GetTypesX(type, addTo);
        }

        #endregion Static Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-03 15:20
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Cs.Compiler
{
    public partial class CompileState 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public CompileState()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##CurrentType## ##Context##
        implement ToString CurrentType=##CurrentType##, Context=##Context##
        implement equals CurrentType, Context
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności CurrentType; 
        /// </summary>
        public const string PropertyNameCurrentType = "CurrentType";
        /// <summary>
        /// Nazwa własności Context; 
        /// </summary>
        public const string PropertyNameContext = "Context";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Type CurrentType
        {
            get
            {
                return _currentType;
            }
            set
            {
                _currentType = value;
            }
        }
        private Type _currentType;
        /// <summary>
        /// 
        /// </summary>
        public LangParseContext Context
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }
        private LangParseContext _context = new LangParseContext();
        #endregion Properties

    }
}
