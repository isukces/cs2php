using Lang.Cs.Compiler;
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
            {
                fti.ScriptName = fieldInfo.Name;
                {
                    var _sctiptName = fieldInfo.GetCustomAttribute<Lang.Php.ScriptNameAttribute>();
                    if (_sctiptName != null)
                    {
                        fti.scriptName = _sctiptName.Name;
                        fti.isScriptNamePhpEncoded = _sctiptName.Kind == ScriptNameAttribute.Kinds.IntIndex;
                    }
                }
            }
            {
                var _definedConst = fieldInfo.GetCustomAttribute<Lang.Php.AsDefinedConst>();
                if (_definedConst != null)
                {
                    fti.Destination = FieldTranslationDestionations.DefinedConst;
                    if (!string.IsNullOrEmpty(_definedConst.DefinedConstName))
                        fti.scriptName = _definedConst.DefinedConstName;
                }
            }
            {
                var _globalVariable = fieldInfo.GetCustomAttribute<Lang.Php.GlobalVariableAttribute>();
                if (_globalVariable != null)
                {
                    Check(fieldInfo, fti);
                    fti.Destination = FieldTranslationDestionations.GlobalVariable;
                    if (!string.IsNullOrEmpty(_globalVariable.GlobalVariableName))
                        fti.scriptName = _globalVariable.GlobalVariableName;
                }
            }
            {
                var _asValue = fieldInfo.GetCustomAttribute<Lang.Php.AsValue>();
                if (_asValue != null)
                {
                    Check(fieldInfo, fti);
                    fti.Destination = FieldTranslationDestionations.JustValue;
                    fti.UsGlueForValue = _asValue.Glue;
                }
            }
            switch (fti.destination)
            {
                case FieldTranslationDestionations.JustValue:
                case FieldTranslationDestionations.GlobalVariable:
                    break;
                case FieldTranslationDestionations.DefinedConst:
                case FieldTranslationDestionations.NormalField:
                    var cti = info.GetOrMakeTranslationInfo(fieldInfo.DeclaringType);
                    fti.IncludeModule = cti.ModuleName;
                    if (fti.includeModule != null)
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
            if (fti.Destination != FieldTranslationDestionations.NormalField)
                throw new Exception(string.Format("Unable to find right way to convert field {0}.{1}", fieldInfo.DeclaringType.FullName, fieldInfo.Name));
        }

    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-13 18:02
// File generated automatically ver 2013-07-10 08:43
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
        public const string PROPERTYNAME_SCRIPTNAME = "ScriptName";
        /// <summary>
        /// Nazwa własności IsScriptNamePhpEncoded; 
        /// </summary>
        public const string PROPERTYNAME_ISSCRIPTNAMEPHPENCODED = "IsScriptNamePhpEncoded";
        /// <summary>
        /// Nazwa własności Destination; 
        /// </summary>
        public const string PROPERTYNAME_DESTINATION = "Destination";
        /// <summary>
        /// Nazwa własności UsGlueForValue; czy sklejać wartość stałą w wyrażeniach
        /// </summary>
        public const string PROPERTYNAME_USGLUEFORVALUE = "UsGlueForValue";
        /// <summary>
        /// Nazwa własności IncludeModule; 
        /// </summary>
        public const string PROPERTYNAME_INCLUDEMODULE = "IncludeModule";
        /// <summary>
        /// Nazwa własności IsDefinedInNonincludableModule; czy jest zdefiniowany w module, którego nie można includować
        /// </summary>
        public const string PROPERTYNAME_ISDEFINEDINNONINCLUDABLEMODULE = "IsDefinedInNonincludableModule";
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
                return scriptName;
            }
            protected set
            {
                value = (value ?? String.Empty).Trim();
                scriptName = value;
            }
        }
        protected string scriptName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool IsScriptNamePhpEncoded
        {
            get
            {
                return isScriptNamePhpEncoded;
            }
            protected set
            {
                isScriptNamePhpEncoded = value;
            }
        }
        protected bool isScriptNamePhpEncoded;
        /// <summary>
        /// 
        /// </summary>
        public FieldTranslationDestionations Destination
        {
            get
            {
                return destination;
            }
            protected set
            {
                destination = value;
            }
        }
        protected FieldTranslationDestionations destination;
        /// <summary>
        /// czy sklejać wartość stałą w wyrażeniach
        /// </summary>
        public bool UsGlueForValue
        {
            get
            {
                return usGlueForValue;
            }
            protected set
            {
                usGlueForValue = value;
            }
        }
        protected bool usGlueForValue;
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeModuleName IncludeModule
        {
            get
            {
                return includeModule;
            }
            protected set
            {
                includeModule = value;
            }
        }
        protected PhpCodeModuleName includeModule;
        /// <summary>
        /// czy jest zdefiniowany w module, którego nie można includować
        /// </summary>
        public bool IsDefinedInNonincludableModule
        {
            get
            {
                return isDefinedInNonincludableModule;
            }
            set
            {
                isDefinedInNonincludableModule = value;
            }
        }
        private bool isDefinedInNonincludableModule;
        #endregion Properties

    }
}
