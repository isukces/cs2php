using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            bool ifTrueAny = PhpCodeBlock.HasAny(ifTrue);
            bool ifFalseAny = PhpCodeBlock.HasAny(ifFalse);
            if (!ifTrueAny && !ifFalseAny) return;

            writer.OpenLnF("if{1}({0}){2}",
                condition.GetPhpCode(style),
                isBeauty ? " " : "",
                ifTrueAny ? "" : "{}");
            if (ifTrueAny)
            {
                var iStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.IfManyItems_OR_IfStatement);
                if (style.UseBracketsEvenIfNotNecessary)
                    iStyle.Brackets = ShowBracketsEnum.Always;
                var bound = PhpCodeBlock.Bound(ifTrue);
                bound.Emit(emiter, writer, iStyle);
            }
            writer.DecIndent();
            if (ifFalseAny)
            {
                bool OneLine = ifFalse is PhpIfStatement;
                int _oldIndent = writer.Intent;
                {
                    if (OneLine)
                    {
                        writer.Write("else ");
                        writer.SkipIndent = true;
                    }
                    else
                        writer.OpenLn("else");
                    bool myBracket = style.UseBracketsEvenIfNotNecessary;

                    var iStyle = PhpEmitStyle.xClone(style,
                        myBracket
                        ? ShowBracketsEnum.Never
                        : ShowBracketsEnum.IfManyItems_OR_IfStatement);

                    if (!myBracket)
                    {
                        var gf = ifFalse.GetStatementEmitInfo(iStyle);
                        if (gf != StatementEmitInfo.NormalSingleStatement)
                            myBracket = true;
                    }
                    if (myBracket)
                    {
                        iStyle.Brackets = ShowBracketsEnum.Never;
                        writer.OpenLn("{");
                    }                   
                    ifFalse.Emit(emiter, writer, iStyle);
                    if (myBracket)
                        writer.CloseLn("}");
                }
                writer.Intent = _oldIndent;
            }
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return Xxx(condition, ifTrue, ifFalse);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var __ifTrue = s.Simplify(ifTrue);
            var __ifFalse = s.Simplify(ifFalse);
            var __condition = s.Simplify(condition);
            if (__ifTrue == ifTrue && __ifFalse == ifFalse && __condition == condition)
                return this;
            return new PhpIfStatement(__condition, __ifTrue, __ifFalse);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-08 11:40
// File generated automatically ver 2013-07-10 08:43
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
        /// <param name="Condition"></param>
        /// <param name="IfTrue"></param>
        /// <param name="IfFalse"></param>
        /// </summary>
        public PhpIfStatement(IPhpValue Condition, IPhpStatement IfTrue, IPhpStatement IfFalse)
        {
            this.Condition = Condition;
            this.IfTrue = IfTrue;
            this.IfFalse = IfFalse;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Condition; 
        /// </summary>
        public const string PROPERTYNAME_CONDITION = "Condition";
        /// <summary>
        /// Nazwa własności IfTrue; 
        /// </summary>
        public const string PROPERTYNAME_IFTRUE = "IfTrue";
        /// <summary>
        /// Nazwa własności IfFalse; 
        /// </summary>
        public const string PROPERTYNAME_IFFALSE = "IfFalse";
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
                return condition;
            }
            set
            {
                condition = value;
            }
        }
        private IPhpValue condition;
        /// <summary>
        /// 
        /// </summary>
        public IPhpStatement IfTrue
        {
            get
            {
                return ifTrue;
            }
            set
            {
                value = PhpCodeBlock.Reduce(value);
                ifTrue = value;
            }
        }
        private IPhpStatement ifTrue;
        /// <summary>
        /// 
        /// </summary>
        public IPhpStatement IfFalse
        {
            get
            {
                return ifFalse;
            }
            set
            {
                value = PhpCodeBlock.Reduce(value);
                ifFalse = value;
            }
        }
        private IPhpStatement ifFalse;
        #endregion Properties
    }
}
