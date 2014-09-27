using System;
using System.Collections.Generic;

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
                return x.StartsWith("$") ? x : "$" + x;
            return x.StartsWith("$") ? x.TrimStart('$') : x;
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
            if (_kind == PhpVariableKind.Global)
                yield return new GlobalVariableRequest(_variableName);
            else
            {
                var a = new LocalVariableRequest(_variableName,
                    _kind == PhpVariableKind.LocalArgument,
                    newName =>
                    {
                        VariableName = newName;
                    });
                yield return a;
            }
        }

        public override string GetPhpCode(PhpEmitStyle style)
        {
            return _variableName;
        }

        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-25 10:57
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="variableName"></param>
        /// <param name="kind"></param>
        /// </summary>
        public PhpVariableExpression(string variableName, PhpVariableKind kind)
        {
            VariableName = variableName;
            Kind = kind;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności VariableName; 
        /// </summary>
        public const string PropertyNameVariableName = "VariableName";
        /// <summary>
        /// Nazwa własności Kind; 
        /// </summary>
        public const string PropertyNameKind = "Kind";
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
                return _variableName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                _variableName = value;
            }
        }
        private string _variableName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public PhpVariableKind Kind
        {
            get
            {
                return _kind;
            }
            set
            {
                _kind = value;
            }
        }
        private PhpVariableKind _kind;
        #endregion Properties

    }
}
