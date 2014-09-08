using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Lang.Php.Compiler.Source
{
    public abstract class IPhpStatementBase : PhpSourceBase, IPhpStatement, ICodeRelated
    {

        public virtual IPhpStatement Simplify(IPhpSimplifier s)
        {
            return this;
        }

        protected void EmitHeaderStatement(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style, string header, IPhpStatement statement)
        {
            style = style ?? new PhpEmitStyle();
            var iStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.IfManyItems);
            if (style.UseBracketsEvenIfNotNecessary)
                iStyle.Brackets = ShowBracketsEnum.Always;

            var statementToEmit = PhpCodeBlock.Reduce(statement);
            var emptyStatement = !PhpCodeBlock.HasAny(statementToEmit);


            if (emptyStatement)
                header += "{}";
            if (style.Compression == EmitStyleCompression.NearCrypto)
                writer.Write(header);
            else
                writer.WriteLn(header);
            if (emptyStatement) return;


            bool myBracket = style.UseBracketsEvenIfNotNecessary;
            if (!myBracket)
            {
                var gf = statementToEmit.GetStatementEmitInfo(iStyle);
                if (gf != StatementEmitInfo.NormalSingleStatement)
                    myBracket = true;
            }
            writer.IncIndent();
            if (myBracket)
            {
                iStyle.Brackets = ShowBracketsEnum.Never;
                writer.OpenLn("{");
            }


            statementToEmit.Emit(emiter, writer, iStyle);
            if (myBracket)
                writer.CloseLn("}");

            writer.DecIndent();

        }
        public static IEnumerable<ICodeRequest> Xxx(params object[] x)
        {
            return Xxx<object>(x);
        }
        public static IEnumerable<ICodeRequest> Xxx<T>(IEnumerable<T> x)
        {
            if (x == null)
                return new ICodeRequest[0];
            IEnumerable<ICodeRequest> result = null;
            foreach (var i in x)
            {
                if (i == null) continue;
                IEnumerable<ICodeRequest> append;
                if (i is ICodeRelated)
                    append = (i as ICodeRelated).GetCodeRequests();
                else if (i is IPhpStatement)
                {
                    throw new Exception();
                }
                else
                    continue;
                if (result == null)
                    result = append;
                else if (append != null)
                    result = result.Concat(append);
            }
            return result ?? new ICodeRequest[0];
        }

        public abstract void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style);


        public abstract IEnumerable<ICodeRequest> GetCodeRequests();


        public virtual StatementEmitInfo GetStatementEmitInfo(PhpEmitStyle style)
        {
            return StatementEmitInfo.NormalSingleStatement;
        }
    }
}
