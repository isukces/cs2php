using Lang.Php.Compiler.Translator;
using Lang.Php.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lang.Php.Compiler.Source
{

    /*
    smartClass
    option NoAdditionalFile
    implement Constructor *
    
    property Expression IPhpValue 
    smartClassEnd
    */

    /// <summary>
    /// Opakowuje expression jako statement, np. postincrementacja, wywołanie metody itp.
    /// </summary>
    public partial class PhpExpressionStatement : IPhpStatementBase
    {
        #region Static Methods

        // Private Methods 

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

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public override void Emit(PhpSourceCodeEmiter emiter, PhpSourceCodeWriter writer, PhpEmitStyle style)
        {
            style = PhpEmitStyle.xClone(style);
            if (!style.AsIncrementor)
                if (_expression is PhpMethodCallExpression)
                {
                    var methodCallExpression = _expression as PhpMethodCallExpression;
                    if (methodCallExpression.CallType == MethodCallStyles.Procedural
                        && methodCallExpression.Name == "echo")
                        if (EmitInlineHtml(writer, style))
                            return;
                }
            var code = _expression.GetPhpCode(style);
            if (style.AsIncrementor)
            {
                writer.Write(code);
            }
            else
                writer.WriteLn(code + ";");

        }

        public override IEnumerable<ICodeRequest> GetCodeRequests()
        {
            return GetCodeRequests(_expression);
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

        public override string ToString()
        {
            return _expression + ";";
        }
        // Private Methods 

        static IPhpValue Aaa(IPhpValue x)
        {
            var constExpression = x as PhpDefinedConstExpression;
            if (constExpression == null) return x;
            return constExpression.DefinedConstName == "PHP_EOL" ? new PhpConstValue("\r\n") : x;
        }

        bool EmitInlineHtml(PhpSourceCodeWriter writer, PhpEmitStyle style)
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
        }

        private EchoEmitItem[] GetEchoItems(PhpEmitStyle style)
        {
            var values = new List<IPhpValue>();
            #region Przygotowanie listy elementów do wyświetlenia
            {
                var methodCall = _expression as PhpMethodCallExpression;
                if (methodCall == null) return
                      null;
                if (methodCall.CallType != MethodCallStyles.Procedural || methodCall.Name != "echo")
                    return null;
                foreach (var xx in methodCall.Arguments)
                    values.AddRange(ExpressionSimplifier.ExplodeConcats(xx, "."));
                values = values.Select(Aaa).ToList();

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
                    values[i - 1] = new PhpConstValue((string)c1.SourceValue + (string)c2.SourceValue);
                    values.RemoveAt(i);
                    i--;
                }

                #endregion
            }
            #endregion
            {
                IPhpValue echoArguments = null;
                Action<IPhpValue> vv = u =>
                {
                    // ReSharper disable once AccessToModifiedClosure
                    echoArguments = echoArguments == null ? u : new PhpBinaryOperatorExpression(".", echoArguments, u);
                };

                var result = new List<EchoEmitItem>();
                foreach (var value in values)
                {
                    if (value is PhpConstValue)
                    {
                        var constValue = (value as PhpConstValue).Value;
                        var constStringValue = constValue as string;
                        if (constStringValue != null)
                        {
                            #region Const-string
                            var lines = SplitToLines(constStringValue);
                            foreach (var i in lines)
                            {
                                if (i.EndsWith("\r\n"))
                                {
                                    vv(new PhpConstValue(i.Substring(0, i.Length - 2)));
                                    vv(new PhpDefinedConstExpression("PHP_EOL", null));
                                    // ReSharper disable once PossibleNullReferenceException
                                    result.Add(new EchoEmitItem(echoArguments.GetPhpCode(style), false));
                                    echoArguments = null;
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
                if (echoArguments != null)
                    result.Add(new EchoEmitItem(echoArguments.GetPhpCode(style), false));
                return result.ToArray();
            }
        }

        #endregion Methods

        #region Properties

        private bool IsEcho
        {
            get
            {
                var callExpression = _expression as PhpMethodCallExpression;
                // ProceduralStyleMethodCall checks if expression is PhpMethodCallExpression
                // ReSharper disable once PossibleNullReferenceException
                return IsProceduralStyleMethodCall && callExpression.Name == "echo";
            }
        }

        public bool IsProceduralStyleMethodCall
        {
            get
            {
                var callExpression = _expression as PhpMethodCallExpression;
                return callExpression != null && callExpression.CallType == MethodCallStyles.Procedural;
            }
        }

        #endregion Properties

        #region Nested Classes


        public class EchoEmitItem
        {
            #region Constructors

            public EchoEmitItem(string code, bool plainHtml)
            {
                Code = code;
                PlainHtml = plainHtml;
            }

            #endregion Constructors

            #region Methods

            // Public Methods 

            public override string ToString()
            {
                if (PlainHtml)
                    return Code;
                return "echo " + Code + ";";
            }

            #endregion Methods

            #region Properties

            public string Code { get; private set; }

            public bool PlainHtml { get; private set; }

            #endregion Properties
        }
        #endregion Nested Classes
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2014-09-08 18:08
// File generated automatically ver 2014-09-01 19:00
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
        /// <param name="expression"></param>
        /// </summary>
        public PhpExpressionStatement(IPhpValue expression)
        {
            Expression = expression;
        }

        #endregion Constructors


        #region Constants
        /// <summary>
        /// Nazwa własności Expression; 
        /// </summary>
        public const string PropertyNameExpression = "Expression";
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
                return _expression;
            }
            set
            {
                _expression = value;
            }
        }
        private IPhpValue _expression;
        #endregion Properties
    }
}
