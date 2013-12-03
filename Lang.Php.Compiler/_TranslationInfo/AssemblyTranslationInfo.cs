using Lang.Php.Compiler.Source;
using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property LibraryName string 
    
    property IncludePathConstOrVarName string nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
    smartClassEnd
    */
    
    public partial class AssemblyTranslationInfo
    {
        public static AssemblyTranslationInfo FromAssembly(Assembly assembly)
        {
            if (assembly == null)
                return null;
            AssemblyTranslationInfo a = new AssemblyTranslationInfo();
            {
                var _ModuleIncludeConst = assembly.GetCustomAttribute<ModuleIncludeConstAttribute>();
                if (_ModuleIncludeConst != null)
                    a.IncludePathConstOrVarName = _ModuleIncludeConst.ConstOrVarName;
            }
            a.LibraryName = PhpCodeModuleName.LibNameFromAssembly(assembly);
            return a;
        }


    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-14 10:17
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class AssemblyTranslationInfo 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public AssemblyTranslationInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##LibraryName## ##IncludePathConstOrVarName##
        implement ToString LibraryName=##LibraryName##, IncludePathConstOrVarName=##IncludePathConstOrVarName##
        implement equals LibraryName, IncludePathConstOrVarName
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności LibraryName; 
        /// </summary>
        public const string PROPERTYNAME_LIBRARYNAME = "LibraryName";
        /// <summary>
        /// Nazwa własności IncludePathConstOrVarName; nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
        /// </summary>
        public const string PROPERTYNAME_INCLUDEPATHCONSTORVARNAME = "IncludePathConstOrVarName";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string LibraryName
        {
            get
            {
                return libraryName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                libraryName = value;
            }
        }
        private string libraryName = string.Empty;
        /// <summary>
        /// nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki
        /// </summary>
        public string IncludePathConstOrVarName
        {
            get
            {
                return includePathConstOrVarName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                includePathConstOrVarName = value;
            }
        }
        private string includePathConstOrVarName = string.Empty;
        #endregion Properties

    }
}
