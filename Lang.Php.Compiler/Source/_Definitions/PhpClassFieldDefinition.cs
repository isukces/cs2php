using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpClassFieldDefinition : IPhpClassMember, ICodeRelated
    {
        // Public Methods 


        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            if (IsConst)
            {
                //  const CONSTANT = 'constant value';
                writer.WriteLnF("const {0} = {1};", Name, ConstValue.GetPhpCode(style));
                return;
            }

            var a = string.Format("{0}{1} ${2}",
                Visibility.ToString().ToLower(),
                IsStatic ? " static" : "",
                Name
            );
            if (ConstValue != null)
                a += " = " + ConstValue.GetPhpCode(style);
            writer.WriteLn(a + ";");
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return PhpStatementBase.GetCodeRequests(ConstValue);
        }

        /// <summary>
        ///     nazwa pola lub stałej
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                value = (value ?? string.Empty).Trim();
                value = PhpVariableExpression.AddDollar(value, false);
                _name = value;
            }
        }

        /// <summary>
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// </summary>
        public bool IsConst { get; set; }

        /// <summary>
        /// </summary>
        public IPhpValue ConstValue { get; set; }

        /// <summary>
        /// </summary>
        public Visibility Visibility { get; set; } = Visibility.Public;

        private string _name = string.Empty;
    }
}