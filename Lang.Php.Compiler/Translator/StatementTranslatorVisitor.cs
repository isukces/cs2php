using Lang.Cs.Compiler;
using Lang.Php.Compiler.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Compiler.Translator
{
    public class StatementTranslatorVisitor : CSharpBaseVisitor<IPhpStatement[]>
    {

        public IPhpStatement[] TranslateStatements(IEnumerable<IStatement> x)
        {
            List<IPhpStatement> re = new List<IPhpStatement>();
            foreach (var i in x)
            {
                var j = TranslateStatement(i);
                foreach (var tt in j)
                    re.Add(tt);
            }
            return re.ToArray();
        }
        protected override IPhpStatement[] VisitCodeBlock(CodeBlock src)
        {
            PhpCodeBlock res = new PhpCodeBlock();
            res.Statements.AddRange(TranslateStatements(src.Items));
            return MkArray(res);
        }
        public StatementTranslatorVisitor(TranslationState state)
        {
            this.state = state;
        }
        TranslationState state;
        static IPhpStatement[] MkArray(IPhpStatement x)
        {
            return new IPhpStatement[] { x };
        }
        static IPhpStatement[] MkArray(IPhpValue x)
        {
            return new IPhpStatement[] { new PhpExpressionStatement(x) };
        }
        IPhpValue TransValue(IValue x)
        {
            return (new PhpValueTranslator(state)).TransValue(x);
        }
        protected override IPhpStatement[] VisitReturnStatement(ReturnStatement src)
        {
            var value = src.ReturnValue == null ? null : TransValue(src.ReturnValue);
            var result = new PhpReturnStatement(value);
            return MkArray(result);
        }

        protected override IPhpStatement[] VisitIncrementDecrementExpression(IncrementDecrementExpression src)
        {
            var o = TransValue(src.Operand);
            var a = new PhpIncrementDecrementExpression(o, src.Increment, src.Pre);
            return MkArray(a);
        }
        private IPhpStatement TranslateStatementOne(IStatement x)
        {
            if (x == null)
                return null;
            var a = TranslateStatement(x);
            if (a.Length == 1)
                return a[0];
            return new PhpCodeBlock() { Statements = a.ToList() };
        }

        private IPhpStatement[] TranslateStatement(IStatement x)
        {
            if (x is CSharpBase)
                return Visit(x as CSharpBase);
            var trans = new Translator(state);
            return trans.TranslateStatement(x);
        }

        protected override IPhpStatement[] VisitBreakStatement(BreakStatement src)
        {
            return MkArray(new PhpBreakStatement());
        }
        protected override IPhpStatement[] VisitContinueStatement(ContinueStatement src)
        {
            return MkArray(new PhpContinueStatement());
        }


        protected override IPhpStatement[] VisitWhileStatement(WhileStatement src)
        {
            var c = TransValue(src.Condition);
            var s = TranslateStatementOne(src.Statement);
            var a = new PhpWhileStatement(c, s);
            return MkArray(a);
        }

        protected override IPhpStatement[] VisitVariableDeclaration(VariableDeclaration src)
        {
            //throw new Exception("DELETE THIS ??????");
            List<IPhpStatement> s = new List<IPhpStatement>();
            foreach (var i in src.Declarators)
            {
                var l = new PhpVariableExpression(PhpVariableExpression.AddDollar(i.Name), PhpVariableKind.Local);
                var r = TransValue(i.Value);
                var tt = new PhpAssignExpression(l, r);
                s.Add(new PhpExpressionStatement(tt));

                //var r = new PhpAssignVariable(PhpVariableExpression.AddDollar(i.Name), false);
                //r.Value = TV(i.Value);
                //s.Add(r);
            }
            return s.ToArray();
        }

        protected override IPhpStatement[] VisitAssignExpression(CsharpAssignExpression src)
        {

            IPhpValue translatedValue = TransValue(src as IValue);
            return MkArray(translatedValue);
        }

        protected override IPhpStatement[] VisitForEachStatement(ForEachStatement src)
        {
            var g = src.VarName;
            var collection = TransValue(src.Collection);
            var statement = TranslateStatementOne(src.Statement);
            PhpForEachStatement a = null;
            if (src.ItemType.DotnetType.IsGenericType)
            {
                var gtd = src.ItemType.DotnetType.GetGenericTypeDefinition();
                if (gtd == typeof(KeyValuePair<,>))
                {
                    a = new PhpForEachStatement(src.VarName, collection, statement);
                    // $i@Key
                    a.KeyVarname = src.VarName + "@Key";
                    a.ValueVarname = src.VarName + "@Value";
                }
            }
            if (a == null)
                a = new PhpForEachStatement(src.VarName, collection, statement);
            return MkArray(a);
        }
        protected override IPhpStatement[] VisitForStatement(ForStatement src)
        {
            var condition = TransValue(src.Condition);
            var statement = TranslateStatementOne(src.Statement);
            var incrementors = TranslateStatements(src.Incrementors);
            IPhpStatement[] declarations = TranslateStatement(src.Declaration).ToArray();
            List<PhpAssignExpression> phpDeclarations = new List<PhpAssignExpression>();
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
            var IfTrue = TranslateStatementOne(src.IfTrue);
            var IfFalse = TranslateStatementOne(src.IfFalse);
            var a = new PhpIfStatement(condition, IfTrue, IfFalse);
            return MkArray(a);
        }
        protected override IPhpStatement[] VisitMethodCallExpression(CsharpMethodCallExpression src)
        {
            var a = TransValue(src as IValue);
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


        protected override IPhpStatement[] VisitLocalDeclarationStatement(LocalDeclarationStatement src)
        {
            List<IPhpStatement> s = new List<IPhpStatement>();
            foreach (var i in src.Declaration.Declarators)
            {
                /// to jest przypadek z c# 'int x;', dla php można to pominąć
                if (i.Value == null)
                    continue;
                if (i.Value is UnknownIdentifierValue)
                    throw new NotImplementedException();
                var l = new PhpVariableExpression(PhpVariableExpression.AddDollar(i.Name), PhpVariableKind.Local);
                var r = TransValue(i.Value);
                var tt = new PhpAssignExpression(l, r);
                s.Add(new PhpExpressionStatement(tt));


                //var r = new PhpAssignVariable( PhpVariableExpression.AddDollar(i.Name), false );
                //// r.Name = "$" + i.Name;
                //r.Value = TV(i.Value);
                //s.Add(r);
            }
            return s.ToArray();
        }




    }
}
