using Lang.Cs.Compiler;
using Lang.Cs.Compiler.Sandbox;
using Lang.Php.Compiler.Source;
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
    
    property ScriptName string 
    	access public protected protected
    
    property IsScriptNamePhpEncoded bool 
    	access public protected protected
    
    property Destination FieldTranslationDestionations 
    	access public protected protected
    
    property UsGlueForValue bool czy sklejać wartość stałą w wyrażeniach
    	access public protected protected
    
    property IncludeModule PhpCodeModuleName 
    	access public protected protected
    
    property IsDefinedInNonincludableModule bool czy jest zdefiniowany w module, którego nie można includować
    smartClassEnd
    */

    public partial class FieldTranslationInfo
    {


        public static FieldTranslationInfo FromFieldInfo(FieldInfo fieldInfo, TranslationInfo info)
        {
            if (fieldInfo == null)
                return null;
            FieldTranslationInfo fti = new FieldTranslationInfo();
            if (fieldInfo.IsLiteral)
                fti._destination = FieldTranslationDestionations.ClassConst;
            {
                fti.ScriptName = fieldInfo.Name;
                {
                    var _sctiptName = fieldInfo.GetCustomAttribute<ScriptNameAttribute>();
                    if (_sctiptName != null)
                    {
                        fti._scriptName = _sctiptName.Name;
                        fti._isScriptNamePhpEncoded = _sctiptName.Kind == ScriptNameAttribute.Kinds.IntIndex;
                    }
                }
            }
            {
                var asDefinedConstAttribute = fieldInfo.GetCustomAttribute<AsDefinedConstAttribute>();
                if (asDefinedConstAttribute != null)
                {
                    fti.Destination = FieldTranslationDestionations.DefinedConst;
                    if (!string.IsNullOrEmpty(asDefinedConstAttribute.DefinedConstName))
                        fti._scriptName = asDefinedConstAttribute.DefinedConstName;
                }
            }
            {
                var globalVariableAttribute = fieldInfo.GetCustomAttribute<GlobalVariableAttribute>();
                if (globalVariableAttribute != null)
                {
                    Check(fieldInfo, fti);
                    fti.Destination = FieldTranslationDestionations.GlobalVariable;
                    if (!string.IsNullOrEmpty(globalVariableAttribute.GlobalVariableName))
                        fti._scriptName = globalVariableAttribute.GlobalVariableName;
                }
            }
            {
                var asValueAttribute = fieldInfo.GetCustomAttribute<AsValueAttribute>();
                if (asValueAttribute != null)
                {
                    Check(fieldInfo, fti);
                    fti.Destination = FieldTranslationDestionations.JustValue;
                    fti.UsGlueForValue = asValueAttribute.Glue;
                }
            }
            switch (fti._destination)
            {
                case FieldTranslationDestionations.JustValue:
                case FieldTranslationDestionations.GlobalVariable:
                    break;
                case FieldTranslationDestionations.DefinedConst:
                case FieldTranslationDestionations.ClassConst:
                case FieldTranslationDestionations.NormalField:
                    var cti = info.GetOrMakeTranslationInfo(fieldInfo.DeclaringType);
                    fti.IncludeModule = cti.ModuleName;
                    if (fti._includeModule != null)
                    {
                        if (cti.IsPage)
                            fti.IsDefinedInNonincludableModule = true;
                        if (cti.Skip)
                            fti.IncludeModule = null;
                    }

                    break;

            }
            return fti;
        }

        private static void Check(FieldInfo fieldInfo, FieldTranslationInfo fti)
        {
            if (fti == null) throw new ArgumentNullException("fti");
            if (fti.Destination != FieldTranslationDestionations.NormalField && fti.Destination != FieldTranslationDestionations.ClassConst)
                throw new Exception(string.Format("Unable to find right way to convert field {0}.{1}", 
                    fieldInfo.DeclaringType.FullName, 
                    fieldInfo.Name));
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-03 17:42
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler
{
    public partial class FieldTranslationInfo
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public FieldTranslationInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##ScriptName## ##IsScriptNamePhpEncoded## ##Destination## ##UsGlueForValue## ##IncludeModule## ##IsDefinedInNonincludableModule##
        implement ToString ScriptName=##ScriptName##, IsScriptNamePhpEncoded=##IsScriptNamePhpEncoded##, Destination=##Destination##, UsGlueForValue=##UsGlueForValue##, IncludeModule=##IncludeModule##, IsDefinedInNonincludableModule=##IsDefinedInNonincludableModule##
        implement equals ScriptName, IsScriptNamePhpEncoded, Destination, UsGlueForValue, IncludeModule, IsDefinedInNonincludableModule
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności ScriptName; 
        /// </summary>
        public const string PropertyNameScriptName = "ScriptName";
        /// <summary>
        /// Nazwa własności IsScriptNamePhpEncoded; 
        /// </summary>
        public const string PropertyNameIsScriptNamePhpEncoded = "IsScriptNamePhpEncoded";
        /// <summary>
        /// Nazwa własności Destination; 
        /// </summary>
        public const string PropertyNameDestination = "Destination";
        /// <summary>
        /// Nazwa własności UsGlueForValue; czy sklejać wartość stałą w wyrażeniach
        /// </summary>
        public const string PropertyNameUsGlueForValue = "UsGlueForValue";
        /// <summary>
        /// Nazwa własności IncludeModule; 
        /// </summary>
        public const string PropertyNameIncludeModule = "IncludeModule";
        /// <summary>
        /// Nazwa własności IsDefinedInNonincludableModule; czy jest zdefiniowany w module, którego nie można includować
        /// </summary>
        public const string PropertyNameIsDefinedInNonincludableModule = "IsDefinedInNonincludableModule";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string ScriptName
        {
            get
            {
                return _scriptName;
            }
            protected set
            {
                value = (value ?? String.Empty).Trim();
                _scriptName = value;
            }
        }
        protected string _scriptName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool IsScriptNamePhpEncoded
        {
            get
            {
                return _isScriptNamePhpEncoded;
            }
            protected set
            {
                _isScriptNamePhpEncoded = value;
            }
        }
        protected bool _isScriptNamePhpEncoded;
        /// <summary>
        /// 
        /// </summary>
        public FieldTranslationDestionations Destination
        {
            get
            {
                return _destination;
            }
            protected set
            {
                _destination = value;
            }
        }
        protected FieldTranslationDestionations _destination;
        /// <summary>
        /// czy sklejać wartość stałą w wyrażeniach
        /// </summary>
        public bool UsGlueForValue
        {
            get
            {
                return _usGlueForValue;
            }
            protected set
            {
                _usGlueForValue = value;
            }
        }
        protected bool _usGlueForValue;
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeModuleName IncludeModule
        {
            get
            {
                return _includeModule;
            }
            protected set
            {
                _includeModule = value;
            }
        }
        protected PhpCodeModuleName _includeModule;
        /// <summary>
        /// czy jest zdefiniowany w module, którego nie można includować
        /// </summary>
        public bool IsDefinedInNonincludableModule
        {
            get
            {
                return _isDefinedInNonincludableModule;
            }
            set
            {
                _isDefinedInNonincludableModule = value;
            }
        }
        private bool _isDefinedInNonincludableModule;
        #endregion Properties

    }
}
