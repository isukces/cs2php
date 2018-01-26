using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{
    public class PhpMethodDefinition : ICodeRelated
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="name">Nazwa metody</param>
        /// </summary>
        public PhpMethodDefinition(string name)
        {
            Name = name;
        }
        // Public Methods 

        public virtual void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter code, PhpEmitStyle style)
        {
            // public function addFunction($function, $namespace = '') 
            var accessModifiers = GetAccessModifiers();
            var param           =
                Arguments == null ? "" : string.Join(", ", Arguments.Select(u => u.GetPhpCode(style)));
            code.OpenLnF("{0} function {1}({2}) {{", accessModifiers, Name, param);
            {
                var g = GetGlobals();
                if (!string.IsNullOrEmpty(g))
                    code.WriteLnF("global {0};", g);
            }
            foreach (var statement in Statements)
            {
                var g      = PhpEmitStyle.xClone(style);
                g.Brackets = ShowBracketsEnum.Never;
                statement.Emit(emiter, code, g);
            }

            code.CloseLn("}");
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = PhpStatementBase.GetCodeRequests(Arguments);
            var b = PhpStatementBase.GetCodeRequests(Statements);
            return a.Union(b);
        }
        // Protected Methods 

        protected virtual string GetAccessModifiers()
        {
            return "";
        }

        protected string GetGlobals()
        {
            var aa = GetCodeRequests()
                .OfType<GlobalVariableRequest>()
                .Where(i => !string.IsNullOrEmpty(i.VariableName))
                .Select(i => PhpVariableExpression.AddDollar(i.VariableName)).Distinct();
            var globals = string.Join(", ", aa);
            return globals;
        }


        /// <summary>
        ///     Nazwa metody
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public List<PhpMethodArgument> Arguments { get; set; } = new List<PhpMethodArgument>();

        /// <summary>
        /// </summary>
        public List<IPhpStatement> Statements { get; set; } = new List<IPhpStatement>();

        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public bool IsAnonymous => string.IsNullOrEmpty(_name);

        private string _name = string.Empty;
    }
}