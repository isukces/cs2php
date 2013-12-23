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
    
    property Expression IPhpValue 
    
    property Sections List<PhpSwitchSection> 
    	init #
    smartClassEnd
    */

    public partial class PhpSwitchStatement : IPhpStatementBase
    {
        #region Methods

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            writer.OpenLnF("switch ({0}) {{", expression.GetPhpCode(style));
            foreach (var sec in sections)
            {
                foreach (var l in sec.Labels)
                    writer.WriteLnF("{0}:", l.IsDefault ? "default" : l.Value.GetPhpCode(style));
                writer.IncIndent();
                sec.Statement.Emit(emiter, writer, style);
                writer.DecIndent();
            }
            writer.CloseLn("}");
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            List<ICodeRequest> result = new List<ICodeRequest>();
            if (expression != null)
                result.AddRange(expression.GetCodeRequests());
            foreach (var sec in sections)
                result.AddRange(sec.GetCodeRequests());
            return result;
        }

        public override IPhpStatement Simplify(IPhpSimplifier s)
        {
            var changed = false;
            var e1 = s.Simplify(expression);
            if (!EqualCode(e1, expression))
                changed = true;


            var s1 = new List<PhpSwitchSection>();
            foreach (var i in sections)
            {
                bool wasChanged;
                var n = i.Simplify(s, out wasChanged);
                if (wasChanged)
                    changed = true;
                s1.Add(n);
            }


            if (!changed)
                return this;
            return new PhpSwitchStatement()
            {
                Expression = e1,
                Sections = s1
            };
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-23 11:08
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpSwitchStatement
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpSwitchStatement()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Expression## ##Sections##
        implement ToString Expression=##Expression##, Sections=##Sections##
        implement equals Expression, Sections
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności Expression; 
        /// </summary>
        public const string PROPERTYNAME_EXPRESSION = "Expression";
        /// <summary>
        /// Nazwa własności Sections; 
        /// </summary>
        public const string PROPERTYNAME_SECTIONS = "Sections";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Expression
        {
            get
            {
                return expression;
            }
            set
            {
                expression = value;
            }
        }
        private IPhpValue expression;
        /// <summary>
        /// 
        /// </summary>
        public List<PhpSwitchSection> Sections
        {
            get
            {
                return sections;
            }
            set
            {
                sections = value;
            }
        }
        private List<PhpSwitchSection> sections = new List<PhpSwitchSection>();
        #endregion Properties
    }
}
