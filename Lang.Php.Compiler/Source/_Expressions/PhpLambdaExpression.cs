using System.Collections.Generic;

namespace Lang.Php.Compiler.Source
{
    public class PhpLambdaExpression : ICodeRelated, IPhpValue
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="methodDefinition"></param>
        /// </summary>
        public PhpLambdaExpression(PhpMethodDefinition methodDefinition)
        {
            MethodDefinition = methodDefinition;
        }

        public IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return MethodDefinition.GetCodeRequests();
        }

        public string GetPhpCode(PhpEmitStyle style)
        {
            /*
             echo preg_replace_callback('~-([a-z])~', function ($match) {
    return strtoupper($match[1]);
}, 'hello-world');
// outputs helloWorld
             */
            var s           = PhpEmitStyle.xClone(style);
            s.AsIncrementor = true;
            var e           = new PhpSourceCodeEmiter();
            var wde         = new PhpSourceCodeWriter();
            wde.Clear();
            MethodDefinition.Emit(e, wde, s);
            var code = wde.GetCode(true).Trim();
            return code;
        }

        /// <summary>
        /// </summary>
        public PhpMethodDefinition MethodDefinition { get; set; }
    }
}