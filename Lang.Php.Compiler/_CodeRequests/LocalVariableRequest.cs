using System;

namespace Lang.Php.Compiler
{
    public class LocalVariableRequest : ICodeRequest
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="variableName">nazwa zmiennej globalnej</param>
        ///     <param name="isArgument">czy zmienna jest deklarowana jako argument funkcji</param>
        ///     <param name="changeNameAction">akcja, która musi być wykonana przy zmianie nazwy zmiennej</param>
        /// </summary>
        public LocalVariableRequest(string variableName, bool isArgument, Action<string> changeNameAction)
        {
            VariableName     = variableName;
            IsArgument       = isArgument;
            ChangeNameAction = changeNameAction;
        }

        public override string ToString()
        {
            return string.Format("LocalVariableRequest {0}", _variableName);
        }

        /// <summary>
        ///     nazwa zmiennej globalnej
        /// </summary>
        public string VariableName
        {
            get => _variableName;
            private set => _variableName = (value ?? string.Empty).Trim();
        }

        /// <summary>
        ///     czy zmienna jest deklarowana jako argument funkcji
        /// </summary>
        public bool IsArgument { get; set; }

        /// <summary>
        ///     akcja, która musi być wykonana przy zmianie nazwy zmiennej
        /// </summary>
        public Action<string> ChangeNameAction { get; set; }

        private string _variableName = string.Empty;
    }
}