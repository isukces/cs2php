using Lang.Cs.Compiler.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            List<Type> result = new List<Type>();
            foreach (Assembly assembly in assemblies)
                GetTypesX(assembly, result);
            return result.Distinct().ToArray();
        }
		// Private Methods 

        static void GetTypesX(Assembly a, List<Type> addTo)
        {
            foreach (var type in a.GetTypes())
                GetTypesX(type, addTo);
        }

        static void GetTypesX(Type a, List<Type> addTo)
        {
            if (addTo.Contains(a))
                return;
            addTo.Add(a);
            var tmp = a.GetNestedTypes();
            if (tmp.Length != 0)
            {
                addTo.AddRange(tmp);
                foreach (var i in tmp)
                    GetTypesX(i, addTo);
            }
            var mm = a.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var mm1 = mm.Where(i => i.IsGenericMethod).ToArray();
            if (mm1.Any())
            {
                foreach (var i in mm1)
                {
                    var h = i.GetGenericArguments();
                    foreach (var j in h)
                        GetTypesX(j, addTo);
                }
            }

        }

		#endregion Static Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-04 09:14
// File generated automatically ver 2013-07-10 08:43
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
        public const string PROPERTYNAME_CURRENTTYPE = "CurrentType";
        /// <summary>
        /// Nazwa własności Context; 
        /// </summary>
        public const string PROPERTYNAME_CONTEXT = "Context";
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
                return currentType;
            }
            set
            {
                currentType = value;
            }
        }
        private Type currentType;
        /// <summary>
        /// 
        /// </summary>
        public LangParseContext Context
        {
            get
            {
                return context;
            }
            set
            {
                context = value;
            }
        }
        private LangParseContext context = new LangParseContext();
        #endregion Properties
    }
}
