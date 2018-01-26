using System.Collections.Generic;
using System.Reflection;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{
    public class AssemblyTranslationInfo
    {
        public static AssemblyTranslationInfo FromAssembly(Assembly assembly, TranslationInfo translationInfo)
        {
            if (assembly == null)
                return null;
            var ati = new AssemblyTranslationInfo();
            {
                ati.Assembly = assembly;

                var moduleIncludeConst = assembly.GetCustomAttribute<ModuleIncludeConstAttribute>();
                if (moduleIncludeConst != null)
                {
                    ati.IncludePathConstOrVarName = (moduleIncludeConst.ConstOrVarName ?? "").Trim();
                    if (ati.IncludePathConstOrVarName.StartsWith("$"))
                    {
                    }
                    else
                    {
                        ati.IncludePathConstOrVarName = "\\" + ati.IncludePathConstOrVarName.TrimStart('\\');
                    }
                }

                ati.RootPath = GetRootPath(assembly);

                var phpPackageSource = assembly.GetCustomAttribute<PhpPackageSourceAttribute>();
                if (phpPackageSource != null)
                {
                    ati.PhpPackageSourceUri = phpPackageSource.SourceUri;
                    ati.PhpPackagePathStrip = phpPackageSource.StripArchivePath;
                }

                {
                    var configModule = assembly.GetCustomAttribute<ConfigModuleAttribute>();
                    if (configModule != null)
                        ati.ConfigModuleName = configModule.Name;
                }
                {
                    var defaultTimezone = assembly.GetCustomAttribute<DefaultTimezoneAttribute>();
                    if (defaultTimezone != null)
                        ati.DefaultTimezone = defaultTimezone.Timezone;
                }
            }
            ati.LibraryName              = LibNameFromAssembly(assembly);
            ati.PhpIncludePathExpression = GetDefaultIncludePath(ati, translationInfo);
            return ati;
        }

        public static string GetRootPath(Assembly assembly)
        {
            var rootPathAttribute = assembly.GetCustomAttribute<RootPathAttribute>();
            if (rootPathAttribute == null)
                return string.Empty;
            var result = (rootPathAttribute.Path ?? "").Replace("\\", "/").TrimEnd('/').TrimStart('/') + "/";
            while (result.Contains("//"))
                result = result.Replace("//", "/");
            return result;
        }

        private static IPhpValue GetDefaultIncludePath(AssemblyTranslationInfo ati, TranslationInfo translationInfo)
        {
            var pathElements = new List<IPhpValue>();

            #region Take include path variable or const

            if (!string.IsNullOrEmpty(ati.IncludePathConstOrVarName))
                if (ati.IncludePathConstOrVarName.StartsWith("$"))
                {
                    pathElements.Add(new PhpVariableExpression(ati.IncludePathConstOrVarName, PhpVariableKind.Global));
                }
                else
                {
                    var tmp = ati.IncludePathConstOrVarName;
                    if (!tmp.StartsWith("\\")) // defined const is in global namespace ALWAYS
                        tmp = "\\" + tmp;

                    KnownConstInfo info;
                    if (translationInfo != null && translationInfo.KnownConstsValues.TryGetValue(tmp, out info) &&
                        info.UseFixedValue)
                        pathElements.Add(new PhpConstValue(info.Value));
                    else
                        pathElements.Add(new PhpDefinedConstExpression(tmp, PhpCodeModuleName.Cs2PhpConfigModuleName));
                }

            #endregion

            //#region RootPathAttribute
            //{
            //    if (!string.IsNullOrEmpty(ati.RootPath) && ati.RootPath != "/")
            //        pathElements.Add(new PhpConstValue(ati.RootPath));
            //}
            //#endregion
            var result = PhpBinaryOperatorExpression.ConcatStrings(pathElements.ToArray());
            return result;
        }

        private static string LibNameFromAssembly(Assembly a)
        {
            var tmp = a.ManifestModule.ScopeName.ToLower();
            if (tmp.EndsWith(".dll"))
                tmp = tmp.Substring(0, tmp.Length - 4);
            return tmp;
        }

        // Private Methods 
        public override string ToString()
        {
            return LibraryName;
        }


        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string LibraryName { get; private set; } = string.Empty;

        /// <summary>
        ///     nazwa stałej lub zmiennej, która oznacza ścieżkę do biblioteki; własność jest tylko do odczytu.
        /// </summary>
        public string IncludePathConstOrVarName { get; private set; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string RootPath { get; private set; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string PhpPackageSourceUri { get; private set; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string PhpPackagePathStrip { get; private set; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public IPhpValue PhpIncludePathExpression { get; private set; }

        /// <summary>
        /// </summary>
        public string ConfigModuleName
        {
            get => _configModuleName;
            private set => _configModuleName = (value ?? string.Empty).Trim();
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public Timezones? DefaultTimezone { get; private set; }

        private string _configModuleName = "cs2php";
    }
}