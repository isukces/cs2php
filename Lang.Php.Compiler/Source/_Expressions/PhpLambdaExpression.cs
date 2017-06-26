using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{


    /*
    smartClass
    option NoAdditionalFile
    implement Constructor MethodDefinition
    
    property MethodDefinition PhpMethodDefinition 
    smartClassEnd
    */

    public partial class PhpLambdaExpression : ICodeRelated, IPhpValue
    {
        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return methodDefinition.GetCodeRequests();
        }

        public string GetPhpCode(PhpEmitStyle style)
        {
            /*
             echo preg_replace_callback('~-([a-z])~', function ($match) {
    return strtoupper($match[1]);
}, 'hello-world');
// outputs helloWorld
             */
            var s = PhpEmitStyle.xClone(style);
            s.AsIncrementor = true;
            var e = new PhpSourceCodeEmiter();
            var wde = new PhpSourceCodeWriter();
            wde.Clear();
            methodDefinition.Emit(e, wde, s);
            var code = wde.GetCode(true).Trim();
            return code;
        }
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-16 10:15
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpLambdaExpression
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpLambdaExpression()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##MethodDefinition##
        implement ToString MethodDefinition=##MethodDefinition##
        implement equals MethodDefinition
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="MethodDefinition"></param>
        /// </summary>
        public PhpLambdaExpression(PhpMethodDefinition MethodDefinition)
        {
            this.MethodDefinition = MethodDefinition;
        }

        #endregion Constructors

        #region Constants
        /// <summary>
        /// Nazwa własności MethodDefinition; 
        /// </summary>
        public const string PROPERTYNAME_METHODDEFINITION = "MethodDefinition";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public PhpMethodDefinition MethodDefinition
        {
            get
            {
                return methodDefinition;
            }
            set
            {
                methodDefinition = value;
            }
        }
        private PhpMethodDefinition methodDefinition;
        #endregion Properties

    }
}
