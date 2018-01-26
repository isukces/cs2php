using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpCodeBlock : PhpSourceBase, IPhpStatement, ICodeRelated
    {
        public PhpCodeBlock(IPhpStatement other)
        {
            Statements.Add(other);
        }

        public PhpCodeBlock()
        {
        }

        public static PhpCodeBlock Bound(IPhpStatement s)
        {
            if (s is PhpCodeBlock)
                return s as PhpCodeBlock;
            var g = new PhpCodeBlock();
            g.Statements.Add(s);
            return g;
        }

        // Public Methods 

        public static bool HasAny(IPhpStatement x)
        {
            if (x == null) return false;
            if (x is PhpCodeBlock)
            {
                var src = x as PhpCodeBlock;
                if (src.Statements.Count == 0) return false;
                if (src.Statements.Count > 1) return true;
                return HasAny(src.Statements[0]);
            }

            return true;
        }

        public static IPhpStatement Reduce(IPhpStatement x)
        {
            if (x == null) return x;
            if (x is PhpCodeBlock)
            {
                var src = x as PhpCodeBlock;
                if (src.Statements.Count == 0) return null;
                if (src.Statements.Count > 1) return x;
                return Reduce(src.Statements[0]);
            }

            return x;
        }

        // Public Methods 

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            if (Statements.Count == 0)
                return;
            var bracketStyle = style == null ? ShowBracketsEnum.IfManyItems : style.Brackets;
            var brack        =
                bracketStyle == ShowBracketsEnum.Never
                    ? false
                    : bracketStyle == ShowBracketsEnum.Always
                        ? true
                        : Statements == null || !(Statements.Count == 1);

            if (Statements != null && Statements.Count == 1 &&
                bracketStyle == ShowBracketsEnum.IfManyItems_OR_IfStatement)
                if (Statements[0] is PhpIfStatement)
                    brack = true;

            var iStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.Never);
            if (!brack && bracketStyle != ShowBracketsEnum.Never && Statements.Count == 1)
            {
                var tmp = Statements[0];
                var gf  = tmp.GetStatementEmitInfo(iStyle);
                if (gf != StatementEmitInfo.NormalSingleStatement)
                    brack = true;
            }

            if (brack)
                writer.OpenLn("{");
            foreach (var i in Statements)
                i.Emit(emiter, writer, iStyle);
            if (brack)
                writer.CloseLn("}");
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (Statements.Count == 0)
                return new ICodeRequest[0];
            var r = new List<ICodeRequest>();
            foreach (var i in Statements)
                r.AddRange(i.GetCodeRequests());
            return r;
        }

        public List<IPhpStatement> GetPlain()
        {
            var o = new List<IPhpStatement>();
            if (Statements == null || Statements.Count == 0)
                return o;
            foreach (var i in Statements)
                if (i is PhpCodeBlock)
                    o.AddRange((i as PhpCodeBlock).GetPlain());
                else
                    o.Add(i);
            return o;
        }


        public StatementEmitInfo GetStatementEmitInfo(PhpEmitStyle style)
        {
            return StatementEmitInfo.NormalSingleStatement; // sam troszczę się o swoje nawiasy
        }


        /// <summary>
        /// </summary>
        public List<IPhpStatement> Statements { get; set; } = new List<IPhpStatement>();
    }
}