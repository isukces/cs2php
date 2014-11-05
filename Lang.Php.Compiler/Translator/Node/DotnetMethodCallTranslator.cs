using System.Diagnostics;
using Lang.Cs.Compiler;
using Lang.Cs.Compiler.Sandbox;
using Lang.Php.Compiler.Source;
using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Lang.Php.Compiler.Translator.Node
{
    class DotnetMethodCallTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        #region Static Methods

        // Private Methods 

        private static IPhpValue _Delegate(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.TargetObject == null)
                throw new NotSupportedException();
            var args = new List<IPhpValue> { ctx.TranslateValue(src.TargetObject) };
            foreach (var i in src.Arguments)
                args.Add(ctx.TranslateValue(i.MyValue));

            var a = new PhpMethodCallExpression("call_user_func", args.ToArray());
            return a;
        }



        #endregion Static Methods

        #region Methods

        // Public Methods 

        public int GetPriority()
        {
            return 999;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.IsDelegate)
                return _Delegate(ctx, src);

            if (src.MethodInfo.DeclaringType == typeof(Html))
            {
                var tmp = new Lang__Php__Html__Methods().TranslateToPhp(ctx, src);
                if (tmp != null)
                    return tmp;
            }
            var principles = ctx.GetTranslationInfo();
            src = SubstituteByReplacerMethod(ctx, src);
            #region Konwersja atrybutami
            {
                var value = Try_DirectCallAttribute(ctx, src);
                if (value != null)
                    return value;
                value = Try_UseExpressionAttribute(ctx, src);
                if (value != null)
                    return value;
            }
            #endregion

            ctx.GetTranslationInfo().CheckAccesibility(src);
            var cti = principles.FindClassTranslationInfo(src.MethodInfo.DeclaringType);
            if (cti == null)
                throw new NotSupportedException();
            var mti = principles.GetOrMakeTranslationInfo(src.MethodInfo);
            {
                var phpMethod = new PhpMethodCallExpression(mti.ScriptName);
                if (src.MethodInfo.IsStatic)
                {
                    var a = principles.GetPhpType(src.MethodInfo.DeclaringType, true, principles.CurrentType);
                    phpMethod.SetClassName(a, mti);
                }
                phpMethod.TargetObject = ctx.TranslateValue(src.TargetObject);
                CopyArguments(ctx, src.Arguments, phpMethod, null);

                if (cti.DontIncludeModuleForClassMembers)
                    phpMethod.DontIncludeClass = true;
                return phpMethod;
            }

            throw new Exception(string.Format("bright cow, {0}", src.MethodInfo.DeclaringType.FullName));
        }

        private static IPhpValue Try_UseExpressionAttribute(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var ats = src.MethodInfo.GetCustomAttribute<UseBinaryExpressionAttribute>(true);
            if (ats == null)
                return null;
            IPhpValue l, r;
            var re = new Regex("^\\s*\\$(\\d+)\\s*$");
            var m = re.Match(ats.Left);
            if (m.Success)
                l = ctx.TranslateValue(src.Arguments[int.Parse(m.Groups[1].Value)].MyValue);
            else
                l = PhpValueTranslator.GetValueForExpression(null, ats.Left);

            m = re.Match(ats.Right);
            if (m.Success)
                r = ctx.TranslateValue(src.Arguments[int.Parse(m.Groups[1].Value)].MyValue);
            else
                r = PhpValueTranslator.GetValueForExpression(null, ats.Right);
            var method = new PhpBinaryOperatorExpression(ats.Operator, l, r);
            return method;
        }
        // Private Methods 

        private static void CopyArguments(IExternalTranslationContext ctx, IEnumerable<FunctionArgument> srcParameters, PhpMethodCallExpression dstMethod, List<int> skipRefIndexList)
        {
            var parameterIdx = 0;
            foreach (var functionArgument in srcParameters)
            {
                var a = ctx.TranslateValue(functionArgument);
                var b = a as PhpMethodInvokeValue;
                if (b == null)
                    throw new NotImplementedException();
                if (b.Expression is PhpParenthesizedExpression)
                    Debug.Write("");
                if (skipRefIndexList != null && skipRefIndexList.Contains(parameterIdx))
                    b.ByRef = false;
                dstMethod.Arguments.Add(b);
                parameterIdx++;
            }
        }

        /// <summary>
        /// Jeśli dla klasy deklarującej metodę jest znana klasa 'replacera' to podmieniam wywołanie metody na wywołanie metody z klasy replacera jeden do jeden
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private CsharpMethodCallExpression SubstituteByReplacerMethod(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var classReplacer = ctx.FindOneClassReplacer(src.MethodInfo.DeclaringType);
            if (classReplacer == null)
                return src;
            var otherClass = classReplacer.ReplaceBy;
            var flags = src.MethodInfo.IsStatic ? BindingFlags.Static : BindingFlags.Instance;
            var search = src.MethodInfo.ToString();
            var found = otherClass.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | flags).Where(i => i.ToString() == search).FirstOrDefault();
            if (found == null)

                throw new Exception(string.Format("Klasa {0} nie zawiera metody lustrzanej {1}\r\nDodaj\r\n{2}", otherClass, search, src.MethodInfo.GetMethodHeader()));
            src = new CsharpMethodCallExpression(found, src.TargetObject, src.Arguments, src.GenericTypes, false);
            return src;
        }

        /// <summary>
        /// Jeśli metoda .NET ma zdefiniowany argument <see cref="DirectCallAttribute">DirectCall</see> to tworzy odpowiednie wywołanie funkcji PHP
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        private static IPhpValue Try_DirectCallAttribute(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var directCallAttribute = src.MethodInfo.GetDirectCallAttribute();
            if (directCallAttribute == null)
                return null;
            return CreateExpressionFromDirectCallAttribute(ctx, directCallAttribute, src.TargetObject, src.Arguments, src.MethodInfo);

        }


        /// <summary>
        /// Creates expression based on DirectCallAttribute
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="directCallAttribute"></param>
        /// <param name="targetObject"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static IPhpValue CreateExpressionFromDirectCallAttribute(IExternalTranslationContext ctx, DirectCallAttribute directCallAttribute, IValue targetObject, FunctionArgument[] arguments, MethodBase methodInfo)
        {

            if (directCallAttribute.CallType == MethodCallStyles.Static)
                throw new NotSupportedException();
            if (directCallAttribute.MemberToCall != ClassMembers.Method)
                throw new NotSupportedException();

            if (string.IsNullOrEmpty(directCallAttribute.Name))
            {
                var ma = directCallAttribute.MapArray;
                if (ma.Length != 1)
                    throw new NotSupportedException("gray horse 1");
                if (ma[0] == DirectCallAttribute.This)
                {
                    if (targetObject == null)
                        throw new NotSupportedException("gray horse 2");
                    return ctx.TranslateValue(targetObject);
                }
                //  return phpMethod.Arguments[ma[0]].Expression
                return ctx.TranslateValue(arguments[ma[0]].MyValue);
            }
            var name = directCallAttribute.Name;


            var phpMethod = new PhpMethodCallExpression(name);
            if (directCallAttribute.CallType == MethodCallStyles.Instance)
            {
                if (targetObject == null)
                    throw new NotSupportedException("gray horse 3");
                phpMethod.TargetObject = ctx.TranslateValue(targetObject);
            }

            {
                List<int> skipRefIndexList = null;
                if (methodInfo != null)
                {
                    var skipRefOrOutArray = directCallAttribute.SkipRefOrOutArray;
                    if (skipRefOrOutArray.Any())
                    {
                        var parameters = methodInfo.GetParameters();
                        for (var index = 0; index < parameters.Length; index++)
                        {
                            if (!skipRefOrOutArray.Contains(parameters[index].Name)) continue;
                            if (skipRefIndexList == null)
                                skipRefIndexList = new List<int>();
                            skipRefIndexList.Add(index);
                        }
                    }
                }
                CopyArguments(ctx, arguments, phpMethod, skipRefIndexList);
            }

            #region Mapping
            if (directCallAttribute.HasMapping)
            {
                var phpArguments = phpMethod.Arguments.ToArray();
                phpMethod.Arguments.Clear();
                foreach (var argNr in directCallAttribute.MapArray)
                {
                    if (argNr == DirectCallAttribute.This)
                    {
                        if (targetObject == null)
                            throw new NotSupportedException();
                        var v = ctx.TranslateValue(targetObject);
                        phpMethod.Arguments.Add(new PhpMethodInvokeValue(v));
                    }
                    else
                    {
                        if (argNr < phpArguments.Length)
                            phpMethod.Arguments.Add(phpArguments[argNr]);
                    }
                }

            }
            #endregion
            #region Out

            if (directCallAttribute.OutNr >= 0)
            {
                var nr = directCallAttribute.OutNr;
                var movedExpression = phpMethod.Arguments[nr].Expression;
                phpMethod.Arguments.RemoveAt(nr);
                var a = new PhpAssignExpression(movedExpression, phpMethod);
                return a;
            }

            #endregion
            return phpMethod;
        }

        #endregion Methods
    }
}
