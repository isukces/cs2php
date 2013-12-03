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
    
    property IsStatic bool 
    
    property Visibility Visibility 
    smartClassEnd
    */

    public partial class PhpClassMethodDefinition : PhpMethodDefinition, IClassMember
    {
        #region Constructors

        public PhpClassMethodDefinition(string Name)
            : base(Name)
        {
        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter code, PhpEmitStyle style)
        {
            Refactor();
            base.Emit(emiter, code, style);
        }

        public void Refactor()
        {
            // return;
            var groups = this.GetCodeRequests()
                  .OfType<LocalVariableRequest>()
                  .Where(i => !string.IsNullOrEmpty(i.VariableName))
                  .GroupBy(i => i.VariableName)
                  .Select(i => i.ToArray())
                 .ToArray();

            int ii = 0;
            foreach (var group in groups)
            {
                if (group.Where(i => i.IsArgument).Any())
                    continue;
                if (!group.First().VariableName.Contains("@"))
                    continue;
                string nn;
                if (ii < 26)
                    nn = ((char)(65 + ii)).ToString();
                else
                    nn = ((char)(65 + ii / 26)).ToString() + ((char)(65 + ii % 26)).ToString();

                nn = "$" + nn;
                nn += "___" + group.First().VariableName.Replace("$", "").Replace("@", "_");

                // po prostu
                nn = group.First().VariableName.Replace("@", "__");
                foreach (var item in group)
                    item.ChangeNameAction(nn);
                ii++;
            }



        }
        // Protected Methods 

        protected override string GetAccessModifiers()
        {
            return PhpSourceCodeEmiter.GetAccessModifiers(this);
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-03 16:23
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpClassMethodDefinition
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpClassMethod()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##IsStatic## ##Visibility##
        implement ToString IsStatic=##IsStatic##, Visibility=##Visibility##
        implement equals IsStatic, Visibility
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constants
        /// <summary>
        /// Nazwa własności IsStatic; 
        /// </summary>
        public const string PROPERTYNAME_ISSTATIC = "IsStatic";
        /// <summary>
        /// Nazwa własności Visibility; 
        /// </summary>
        public const string PROPERTYNAME_VISIBILITY = "Visibility";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool IsStatic
        {
            get
            {
                return isStatic;
            }
            set
            {
                isStatic = value;
            }
        }
        private bool isStatic;
        /// <summary>
        /// 
        /// </summary>
        public Visibility Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
            }
        }
        private Visibility visibility;
        #endregion Properties
    }
}
