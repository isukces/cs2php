using System;
using System.Collections.Generic;
using Lang.Php.Compiler;
using Lang.Cs.Compiler;
using System.Reflection;
using Lang.Php.Compiler.Source;

namespace Lang.Php.Wp.Compile
{
    class MethodCallConverter : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        #region Static Methods

        // Private Methods 
        public int GetPriority()
        {
            return 1;
        }

        private static IPhpValue _AddAction(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var mm = src.MethodInfo.GetCustomAttribute<HookAttribute>();
            if (mm != null)
            {
                List<IPhpValue> a = new List<IPhpValue>();
                a.Add(ctx.TranslateValue(new ConstValue(mm.Hook)));
                foreach (var i in src.Arguments)
                    a.Add(ctx.TranslateValue(i.MyValue));
                var phpMethod = new PhpMethodCallExpression("add_action", a.ToArray());
                return phpMethod;
            }
            throw new NotSupportedException();
        }

        private static IPhpValue _AddFilter(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var methodName = src.MethodInfo.Name;
            WpTags tag;
            if (Enum.TryParse(methodName, out tag))
            {
                var tt = typeof(WpTags);
                var methodCallExpression = new PhpMethodCallExpression("add_filter");
                var fi = tt.GetField(methodName);
                var sss = new ClassFieldAccessExpression(fi, fi.IsStatic);
                var sss1 = ctx.TranslateValue(sss);
                IValue v = new ConstValue(tag);
                var gg = ctx.TranslateValue(v);

                methodCallExpression.Arguments.Add(new PhpMethodInvokeValue(sss1));
                foreach (var argument in src.Arguments)
                {
                    var phpValue = ctx.TranslateValue(argument.MyValue);
                    methodCallExpression.Arguments.Add(new PhpMethodInvokeValue(phpValue));
                }
                return methodCallExpression;
            }
            throw new NotImplementedException();
        }

        private static IPhpValue _WpPost(CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.Name == "PostType")
            {
                var a1 = new PhpVariableExpression("$_POST", PhpVariableKind.Global);
                var a2 = new PhpArrayAccessExpression(a1, new PhpConstValue("post_type"));
                return a2;
            }
            return null;
            throw new NotSupportedException();
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {

            var declaringType = src.MethodInfo.DeclaringType;
            if (declaringType == typeof(AddAction))
                return _AddAction(ctx, src);
            if (declaringType == typeof(Wp))
            {
                return null;
                throw new NotSupportedException();
            }
            if (declaringType == typeof(WpPost))
                return _WpPost(src);
            if (declaringType == typeof(AddFilter))
                return _AddFilter(ctx, src);
            return null;
        }

        #endregion Methods
    }
}
