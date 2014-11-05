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
    implement Constructor Name
    
    property Name string Nazwa metody
    
    property Arguments List<PhpMethodArgument> 
    	init #
    
    property Statements List<IPhpStatement> 
    	init #
    
    property IsAnonymous bool 
    	read only string.IsNullOrEmpty(name)
    smartClassEnd
    */

    public partial class PhpMethodDefinition : ICodeRelated
    {
        #region Methods

        // Public Methods 

        public virtual void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter code, PhpEmitStyle style)
        {

            // public function addFunction($function, $namespace = '') 
            var accessModifiers = GetAccessModifiers();
            var param = Arguments == null ? "" : string.Join(", ", Arguments.Select(u => u.GetPhpCode(style)));
            code.OpenLnF("{0} function {1}({2}) {{", accessModifiers, Name, param);
            {
                var g = GetGlobals();
                if (!string.IsNullOrEmpty(g))
                    code.WriteLnF("global {0};", g);
            }
            foreach (var statement in Statements)
            {
                var g = PhpEmitStyle.xClone(style);
                g.Brackets = ShowBracketsEnum.Never;
                statement.Emit(emiter, code, g);
            }
            code.CloseLn("}");
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            var a = IPhpStatementBase.GetCodeRequests(arguments);
            var b = IPhpStatementBase.GetCodeRequests(statements);
            return a.Union(b);
        }
        // Protected Methods 

        protected virtual string GetAccessModifiers()
        {
            return "";
        }

        protected string GetGlobals()
        {
            var aa = GetCodeRequests()
                .OfType<GlobalVariableRequest>()
                .Where(i => !string.IsNullOrEmpty(i.VariableName))
                .Select(i => PhpVariableExpression.AddDollar(i.VariableName)).Distinct();
            var globals = string.Join(", ", aa);
            return globals;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-16 10:19
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpMethodDefinition
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpMethodDefinition()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##Arguments## ##Statements## ##IsAnonymous##
        implement ToString Name=##Name##, Arguments=##Arguments##, Statements=##Statements##, IsAnonymous=##IsAnonymous##
        implement equals Name, Arguments, Statements, IsAnonymous
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name">Nazwa metody</param>
        /// </summary>
        public PhpMethodDefinition(string Name)
        {
            this.Name = Name;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Name; Nazwa metody
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności Arguments; 
        /// </summary>
        public const string PROPERTYNAME_ARGUMENTS = "Arguments";
        /// <summary>
        /// Nazwa własności Statements; 
        /// </summary>
        public const string PROPERTYNAME_STATEMENTS = "Statements";
        /// <summary>
        /// Nazwa własności IsAnonymous; 
        /// </summary>
        public const string PROPERTYNAME_ISANONYMOUS = "IsAnonymous";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// Nazwa metody
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                name = value;
            }
        }
        private string name = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<PhpMethodArgument> Arguments
        {
            get
            {
                return arguments;
            }
            set
            {
                arguments = value;
            }
        }
        private List<PhpMethodArgument> arguments = new List<PhpMethodArgument>();
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
        /// <summary>
        /// Własność jest tylko do odczytu.
        /// </summary>
        public bool IsAnonymous
        {
            get
            {
                return string.IsNullOrEmpty(name);
            }
        }
        #endregion Properties
    }
}
