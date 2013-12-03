using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Php;
using Lang.Php.Compiler;
using Lang.Cs.Compiler;
using System.Reflection;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Wp.Compile
{
    class ClassFieldAccessExpressionConverter : IPhpNodeTranslator<ClassFieldAccessExpression>
    {

        public int getPriority()
        {
            return 1;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, ClassFieldAccessExpression src)
        {
            var t = src.Member.DeclaringType;
            string n = src.Member.Name;
            if (t == typeof(Wp))
            {
                if (n == "DoingAutosave")
                {
                    //  if ( defined( 'DOING_AUTOSAVE' ) && DOING_AUTOSAVE )
                    var a1 = new PhpMethodCallExpression("defined", new PhpConstValue("DOING_AUTOSAVE"));
                    var a2 = new PhpDefinedConstExpression("DOING_AUTOSAVE", null);
                    var a3 = new PhpBinaryOperatorExpression("&&", a1, a2);
                    var a4 = new PhpParenthesizedExpression(a3);
                    return a4;
                }
                if (n == "HookSuffix")
                {
                    var a1 = new PhpVariableExpression("hook_suffix", PhpVariableKind.Global);
                    return a1;
                }
            }
            return null;
        }
    }
}
