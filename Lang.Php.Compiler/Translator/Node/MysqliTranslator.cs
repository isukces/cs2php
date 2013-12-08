using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    public class MysqliTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>,
        IPhpNodeTranslator<CsharpInstancePropertyAccessExpression>
    {
		#region Methods 

		// Public Methods 

        public int getPriority()
        {
            return 1;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType == typeof(MySQLiResult))
            {
                var fn = src.MethodInfo.ToString();
                if (src.MethodInfo.IsGenericMethod)
                    fn = src.MethodInfo.GetGenericMethodDefinition().ToString();
                if (fn == "Boolean Fetch[T](T ByRef)")
                {
                    // was  "T Fetch[T]()"
                    var typeArgs = src.MethodInfo.GetGenericArguments();
                    var genTypeInfo = ctx.GetTranslationInfo().GetOrMakeTranslationInfo(typeArgs[0]);
                    if (genTypeInfo.IsArray)
                    {
                        var arg = ctx.TranslateValue(src.Arguments[0].MyValue);
                        var fetchMethod = new PhpMethodCallExpression("fetch_assoc");
                        fetchMethod.TargetObject = ctx.TranslateValue(src.TargetObject);
                        var assoc = new PhpAssignExpression(arg, fetchMethod);
                        return assoc;
                    }
                    else
                    {
                        throw new NotSupportedException(fn);
                    }
                    // Console.WriteLine(c);
                }
                // throw new NotSupportedException(fn);
            }
            return null;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpInstancePropertyAccessExpression src)
        {
            if (src.Member.DeclaringType == typeof(MySQLi))
            {
                var n = src.Member.Name;
                if (n == "WasConnectionError" || n == "WasSuccessfulConnection")
                {
                    // according to error http://www.php.net/manual/en/mysqli.construct.php
                    IPhpValue getError;
                    if (ctx.PhpVersion <= PhpVersions.PHP_5_3_0)
                        getError = new PhpMethodCallExpression("mysqli_connect_error");
                    else
                        getError = new PhpInstanceFieldAccessExpression("connect_error", ctx.TranslateValue(src.TargetObject), null);
                    var checkEmpty = new PhpMethodCallExpression("empty", getError);
                    if (n == "WasSuccessfulConnection")
                        return checkEmpty;
                    var checkNotEmpty = new PhpUnaryOperatorExpression(checkEmpty, "!");
                    return checkNotEmpty;

                }
            }
            return null;
        }

		#endregion Methods 
    }
}
