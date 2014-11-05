using System.Collections.Generic;

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
            var newCondition = s.Simplify(_condition);
            var newStatement = s.Simplify(_statement);
            if (newCondition == _condition && newStatement == _statement)
                return this;
            return new PhpWhileStatement(newCondition, newStatement);
        }
        #region Methods

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            style = style ?? new PhpEmitStyle();
            var header = string.Format("while({0})", _condition.GetPhpCode(style));
            EmitHeaderStatement(emiter, writer, style, header, _statement);
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return GetCodeRequests(_condition, _statement);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-08 14:15
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="condition"></param>
        /// <param name="statement"></param>
        /// </summary>
        public PhpWhileStatement(IPhpValue condition, IPhpStatement statement)
        {
            Condition = condition;
            Statement = statement;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Condition; 
        /// </summary>
        public const string PropertyNameCondition = "Condition";
        /// <summary>
        /// Nazwa własności Statement; 
        /// </summary>
        public const string PropertyNameStatement = "Statement";
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
        public IPhpStatement Statement
        {
            get
            {
                return _statement;
            }
            set
            {
                _statement = value;
            }
        }
        private IPhpStatement _statement;
        #endregion Properties

    }
}
