using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpVariableExpression : PhpValueBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="variableName"></param>
        ///     <param name="variableKind"></param>
        /// </summary>
        public PhpVariableExpression(string variableName, PhpVariableKind variableKind)
        {
            VariableName = variableName;
            VariableKind         = variableKind;
        }
        // Public Methods 

        public static string AddDollar(string x, bool condition = true)
        {
            if (condition)
                return x.StartsWith("$") ? x : "$" + x;
            return x.StartsWith("$") ? x.TrimStart('$') : x;
        }

        public static PhpVariableExpression MakeGlobal(string name)
        {
            return new PhpVariableExpression(AddDollar(name), PhpVariableKind.Global);
        }

        public static PhpVariableExpression MakeLocal(string name, bool isFunctionArgument)
        {
            return new PhpVariableExpression(AddDollar(name),
                isFunctionArgument ? PhpVariableKind.LocalArgument : PhpVariableKind.Local);
        }

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (VariableKind == PhpVariableKind.Global)
            {
                yield return new GlobalVariableRequest(_variableName);
            }
            else
            {
                var a = new LocalVariableRequest(_variableName,
                    VariableKind == PhpVariableKind.LocalArgument,
                    newName => { VariableName = newName; });
                yield return a;
            }
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return _variableName;
        }


        /// <summary>
        /// </summary>
        public string VariableName
        {
            get => _variableName;
            set => _variableName = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public PhpVariableKind VariableKind { get; set; }

        private string _variableName = string.Empty;
    }
}