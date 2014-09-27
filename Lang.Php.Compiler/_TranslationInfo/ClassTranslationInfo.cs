using System.Globalization;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Linq;
using System.Reflection;

namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Type Type 
    	read only
    
    property ParsedCode CompilationUnit 
    
    property IgnoreNamespace bool 
    	read only GetIgnoreNamespace()
    
    property ScriptName PhpQualifiedName 
    	read only GetScriptName()
    
    property IsPage bool czy klasa ma wygenerować moduł z odpalaną metodą PHPMain
    	read only GetIsPage()
    
    property Skip bool czy pominąć generowanie klasy
    	read only GetSkip()
    
    property BuildIn bool class from host application i.e. wordpress
    	read only GetBuildIn()
    
    property DontIncludeModuleForClassMembers bool czy pominąć includowanie modułu z klasą
    	read only GetDontIncludeModuleForClassMembers()
    
    property PageMethod MethodInfo metoda generowana jako kod strony
    	read only GetPageMethod()
    
    property ModuleName PhpCodeModuleName 
    	read only GetModuleName()
    
    property IncludeModule PhpCodeModuleName 
    	read only GetIncluideModule()
    
    property IsReflected bool Infomacja pochodzi jedynie z refleksji a nie z kodu tłumaczonego (prawdopodobnie dotyczy typu z referencyjnej biblioteki)
    	read only GetIsReflected()
    
    property IsArray bool Czy klasa posiada atrybut ARRAY
    	read only GetIsArray()
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
            _type = type;
            _info = info;
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
                    select PhpQualifiedName.TokenNsSeparator + PhpQualifiedName.SanitizePhpName(i));
        }

        static MethodInfo FindPhpMainMethod(Type type)
        {
            var a = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            var aa = a.FirstOrDefault(i => i.Name == "PhpMain");
            if (aa != null) return aa;
            aa = a.FirstOrDefault(i => i.Name == "PHPMain");
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
            return string.Format("{0} => {1}@{2}", _type.FullName, _scriptName, _moduleName);
        }
        // Private Methods 

        private bool GetDontIncludeModuleForClassMembers()
        {
            Update();
            return _skip || _isArray || _type.IsEnum;
        }

        bool GetBuildIn()
        {
            Update();
            return _buildIn;
        }

        bool GetIgnoreNamespace()
        {
            Update();
            return _ignoreNamespace;
        }

        private PhpCodeModuleName GetIncluideModule()
        {
            Update();
            return _isArray || _skip ? null : _moduleName;
        }

        bool GetIsArray()
        {
            Update();
            return _isArray;
        }

        bool GetIsPage()
        {
            Update();
            return _isPage;
        }

        private bool GetIsReflected()
        {
            Update();
            return _isReflected;
        }

        private PhpCodeModuleName GetModuleName()
        {
            Update();
            return _moduleName;
        }

        private MethodInfo GetPageMethod()
        {
            Update();
            return _pageMethod;
        }

        private PhpQualifiedName GetScriptName()
        {
            Update();
            return _scriptName;
        }

        bool GetSkip()
        {
            Update();
            return _skip;
        }

        void Update()
        {
            if (_initialized) return;
            _initialized = true;


            var ati = _info.GetOrMakeTranslationInfo(_type.Assembly);


            var declaringTypeTranslationInfo = (object)_type.DeclaringType == null
                ? null
                : _info.GetOrMakeTranslationInfo(_type.DeclaringType);
            var ats = _type.GetCustomAttributes(false);
            _ignoreNamespace = ats.OfType<IgnoreNamespaceAttribute>().Any();

            #region ScriptName
            {
                if (_ignoreNamespace)
                    _scriptName = (PhpQualifiedName)PhpQualifiedName.SanitizePhpName(_type.Name); // only short name without namespace
                else if (_type.IsGenericType)
                    _scriptName = (PhpQualifiedName)DotNetNameToPhpName(_type.FullName ?? _type.Name); // beware of generic types
                else
                    _scriptName = (PhpQualifiedName)DotNetNameToPhpName(_type.FullName ?? _type.Name);

                var scriptNameAttribute = ats.OfType<ScriptNameAttribute>().FirstOrDefault();
                if (scriptNameAttribute != null)
                {
                    if (scriptNameAttribute.Name.StartsWith(PhpQualifiedName.TokenNsSeparator.ToString(CultureInfo.InvariantCulture)))
                        _scriptName = (PhpQualifiedName)scriptNameAttribute.Name;
                    else if (IgnoreNamespace)
                        _scriptName = (PhpQualifiedName)(PhpQualifiedName.TokenNsSeparator + scriptNameAttribute.Name);
                    else
                        _scriptName =
                            (PhpQualifiedName)
                                (DotNetNameToPhpName(_type.FullName) + PhpQualifiedName.TokenNsSeparator +
                                 scriptNameAttribute.Name);
                }
                if (declaringTypeTranslationInfo != null)
                    _scriptName = (PhpQualifiedName)(declaringTypeTranslationInfo.ScriptName + "__" + _type.Name); // parent clas followed by __ and short name
            }
            #endregion
            #region Module name
            {
                //if (declaringTypeTranslationInfo != null && declaringTypeTranslationInfo.ModuleName != null)
                _moduleName = new PhpCodeModuleName(_type, ati, declaringTypeTranslationInfo);
            }
            #endregion
            #region PageAttribute
            {
                var pageAttribute = ats.OfType<PageAttribute>().FirstOrDefault();
                _isPage = pageAttribute != null;
                _pageMethod = _isPage ? FindPhpMainMethod(_type) : null;
            }
            #endregion
            #region AsArrayAttribute
            {
                var asArrayAttribute = ats.OfType<AsArrayAttribute>().FirstOrDefault();
                _isArray = asArrayAttribute != null;
            }
            #endregion
            #region SkipAttribute
            {
                var skipAttribute = ats.OfType<SkipAttribute>().FirstOrDefault();
                if (skipAttribute != null)
                    _skip = true;
            }
            #endregion
            #region BuiltInAttribute
            {
                var builtInAttribute = ats.OfType<BuiltInAttribute>().FirstOrDefault();
                if (builtInAttribute != null)
                    _buildIn = true;
            }
            #endregion

            if (_skip && _buildIn)
                throw new Exception("Don't mix SkipAttribute and BuiltInAttribute for type " + _type.ExcName());
            if (_buildIn)
                _skip = true;
            if (_type.IsGenericParameter)
                _skip = true;
            if (_isArray)
                _skip = true;
        }

            #endregion Methods

        #region Fields

        private bool _buildIn;
        private bool _ignoreNamespace;
        private readonly TranslationInfo _info;
        bool _initialized;
        private bool _isArray;
        private bool _isPage;
        private bool _isReflected;
        private PhpCodeModuleName _moduleName;
        private MethodInfo _pageMethod;
        private PhpQualifiedName _scriptName;
        private bool _skip;

        #endregion Fields
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-27 11:28
// File generated automatically ver 2014-09-01 19:00
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
        implement ToString ##Type## ##ParsedCode## ##IgnoreNamespace## ##ScriptName## ##IsPage## ##Skip## ##BuildIn## ##DontIncludeModuleForClassMembers## ##PageMethod## ##ModuleName## ##IncludeModule## ##IsReflected## ##IsArray##
        implement ToString Type=##Type##, ParsedCode=##ParsedCode##, IgnoreNamespace=##IgnoreNamespace##, ScriptName=##ScriptName##, IsPage=##IsPage##, Skip=##Skip##, BuildIn=##BuildIn##, DontIncludeModuleForClassMembers=##DontIncludeModuleForClassMembers##, PageMethod=##PageMethod##, ModuleName=##ModuleName##, IncludeModule=##IncludeModule##, IsReflected=##IsReflected##, IsArray=##IsArray##
        implement equals Type, ParsedCode, IgnoreNamespace, ScriptName, IsPage, Skip, BuildIn, DontIncludeModuleForClassMembers, PageMethod, ModuleName, IncludeModule, IsReflected, IsArray
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności Type; 
        /// </summary>
        public const string PropertyNameType = "Type";
        /// <summary>
        /// Nazwa własności ParsedCode; 
        /// </summary>
        public const string PropertyNameParsedCode = "ParsedCode";
        /// <summary>
        /// Nazwa własności IgnoreNamespace; 
        /// </summary>
        public const string PropertyNameIgnoreNamespace = "IgnoreNamespace";
        /// <summary>
        /// Nazwa własności ScriptName; 
        /// </summary>
        public const string PropertyNameScriptName = "ScriptName";
        /// <summary>
        /// Nazwa własności IsPage; czy klasa ma wygenerować moduł z odpalaną metodą PHPMain
        /// </summary>
        public const string PropertyNameIsPage = "IsPage";
        /// <summary>
        /// Nazwa własności Skip; czy pominąć generowanie klasy
        /// </summary>
        public const string PropertyNameSkip = "Skip";
        /// <summary>
        /// Nazwa własności BuildIn; class from host application i.e. wordpress
        /// </summary>
        public const string PropertyNameBuildIn = "BuildIn";
        /// <summary>
        /// Nazwa własności DontIncludeModuleForClassMembers; czy pominąć includowanie modułu z klasą
        /// </summary>
        public const string PropertyNameDontIncludeModuleForClassMembers = "DontIncludeModuleForClassMembers";
        /// <summary>
        /// Nazwa własności PageMethod; metoda generowana jako kod strony
        /// </summary>
        public const string PropertyNamePageMethod = "PageMethod";
        /// <summary>
        /// Nazwa własności ModuleName; 
        /// </summary>
        public const string PropertyNameModuleName = "ModuleName";
        /// <summary>
        /// Nazwa własności IncludeModule; 
        /// </summary>
        public const string PropertyNameIncludeModule = "IncludeModule";
        /// <summary>
        /// Nazwa własności IsReflected; Infomacja pochodzi jedynie z refleksji a nie z kodu tłumaczonego (prawdopodobnie dotyczy typu z referencyjnej biblioteki)
        /// </summary>
        public const string PropertyNameIsReflected = "IsReflected";
        /// <summary>
        /// Nazwa własności IsArray; Czy klasa posiada atrybut ARRAY
        /// </summary>
        public const string PropertyNameIsArray = "IsArray";
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
                return _type;
            }
        }
        private Type _type;
        /// <summary>
        /// 
        /// </summary>
        public CompilationUnit ParsedCode
        {
            get
            {
                return _parsedCode;
            }
            set
            {
                _parsedCode = value;
            }
        }
        private CompilationUnit _parsedCode;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool IgnoreNamespace
        {
            get
            {
                return GetIgnoreNamespace();
            }
        }
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public PhpQualifiedName ScriptName
        {
            get
            {
                return GetScriptName();
            }
        }
        /// <summary>
        /// czy klasa ma wygenerować moduł z odpalaną metodą PHPMain; własność jest tylko do odczytu.
        /// </summary>
        public bool IsPage
        {
            get
            {
                return GetIsPage();
            }
        }
        /// <summary>
        /// czy pominąć generowanie klasy; własność jest tylko do odczytu.
        /// </summary>
        public bool Skip
        {
            get
            {
                return GetSkip();
            }
        }
        /// <summary>
        /// class from host application i.e. wordpress; własność jest tylko do odczytu.
        /// </summary>
        public bool BuildIn
        {
            get
            {
                return GetBuildIn();
            }
        }
        /// <summary>
        /// czy pominąć includowanie modułu z klasą; własność jest tylko do odczytu.
        /// </summary>
        public bool DontIncludeModuleForClassMembers
        {
            get
            {
                return GetDontIncludeModuleForClassMembers();
            }
        }
        /// <summary>
        /// metoda generowana jako kod strony; własność jest tylko do odczytu.
        /// </summary>
        public MethodInfo PageMethod
        {
            get
            {
                return GetPageMethod();
            }
        }
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public PhpCodeModuleName ModuleName
        {
            get
            {
                return GetModuleName();
            }
        }
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
        /// Infomacja pochodzi jedynie z refleksji a nie z kodu tłumaczonego (prawdopodobnie dotyczy typu z referencyjnej biblioteki); własność jest tylko do odczytu.
        /// </summary>
        public bool IsReflected
        {
            get
            {
                return GetIsReflected();
            }
        }
        /// <summary>
        /// Czy klasa posiada atrybut ARRAY; własność jest tylko do odczytu.
        /// </summary>
        public bool IsArray
        {
            get
            {
                return GetIsArray();
            }
        }
        #endregion Properties
    }
}
