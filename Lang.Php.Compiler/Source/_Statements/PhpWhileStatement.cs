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
    
    property Statement IPhpStatement 
    smartClassEnd
    */

    public partial class PhpWhileStatement : IPhpStatementBase
    {
        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var __condition = s.Simplify(condition);
            var __statement = s.Simplify(statement);
            if (__condition == condition && __statement == statement)
                return this;
            return new PhpWhileStatement(__condition, __statement);
        }
        #region Methods

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            style = style ?? new PhpEmitStyle();
            string header = string.Format("while({0})", condition.GetPhpCode(style));
            EmitHeaderStatement(emiter, writer, style, header, statement);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return GetCodeRequests(condition, statement);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-07 18:04
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpWhileStatement
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpWhileStatement()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Condition## ##Statement##
        implement ToString Condition=##Condition##, Statement=##Statement##
        implement equals Condition, Statement
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Condition"></param>
        /// <param name="Statement"></param>
        /// </summary>
        public PhpWhileStatement(IPhpValue Condition, IPhpStatement Statement)
        {
            this.Condition = Condition;
            this.Statement = Statement;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Condition; 
        /// </summary>
        public const string PROPERTYNAME_CONDITION = "Condition";
        /// <summary>
        /// Nazwa własności Statement; 
        /// </summary>
        public const string PROPERTYNAME_STATEMENT = "Statement";
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
        public IPhpStatement Statement
        {
            get
            {
                return statement;
            }
            set
            {
                statement = value;
            }
        }
        private IPhpStatement statement;
        #endregion Properties
    }
}
