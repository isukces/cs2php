using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler
{
    public class ModuleCodeRequest : ICodeRequest
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="moduleName"></param>
        ///     <param name="why">Why this Module is requested</param>
        /// </summary>
        public ModuleCodeRequest(PhpCodeModuleName moduleName, string why)
        {
            ModuleName = moduleName;
            Why        = why;
        }


        /// <summary>
        ///     Zwraca tekstową reprezentację obiektu
        /// </summary>
        /// <returns>Tekstowa reprezentacja obiektu</returns>
        public override string ToString()
        {
            return string.Format("{0}", ModuleName);
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public PhpCodeModuleName ModuleName { get; }

        /// <summary>
        ///     Why this Module is requested; własność jest tylko do odczytu.
        /// </summary>
        public string Why { get; } = string.Empty;
    }
}