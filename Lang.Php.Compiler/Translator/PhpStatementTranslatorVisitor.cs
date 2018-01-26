using System;
using System.Collections.Generic;
using System.Linq;
using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Compiler.Translator
{
    public class PhpStatementTranslatorVisitor : CSharpBaseVisitor<IPhpStatement[]>
    {
        public PhpStatementTranslatorVisitor(TranslationState state)
        {
            _state = state;
        }

        private static IPhpStatement[] MkArray(IPhpStatement x)
        {
            return new[] {x};
        }

        private static IPhpStatement[] MkArray(IPhpValue x)
        {
            return new IPhpStatement[] {new PhpExpressionStatement(x)};
        }

        public IPhpStatement[] TranslateStatements(IEnumerable<IStatement> x)
        {
            var re = new List<IPhpStatement>();
            foreach (var i in x)
            {
                var j = TranslateStatement(i);
                foreach (var tt in j)
                    re.Add(tt);
            }

            return re.ToArray();
        }

        protected override IPhpStatement[] VisitAssignExpression(CsharpAssignExpression src)
        {
            var translatedValue = TransValue(src);
            return MkArray(translatedValue);
        }

        protected override IPhpStatement[] VisitBreakStatement(BreakStatement src)
        {
            return MkArray(new PhpBreakStatement());
        }

        protected override IPhpStatement[] VisitCodeBlock(CodeBlock src)
        {
            var res = new PhpCodeBlock();
            res.Statements.AddRange(TranslateStatements(src.Items));
            return MkArray(res);
        }

        protected override IPhpStatement[] VisitContinueStatement(ContinueStatement src)
        {
            return MkArray(new PhpContinueStatement());
        }

        protected override IPhpStatement[] VisitForEachStatement(ForEachStatement src)
        {
            var                 g          = src.VarName;
            var                 collection = TransValue(src.Collection);
            var                 statement  = TranslateStatementOne(src.Statement);
            PhpForEachStatement a          = null;
            if (src.ItemType.DotnetType.IsGenericType)
            {
                var gtd = src.ItemType.DotnetType.GetGenericTypeDefinition();
                if (gtd == typeof(KeyValuePair<,>))
                {
                    a = new PhpForEachStatement(src.VarName, collection, statement);
                    // $i@Key
                    a.KeyVarname   = src.VarName + "@Key";
                    a.ValueVarname = src.VarName + "@Value";
                }
            }

            if (a == null)
                a = new PhpForEachStatement(src.VarName, collection, statement);
            return MkArray(a);
        }

        protected override IPhpStatement[] VisitForStatement(ForStatement src)
        {
            var condition       = TransValue(src.Condition);
            var statement       = TranslateStatementOne(src.Statement);
            var incrementors    = TranslateStatements(src.Incrementors);
            var declarations    = TranslateStatement(src.Declaration).ToArray();
            var phpDeclarations = new List<PhpAssignExpression>();
            foreach (object declaration in declarations)
            {
                var d = declaration;
                if (declaration is PhpExpressionStatement)
                    d = (declaration as PhpExpressionStatement).Expression;
                if (d is PhpAssignExpression)
                    phpDeclarations.Add(d as PhpAssignExpression);
                else
                    throw new NotSupportedException();
            }

            var result = new PhpForStatement(phpDeclarations.ToArray(), condition, statement, incrementors);
            return MkArray(result);
        }

        protected override IPhpStatement[] VisitIfStatement(IfStatement src)
        {
            var condition = TransValue(src.Condition);
            var ifTrue    = TranslateStatementOne(src.IfTrue);
            var ifFalse   = TranslateStatementOne(src.IfFalse);
            var a         = new PhpIfStatement(condition, ifTrue, ifFalse);
            return MkArray(a);
        }

        protected override IPhpStatement[] VisitIncrementDecrementExpression(IncrementDecrementExpression src)
        {
            var o = TransValue(src.Operand);
            var a = new PhpIncrementDecrementExpression(o, src.Increment, src.Pre);
            return MkArray(a);
        }


        protected override IPhpStatement[] VisitLocalDeclarationStatement(LocalDeclarationStatement src)
        {
            var s = new List<IPhpStatement>();
            foreach (var i in src.Declaration.Declarators)
            {
                /// to jest przypadek z c# 'int x;', dla php można to pominąć
                if (i.Value == null)
                    continue;
                if (i.Value is UnknownIdentifierValue)
                    throw new NotImplementedException();
                var l  = new PhpVariableExpression(PhpVariableExpression.AddDollar(i.Name), PhpVariableKind.Local);
                var r  = TransValue(i.Value);
                var tt = new PhpAssignExpression(l, r);
                s.Add(new PhpExpressionStatement(tt));

                //var r = new PhpAssignVariable( PhpVariableExpression.AddDollar(i.Name), false );
                //// r.Name = "$" + i.Name;
                //r.Value = TV(i.Value);
                //s.Add(r);
            }

            return s.ToArray();
        }

        protected override IPhpStatement[] VisitMethodCallExpression(CsharpMethodCallExpression src)
        {
            var a = TransValue(src);
            return MkArray(a);
            //var u = new DotnetMethodCallTranslator(state);

            //PhpMethodStatement phpMethod;
            //IPhpValue simplaeValue;
            //bool ok = u.TryReplaceByPhpDirectCall(src, out phpMethod, out simplaeValue);
            //if (ok)
            //{
            //    if (phpMethod == null)
            //        throw new NotSupportedException("smooth horse");
            //    return MkArray(phpMethod);
            //}
            //var mi = src.MethodInfo;

            //{
            //    if (mi.IsStatic)
            //    {
            //        throw new Exception(src.GetType().FullName);
            //    }
            //    else
            //    {
            //        System.Diagnostics.Debug.Assert(src.TargetObject != null);
            //        var phpTargetObject = TV(src.TargetObject);
            //        var method = new PhpMethodStatement(src.MethodInfo.Name);
            //        method.TargetObject = TV(src.TargetObject);
            //        new PhpValueTranslator(state).CopyArguments(src.Arguments, method);
            //        return MkArray(method);
            //    }
            //}

            //if (state.CurrentType == src.MethodInfo.ReflectedType)
            //{
            //    var method = new PhpMethodStatement(src.MethodInfo.Name);
            //    method.TargetObject = new PhpThisExpression();
            //    new PhpValueTranslator(state).CopyArguments(src.Arguments, method);
            //    return MkArray(method);
            //}
            //throw new Exception(src.GetType().FullName);
        }

        protected override IPhpStatement[] VisitReturnStatement(ReturnStatement src)
        {
            var value  = src.ReturnValue == null ? null : TransValue(src.ReturnValue);
            var result = new PhpReturnStatement(value);
            return MkArray(result);
        }

        protected override IPhpStatement[] VisitSwitchStatement(CsharpSwitchStatement src)
        {
            var switchStatement = new PhpSwitchStatement
            {
                Expression = TransValue(src.Expression)
            };
            foreach (var sec in src.Sections)
            {
                var section = new PhpSwitchSection
                {
                    Labels = sec.Labels.Select(q => new PhpSwitchLabel
                    {
                        Value     = q.Expression == null ? null : TransValue(q.Expression),
                        IsDefault = q.IsDefault
                    }).ToArray()
                };
                var statements = TranslateStatements(sec.Statements);
                var block      = new PhpCodeBlock();
                block.Statements.AddRange(statements);
                section.Statement = PhpCodeBlock.Reduce(block);
                switchStatement.Sections.Add(section);
            }

            return MkArray(switchStatement);
        }

        protected override IPhpStatement[] VisitVariableDeclaration(VariableDeclaration src)
        {
            //throw new Exception("DELETE THIS ??????");
            var s = new List<IPhpStatement>();
            foreach (var i in src.Declarators)
            {
                var l  = new PhpVariableExpression(PhpVariableExpression.AddDollar(i.Name), PhpVariableKind.Local);
                var r  = TransValue(i.Value);
                var tt = new PhpAssignExpression(l, r);
                s.Add(new PhpExpressionStatement(tt));

                //var r = new PhpAssignVariable(PhpVariableExpression.AddDollar(i.Name), false);
                //r.Value = TV(i.Value);
                //s.Add(r);
            }

            return s.ToArray();
        }


        protected override IPhpStatement[] VisitWhileStatement(WhileStatement src)
        {
            var c = TransValue(src.Condition);
            var s = TranslateStatementOne(src.Statement);
            var a = new PhpWhileStatement(c, s);
            return MkArray(a);
        }

        private IPhpStatement[] TranslateStatement(IStatement x)
        {
            if (x is CSharpBase)
                return Visit(x as CSharpBase);
            var trans = new Translator(_state);
            return trans.TranslateStatement(x);
        }

        private IPhpStatement TranslateStatementOne(IStatement x)
        {
            if (x == null)
                return null;
            var a = TranslateStatement(x);
            if (a.Length == 1)
                return a[0];
            return new PhpCodeBlock {Statements = a.ToList()};
        }

        private IPhpValue TransValue(IValue x)
        {
            return new PhpValueTranslator(_state).TransValue(x);
        }

        private readonly TranslationState _state;
    }
}