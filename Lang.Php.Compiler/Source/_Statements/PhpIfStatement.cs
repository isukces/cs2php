using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property Condition IPhpValue 
    
    property IfTrue IPhpStatement 
    	preprocess value = PhpCodeBlock.Reduce(value);
    
    property IfFalse IPhpStatement 
    	preprocess value = PhpCodeBlock.Reduce(value);
    smartClassEnd
    */
    
    public partial class PhpIfStatement : IPhpStatementBase
    {
        #region Methods

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var isBeauty = style == null || style.Compression == EmitStyleCompression.Beauty;

            var ifTrueAny = PhpCodeBlock.HasAny(_ifTrue);
            var ifFalseAny = PhpCodeBlock.HasAny(_ifFalse);
            if (!ifTrueAny && !ifFalseAny) return;

            writer.OpenLnF("if{1}({0}){2}",
                _condition.GetPhpCode(style),
                isBeauty ? " " : "",
                ifTrueAny ? "" : "{}");
            if (ifTrueAny)
            {
                var iStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.IfManyItems_OR_IfStatement);
                if (style != null && style.UseBracketsEvenIfNotNecessary)
                    iStyle.Brackets = ShowBracketsEnum.Always;
                var bound = PhpCodeBlock.Bound(_ifTrue);
                bound.Emit(emiter, writer, iStyle);
            }
            writer.DecIndent();
            if (!ifFalseAny) return;
            var oneLine = _ifFalse is PhpIfStatement;
            var oldIndent = writer.Intent;
            {
                if (oneLine)
                {
                    writer.Write("else ");
                    writer.SkipIndent = true;
                }
                else
                    writer.OpenLn("else");
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
            return GetCodeRequests(_condition, _ifTrue, _ifFalse);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var newIfTrue = s.Simplify(_ifTrue);
            var newIfFalse = s.Simplify(_ifFalse);
            var newCondition = s.Simplify(_condition);
            if (newIfTrue == _ifTrue && newIfFalse == _ifFalse && newCondition == _condition)
                return this;
            return new PhpIfStatement(newCondition, newIfTrue, newIfFalse);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-08 14:13
// File generated automatically ver 2014-09-01 19:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpIfStatement 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpIfStatement()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Condition## ##IfTrue## ##IfFalse##
        implement ToString Condition=##Condition##, IfTrue=##IfTrue##, IfFalse=##IfFalse##
        implement equals Condition, IfTrue, IfFalse
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="condition"></param>
        /// <param name="ifTrue"></param>
        /// <param name="ifFalse"></param>
        /// </summary>
        public PhpIfStatement(IPhpValue condition, IPhpStatement ifTrue, IPhpStatement ifFalse)
        {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Condition; 
        /// </summary>
        public const string PropertyNameCondition = "Condition";
        /// <summary>
        /// Nazwa własności IfTrue; 
        /// </summary>
        public const string PropertyNameIfTrue = "IfTrue";
        /// <summary>
        /// Nazwa własności IfFalse; 
        /// </summary>
        public const string PropertyNameIfFalse = "IfFalse";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                _condition = value;
            }
        }
        private IPhpValue _condition;
        /// <summary>
        /// 
        /// </summary>
        public IPhpStatement IfTrue
        {
            get
            {
                return _ifTrue;
            }
            set
            {
                value = PhpCodeBlock.Reduce(value);
                _ifTrue = value;
            }
        }
        private IPhpStatement _ifTrue;
        /// <summary>
        /// 
        /// </summary>
        public IPhpStatement IfFalse
        {
            get
            {
                return _ifFalse;
            }
            set
            {
                value = PhpCodeBlock.Reduce(value);
                _ifFalse = value;
            }
        }
        private IPhpStatement _ifFalse;
        #endregion Properties

    }
}
