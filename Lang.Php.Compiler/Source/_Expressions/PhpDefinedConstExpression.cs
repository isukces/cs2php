using System;
using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpDefinedConstExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="definedConstName"></param>
        ///     <param name="moduleName"></param>
        /// </summary>
        public PhpDefinedConstExpression(string definedConstName, PhpCodeModuleName moduleName)
        {
            if (definedConstName == "PHP_EOL" && moduleName != null)
                throw new Exception("PHP_EOL is built in");
            DefinedConstName = definedConstName;
            _moduleName      = moduleName;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (_moduleName != null)
                yield return new ModuleCodeRequest(_moduleName, "defined const " + DefinedConstName);
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return DefinedConstName;
        }

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public string DefinedConstName { get; } = string.Empty;

        private readonly PhpCodeModuleName _moduleName;
    }
}