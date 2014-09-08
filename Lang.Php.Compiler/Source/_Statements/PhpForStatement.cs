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
    
    property InitVariables PhpAssignExpression[] 
    
    property Condition IPhpValue 
    
    property Statement IPhpStatement 
    
    property Incrementors IPhpStatement[] 
    smartClassEnd
    */

    public partial class PhpForStatement : IPhpStatementBase
    {
		#region Static Methods 

		// Private Methods 

        private static string Collect(PhpSourceCodeEmiter emiter, PhpEmitStyle style, IPhpStatement[] collection)
        {
            List<string> list = new List<string>();
            var xStyle = PhpEmitStyle.xClone(style);
            xStyle.AsIncrementor = true;
            foreach (var item in collection)
            {
                PhpSourceCodeWriter writer = new PhpSourceCodeWriter();
                writer.Clear();
                item.Emit(emiter, writer, xStyle);
                list.Add(writer.GetCode(true).Trim());
            }
            return string.Join(", ", list);
        }

        private static string Collect(PhpSourceCodeEmiter emiter, PhpEmitStyle style, IPhpValue[] collection)
        {
            List<string> list = new List<string>();
            var xStyle = PhpEmitStyle.xClone(style);
            xStyle.AsIncrementor = true;
            foreach (var item in collection)
            {
                list.Add(item.GetPhpCode(xStyle));
            }
            return string.Join(", ", list);
        }

		#endregion Static Methods 

		#region Methods 

		// Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            var phpIncrementrors = Collect(emiter, style, incrementors);
            var phpInitVariables = Collect(emiter, style, initVariables);

            style = style ?? new PhpEmitStyle();

            string header =
                    style.Compression == EmitStyleCompression.Beauty
                    ? "for({0}; {1}; {2})"
                    : "for({0};{1};{2})";
            header = string.Format(header, phpInitVariables, condition.GetPhpCode(style), phpIncrementrors);


            EmitHeaderStatement(emiter, writer, style, header, statement);

            /*
            if (statement == null)
                text += "{}";
            if (style.Compression == EmitStyleCompression.NearCrypto)
                writer.Write(text);
            else
                writer.WriteLn(text);
            if (statement == null) return;
            {
                var iStyle = PhpEmitStyle.xClone(style, ShowBracketsEnum.IfManyItems);
                writer.IncIndent();
                statement.Emit(emiter, writer, iStyle);
                writer.DecIndent();
            }
             */
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = GetCodeRequests<PhpAssignExpression>(initVariables);
            var b = GetCodeRequests<IPhpStatement>(incrementors);
            var c = GetCodeRequests(condition, statement);
            return a.Union(b).Union(c);
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            PhpAssignExpression[] _initVariables = initVariables == null
                ? null
                : initVariables.Select(u => s.Simplify(u)).Cast<PhpAssignExpression>().ToArray();
            IPhpValue _condition = s.Simplify(condition);
            IPhpStatement _statement = s.Simplify(statement);
            IPhpStatement[] _incrementors = incrementors == null
                ? null
                : incrementors.Select(u => s.Simplify(u)).ToArray();
            var theSame = EqualCode(_condition, condition) && EqualCode(_statement, statement) && EqualCode_Array(_initVariables, initVariables) && EqualCode_Array(_incrementors, incrementors);
            if (theSame)
                return this;
            return new PhpForStatement(_initVariables, _condition, _statement, _incrementors);
        }

		#endregion Methods 
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-12 13:31
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpForStatement
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpForStatement()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##InitVariables## ##Condition## ##Statement## ##Incrementors##
        implement ToString InitVariables=##InitVariables##, Condition=##Condition##, Statement=##Statement##, Incrementors=##Incrementors##
        implement equals InitVariables, Condition, Statement, Incrementors
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="InitVariables"></param>
        /// <param name="Condition"></param>
        /// <param name="Statement"></param>
        /// <param name="Incrementors"></param>
        /// </summary>
        public PhpForStatement(PhpAssignExpression[] InitVariables, IPhpValue Condition, IPhpStatement Statement, IPhpStatement[] Incrementors)
        {
            this.InitVariables = InitVariables;
            this.Condition = Condition;
            this.Statement = Statement;
            this.Incrementors = Incrementors;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności InitVariables; 
        /// </summary>
        public const string PROPERTYNAME_INITVARIABLES = "InitVariables";
        /// <summary>
        /// Nazwa własności Condition; 
        /// </summary>
        public const string PROPERTYNAME_CONDITION = "Condition";
        /// <summary>
        /// Nazwa własności Statement; 
        /// </summary>
        public const string PROPERTYNAME_STATEMENT = "Statement";
        /// <summary>
        /// Nazwa własności Incrementors; 
        /// </summary>
        public const string PROPERTYNAME_INCREMENTORS = "Incrementors";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public PhpAssignExpression[] InitVariables
        {
            get
            {
                return initVariables;
            }
            set
            {
                initVariables = value;
            }
        }
        private PhpAssignExpression[] initVariables;
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
        /// <summary>
        /// 
        /// </summary>
        public IPhpStatement[] Incrementors
        {
            get
            {
                return incrementors;
            }
            set
            {
                incrementors = value;
            }
        }
        private IPhpStatement[] incrementors;
        #endregion Properties
    }
}
