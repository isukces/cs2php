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
    implement Constructor VariableName, Kind
    
    property VariableName string 
    
    property Kind PhpVariableKind 
    smartClassEnd
    */

    public partial class PhpVariableExpression : IPhpValueBase
    {
        #region Static Methods

        // Public Methods 

        public static string AddDollar(string x, bool condition = true)
        {
            if (condition)
            {
                if (x.StartsWith("$"))
                    return x;
                return "$" + x;
            }
            else
            {
                if (x.StartsWith("$"))
                    return x.Substring(1);
                return x;
            }
        }

        public static PhpVariableExpression MakeGlobal(string name)
        {
            return new PhpVariableExpression(AddDollar(name), PhpVariableKind.Global);
        }

        public static PhpVariableExpression MakeLocal(string name, bool isFunctionArgument)
        {
            return new PhpVariableExpression(AddDollar(name), isFunctionArgument ? PhpVariableKind.LocalArgument : PhpVariableKind.Local);
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            if (kind == PhpVariableKind.Global)
                yield return new GlobalVariableRequest(variableName);
            else
            {
                var a = new LocalVariableRequest(variableName,
                    kind == PhpVariableKind.LocalArgument,
                    (newName) =>
                    {
                        this.VariableName = newName;
                    }
                    );
                yield return a;
            }
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return variableName;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-12 11:59
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpVariableExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpVariableExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##VariableName## ##Kind##
        implement ToString VariableName=##VariableName##, Kind=##Kind##
        implement equals VariableName, Kind
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="VariableName"></param>
        /// <param name="Kind"></param>
        /// </summary>
        public PhpVariableExpression(string VariableName, PhpVariableKind Kind)
        {
            this.VariableName = VariableName;
            this.Kind = Kind;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności VariableName; 
        /// </summary>
        public const string PROPERTYNAME_VARIABLENAME = "VariableName";
        /// <summary>
        /// Nazwa własności Kind; 
        /// </summary>
        public const string PROPERTYNAME_KIND = "Kind";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string VariableName
        {
            get
            {
                return variableName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                variableName = value;
            }
        }
        private string variableName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public PhpVariableKind Kind
        {
            get
            {
                return kind;
            }
            set
            {
                kind = value;
            }
        }
        private PhpVariableKind kind;
        #endregion Properties

    }
}
