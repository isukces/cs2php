using System;

namespace Lang.Php.Compiler
{

    public  class GlobalVariableRequest : ICodeRequest
    {
        public override string ToString()
        {
            return string.Format("GlobalVariableRequest {0}", _variableName);
        }


        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="variableName">nazwa zmiennej globalnej</param>
        /// </summary>
        public GlobalVariableRequest(string variableName)
        {
            VariableName = variableName;
        }

        /// <summary>
        /// nazwa zmiennej globalnej
        /// </summary>
        public string VariableName
        {
            get => _variableName;
            private set => _variableName = (value ?? String.Empty).Trim();
        }
        private string _variableName = string.Empty;
    }
}
