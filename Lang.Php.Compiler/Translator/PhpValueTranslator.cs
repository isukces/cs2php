using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php;
using Lang.Php.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator
{
    class PhpValueTranslator : CSharpBaseVisitor<IPhpValue>
    {
        #region Constructors

        public PhpValueTranslator(TranslationState state)
        {
            this.state = state;
        }

        #endregion Constructors

        #region Static Methods

        // Public Methods 

        public static IPhpValue GetValueForExpression(IPhpValue phpTargetObject, string valueAsString)
        {
            valueAsString = (valueAsString ?? "").Trim();
            if (valueAsString.ToLower() == "this")
                return phpTargetObject;
            if (valueAsString == "false")
                return new PhpConstValue(false);
            if (valueAsString == "true")
                return new PhpConstValue(true);
            int i;
            if (int.TryParse(valueAsString, out i))
                return new PhpConstValue(i);
            double d;

            if (double.TryParse(valueAsString, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
                return new PhpConstValue(d);
            {
                string x;
                if (PhpValues.TryGetPhpStringValue(valueAsString, out x))
                    return new PhpConstValue(x);
            }
            throw new Exception(string.Format("bald boa, Unable to convert {0} into php value", valueAsString));
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public IPhpValue ConvertValueToPredefined(object o)
        {
            if (o is double)
            {
                if ((double)o == Math.PI)
                    return new PhpMethodCallExpression("pi");
            }
            return new PhpConstValue(o);
        }

        public void CopyArguments(IEnumerable<FunctionArgument> srcParameters, PhpMethodCallExpression dstMethod)
        {
            foreach (FunctionArgument functionArgument in srcParameters)
                dstMethod.Arguments.Add(VisitFunctionArgument(functionArgument) as PhpMethodInvokeValue);
        }

        public PhpMethodInvokeValue TransFunctionArgument(FunctionArgument a)
        {
            var r = new PhpMethodInvokeValue(TransValue(a.MyValue));
            return r;
        }

        public IPhpValue TransValue(IValue value)
        {
            if (value == null)
                return null;
            if (value is CSharpBase)
                return Visit(value as CSharpBase);
            throw new NotSupportedException();
        }
        // Protected Methods 

        protected override IPhpValue VisitArgumentExpression(ArgumentExpression src)
        {
            return PhpVariableExpression.MakeLocal(src.Name, true);
        }

        protected override IPhpValue VisitArrayCreateExpression(ArrayCreateExpression src)
        {
            var a = new PhpArrayCreateExpression();
            if (src.Initializers != null && src.Initializers.Any())
                a.Initializers = src.Initializers.Select(i => TransValue(i)).ToArray();
            return SimplifyPhpExpression(a);
        }

        protected override IPhpValue VisitAssignExpression(CsharpAssignExpression src)
        {
            var l = TransValue(src.Left);
            var r = TransValue(src.Right);
            string op = src.OptionalOperator;
            if (op == "+")
            {
                var vt = (src as IValue).ValueType;
                if (vt == typeof(string))
                    op = ".";

            }
            var a = new PhpAssignExpression(l, r, op);
            return SimplifyPhpExpression(a);
        }

        protected override IPhpValue VisitBinaryOperatorExpression(BinaryOperatorExpression src)
        {
            var t1 = src.Left.ValueType;
            var t2 = src.Right.ValueType;
            //if (t1 == null && src.Left is PhpConstValue && (src.Left as PhpConstValue).Value == null)
            //    t1 = typeof(object);

            //if (t2 == null && src.Right is PhpConstValue && (src.Right as PhpConstValue).Value == null)
            //    t2 = typeof(object);

            if (src.OperatorMethod != null)
            {
                if (!src.OperatorMethod.IsStatic)
                    throw new NotSupportedException();

                var a = new CsharpMethodCallExpression(
                    src.OperatorMethod, null,
                    new FunctionArgument[] { new FunctionArgument("", src.Left), new FunctionArgument("", src.Right) },
                    new Type[0], false);
                var trans = TransValue(a);
                return trans;

            }
            var l = TransValue(src.Left);
            if (src.Operator == "as")
            {
                return l;
            }


            var r = TransValue(src.Right);
            if (t1 == typeof(string) || t2 == typeof(string))
            {
                if (src.Operator == "+")
                {
                    if (l is PhpConstValue && r is PhpConstValue)
                    {
                        var s1 = (l as PhpConstValue).Value as string;
                        var s2 = (r as PhpConstValue).Value as string;
                        return new PhpConstValue(s1 + s2);
                    }
                    return new PhpBinaryOperatorExpression(".", l, r);
                }
            }
            //if (src.Operator == "==")
            //{
            //    var operators = MethodUtils.GetOperators("op_Equality", t1, t2).Where(i => i.GetParameters().Length == 2).ToArray();
            //    var matched = MethodUtils.TryMatch(operators, new Type[] { t1, t2 });
            //    if (matched != null)
            //    {
            //        var attribute = matched.GetCustomAttribute<UseOperatorAttribute>();
            //        if (attribute != null)
            //            return new PhpBinaryOperatorExpression(attribute.Operator, l, r);
            //    }
            //}
            return new PhpBinaryOperatorExpression(src.Operator, l, r);
        }

        protected override IPhpValue VisitCallConstructor(CallConstructor src)
        {
            var tmp = state.Principles.NodeTranslators.Translate(state, src);
            if (tmp != null)
                return SimplifyPhpExpression(tmp);



            var r = new PhpMethodCallExpression("*");
            if (src.Info.ReflectedType != src.Info.DeclaringType)
                throw new NotSupportedException();

            r.ClassName = state.Principles.GetPhpType(src.Info.ReflectedType, true);

            var cccc = state.Principles.GetTI(src.Info.ReflectedType);
            if (cccc.IsArray)
            {
                if (src.Initializers != null && src.Initializers.Any())
                {
                    var ggg = src.Initializers.Select(i => TransValue(i)).ToArray();
                    var h = new PhpArrayCreateExpression(ggg);
                    return SimplifyPhpExpression(h);
                }
                else
                {
                    var h = new PhpArrayCreateExpression();
                    return SimplifyPhpExpression(h);
                    throw new NotImplementedException();
                }

            }
            {
                var cti = state.Principles.GetTI(src.Info.ReflectedType);
                if (cti.IsReflected)
                {
                    var replacer = state.FindOneClassReplacer(src.Info.ReflectedType);
                    if (replacer != null)
                    {
                        var translationMethods = replacer.ReplaceBy.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(m => m.IsDefined(typeof(TranslatorAttribute))).ToArray();
                        foreach (var m in translationMethods)
                        {
                            var translated = m.Invoke(null, new object[] { state, src });
                            if (translated is IPhpValue)
                                return translated as IPhpValue;
                        }

                        throw new Exception(string.Format("Klasa {0} nie umie przetłumaczyć konstruktora {1}", replacer.ReplaceBy.FullName, replacer.SourceType.FullName));
                    }
                    else
                    {
#if sprawdzac_includy 
                        // tutaj trzeba dorobić jakieś sprawdzanie klas WP
                        var a = src.Info.ReflectedType.Assembly;
                        var ati = AssemblyTranslationInfo.FromAssembly(a);
                        if (!string.IsNullOrEmpty(ati.IncludePathConstOrVarName))
                        {
                            // klasa jest w innym dołączanym module, więc OK
                        }
                        else
                            throw new Exception(""); 
#endif
                    }
                }

            }
            foreach (var functionArgument in src.Arguments)
                r.Arguments.Add(TransFunctionArgument(functionArgument));
            return r;
        }

        protected override IPhpValue VisitCastExpression(CastExpression src)
        {
            var a = TransValue(src.Expression);
            return SimplifyPhpExpression(a);
        }

        protected override IPhpValue VisitClassFieldAccessExpression(ClassFieldAccessExpression src)
        {
            var tmp = state.Principles.NodeTranslators.Translate(state, src);
            if (tmp != null)
                return SimplifyPhpExpression(tmp);

            bool isStatic = src.IsStatic;
            FieldInfo member = src.Member;
            var member_name = member.Name;
            var member_declaring_type = member.DeclaringType;

            {
                FieldTranslationInfo tInfo = state.Principles.GetOrMakeTranslationInfo(src.Member);
                if (tInfo.IsDefinedInNonincludableModule)
                {
                    var a = state.Principles.CurrentType;
                    var b = state.Principles.GetTI(this.state.Principles.CurrentType);
                    if (tInfo.IncludeModule != b.ModuleName)
                        throw new Exception(
                             string.Format(
                                 "Unable to reference to field {1}.{0} from {2}.{3}. Containing module is page and cannot be included.",
                                 member_name,
                                 member_declaring_type.FullName,
                                 state.Principles.CurrentType.FullName,
                                 state.Principles.CurrentMethod
                                 ));

                }
                var fieldDeclaringType = member_declaring_type;
                var fieldDeclaringTypeInfo = state.Principles.GetTI(fieldDeclaringType, false);
                #region Konwersja ENUMA
                {
                    if (fieldDeclaringType.IsEnum)
                    {
                        if (!isStatic)
                            throw new NotSupportedException();
                        AsDefinedConst _asDefinedConst = member.GetCustomAttribute<AsDefinedConst>();
                        if (_asDefinedConst != null)
                        {
                            var definedExpression = new PhpDefinedConstExpression(_asDefinedConst.DefinedConstName, tInfo.IncludeModule);
                            return SimplifyPhpExpression(definedExpression);
                        }
                        var _renderValue = member.GetCustomAttribute<RenderValueAttribute>();
                        if (_renderValue != null)
                        {
                            string strCandidate;
                            if (PhpValues.TryGetPhpStringValue(_renderValue.Name, out strCandidate))
                            {
                                var valueExpression = new PhpConstValue(strCandidate);
#if DEBUG
                                {
                                    var a1 = _renderValue.Name.Trim();
                                    var a2 = valueExpression.ToString();
                                    if (a1 != a2)
                                        throw new InvalidOperationException();
                                }
#endif
                                return SimplifyPhpExpression(valueExpression);
                            }
                            else
                            {
                                var valueExpression = new PhpFreeExpression(_renderValue.Name);
                                return SimplifyPhpExpression(valueExpression);
                            }
                        }
                        {
                            // object v1 = ReadEnumValueAndProcessForPhp(member);
                            object v1 = member.GetValue(null);
                            var g = new PhpConstValue(v1);
                            return SimplifyPhpExpression(g);
                        }
                        //throw new NotSupportedException();
                    }
                }
                #endregion

                switch (tInfo.Destination)
                {
                    case FieldTranslationDestionations.DefinedConst:
                        if (!member.IsStatic)
                            throw new NotSupportedException("Unable to convert instance field into PHP defined const");
                        var definedExpression = new PhpDefinedConstExpression(tInfo.ScriptName, tInfo.IncludeModule);
                        return SimplifyPhpExpression(definedExpression);
                    case FieldTranslationDestionations.GlobalVariable:
                        if (!member.IsStatic)
                            throw new NotSupportedException("Unable to convert instance field into PHP global variable");
                        var globalVariable = PhpVariableExpression.MakeGlobal(tInfo.ScriptName);
                        return SimplifyPhpExpression(globalVariable);
                    case FieldTranslationDestionations.JustValue:
                        if (!member.IsStatic)
                            throw new NotSupportedException("Unable to convert instance field into compile-time value");
                        object constValue = member.GetValue(null);
                        var phpConstValue = new PhpConstValue(constValue, tInfo.UsGlueForValue);
                        return SimplifyPhpExpression(phpConstValue);
                    case FieldTranslationDestionations.NormalField:
                        var rr = new PhpClassFieldAccessExpression();
                        rr.FieldName = isStatic ? tInfo.ScriptName : PhpVariableExpression.AddDollar(tInfo.ScriptName);
                        rr.ClassName = state.Principles.GetPhpType(member_declaring_type, true);
                        return SimplifyPhpExpression(rr);
                    default:
                        throw new NotSupportedException();
                }


            }
        }

        protected override IPhpValue VisitClassPropertyAccessExpression(ClassPropertyAccessExpression src)
        {
            var tmp = state.Principles.NodeTranslators.Translate(state, src);
            if (tmp != null)
                return SimplifyPhpExpression(tmp);
            throw new NotSupportedException();
        }

        protected override IPhpValue VisitConditionalExpression(ConditionalExpression src)
        {
            var condition = TransValue(src.Condition);
            var whenTrue = TransValue(src.WhenTrue);
            var whenFalse = TransValue(src.WhenFalse);
            var result = new PhpConditionalExpression(condition, whenTrue, whenFalse);
            return SimplifyPhpExpression(result);
        }

        protected override IPhpValue VisitConstValue(ConstValue src)
        {
            return new PhpConstValue(src.MyValue);
        }

        protected override IPhpValue VisitElementAccessExpression(ElementAccessExpression src)
        {
            var expression = TransValue(src.Expression);
            var arg = src.Arguments.Select(i => TransValue(i)).ToArray();
            var a = new PhpElementAccessExpression(expression, arg);
            return SimplifyPhpExpression(a);
        }

        protected override IPhpValue VisitFunctionArgument(FunctionArgument src)
        {

            var expression = TransValue(src.MyValue);
            var a = expression.GetPhpCode(null);
            var result = new PhpMethodInvokeValue(expression);
            if (!string.IsNullOrEmpty(src.RefOrOutKeyword))
                result.ByRef = true;
            return SimplifyPhpExpression(result);
        }

        protected override IPhpValue VisitIncrementDecrementExpression(IncrementDecrementExpression src)
        {
            var o = TransValue(src.Operand);
            var r = new PhpIncrementDecrementExpression(o, src.Increment, src.Pre);
            return SimplifyPhpExpression(r);
        }

        protected override IPhpValue VisitInstanceFieldAccessExpression(InstanceFieldAccessExpression src)
        {
            var fti = state.Principles.GetOrMakeTranslationInfo(src.Member);
            var to = TransValue(src.TargetObject);
            if (src.Member.DeclaringType.IsDefined(typeof(AsArrayAttribute)))
            {
                switch (fti.Destination)
                {
                    case FieldTranslationDestionations.NormalField:
                        var tmp = new PhpArrayAccessExpression(to, new PhpConstValue(fti.ScriptName));
                        return SimplifyPhpExpression(tmp);
                    case FieldTranslationDestionations.DefinedConst:
                        break; // obsłużę to dalej jak dla zwykłej klasy
                    default:
                        throw new NotSupportedException();
                }
            }
            var a = new PhpInstanceFieldAccessExpression(fti.ScriptName, to, fti.IncludeModule);
            return a;
        }

        protected override IPhpValue VisitInstanceMemberAccessExpression(InstanceMemberAccessExpression src)
        {

            if (src.Member == null)
                throw new NotSupportedException();
            if (src.Member is MethodInfo)
            {
                var mi = src.Member as MethodInfo;
                if (mi.IsStatic)
                    throw new Exception("Metoda nie może być statyczna");
                var mmi = MethodTranslationInfo.FromMethodInfo(mi);
                var a = new PhpConstValue(TransValue(src.Expression));
                var b = new PhpConstValue(mmi.ScriptName);
                var o = new PhpArrayCreateExpression(new IPhpValue[] { a, b });
                return o;
            }
            throw new NotSupportedException();
        }

        protected override IPhpValue VisitInstancePropertyAccessExpression(CsharpInstancePropertyAccessExpression src)
        {
            var pri = PropertyTranslationInfo.FromPropertyInfo(src.Member);
            var ownerInfo = this.state.Principles.GetOrMakeTranslationInfo(src.Member.DeclaringType);
            if (src.TargetObject == null)
                throw new NotImplementedException("statyczny");
            var translatedByExternalNodeTranslator = state.Principles.NodeTranslators.Translate(state, src);
            if (translatedByExternalNodeTranslator != null)
                return SimplifyPhpExpression(translatedByExternalNodeTranslator);


            IPhpValue phpTargetObject = TransValue(src.TargetObject);
            if (ownerInfo.IsArray)
            {
                var idx = new PhpConstValue(pri.FieldScriptName);
                var arrayExpr = new PhpArrayAccessExpression(phpTargetObject, idx);
                return arrayExpr;
            }
            {
                PropertyInfo propertyInfo = src.Member;
                var classReplacer = state.FindOneClassReplacer(propertyInfo.DeclaringType);
                if (classReplacer != null)
                {
                    var newPropertyInfo = classReplacer.ReplaceBy.GetProperty(src.Member.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (newPropertyInfo == null)
                        throw new Exception(string.Format("Klasa {0} nie zawiera własności {1}", classReplacer.ReplaceBy, src.Member));
                    if (newPropertyInfo.GetIndexParameters().Length > 0)
                        throw new NotSupportedException("energetic gecko, Property with index");
                    propertyInfo = newPropertyInfo;
                }



                #region DirectCallAttribute
                {
                    var ats = propertyInfo.GetCustomAttribute<DirectCallAttribute>(true);
                    if (ats != null)
                    {
                        if (string.IsNullOrEmpty(ats.Name))
                        {
                            if (ats.MapArray.Length > 1)
                                throw new NotSupportedException();
                            if (ats.MapArray.Length == 1)
                                if (ats.MapArray[0] != DirectCallAttribute.THIS)
                                    throw new NotSupportedException();
                            return phpTargetObject;
                        }
                        switch (ats.MemberToCall)
                        {
                            case ClassMembers.Method:
                                if (ats.Name == "this")
                                    return phpTargetObject;

                                var method = new PhpMethodCallExpression(ats.Name);
                                //switch (ats.CallType)
                                //{
                                //    case MethodCallStyles.Procedural:
                                //        method.Arguments.Add(new PhpMethodInvokeValue(phpTargetObject));
                                //        return method;
                                //    case MethodCallStyles.:
                                //        method.Arguments.Add(new PhpMethodInvokeValue(phpTargetObject));
                                //        return method;
                                //    default:
                                //        throw new NotSupportedException();
                                //}
                                throw new NotImplementedException();
                            case ClassMembers.Field:
                                switch (ats.CallType)
                                {
                                    case MethodCallStyles.Instance:
                                        if (ats.Name == "this")
                                            return phpTargetObject;
                                        var includeModule = ownerInfo.IncludeModule;
                                        var field = new PhpInstanceFieldAccessExpression(ats.Name, phpTargetObject, includeModule);
                                        return field;
                                    default:
                                        throw new NotSupportedException();
                                }
                            //var f = new PhpMethodCallExpression(ats.Name);
                            //method.Arguments.Add(new PhpMethodInvokeValue(phpTargetObject));
                            //return method;
                            default:
                                throw new NotSupportedException();
                        }
                    }
                }
                #endregion
                #region UseExpressionAttribute
                {
                    var ats = propertyInfo.GetCustomAttribute<UseBinaryExpressionAttribute>(true);
                    if (ats != null)
                    {
                        IPhpValue l, r;
                        var x = ats.Left;
                        l = GetValueForExpression(phpTargetObject, ats.Left);
                        r = GetValueForExpression(phpTargetObject, ats.Right);

                        var method = new PhpBinaryOperatorExpression(ats.Operator, l, r);
                        return method;
                    }
                }
                #endregion
                {
                    pri = PropertyTranslationInfo.FromPropertyInfo(src.Member);
                    var to = this.TransValue(src.TargetObject);
                    var a = new PhpPropertyAccessExpression(pri, to);
                    return a;
                }
            }
        }

        protected override IPhpValue VisitLambdaExpression(LambdaExpression src)
        {
            var T = new Translator(state);
            var a = new PhpMethodDefinition("");
            a.Statements.AddRange(T.TranslateStatement(src.Body));
            foreach (var p in src.Parameters)
            {
                PhpMethodArgument phpParameter = new PhpMethodArgument();
                phpParameter.Name = p.Name;
                a.Arguments.Add(phpParameter);
            }
            var b = new PhpLambdaExpression(a);
            return SimplifyPhpExpression(b);
        }

        protected override IPhpValue VisitLocalVariableExpression(LocalVariableExpression src)
        {
            bool isArgument = false;
            if (state.Principles.CurrentMethod != null)
            {
                var g = state.Principles.CurrentMethod.GetParameters().Where(u => u.Name == src.Name).Any();
                if (g)
                    isArgument = true;
            }
            return PhpVariableExpression.MakeLocal(src.Name, isArgument);
        }

        protected override IPhpValue VisitMethodCallExpression(CsharpMethodCallExpression src)
        {
            var x = state.Principles.NodeTranslators.Translate(state, src);
            if (x != null)
                return this.SimplifyPhpExpression(x);
            throw new NotSupportedException();
        }

        protected override IPhpValue VisitMethodExpression(MethodExpression src)
        {
            if (src.Method.IsStatic)
            {
                var PhpClassName = state.Principles.GetPhpType(src.Method.DeclaringType, true);
                PhpClassName.CurrentEffectiveName = "";
                var className = new PhpConstValue(PhpClassName.FullName);
                var methodTranslationInfo = MethodTranslationInfo.FromMethodInfo(src.Method);
                var methodName = new PhpConstValue(methodTranslationInfo.ScriptName);
                var arrayCreation = new PhpArrayCreateExpression(new IPhpValue[] { className, methodName });
                return SimplifyPhpExpression(arrayCreation);
            }
            {
                // ryzykuję z this
                var targetObject = new PhpThisExpression();
                var methodTranslationInfo = MethodTranslationInfo.FromMethodInfo(src.Method);
                var methodName = new PhpConstValue(methodTranslationInfo.ScriptName);
                var arrayCreation = new PhpArrayCreateExpression(new IPhpValue[] { targetObject, methodName });
                return SimplifyPhpExpression(arrayCreation);
            }
            throw new NotSupportedException();
        }

        protected override IPhpValue VisitParenthesizedExpression(ParenthesizedExpression src)
        {
            var e = TransValue(src.Expression);
            var a = new PhpParenthesizedExpression(e);
            return SimplifyPhpExpression(a);
        }

        protected override IPhpValue VisitStaticMemberAccessExpression(StaticMemberAccessExpression src)
        {
            var yy = (src.Expression as LangType).DotnetType;


            var mem = yy.GetMembers(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToArray().Where(i => i.Name == src.MemberName).ToArray();
            if (mem.Length == 1)
            {
                var g = mem[0];
                if (g is FieldInfo)
                    return ConvertValueToPredefined(((FieldInfo)g).GetValue(null));

            }
            throw new NotSupportedException();
        }

        protected override IPhpValue VisitThisExpression(ThisExpression src)
        {
            return new PhpThisExpression();
        }

        protected override IPhpValue VisitTypeOfExpression(TypeOfExpression src)
        {
            return new PhpConstValue(src.DotnetType.Name);
            throw new NotSupportedException();
        }

        protected override IPhpValue VisitTypeValue(TypeValue src)
        {
            throw new NotSupportedException("Uzupełnij VisitTypeValue");
        }

        protected override IPhpValue VisitUnaryOperatorExpression(UnaryOperatorExpression src)
        {
            var v = TransValue(src.Operand);
            var a = new PhpUnaryOperatorExpression(v, src.Operator);
            return SimplifyPhpExpression(a);
        }
        // Private Methods         

        IPhpValue SimplifyPhpExpression(IPhpValue v)
        {
            var s = new ExpressionSimplifier(new OptimizeOptions());
            return s.Visit(v as PhpSourceBase);

        }

        #endregion Methods

        #region Fields

        TranslationState state;

        #endregion Fields
    }
}
