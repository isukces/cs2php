using Lang.Cs.Compiler;
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
            List<IPhpValue> args = new List<IPhpValue>();
            args.Add(ctx.TranslateValue(src.TargetObject));
            foreach (var i in src.Arguments)
                args.Add(ctx.TranslateValue(i.MyValue));

            var a = new PhpMethodCallExpression("call_user_func", args.ToArray());
            return a;
        }

        private static DirectCallAttribute GetDirectCallAttribute(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");
            return methodInfo.GetCustomAttributes(true).OfType<DirectCallAttribute>().FirstOrDefault();
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public int getPriority()
        {
            return 999;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.IsDelegate)
                return _Delegate(ctx, src);

            if (src.MethodInfo.DeclaringType == typeof(Lang.Php.Html))
            {
                var tmp = new Lang__Php__Html__Methods().TranslateToPhp(ctx, src);
                if (tmp != null)
                    return tmp;
            }
            var principles = ctx.GetTranslationInfo();
            src = SubstituteByReplacerMethod(ctx, src);
            #region Konwersja atrybutami
            {
                IPhpValue value = Try_DirectCallAttribute(ctx, src);
                if (value != null)
                    return value;
                value = Try_UseExpressionAttribute(ctx, src);
                if (value != null)
                    return value;
            }
            #endregion

            ctx.GetTranslationInfo().CheckAccesibility(src);
            MethodTranslationInfo mti = MethodTranslationInfo.FromMethodInfo(src.MethodInfo);
            ClassTranslationInfo cti = principles.FindClassTranslationInfo(src.MethodInfo.DeclaringType);
            if (cti == null)
                throw new NotSupportedException();


            {

                if (cti == null)
                    cti = principles.GetTI(src.MethodInfo.DeclaringType);
                if (cti != null)
                {
                    //if (MethodUtils.IsExtensionMethod(src.MethodInfo))
                    //    throw new NotSupportedException();

                    var phpMethod = new PhpMethodCallExpression(mti.ScriptName);
                    if (src.MethodInfo.IsStatic)
                    {
                        phpMethod.ClassName = principles.GetPhpType(src.MethodInfo.DeclaringType, true);
                    }
                    phpMethod.TargetObject = ctx.TranslateValue(src.TargetObject);
                    CopyArguments(ctx, src.Arguments, phpMethod);
                    return phpMethod;
                }
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

        private static void CopyArguments(IExternalTranslationContext ctx, IEnumerable<FunctionArgument> srcParameters, PhpMethodCallExpression dstMethod)
        {
            foreach (FunctionArgument functionArgument in srcParameters)
            {
                var a = ctx.TranslateValue(functionArgument);
                var b = a as PhpMethodInvokeValue;
                if (b == null)
                    throw new NotImplementedException();
                dstMethod.Arguments.Add(b);
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
            BindingFlags flags = src.MethodInfo.IsStatic ? BindingFlags.Static : BindingFlags.Instance;
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
        /// <param name="src"></param>
        /// <param name="phpMethod"></param>
        /// <returns></returns>
        private IPhpValue Try_DirectCallAttribute(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            DirectCallAttribute directCallAttribute = GetDirectCallAttribute(src.MethodInfo);
            if (directCallAttribute == null)
                return null;
            return CreateExpressionFromDirectCallAttribute(ctx, directCallAttribute, src.TargetObject, src.Arguments);

        }


        /// <summary>
        /// Creates expression based on DirectCallAttribute
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="directCallAttribute"></param>
        /// <param name="targetObject"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static IPhpValue CreateExpressionFromDirectCallAttribute(IExternalTranslationContext ctx, DirectCallAttribute directCallAttribute, IValue targetObject, FunctionArgument[] arguments)
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
                if (ma[0] == DirectCallAttribute.THIS)
                {
                    if (targetObject == null)
                        throw new NotSupportedException("gray horse 2");
                    return ctx.TranslateValue(targetObject);
                }
                //  return phpMethod.Arguments[ma[0]].Expression
                else
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
            CopyArguments(ctx, arguments, phpMethod);
            #region Mapping
            if (directCallAttribute.HasMapping)
            {
                PhpMethodInvokeValue[] phpArguments = phpMethod.Arguments.ToArray();
                phpMethod.Arguments.Clear();
                foreach (var argNr in directCallAttribute.MapArray)
                {
                    if (argNr == DirectCallAttribute.THIS)
                    {
                        if (targetObject == null)
                            throw new NotSupportedException();
                        phpMethod.Arguments.Add(new PhpMethodInvokeValue(ctx.TranslateValue(targetObject)));
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
