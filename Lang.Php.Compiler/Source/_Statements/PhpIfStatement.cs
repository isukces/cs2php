using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpIfStatement : PhpStatementBase
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="condition"></param>
        ///     <param name="ifTrue"></param>
        ///     <param name="ifFalse"></param>
        /// </summary>
        public PhpIfStatement(IPhpValue condition, IPhpStatement ifTrue, IPhpStatement ifFalse)
        {
            Condition = condition;
            IfTrue    = ifTrue;
            IfFalse   = ifFalse;
        }
        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var isBeauty = style == null || style.Compression == EmitStyleCompression.Beauty;

            var ifTrueAny  = PhpCodeBlock.HasAny(_ifTrue);
            var ifFalseAny = PhpCodeBlock.HasAny(_ifFalse);
            if (!ifTrueAny && !ifFalseAny) return;

            writer.OpenLnF("if{1}({0}){2}",
                Condition.GetPhpCode(style),
                isBeauty ? " " : "",
                ifTrueAny ? "" : "{}");
            if (ifTrueAny)
            {
                var iStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.IfManyItems_OR_IfStatement);
                if (style != null && style.UseBracketsEvenIfNotNecessary)
                    iStyle.Brackets = ShowBracketsEnum.Always;
                var bound           = PhpCodeBlock.Bound(_ifTrue);
                bound.Emit(emiter, writer, iStyle);
            }

            writer.DecIndent();
            if (!ifFalseAny) return;
            var oneLine   = _ifFalse is PhpIfStatement;
            var oldIndent = writer.Intent;
            {
                if (oneLine)
                {
                    writer.Write("else ");
                    writer.SkipIndent = true;
                }
                else
                {
                    writer.OpenLn("else");
                }

                var myBracket = style != null && style.UseBracketsEvenIfNotNecessary;

                var iStyle = PhpEmitStyle.xClone(style,
                    myBracket
                        ? ShowBracketsEnum.Never
                        : ShowBracketsEnum.IfManyItems_OR_IfStatement);

                if (!myBracket)
                {
                    var gf = _ifFalse.GetStatementEmitInfo(iStyle);
                    if (gf != StatementEmitInfo.NormalSingleStatement)
                        myBracket = true;
                }

                if (myBracket)
                {
                    iStyle.Brackets = ShowBracketsEnum.Never;
                    writer.OpenLn("{");
                }

                _ifFalse.Emit(emiter, writer, iStyle);
                if (myBracket)
                    writer.CloseLn("}");
            }
            writer.Intent = oldIndent;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return GetCodeRequests(Condition, _ifTrue, _ifFalse);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var newIfTrue    = s.Simplify(_ifTrue);
            var newIfFalse   = s.Simplify(_ifFalse);
            var newCondition = s.Simplify(Condition);
            if (newIfTrue == _ifTrue && newIfFalse == _ifFalse && newCondition == Condition)
                return this;
            return new PhpIfStatement(newCondition, newIfTrue, newIfFalse);
        }


        /// <summary>
        /// </summary>
        public IPhpValue Condition { get; set; }

        /// <summary>
        /// </summary>
        public IPhpStatement IfTrue
        {
            get => _ifTrue;
            set => _ifTrue = PhpCodeBlock.Reduce(value);
        }

        /// <summary>
        /// </summary>
        public IPhpStatement IfFalse
        {
            get => _ifFalse;
            set
            {
                value    = PhpCodeBlock.Reduce(value);
                _ifFalse = value;
            }
        }

        private IPhpStatement _ifTrue;
        private IPhpStatement _ifFalse;
    }
}