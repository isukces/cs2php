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
    
    property Labels PhpSwitchLabel[] 
    
    property Statement IPhpStatement 
    smartClassEnd
    */

    public partial class PhpSwitchSection : ICodeRelated
    {
        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            List<ICodeRequest> result = new List<ICodeRequest>();
            if (labels != null)
                foreach (var _label in labels)
                    result.AddRange(_label.GetCodeRequests());
            if (statement != null)
                result.AddRange(statement.GetCodeRequests());
            return result;
        }

        public PhpSwitchSection Simplify(IPhpSimplifier s, out bool wasChanged)
        {
            wasChanged = false;
            List<PhpSwitchLabel> nLabels = new List<PhpSwitchLabel>();
            foreach (var lab in labels)
            {
                bool labelWasChanged;
                nLabels.Add(lab.Simplify(s, out labelWasChanged));
                if (labelWasChanged) wasChanged = true;
            }
            var nStatement = s.Simplify(statement);
            if (!PhpSourceBase.EqualCode(nStatement, statement))
                wasChanged = true;
            if (!wasChanged)
                return this;
            return new PhpSwitchSection()
            {
                labels = nLabels.ToArray(),
                statement = nStatement
            };

        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-12-23 11:07
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpSwitchSection
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpSwitchSection()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Labels## ##Statement##
        implement ToString Labels=##Labels##, Statement=##Statement##
        implement equals Labels, Statement
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Labels; 
        /// </summary>
        public const string PROPERTYNAME_LABELS = "Labels";
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
        public PhpSwitchLabel[] Labels
        {
            get
            {
                return labels;
            }
            set
            {
                labels = value;
            }
        }
        private PhpSwitchLabel[] labels;
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
