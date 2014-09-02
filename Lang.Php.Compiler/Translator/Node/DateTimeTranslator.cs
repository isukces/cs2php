using Lang.Cs.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator.Node
{
    public class DateTimeTranslator : IPhpNodeTranslator<CallConstructor>,
                IPhpNodeTranslator<ClassPropertyAccessExpression>,
                IPhpNodeTranslator<CsharpMethodCallExpression>,
                IPhpNodeTranslator<BinaryOperatorExpression>,
                IPhpNodeTranslator<CsharpInstancePropertyAccessExpression>
    {
        #region Methods

        // Public Methods 

        public int GetPriority()
        {
            return 1;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CallConstructor src)
        {
            if (src.Info.DeclaringType != typeof(DateTime))
                return null;
            if (src.Info.ToString() == "Void .ctor(Int32, Int32, Int32)")
            {
                //                $date = new DateTime();
                //$date->setDate(2001, 2, 3);
                var dtObject = PhpMethodCallExpression.MakeConstructor("DateTime");
                dtObject.IsStandardPhpClass = true;
                // date_date_set 
                var b = new PhpMethodCallExpression("date_date_set ",
                        dtObject,
                        ctx.TranslateValue(src.Arguments[0]),
                        ctx.TranslateValue(src.Arguments[1]),
                        ctx.TranslateValue(src.Arguments[2])
                    );
                var c = new PhpMethodCallExpression("date_time_set ",
                        b,
                        new PhpConstValue(0),
                        new PhpConstValue(0),
                        new PhpConstValue(0)
                    );
                var mktime = new PhpMethodCallExpression("mktime",
                        new PhpConstValue(0),
                        new PhpConstValue(0),
                        new PhpConstValue(0),
                        ctx.TranslateValue(src.Arguments[1]), //month
                        ctx.TranslateValue(src.Arguments[2]),// day
                        ctx.TranslateValue(src.Arguments[0]) // year
                    );
                var epoch = new PhpBinaryOperatorExpression(".", new PhpConstValue("@"), mktime);
                dtObject.Arguments.Add(new PhpMethodInvokeValue(epoch));
                return dtObject;
                // int mktime ([ int $hour = date("H") [, int $minute = date("i") [, int $second = date("s") [, int $month = date("n") [, int $day = date("j") [, int $year = date("Y") [, int $is_dst = -1 ]]]]]]] )
                // $datetimeobject = new DateTime(mktime(0, 0, 0, $data[$j]['month'], $data[$j]['day'],$data[$j]['year']));
                return c;
                throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, ClassPropertyAccessExpression src)
        {
            if (src.Member.DeclaringType != typeof(DateTime))
                return null;
            if (src.Member.Name == "Now")
            {
                // $date = new DateTime('2000-01-01');
                var c = PhpMethodCallExpression.MakeConstructor("DateTime");
                c.IsStandardPhpClass = true;
                return c;
            }
            throw new NotImplementedException();
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType != typeof(DateTime))
                return null;
            var fn = src.MethodInfo.ToString();
            if (src.MethodInfo.Name == "ToString")
            {
                var to = ctx.TranslateValue(src.TargetObject);
                var c = new PhpMethodCallExpression(to, "format", new PhpConstValue("Y-m-d H:i:s"));
                return c;
            }
            if (fn == "System.TimeSpan op_Subtraction(System.DateTime, System.DateTime)")
            {
                var l = ctx.TranslateValue(src.Arguments[0].MyValue);
                var r = ctx.TranslateValue(src.Arguments[1].MyValue);
                var c = new PhpMethodCallExpression("date_diff", r, l);
                return c;
            }
            if (fn == "Boolean op_GreaterThanOrEqual(System.DateTime, System.DateTime)")
                return new PhpBinaryOperatorExpression(">=",
                    ctx.TranslateValue(src.Arguments[0]),
                    ctx.TranslateValue(src.Arguments[1]));
            if (fn == "Boolean op_Inequality(System.DateTime, System.DateTime)")
                return new PhpBinaryOperatorExpression("!=",
                      ctx.TranslateValue(src.Arguments[0]),
                      ctx.TranslateValue(src.Arguments[1]));
            if (fn == "Boolean op_Equality(System.DateTime, System.DateTime)")
                return new PhpBinaryOperatorExpression("==",
                      ctx.TranslateValue(src.Arguments[0]),
                      ctx.TranslateValue(src.Arguments[1]));

            if (fn == "System.DateTime AddDays(Double)")
                return TranslateAddInterval(ctx, src, SEC_PER_DAY);
            if (fn == "System.DateTime AddHours(Double)")
                return TranslateAddInterval(ctx, src, SEC_PER_HOUR);

            throw new NotImplementedException(fn);
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, BinaryOperatorExpression src)
        {
            if (src.OperatorMethod == null) return null;
            return null;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpInstancePropertyAccessExpression src)
        {
            if (src.Member.DeclaringType != typeof(DateTime))
                return null;

            var to = ctx.TranslateValue(src.TargetObject);
            if (src.Member.Name == "Date")
            {
                if (to.ToString() == "new DateTime()")
                {
                    // echo 'Current time: ' . date('Y-m-d H:i:s') . "\n";
                    var co = new PhpMethodCallExpression("date", new PhpConstValue(DATE_ONLY));
                    var co1 = new PhpMethodCallExpression("date_create_from_format ", new PhpConstValue(DATE_ONLY), co);
                    return co1;
                }
                else
                {
                    var _for = new PhpMethodCallExpression(to, "format", new PhpConstValue(DATE_ONLY));
                    var co = new PhpMethodCallExpression("date_create_from_format", _for, new PhpConstValue(DATE_ONLY));
                    return co;
                }
            }
            if (src.Member.Name == "Day")
                return GetDatePart(to, "d");
            if (src.Member.Name == "Month")
                return GetDatePart(to, "m");
            if (src.Member.Name == "Year")
                return GetDatePart(to, "Y");

            if (src.Member.Name == "Hour")
                return GetDatePart(to, "H");
            if (src.Member.Name == "Minute")
                return GetDatePart(to, "i");
            if (src.Member.Name == "Second")
                return GetDatePart(to, "s");

            throw new NotImplementedException();
        }
        // Private Methods 

        private IPhpValue GetDatePart(IPhpValue to, string phpDatePart)
        {
            if (to.ToString() == "new DateTime()")
            {
                throw new NotImplementedException();
                // echo 'Current time: ' . date('Y-m-d H:i:s') . "\n";
                var co = new PhpMethodCallExpression("date", new PhpConstValue(DATE_ONLY));
                var co1 = new PhpMethodCallExpression("date_create_from_format ", new PhpConstValue(DATE_ONLY), co);
                return co1;
            }
            else
            {
                var _for = new PhpMethodCallExpression(to, "format", new PhpConstValue(phpDatePart));
                return MakeInt(_for);
            }
        }

        IPhpValue GetIntervalString(IExternalTranslationContext ctx, IValue v, int mnoznik)
        {
            IPhpValue iphp;
            if (v is FunctionArgument)
                v = (v as FunctionArgument).MyValue;

            if (v is ConstValue)
            {
                var vv = (v as ConstValue).MyValue;
                if (vv is double)
                {
                    var phpString = mk((double)vv * mnoznik);
                    IValue cs = new ConstValue(phpString);
                    iphp = ctx.TranslateValue(cs);
                    return iphp;
                }
                else if (vv is int)
                {
                    var phpString = mk((int)vv * mnoznik);
                    IValue cs = new ConstValue(phpString);
                    iphp = ctx.TranslateValue(cs);
                    return iphp;
                }
                else
                    throw new NotSupportedException();
            }
            else
                throw new NotSupportedException();
        }

        IPhpValue MakeInt(IPhpValue x)
        {
            return new PhpMethodCallExpression("intval", x);
        }

        string mk(double sec)
        {
            int sec1 = (int)Math.Round(sec);
            if (sec1 % SEC_PER_DAY == 0)
                return string.Format("P{0}D", sec1 / SEC_PER_DAY);
            if (sec1 % SEC_PER_HOUR == 0)
                return string.Format("PT{0}H", sec1 / SEC_PER_HOUR);
            if (sec1 % 60 == 0)
                return string.Format("PT{0}M", sec1 / SEC_PER_MIN);
            return string.Format("PT{0}S", sec1);
        }

        private IPhpValue TranslateAddInterval(IExternalTranslationContext ctx, CsharpMethodCallExpression src, int mnoznik)
        {
            var intervalString = GetIntervalString(ctx, src.Arguments[0], mnoznik);
            var interval = PhpMethodCallExpression.MakeConstructor("DateInterval", intervalString);
            interval.IsStandardPhpClass = true;
            var targetObject = ctx.TranslateValue(src.TargetObject);
            var methd = new PhpMethodCallExpression("date_add", targetObject, interval);
            return methd;
        }

        #endregion Methods

        #region Fields

        const string DATE_ONLY = "Y-m-d";
        const int SEC_PER_DAY = 24 * 3600;
        const int SEC_PER_HOUR = 3600;
        const int SEC_PER_MIN = 60;

        #endregion Fields
    }
}
