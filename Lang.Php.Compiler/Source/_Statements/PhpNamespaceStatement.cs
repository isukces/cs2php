using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpNamespaceStatement : PhpSourceBase, IPhpStatement, ICodeRelated
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="Name">namespace name</param>
        /// </summary>
        public PhpNamespaceStatement(PhpNamespace Name)
        {
            this.Name = Name;
        }
        // Public Methods 

        public static bool IsRootNamespace(string name)
        {
            return string.IsNullOrEmpty(name) || name == PathUtil.WIN_SEP;
        }

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            if (Name.IsRoot)
                writer.OpenLn("namespace {");
            else
                writer.OpenLnF("namespace {0} {{", Name);
            Code.Emit(emiter, writer, style);
            writer.CloseLn("}");
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return Code.GetCodeRequests();
        }

        public StatementEmitInfo GetStatementEmitInfo(PhpEmitStyle style)
        {
            return StatementEmitInfo.NormalSingleStatement;
        }

        public override string ToString()
        {
            if (Name.IsRoot)
                return "Root Php namespace";
            return string.Format("Php namespace {0}", Name);
        }

        /// <summary>
        ///     namespace name
        /// </summary>
        public PhpNamespace Name { get; set; }

        /// <summary>
        /// </summary>
        public PhpCodeBlock Code { get; set; } = new PhpCodeBlock();
    }
}