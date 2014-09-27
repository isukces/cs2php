using Lang.Php.Compiler.Source;
using System;
using System.Reflection;


namespace Lang.Php.Compiler
{

    /*
    smartClass
    option NoAdditionalFile
    
    property ScriptName string 
    	read only
    
    property IsScriptNamePhpEncoded bool 
    	read only
    
    property Destination FieldTranslationDestionations 
    	read only
    
    property UsGlueForValue bool czy sklejać wartość stałą w wyrażeniach
    	read only
    
    property IncludeModule PhpCodeModuleName 
    	read only
    
    property IsDefinedInNonincludableModule bool czy jest zdefiniowany w module, którego nie można includować
    	read only
    smartClassEnd
    */

    public partial class FieldTranslationInfo
    {


        public static FieldTranslationInfo FromFieldInfo(FieldInfo fieldInfo, TranslationInfo info)
        {
            if (fieldInfo == null)
                return null;

            var fieldInfoDeclaringType = fieldInfo.DeclaringType;
            if (fieldInfoDeclaringType == null)
                throw new Exception("fieldInfo.DeclaringType is null"); // Resharper
            var fti = new FieldTranslationInfo();
            if (fieldInfo.IsLiteral)
                fti._destination = FieldTranslationDestionations.ClassConst;
            {
                fti._scriptName = fieldInfo.Name;
                var scriptNameAttribute = fieldInfo.GetCustomAttribute<ScriptNameAttribute>();
                if (scriptNameAttribute != null)
                {
                    fti._scriptName = scriptNameAttribute.Name;
                    fti._isScriptNamePhpEncoded = scriptNameAttribute.Kind == ScriptNameAttribute.Kinds.IntIndex;
                }
            }
            {
                var asDefinedConstAttribute = fieldInfo.GetCustomAttribute<AsDefinedConstAttribute>();
                if (asDefinedConstAttribute != null)
                {
                    fti._destination = FieldTranslationDestionations.DefinedConst;
                    if (!string.IsNullOrEmpty(asDefinedConstAttribute.DefinedConstName))
                        fti._scriptName = asDefinedConstAttribute.DefinedConstName;
                }
            }
            {
                var globalVariableAttribute = fieldInfo.GetCustomAttribute<GlobalVariableAttribute>();
                if (globalVariableAttribute != null)
                {
                    Check(fieldInfo, fti);
                    fti._destination = FieldTranslationDestionations.GlobalVariable;
                    if (!string.IsNullOrEmpty(globalVariableAttribute.GlobalVariableName))
                        fti._scriptName = globalVariableAttribute.GlobalVariableName;
                }
            }
            {
                var asValueAttribute = fieldInfo.GetCustomAttribute<AsValueAttribute>();
                if (asValueAttribute != null)
                {
                    Check(fieldInfo, fti);
                    fti._destination = FieldTranslationDestionations.JustValue;
                    fti._usGlueForValue = asValueAttribute.Glue;
                }
            }
            var canBeNull = false;
            switch (fti._destination)
            {
                case FieldTranslationDestionations.JustValue:
                case FieldTranslationDestionations.GlobalVariable:
                    canBeNull = true;
                     fti._includeModule = null; // force null
                    break;
                case FieldTranslationDestionations.DefinedConst:
                case FieldTranslationDestionations.ClassConst:
                case FieldTranslationDestionations.NormalField:
                    var cti = info.GetOrMakeTranslationInfo(fieldInfoDeclaringType);
                    fti._includeModule = cti.ModuleName;
                    if (cti.BuildIn)
                    {
                        fti._includeModule = null;
                        canBeNull = true;
                    }
                    var isFieldOutsideClass = fti._destination == FieldTranslationDestionations.GlobalVariable ||
                                  fti._destination == FieldTranslationDestionations.DefinedConst;
                    {
                        // can be in other module for GlobalVariable and DefinedConst
                        var moduleAttribute = fieldInfo.GetCustomAttribute<ModuleAttribute>();
                        if (moduleAttribute != null)
                        {
                            if (!isFieldOutsideClass)
                                throw new Exception(string.Format("Module attribute can only be defined for GlobalVariable or DefinedConst. Check {0}.", fieldInfo.ExcName()));
                            fti._includeModule = new PhpCodeModuleName(moduleAttribute.ModuleShortName,
                                info.GetOrMakeTranslationInfo(fieldInfoDeclaringType.Assembly));
                        }
                    }
                    if (cti.IsPage)
                        fti._isDefinedInNonincludableModule = true;
                    if (!isFieldOutsideClass)
                    {
                        if (cti.IsArray || cti.Type.IsEnum || cti.BuildIn)
                        {
                            canBeNull = true;
                            fti._includeModule = null; // force null
                        }
                        else if (cti.DontIncludeModuleForClassMembers)
                            throw new Exception(
                                string.Format("field {0} belongs to nonincludable class (array, enum or skipped)",
                                    fieldInfo.ExcName()));
                    }
                    break;
            }

            if (!fti._includeModule.IsEmpty())
                return fti;
            if (canBeNull)
            {
                fti._includeModule = null; // can be not null but empty
                fti._isDefinedInNonincludableModule = false;
            }
            else
                throw new Exception(string.Format("Include module is empty for field {0}.",
                    fieldInfo.ExcName()));
            return fti;
        }

        private static void Check(FieldInfo fieldInfo, FieldTranslationInfo fti)
        {
            if (fti == null) throw new ArgumentNullException("fti");
            if (fti.Destination != FieldTranslationDestionations.NormalField &&
                fti.Destination != FieldTranslationDestionations.ClassConst)
                throw new Exception(string.Format("Unable to find right way to convert field {0}",
                    fieldInfo.ExcName()));
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-27 10:30
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
        /// Własność jest tylko do odczytu.
        /// </summary>
        public string ScriptName
        {
            get
            {
                return _scriptName;
            }
        }
        private string _scriptName = string.Empty;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool IsScriptNamePhpEncoded
        {
            get
            {
                return _isScriptNamePhpEncoded;
            }
        }
        private bool _isScriptNamePhpEncoded;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public FieldTranslationDestionations Destination
        {
            get
            {
                return _destination;
            }
        }
        private FieldTranslationDestionations _destination;
        /// <summary>
        /// czy sklejać wartość stałą w wyrażeniach; własność jest tylko do odczytu.
        /// </summary>
        public bool UsGlueForValue
        {
            get
            {
                return _usGlueForValue;
            }
        }
        private bool _usGlueForValue;
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public PhpCodeModuleName IncludeModule
        {
            get
            {
                return _includeModule;
            }
        }
        private PhpCodeModuleName _includeModule;
        /// <summary>
        /// czy jest zdefiniowany w module, którego nie można includować; własność jest tylko do odczytu.
        /// </summary>
        public bool IsDefinedInNonincludableModule
        {
            get
            {
                return _isDefinedInNonincludableModule;
            }
        }
        private bool _isDefinedInNonincludableModule;
        #endregion Properties

    }
}
