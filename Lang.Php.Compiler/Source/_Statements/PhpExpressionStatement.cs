using Lang.Php.Compiler.Translator;
using Lang.Php.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property Expression IPhpValue 
     * 
    
    smartClassEnd
    */
    /// <summary>
    /// Opakowuje expression jako statement, np. postincrementacja, wywołanie metody itp.
    /// </summary>
    public partial class PhpExpressionStatement : IPhpStatementBase
    {
        #region Methods

        // Public Methods 


        private bool IsEcho
        {
            get
            {
                var callExpression = expression as PhpMethodCallExpression;
                // ProceduralStyleMethodCall checks if expression is PhpMethodCallExpression
                // ReSharper disable once PossibleNullReferenceException
                return IsProceduralStyleMethodCall && callExpression.Name == "echo";
            }
        }

        public bool IsProceduralStyleMethodCall
        {
            get
            {
                var callExpression = expression as PhpMethodCallExpression;
                return callExpression != null && callExpression.CallType == MethodCallStyles.Procedural;
            }
        }

        public override StatementEmitInfo GetStatementEmitInfo(PhpEmitStyle style)
        {
            if (!IsEcho)
                return StatementEmitInfo.NormalSingleStatement;
            var a = GetEchoItems(null);
            if (a.Length == 0)
                return StatementEmitInfo.Empty;
            if (a.Length != 1)
                return StatementEmitInfo.ManyItemsOrPlainHtml;
            return a[0].PlainHtml
                ? StatementEmitInfo.ManyItemsOrPlainHtml
                : StatementEmitInfo.NormalSingleStatement;
        }


        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            style = PhpEmitStyle.xClone(style);
            if (!style.AsIncrementor)
                if (expression is PhpMethodCallExpression)
                {
                    var methodCallExpression = expression as PhpMethodCallExpression;
                    if (methodCallExpression.CallType == MethodCallStyles.Procedural
                        && methodCallExpression.Name == "echo")
                        if (EmitInlineHtml(emiter, writer, style))
                            return;
                }
            var code = expression.GetPhpCode(style);
            if (style.AsIncrementor)
            {
                writer.Write(code);
            }
            else
                writer.WriteLn(code + ";");

        }
        IPhpValue AAA(IPhpValue x)
        {
            var constExpression = x as PhpDefinedConstExpression;
            if (constExpression == null) return x;
            return constExpression.DefinedConstName == "PHP_EOL" ? new PhpConstValue("\r\n") : x;
        }

        public class EchoEmitItem
        {
            public EchoEmitItem(string code, bool plainHtml)
            {
                Code = code;
                PlainHtml = plainHtml;
            }
            public override string ToString()
            {
                if (PlainHtml)
                    return Code;
                return "echo " + Code + ";";
            }
            public string Code { get; private set; }
            public bool PlainHtml { get; private set; }
        }

        static IEnumerable<string> SplitToLines(string code)
        {
            var result = new List<string>();
            while (code.IndexOf("\r\n", StringComparison.Ordinal) > 0)
            {
                var idx = code.IndexOf("\r\n", StringComparison.Ordinal) + 2;
                result.Add(code.Substring(0, idx));
                code = code.Substring(idx);
            }
            if (code != "")
                result.Add(code);
            return result.ToArray();
        }


        private EchoEmitItem[] GetEchoItems(PhpEmitStyle style)
        {
            var values = new List<IPhpValue>();
            #region Przygotowanie listy elementów do wyświetlenia
            {
                var methodCall = expression as PhpMethodCallExpression;
                if (methodCall == null) return
                      null;
                if (methodCall.CallType != MethodCallStyles.Procedural || methodCall.Name != "echo")
                    return null;
                foreach (var xx in methodCall.Arguments)
                    values.AddRange(ExpressionSimplifier.ExplodeConcats(xx, "."));
                values = values.Select(AAA).ToList();

                #region Łączenie const string

                for (var i = 1; i < values.Count; i++)
                {
                    var a1 = values[i - 1];
                    var a2 = values[i];
                    if (!(a1 is PhpConstValue) || !(a2 is PhpConstValue)) continue;
                    var b1 = (a1 as PhpConstValue).Value;
                    var b2 = (a2 as PhpConstValue).Value;
                    var c1 = PhpValues.ToPhpCodeValue(b1);
                    var c2 = PhpValues.ToPhpCodeValue(b2);
                    if (c1.Kind != PhpCodeValue.Kinds.StringConstant || c1.Kind != PhpCodeValue.Kinds.StringConstant)
                        continue;
                    values[i - 1] = new PhpConstValue((string) c1.SourceValue + (string) c2.SourceValue);
                    values.RemoveAt(i);
                    i--;
                }

                #endregion
            }
            #endregion

            {
                IPhpValue v = null;
                Action<IPhpValue> vv = u =>
                {
                    v = v == null ? u : new PhpBinaryOperatorExpression(".", v, u);
                };

                var result = new List<EchoEmitItem>();
                foreach (var value in values)
                {
                    if (value is PhpConstValue)
                    {
                        var constValue = (value as PhpConstValue).Value;
                        if (constValue is string)
                        {
                            #region Const-string
                            var lines = SplitToLines((string)constValue);
                            foreach (var i in lines)
                            {
                                if (i.EndsWith("\r\n"))
                                {
                                    vv(new PhpConstValue(i.Substring(0, i.Length - 2)));
                                    vv(new PhpDefinedConstExpression("PHP_EOL", null));
                                    result.Add(new EchoEmitItem(v.GetPhpCode(style), false));
                                    v = null;
                                }
                                else
                                    vv(new PhpConstValue(i));
                            }
                            continue;
                            #endregion
                        }
                    }
                    vv(value);
                }
                if (v != null)
                    result.Add(new EchoEmitItem(v.GetPhpCode(style), false));

                return result.ToArray();

#if OLD


                foreach (var value in Values)
                {
                    bool plainHtml = false;
                    var code = value.GetPhpCode(style);
                #region Przygotowanie kodu
                    if (value is PhpConstValue)
                    {
                        var constValue = (value as PhpConstValue).Value;
                        if (constValue is string)
                        {
                            code = (string)constValue; plainHtml = true;
                            while (code.IndexOf("\r\n") > 0)
                            {
                                var _i = code.IndexOf("\r\n");
                                var g = code.Substring(0, _i);
                                {
                                    var l = new PhpConstValue(code.Substring(0, _i));
                                    var r = new PhpDefinedConstExpression("PHP_EOL", null);
                                    var bin = new PhpBinaryOperatorExpression(".", l, r);
                                    result.Add(new EEi(bin.GetPhpCode(style), false));
                                }
                                code = code.Substring(_i + 2);
                            }
                            if (code != "")
                            {
                                var l = new PhpConstValue(code);
                                result.Add(new EEi(l.GetPhpCode(style), false));
                            }
                        }
                        continue;
                    }
                #endregion
                    result.Add(new EEi(code, plainHtml));
                }
                return result.ToArray();

                
#endif
            }
        }

        bool EmitInlineHtml(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            // return false;
            var values = GetEchoItems(style);
            //if (Values.Length == 1)
            //    return false;

            #region Emisja
            {
                foreach (var i in values)
                {
                    if (i.PlainHtml)
                        throw new NotSupportedException();
                    writer.WriteLn("echo " + i.Code + ";");
                }
                return true;
            }
#if VERSION1
            {
                {
                    if (Values.Length == 1)
                    {
                        if (!Values[0].PlainHtml)
                        {
                            return false;
                        }
                    }
                }

                bool isHtml = false;
                int iii = Values.Length;
                bool isFirst = true;
                foreach (var i in Values)
                {
                    bool isLast = --iii == 0;
                    try
                    {
                        var code = i.Code;
                        if (isFirst && !i.PlainHtml)
                        {
                            writer.WriteLn("echo " + code + ";");
                            continue;
                        }

                        if (!i.PlainHtml)
                            code = "<?= " + code + " ?>";
                        if (!isHtml)
                        {
                            code = "?>" + code;
                            isHtml = true;
                        }
                        if (isLast && isHtml)
                            code += "<?php";
                        if (!isFirst)
                            writer.SkipIndent = true;
                        if (isHtml && !isLast)
                            writer.Write(code);
                        else
                            writer.WriteLn(code);
                    }
                    finally
                    {
                        isFirst = false;
                    }
                }
            } 
#endif
            #endregion
            return true;
        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return GetCodeRequests(expression);
        }

        public override string ToString()
        {
            return expression + ";";
        }
        #endregion Methods
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-11-08 08:58
// File generated automatically ver 2013-07-10 08:43
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php.Compiler.Source
{
    public partial class PhpExpressionStatement
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public PhpExpressionStatement()
        {
        }
        Przykłady użycia
        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Expression##
        implement ToString Expression=##Expression##
        implement equals Expression
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */


        #region Constructors
        /// <summary>
        /// Tworzy instancję obiektu
        /// <param name="Expression"></param>
        /// </summary>
        public PhpExpressionStatement(IPhpValue Expression)
        {
            this.Expression = Expression;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Expression; 
        /// </summary>
        public const string PROPERTYNAME_EXPRESSION = "Expression";
        #endregion Constants


        #region Methods
        #endregion Methods


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IPhpValue Expression
        {
            get
            {
                return expression;
            }
            set
            {
                expression = value;
            }
        }
        private IPhpValue expression;
        #endregion Properties
    }
}
