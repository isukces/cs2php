using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator.Node
{
    class Lang__Php__Html__Methods
    {
        #region Methods

        // Public Methods 

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType == typeof(Html))
            {
                this.ctx = ctx;
                list = new List<IPhpValue>();
                var methodName = src.MethodInfo.Name;
                var isecho = methodName.StartsWith("Echo");
                if (isecho)
                    methodName = methodName.Substring(4);
                if (methodName == "TagBound")
                {
                    FillTagOpen(2, ctx, src, ">");
                    Append(src.Arguments[1]);
                    FillTagEnd(src.Arguments[0]);
                    return MakeEchoIfNecessary(isecho);
                }
                if (methodName == "Attributes")
                {
                    for (var i = 1; i < src.Arguments.Length; i += 2)
                    {
                        var key = src.Arguments[i - 1];
                        var value = src.Arguments[i];
                        Append(" ");
                        Append(key);
                        Append("=\"");
                        Append(value);
                        Append("\"");
                    }
                    return MakeEchoIfNecessary(isecho);
                }
                if (methodName == "TagOpen" || methodName == "TagSingle")
                {
                    var end = methodName == "TagOpen" ? ">" : " />";
                    FillTagOpen(1, ctx, src, end);
                    return MakeEchoIfNecessary(isecho);
                }
                if (methodName == "TagOpenOpen")
                {
                    FillTagOpen(1, ctx, src, "");
                    return MakeEchoIfNecessary(isecho);
                }
                if (methodName == "TagClose")
                {
                    FillTagEnd(src.Arguments[0]);
                    if (src.Arguments.Length == 2)
                    {
//                        throw new NotSupportedException();
                        Append("\r\n");
                    }
                    return MakeEchoIfNecessary(isecho);
                }
                if (methodName == "Comment")
                {
                    Append("<!-- ");
                    Append(src.Arguments[0]);
                    Append(" -->");
                    return MakeEchoIfNecessary(isecho);
                }
                if (methodName == "Pixels" || methodName == "Percent")
                {
                    Append(src.Arguments[0]);
                    Append(methodName == "Pixels" ? "px" : "%");
                    return MakeEchoIfNecessary(isecho);
                }
                if (methodName == "Mm")
                {
                    Append(src.Arguments[0]);
                    Append("mm");
                    return MakeEchoIfNecessary(isecho);
                }

                if (methodName == "Css")
                {
                    for (var i = 1; i < src.Arguments.Length; i += 2)
                    {
                        var key = src.Arguments[i - 1];
                        var value = src.Arguments[i];
                        Append(key);
                        Append(":");
                        Append(value);
                        Append(";");
                    }
                    return MakeEchoIfNecessary(isecho);
                }
                if (methodName == "CssBorder")
                {
                    Append(src.Arguments[0]);
                    Append(" ");
                    Append(src.Arguments[1]);
                    Append(" ");
                    Append(src.Arguments[2]);
                    return MakeEchoIfNecessary(isecho);
                }
                throw new NotSupportedException();
            }
            return null;
        }

        private void FillTagEnd(FunctionArgument tagname)
        {
            Append("</");
            Append(tagname);
            Append(">");
        }
        // Private Methods 

        private IPhpValue _JoinArray()
        {
            if (list.Count == 1)
                return list[0];
            var e = list[0];
            for (var i = 1; i < list.Count; i++)
            {
                var a = list[i];
                e = new PhpBinaryOperatorExpression(".", e, a);
            }
            return e;
        }

        void Append(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            if (list.Count > 0 && list.Last() is PhpConstValue)
            {
                var lastValue = (PhpConstValue)list.Last();
                if (lastValue.Value is string)
                {
                    lastValue.Value = (string)lastValue.Value + text;
                    return;
                }
                if (lastValue.Value is int)
                {
                    lastValue.Value = (int)lastValue.Value + text;
                    return;
                }
            }
            list.Add(new PhpConstValue(text));
        }

        void Append(FunctionArgument arg)
        {
            var v = ctx.TranslateValue(arg.MyValue);
            Append(v);
        }

        void Append(IPhpValue value)
        {
            if (value is PhpBinaryOperatorExpression)
            {
                var v1 = value as PhpBinaryOperatorExpression;
                if (v1.Operator == ".")
                {
                    Append(v1.Left);
                    Append(v1.Right);
                    return;
                }
            }
            if (value is PhpConstValue)
            {
                var vv = (PhpConstValue)value;
                if (vv.Value is string)
                {
                    Append((string)vv.Value);
                    return;
                }
                if (vv.Value is int)
                {
                    Append(((int)vv.Value).ToString());
                    return;
                }
            }
            if (value is PhpConditionalExpression || value is PhpBinaryOperatorExpression)
            {
                var p = new PhpParenthesizedExpression(value);
                Append(p);
                return;
            }
            list.Add(value);
        }

        private void FillTagOpen(int argsFrom, IExternalTranslationContext ctx, CsharpMethodCallExpression src, string end)
        {
            var tagName = src.Arguments[0];
            Append("<");
            Append(tagName);
            for (var i = argsFrom + 1; i < src.Arguments.Length; i += 2)
            {
                var key = src.Arguments[i - 1];
                var value = src.Arguments[i];
                Append(" ");
                Append(key);
                Append("=\"");
                Append(value);
                Append("\"");
            }
            Append(end);

        }

        private IPhpValue MakeEchoIfNecessary(bool isecho)
        {
            var v = _JoinArray();
            if (isecho)
                v = new PhpMethodCallExpression("echo", v);
            return v;
        }

        #endregion Methods

        #region Fields

        IExternalTranslationContext ctx;
        List<IPhpValue> list;

        #endregion Fields
    }
}
