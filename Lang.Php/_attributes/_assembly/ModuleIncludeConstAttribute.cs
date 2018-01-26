using System;

namespace Lang.Php
{
    /// <summary>
    ///     Atrybut dołączany do assemby, który wskazuje jaka stała PHP określa ścieżkę bazową dla biblioteki
    ///     może być np. MY_LIB_PATH (define) lub $MyLibPath (global var)
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ModuleIncludeConstAttribute : Attribute
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="constOrVarName">Module filename</param>
        /// </summary>
        public ModuleIncludeConstAttribute(string constOrVarName)
        {
            ConstOrVarName = constOrVarName;
        }


        /// <summary>
        ///     Module filename; własność jest tylko do odczytu.
        /// </summary>
        public string ConstOrVarName { get; } = string.Empty;
    }
}