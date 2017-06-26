using System;
using System.Collections.Generic;
using System.Linq;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator.Node
{
    internal class Lang__Php__Html__Methods
    {
        // Public Methods 

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType == typeof(Html))
            {
                _ctx = ctx;
                _list = new List<IPhpValue>();
                var methodName = src.MethodInfo.Name;
                var isecho = methodName.StartsWith("Echo");
                if (isecho)
                    methodName = methodName.Substring(4);
                switch (methodName)
                {
                    case "TagBound":
                        FillTagOpen(2, ctx, src, ">");
                        Append(src.Arguments[1]);
                        FillTagEnd(src.Arguments[0]);
                        return MakeEchoIfNecessary(isecho);
                    case "Attributes":
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
                    case "TagOpen":
                    case "TagSingle":
                        var end = methodName == "TagOpen" ? ">" : " />";
                        FillTagOpen(1, ctx, src, end);
                        return MakeEchoIfNecessary(isecho);
                    case "TagOpenOpen":
                        FillTagOpen(1, ctx, src, "");
                        return MakeEchoIfNecessary(isecho);
                    case "TagClose":
                        FillTagEnd(src.Arguments[0]);
                        if (src.Arguments.Length == 2)
                        {
//                        throw new NotSupportedException();
                            Append("\r\n");
                        }
                        return MakeEchoIfNecessary(isecho);
                    case "Comment":
                        Append("<!-- ");
                        Append(src.Arguments[0]);
                        Append(" -->");
                        return MakeEchoIfNecessary(isecho);
                    case "Pixels":
                    case "Percent":
                        Append(src.Arguments[0]);
                        Append(methodName == "Pixels" ? "px" : "%");
                        return MakeEchoIfNecessary(isecho);
                    case "Mm":
                        Append(src.Arguments[0]);
                        Append("mm");
                        return MakeEchoIfNecessary(isecho);
                    case "Css":
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
                    case "CssBorder":
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
        // Private Methods 

        private IPhpValue _JoinArray()
        {
            if (_list.Count == 1)
                return _list[0];
            var e = _list[0];
            for (var i = 1; i < _list.Count; i++)
            {
                var a = _list[i];
                e = new PhpBinaryOperatorExpression(".", e, a);
            }
            return e;
        }

        private void Append(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            if (_list.Count > 0 && _list.Last() is PhpConstValue)
            {
                var lastValue = (PhpConstValue)_list.Last();
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
            _list.Add(new PhpConstValue(text));
        }

        private void Append(FunctionArgument arg)
        {
            var v = _ctx.TranslateValue(arg.MyValue);
            Append(v);
        }

        private void Append(IPhpValue value)
        {
            if (value is PhpBinaryOperatorExpression v1)
            {
                if (v1.Operator == ".")
                {
                    Append(v1.Left);
                    Append(v1.Right);
                    return;
                }
            }
            if (value is PhpConstValue vv)
            {
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
            _list.Add(value);
        }

        private void FillTagEnd(FunctionArgument tagname)
        {
            Append("</");
            Append(tagname);
            Append(">");
        }

        private void FillTagOpen(int argsFrom, IExternalTranslationContext ctx, CsharpMethodCallExpression src,
            string end)
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

        private IExternalTranslationContext _ctx;
        private List<IPhpValue> _list;
    }
}