using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lang.Php.Compiler.Translator.Node
{
    public class MysqliTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>,
        IPhpNodeTranslator<CsharpInstancePropertyAccessExpression>
    {
        #region Methods

        // Public Methods 

        public int GetPriority()
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
            if (src.MethodInfo.DeclaringType == typeof(MySQLiStatement))
            {
                if (src.MethodInfo.Name == "BindParams") // all BindParams methods
                {
                    #region BindParams
                    var a = src.MethodInfo.GetParameters();
                    var phptypes = new StringBuilder(a.Length);
                    foreach (var i in a)
                    {
                        var j = i.ParameterType;
                        var t = j.GetGenericArguments()[0];
                        if (t == typeof(string))
                            phptypes.Append("s");
                        else if (t == typeof(Double)
                                || t == typeof(Single)
                                || t == typeof(Decimal))
                            phptypes.Append("d");
                        else if (t == typeof(Int64)
                                || t == typeof(Int32)
                                || t == typeof(Byte)
                                || t == typeof(UInt64)
                                || t == typeof(UInt32)
                                || t == typeof(UInt16))
                            phptypes.Append("i");
                        else if (t == typeof(byte[]))
                            phptypes.Append("b");
                        else throw new NotSupportedException(string.Format("Type {0} is not supported by MySQLiStatement.BindParams", t));
                    }
                    var values = new List<IPhpValue>();
                    values.Add(new PhpConstValue(phptypes.ToString()));
                    foreach (var i in src.Arguments)
                        values.Add(ctx.TranslateValue(i.MyValue));
                    var result = new PhpMethodCallExpression("bind_param", values.ToArray());
                    result.TargetObject = ctx.TranslateValue(src.TargetObject);
                    return result; 
                    #endregion
                }
                if (src.MethodInfo.Name == "BindResult") // all BindResult methods
                {
                    #region BindResult
                    var a = src.MethodInfo.GetParameters();                     
                    var values = new List<IPhpValue>();
                    foreach (var i in src.Arguments)
                        values.Add(ctx.TranslateValue(i.MyValue));
                    var result = new PhpMethodCallExpression("bind_result", values.ToArray());
                    result.TargetObject = ctx.TranslateValue(src.TargetObject);
                    return result;
                    #endregion
                }
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
