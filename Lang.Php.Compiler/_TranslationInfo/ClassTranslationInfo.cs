using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Type Type 
    	read only
    
    property ParsedCode CompilationUnit 
    
    property IgnoreNamespace bool 
    
    property ScriptName PhpQualifiedName 
    
    property IsPage bool czy klasa ma wygenerować moduł z odpalaną metodą PHPMain
    
    property Skip bool czy pominąć generowanie klasy
    
    property PageMethod MethodInfo metoda generowana jako kod strony
    
    property ModuleName PhpCodeModuleName 
    
    property IncludeModule PhpCodeModuleName 
    	read only GetIncluideModule()
    
    property IsReflected bool Infomacja pochodzi jedynie z refleksji a nie z kodu tłumaczonego (prawdopodobnie dotyczy typu z referencyjnej biblioteki)
    
    property IsArray bool Czy klasa posiada atrybut ARRAY
    smartClassEnd
    */
    
    public partial class ClassTranslationInfo
    {
        #region Constructors

        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="type"></param>
        /// </summary>
        public ClassTranslationInfo(Type type, TranslationInfo info)
        {
            var ati = info.GetOrMakeTranslationInfo(type.Assembly);
            this.type = type;

            ClassTranslationInfo declaringTypeTI = type.DeclaringType == null
                ? null
                : info.GetOrMakeTranslationInfo(type.DeclaringType);
            var ats = type.GetCustomAttributes(false);
            IgnoreNamespace = ats.OfType<IgnoreNamespaceAttribute>().Any();

            #region ScriptName
            {
                if (IgnoreNamespace)
                    ScriptName = PhpQualifiedName.SanitizePhpName(type.Name); // only short name without namespace
                else if (type.IsGenericType)
                    ScriptName = DotNetNameToPhpName(type.FullName == null ? type.Name : type.FullName); // beware of generic types
                else
                    ScriptName = DotNetNameToPhpName(type.FullName == null ? type.Name : type.FullName);  

                var _scriptName = ats.OfType<ScriptNameAttribute>().FirstOrDefault();
                if (_scriptName != null)
                {
                    if (_scriptName.Name.StartsWith(PhpQualifiedName.T_NS_SEPARATOR.ToString()))
                        ScriptName = _scriptName.Name;
                    else if (IgnoreNamespace)
                        ScriptName = PhpQualifiedName.T_NS_SEPARATOR + _scriptName.Name;
                    else
                        ScriptName = DotNetNameToPhpName(type.FullName) + PhpQualifiedName.T_NS_SEPARATOR + _scriptName.Name;
                }
                if (declaringTypeTI != null)
                    ScriptName = declaringTypeTI.ScriptName + "__" + type.Name; // parent clas followed by __ and short name
               
            
            }
            #endregion
            #region Module name
            {
                moduleName = new PhpCodeModuleName(type, ati, declaringTypeTI);
            }
            #endregion
            #region PageAttribute
            {
                var _page = ats.OfType<PageAttribute>().FirstOrDefault();
                IsPage = _page != null;
                pageMethod = isPage ? FindPHPMainMethod(type) : null;
            }
            #endregion
            #region AsArrayAttribute
            {
                var _page = ats.OfType<AsArrayAttribute>().FirstOrDefault();
                IsArray = _page != null;
            }
            #endregion
            #region SkipAttribute
            {
                var _skip = ats.OfType<SkipAttribute>().FirstOrDefault();
                if (_skip != null)
                    Skip = true;
            }
            #endregion
 
            if (type.IsGenericParameter)
                Skip = true;
        }

        #endregion Constructors

        #region Static Methods

        // Private Methods 

        static string DotNetNameToPhpName(string fullName)
        {
            if (fullName == null)
                throw new ArgumentNullException("fullName");
            fullName = fullName.Replace("`", "__");

            return string.Join("",
                    from i in fullName.Replace("+", ".").Split('.')
                    select PhpQualifiedName.T_NS_SEPARATOR + PhpQualifiedName.SanitizePhpName(i));
        }

        static MethodInfo FindPHPMainMethod(Type type)
        {
            var a = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            var aa = a.Where(i => i.Name == "PhpMain").FirstOrDefault();
            if (aa != null) return aa;
            aa = a.Where(i => i.Name == "PHPMain").FirstOrDefault();
            if (aa != null) return aa;
            var bb = a.Where(i => i.Name.ToLower() == "phpmain").ToArray();
            if (bb.Length == 1)
                return bb[0];
            throw new Exception(string.Format("tearful leopard: Type {0} has no PhpMain method", type.FullName));
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public override string ToString()
        {
            return string.Format("{0} => {1}@{2}", type.FullName, this.scriptName, this.moduleName);
        }
        // Private Methods 

        private PhpCodeModuleName GetIncluideModule()
        {
            if (isArray || skip)
                return null;
            return moduleName;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-01-05 09:59
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class ClassTranslationInfo 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public ClassTranslationInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Type## ##ParsedCode## ##IgnoreNamespace## ##ScriptName## ##IsPage## ##Skip## ##PageMethod## ##ModuleName## ##IncludeModule## ##IsReflected## ##IsArray##
        implement ToString Type=##Type##, ParsedCode=##ParsedCode##, IgnoreNamespace=##IgnoreNamespace##, ScriptName=##ScriptName##, IsPage=##IsPage##, Skip=##Skip##, PageMethod=##PageMethod##, ModuleName=##ModuleName##, IncludeModule=##IncludeModule##, IsReflected=##IsReflected##, IsArray=##IsArray##
        implement equals Type, ParsedCode, IgnoreNamespace, ScriptName, IsPage, Skip, PageMethod, ModuleName, IncludeModule, IsReflected, IsArray
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Type; 
        /// </summary>
        public const string PROPERTYNAME_TYPE = "Type";
        /// <summary>
        /// Nazwa własności ParsedCode; 
        /// </summary>
        public const string PROPERTYNAME_PARSEDCODE = "ParsedCode";
        /// <summary>
        /// Nazwa własności IgnoreNamespace; 
        /// </summary>
        public const string PROPERTYNAME_IGNORENAMESPACE = "IgnoreNamespace";
        /// <summary>
        /// Nazwa własności ScriptName; 
        /// </summary>
        public const string PROPERTYNAME_SCRIPTNAME = "ScriptName";
        /// <summary>
        /// Nazwa własności IsPage; czy klasa ma wygenerować moduł z odpalaną metodą PHPMain
        /// </summary>
        public const string PROPERTYNAME_ISPAGE = "IsPage";
        /// <summary>
        /// Nazwa własności Skip; czy pominąć generowanie klasy
        /// </summary>
        public const string PROPERTYNAME_SKIP = "Skip";
        /// <summary>
        /// Nazwa własności PageMethod; metoda generowana jako kod strony
        /// </summary>
        public const string PROPERTYNAME_PAGEMETHOD = "PageMethod";
        /// <summary>
        /// Nazwa własności ModuleName; 
        /// </summary>
        public const string PROPERTYNAME_MODULENAME = "ModuleName";
        /// <summary>
        /// Nazwa własności IncludeModule; 
        /// </summary>
        public const string PROPERTYNAME_INCLUDEMODULE = "IncludeModule";
        /// <summary>
        /// Nazwa własności IsReflected; Infomacja pochodzi jedynie z refleksji a nie z kodu tłumaczonego (prawdopodobnie dotyczy typu z referencyjnej biblioteki)
        /// </summary>
        public const string PROPERTYNAME_ISREFLECTED = "IsReflected";
        /// <summary>
        /// Nazwa własności IsArray; Czy klasa posiada atrybut ARRAY
        /// </summary>
        public const string PROPERTYNAME_ISARRAY = "IsArray";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public Type Type
        {
            get
            {
                return type;
            }
        }
        private Type type;
        /// <summary>
        /// 
        /// </summary>
        public CompilationUnit ParsedCode
        {
            get
            {
                return parsedCode;
            }
            set
            {
                parsedCode = value;
            }
        }
        private CompilationUnit parsedCode;
        /// <summary>
        /// 
        /// </summary>
        public bool IgnoreNamespace
        {
            get
            {
                return ignoreNamespace;
            }
            set
            {
                ignoreNamespace = value;
            }
        }
        private bool ignoreNamespace;
        /// <summary>
        /// 
        /// </summary>
        public PhpQualifiedName ScriptName
        {
            get
            {
                return scriptName;
            }
            set
            {
                scriptName = value;
            }
        }
        private PhpQualifiedName scriptName;
        /// <summary>
        /// czy klasa ma wygenerować moduł z odpalaną metodą PHPMain
        /// </summary>
        public bool IsPage
        {
            get
            {
                return isPage;
            }
            set
            {
                isPage = value;
            }
        }
        private bool isPage;
        /// <summary>
        /// czy pominąć generowanie klasy
        /// </summary>
        public bool Skip
        {
            get
            {
                return skip;
            }
            set
            {
                skip = value;
            }
        }
        private bool skip;
        /// <summary>
        /// metoda generowana jako kod strony
        /// </summary>
        public MethodInfo PageMethod
        {
            get
            {
                return pageMethod;
            }
            set
            {
                pageMethod = value;
            }
        }
        private MethodInfo pageMethod;
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeModuleName ModuleName
        {
            get
            {
                return moduleName;
            }
            set
            {
                moduleName = value;
            }
        }
        private PhpCodeModuleName moduleName;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public PhpCodeModuleName IncludeModule
        {
            get
            {
                return GetIncluideModule();
            }
        }
        /// <summary>
        /// Infomacja pochodzi jedynie z refleksji a nie z kodu tłumaczonego (prawdopodobnie dotyczy typu z referencyjnej biblioteki)
        /// </summary>
        public bool IsReflected
        {
            get
            {
                return isReflected;
            }
            set
            {
                isReflected = value;
            }
        }
        private bool isReflected;
        /// <summary>
        /// Czy klasa posiada atrybut ARRAY
        /// </summary>
        public bool IsArray
        {
            get
            {
                return isArray;
            }
            set
            {
                isArray = value;
            }
        }
        private bool isArray;
        #endregion Properties

    }
}
