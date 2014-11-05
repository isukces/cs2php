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
    
    property Statements List<IPhpStatement> 
    	init #
    smartClassEnd
    */

    public partial class PhpCodeBlock : PhpSourceBase, IPhpStatement, ICodeRelated
    {
        #region Constructors


        public static PhpCodeBlock Bound(IPhpStatement s)
        {
            if (s is PhpCodeBlock)
                return s as PhpCodeBlock;
            var g = new PhpCodeBlock();
            g.statements.Add(s);
            return g;
        }
        public List<IPhpStatement> GetPlain()
        {
            var o = new List<IPhpStatement>();
            if (statements == null || statements.Count == 0)
                return o;
            foreach (var i in statements)
            {
                if (i is PhpCodeBlock)
                    o.AddRange((i as PhpCodeBlock).GetPlain());
                else
                    o.Add(i);
            }
            return o;
        }
        public PhpCodeBlock(IPhpStatement other)
        {
            statements.Add(other);
        }

        public PhpCodeBlock()
        {

        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 

        public static bool HasAny(IPhpStatement x)
        {
            if (x == null) return false;
            if (x is PhpCodeBlock)
            {
                var src = x as PhpCodeBlock;
                if (src.statements.Count == 0) return false;
                if (src.statements.Count > 1) return true;
                return HasAny(src.statements[0]);
            }
            return true;
        }

        public static IPhpStatement Reduce(IPhpStatement x)
        {
            if (x == null) return x;
            if (x is PhpCodeBlock)
            {
                var src = x as PhpCodeBlock;
                if (src.statements.Count == 0) return null;
                if (src.statements.Count > 1) return x;
                return Reduce(src.statements[0]);
            }
            return x;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            if (statements.Count == 0)
                return;
            var bracketStyle = style == null ? ShowBracketsEnum.IfManyItems : style.Brackets;
            var brack =
                bracketStyle == ShowBracketsEnum.Never
                ? false
                : bracketStyle == ShowBracketsEnum.Always
                ? true
                : statements == null || !(statements.Count == 1);

            if (statements != null && statements.Count == 1 && bracketStyle == ShowBracketsEnum.IfManyItems_OR_IfStatement)
                if (statements[0] is PhpIfStatement)
                    brack = true;


            var iStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.Never);
            if (!brack && bracketStyle != ShowBracketsEnum.Never && statements.Count == 1)
            {

                var tmp = statements[0];
                var gf = tmp.GetStatementEmitInfo(iStyle);
                if (gf != StatementEmitInfo.NormalSingleStatement)
                    brack = true;

            }
            if (brack)
                writer.OpenLn("{");
            foreach (var i in statements)
                i.Emit(emiter, writer, iStyle);
            if (brack)
                writer.CloseLn("}");
            return;
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (statements.Count == 0)
                return new ICodeRequest[0];
            var r = new List<ICodeRequest>();
            foreach (var i in statements)
                r.AddRange(i.GetCodeRequests());
            return r;
        }

        #endregion Methods


        public StatementEmitInfo GetStatementEmitInfo(PhpEmitStyle style)
        {
            return StatementEmitInfo.NormalSingleStatement; // sam troszczę się o swoje nawiasy
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-11 17:53
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpCodeBlock
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpCodeBlock()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Statements##
        implement ToString Statements=##Statements##
        implement equals Statements
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności Statements; 
        /// </summary>
        public const string PROPERTYNAME_STATEMENTS = "Statements";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public List<IPhpStatement> Statements
        {
            get
            {
                return statements;
            }
            set
            {
                statements = value;
            }
        }
        private List<IPhpStatement> statements = new List<IPhpStatement>();
        #endregion Properties
    }
}
