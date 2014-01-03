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
    
    property Name PhpNamespace namespace name
    
    property Code PhpCodeBlock 
    	init #
    smartClassEnd
    */
    
    public partial class PhpNamespaceStatement : PhpSourceBase, IPhpStatement, ICodeRelated
    {
        #region Methods

        // Public Methods 

        public static bool IsRootNamespace(string name)
        {
            return string.IsNullOrEmpty(name) || name == PathUtil.WIN_SEP;
        }

        public void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            if (name.IsRoot)
                writer.OpenLn("namespace {");
            else
                writer.OpenLnF("namespace {0} {{", name);
            code.Emit(emiter, writer, style);
            writer.CloseLn("}");
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return code.GetCodeRequests();
        }

        public StatementEmitInfo GetStatementEmitInfo(PhpEmitStyle style)
        {
            return StatementEmitInfo.NormalSingleStatement;
        }

        public override string ToString()
        {
            if (name.IsRoot)
                return "Root Php namespace";
            return string.Format("Php namespace {0}", name);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-01-03 13:07
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpNamespaceStatement 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpNamespaceStatement()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##Code##
        implement ToString Name=##Name##, Code=##Code##
        implement equals Name, Code
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Name">namespace name</param>
        /// </summary>
        public PhpNamespaceStatement(PhpNamespace Name)
        {
            this.Name = Name;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności Name; namespace name
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności Code; 
        /// </summary>
        public const string PROPERTYNAME_CODE = "Code";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// namespace name
        /// </summary>
        public PhpNamespace Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private PhpNamespace name;
        /// <summary>
        /// 
        /// </summary>
        public PhpCodeBlock Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }
        private PhpCodeBlock code = new PhpCodeBlock();
        #endregion Properties

    }
}
