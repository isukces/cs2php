using System;

namespace Lang.Php
{
    [AttributeUsage(AttributeTargets.All)]
    public class ModuleAttribute : Attribute
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="moduleShortName">Module short name i.e "hello-page" or "mynamespace/hello-class"</param>
        ///     <param name="includePathPrefix"></param>
        /// </summary>
        public ModuleAttribute(string moduleShortName, params string[] includePathPrefix)
        {
            ModuleShortName   = moduleShortName;
            IncludePathPrefix = includePathPrefix;
        }


        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="moduleShortName">Module short name i.e "hello-page" or "mynamespace/hello-class"</param>
        /// </summary>
        public ModuleAttribute(string moduleShortName)
        {
            ModuleShortName = moduleShortName;
        }

        /// <summary>
        ///     Module short name i.e "hello-page" or "mynamespace/hello-class"; własność jest tylko do odczytu.
        /// </summary>
        public string ModuleShortName { get; } = string.Empty;

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string[] IncludePathPrefix { get; }
    }
}