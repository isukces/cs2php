using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpSwitchStatement : PhpStatementBase
    {
        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            writer.OpenLnF("switch ({0}) {{", Expression.GetPhpCode(style));
            foreach (var sec in Sections)
            {
                foreach (var l in sec.Labels)
                    writer.WriteLnF("{0}{1}:",
                        l.IsDefault ? "" : "case ",
                        l.IsDefault ? "default" : l.Value.GetPhpCode(style));
                writer.IncIndent();
                sec.Statement.Emit(emiter, writer, style);
                writer.DecIndent();
            }

            writer.CloseLn("}");
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var result = new List<ICodeRequest>();
            if (Expression != null)
                result.AddRange(Expression.GetCodeRequests());
            foreach (var sec in Sections)
                result.AddRange(sec.GetCodeRequests());
            return result;
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var changed = false;
            var e1      = s.Simplify(Expression);
            if (!EqualCode(e1, Expression))
                changed = true;

            var s1 = new List<PhpSwitchSection>();
            foreach (var i in Sections)
            {
                bool wasChanged;
                var  n = i.Simplify(s, out wasChanged);
                if (wasChanged)
                    changed = true;
                s1.Add(n);
            }

            if (!changed)
                return this;
            return new PhpSwitchStatement
            {
                Expression = e1,
                Sections   = s1
            };
        }


        /// <summary>
        /// </summary>
        public IPhpValue Expression { get; set; }

        /// <summary>
        /// </summary>
        public List<PhpSwitchSection> Sections { get; set; } = new List<PhpSwitchSection>();
    }
}