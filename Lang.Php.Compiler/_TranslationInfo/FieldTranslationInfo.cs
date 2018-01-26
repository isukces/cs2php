using System;
using System.Reflection;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{
    public class FieldTranslationInfo
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
                fti.Destination = FieldTranslationDestionations.ClassConst;
            {
                fti.ScriptName          = fieldInfo.Name;
                var scriptNameAttribute = fieldInfo.GetCustomAttribute<ScriptNameAttribute>();
                if (scriptNameAttribute != null)
                {
                    fti.ScriptName             = scriptNameAttribute.Name;
                    fti.IsScriptNamePhpEncoded = scriptNameAttribute.Kind == ScriptNameAttribute.Kinds.IntIndex;
                }
            }
            {
                var asDefinedConstAttribute = fieldInfo.GetCustomAttribute<AsDefinedConstAttribute>();
                if (asDefinedConstAttribute != null)
                {
                    fti.Destination = FieldTranslationDestionations.DefinedConst;
                    if (!string.IsNullOrEmpty(asDefinedConstAttribute.DefinedConstName))
                        fti.ScriptName = asDefinedConstAttribute.DefinedConstName;
                }
            }
            {
                var globalVariableAttribute = fieldInfo.GetCustomAttribute<GlobalVariableAttribute>();
                if (globalVariableAttribute != null)
                {
                    Check(fieldInfo, fti);
                    fti.Destination = FieldTranslationDestionations.GlobalVariable;
                    if (!string.IsNullOrEmpty(globalVariableAttribute.GlobalVariableName))
                        fti.ScriptName = globalVariableAttribute.GlobalVariableName;
                }
            }
            {
                var asValueAttribute = fieldInfo.GetCustomAttribute<AsValueAttribute>();
                if (asValueAttribute != null)
                {
                    Check(fieldInfo, fti);
                    fti.Destination    = FieldTranslationDestionations.JustValue;
                    fti.UsGlueForValue = asValueAttribute.Glue;
                }
            }
            var canBeNull = false;
            switch (fti.Destination)
            {
                case FieldTranslationDestionations.JustValue:
                case FieldTranslationDestionations.GlobalVariable:
                    canBeNull         = true;
                    fti.IncludeModule = null; // force null
                    break;
                case FieldTranslationDestionations.DefinedConst:
                case FieldTranslationDestionations.ClassConst:
                case FieldTranslationDestionations.NormalField:
                    var cti           = info.GetOrMakeTranslationInfo(fieldInfoDeclaringType);
                    fti.IncludeModule = cti.ModuleName;
                    if (cti.BuildIn)
                    {
                        fti.IncludeModule = null;
                        canBeNull         = true;
                    }

                    var isFieldOutsideClass = fti.Destination == FieldTranslationDestionations.GlobalVariable ||
                                              fti.Destination == FieldTranslationDestionations.DefinedConst;
                {
                    // can be in other module for GlobalVariable and DefinedConst
                    var moduleAttribute = fieldInfo.GetCustomAttribute<ModuleAttribute>();
                    if (moduleAttribute != null)
                    {
                        if (!isFieldOutsideClass)
                            throw new Exception(string.Format(
                                "Module attribute can only be defined for GlobalVariable or DefinedConst. Check {0}.",
                                fieldInfo.ExcName()));
                        fti.IncludeModule = new PhpCodeModuleName(moduleAttribute.ModuleShortName,
                            info.GetOrMakeTranslationInfo(fieldInfoDeclaringType.Assembly));
                    }
                }
                    if (cti.IsPage)
                        fti.IsDefinedInNonincludableModule = true;
                    if (!isFieldOutsideClass)
                        if (cti.IsArray || cti.Type.IsEnum || cti.BuildIn)
                        {
                            canBeNull         = true;
                            fti.IncludeModule = null; // force null
                        }
                        else if (cti.DontIncludeModuleForClassMembers)
                        {
                            throw new Exception(
                                string.Format("field {0} belongs to nonincludable class (array, enum or skipped)",
                                    fieldInfo.ExcName()));
                        }

                    break;
            }

            if (!fti.IncludeModule.IsEmpty())
                return fti;
            if (canBeNull)
            {
                fti.IncludeModule                  = null; // can be not null but empty
                fti.IsDefinedInNonincludableModule = false;
            }
            else
            {
                throw new Exception(string.Format("Include module is empty for field {0}.",
                    fieldInfo.ExcName()));
            }

            return fti;
        }

        private static void Check(FieldInfo fieldInfo, FieldTranslationInfo fti)
        {
            if (fti == null) throw new ArgumentNullException(nameof(fti));
            if (fti.Destination != FieldTranslationDestionations.NormalField &&
                fti.Destination != FieldTranslationDestionations.ClassConst)
                throw new Exception(string.Format("Unable to find right way to convert field {0}",
                    fieldInfo.ExcName()));
        }


        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string ScriptName { get; private set; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public bool IsScriptNamePhpEncoded { get; private set; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public FieldTranslationDestionations Destination { get; private set; }

        /// <summary>
        ///     czy sklejać wartość stałą w wyrażeniach; własność jest tylko do odczytu.
        /// </summary>
        public bool UsGlueForValue { get; private set; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public PhpCodeModuleName IncludeModule { get; private set; }

        /// <summary>
        ///     czy jest zdefiniowany w module, którego nie można includować; własność jest tylko do odczytu.
        /// </summary>
        public bool IsDefinedInNonincludableModule { get; private set; }
    }
}