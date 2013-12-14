using Lang.Cs.Compiler;
using Lang.Php.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang.Php.Compiler.Source;
using Lang.Php.Runtime;

namespace Lang.Php.Compiler.Translator.Node
{
    public class FilterTranslator : IPhpNodeTranslator<CsharpMethodCallExpression>
    {
        #region Static Methods

        // Private Methods 

        private static IPhpValue _FilterInput(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var mn = src.MethodInfo.Name;
            var fn = src.MethodInfo.ToString();
            if (fn == "System.Nullable`1[System.Int32] ValidateInt(Type, System.String, Lang.Php.Filters.IntFlags, Lang.Php.Filters.IntOptions)")
                return Make_FilterInput(
                    ctx.TranslateValue(src.Arguments[0]), ctx.TranslateValue(src.Arguments[1]),
                    FILTER_VALIDATE_INT,
                    src.Arguments.Length < 4 ? null : ctx.TranslateValue(src.Arguments[3]),
                    ctx.TranslateValue(src.Arguments[2])
                    );
            if (fn == "System.Nullable`1[System.Boolean] ValidateBoolean(Type, System.String)")
            {
                return Make_FilterInput(
                      ctx.TranslateValue(src.Arguments[0]), ctx.TranslateValue(src.Arguments[1]),
                      FILTER_VALIDATE_BOOLEAN,
                      null,
                      FILTER_NULL_ON_FAILURE
                  );
            }
            if (fn == "Boolean ValidateBoolean(Type, System.String, Boolean)")
            {
                var options = PhpArrayCreateExpression.MakeKeyValue(
                  new PhpConstValue("default"), ctx.TranslateValue(src.Arguments[2].MyValue));
                return Make_FilterInput(
                      ctx.TranslateValue(src.Arguments[0]), ctx.TranslateValue(src.Arguments[1]),
                      FILTER_VALIDATE_BOOLEAN,
                      options);
            }
            throw new NotImplementedException();
        }

        private static IPhpValue _FilterInputServer(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var mn = src.MethodInfo.Name;
            var fn = src.MethodInfo.ToString();
            if (fn == "System.String ValidateIp(Lang.Php.Filters.ServerVariables, Lang.Php.Filters.IpFlags, Lang.Php.Filters.IpOptions)")
            {
                var filter = KnownFilters.FilterValidateIp;
                var result = MakeFilter(ctx, src, filter);
                return result;
            }
            if (fn == "Boolean ValidateBoolean(Lang.Php.Filters.ServerVariables, Boolean)")
                return Make_FilterInput(
                      new PhpConstValue(FilterInput.Type.Server),
                      ctx.TranslateValue(src.Arguments[0]),
                      FILTER_VALIDATE_BOOLEAN,
                      PhpArrayCreateExpression.MakeKeyValue(new PhpConstValue("default"), ctx.TranslateValue(src.Arguments[1].MyValue))
                  );
            if (fn == "System.Nullable`1[System.Boolean] ValidateBoolean(Lang.Php.Filters.ServerVariables)")
                return Make_FilterInput(
                        new PhpConstValue(FilterInput.Type.Server),
                        ctx.TranslateValue(src.Arguments[0]),
                        FILTER_VALIDATE_BOOLEAN,
                        null,
                        FILTER_NULL_ON_FAILURE
                    );
            throw new NotImplementedException();
        }

        private static IPhpValue Make_FilterInput(IPhpValue type, IPhpValue name, IPhpValue filter, IPhpValue options, IPhpValue flags = null)
        {
            // mixed filter_input ( int $type , string $variable_name [, int $filter = FILTER_DEFAULT [, mixed $options ]] )
            var result = new PhpMethodCallExpression("filter_input",
                type,
                name,
                filter,
                MakeOptionsFlags(options, flags)
                );
            return result;
        }

        private static IPhpValue Make_FilterVar(IPhpValue value, IPhpValue filter, IPhpValue options, IPhpValue flags = null)
        {
            // mixed filter_var ( mixed $variable [, int $filter = FILTER_DEFAULT [, mixed $options ]] )
            var result = new PhpMethodCallExpression("filter_var",
                value,
                filter,
                MakeOptionsFlags(options, flags)
                );
            return result;
        }

        private static PhpMethodCallExpression MakeFilter(IExternalTranslationContext ctx, CsharpMethodCallExpression src, KnownFilters filter)
        {
            List<IPhpValue> v = new List<IPhpValue>();
            v.Add(new PhpDefinedConstExpression("INPUT_SERVER", null));
            v.Add(ctx.TranslateValue(src.Arguments[0]));
            v.Add(new PhpConstValue(filter));
            v.Add(ctx.TranslateValue(src.Arguments[1]));

            if (src.Arguments.Length > 2)
                v.Add(ctx.TranslateValue(src.Arguments[2]));
            var result = new PhpMethodCallExpression("filter_input", v.ToArray());
            return result;
        }

        static PhpMethodInvokeValue MakeOptionsFlags(IPhpValue options, IPhpValue flags)
        {
            if (flags == null)
                return new PhpMethodInvokeValue(options);
            if (options == null)
                return new PhpMethodInvokeValue(flags);
            var joined = PhpArrayCreateExpression.MakeKeyValue(
                                new PhpConstValue("options"), options,
                                new PhpConstValue("flags"), flags
                                );
            return new PhpMethodInvokeValue(joined);
            // new PhpConstValue("flags"), a1);
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public int getPriority()
        {
            return 1;
        }

        public IPhpValue TranslateToPhp(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            if (src.MethodInfo.DeclaringType == typeof(FilterInputServer))
                return _FilterInputServer(ctx, src);
            if (src.MethodInfo.DeclaringType == typeof(FilterInput))
                return _FilterInput(ctx, src);
            if (src.MethodInfo.DeclaringType == typeof(FilterVar))
                return _FilterVar(ctx, src);
            return null;
        }
        // Private Methods 

        private IPhpValue _FilterVar(IExternalTranslationContext ctx, CsharpMethodCallExpression src)
        {
            var mn = src.MethodInfo.Name;
            var fn = src.MethodInfo.ToString();
            #region ValidateBoolean(System.Object)
            if (fn == "System.Nullable`1[System.Boolean] ValidateBoolean(System.Object)")
            {
                var filter_var = Make_FilterVar(
                       ctx.TranslateValue(src.Arguments[0].MyValue),
                       FILTER_VALIDATE_BOOLEAN,
                       null,
                       FILTER_NULL_ON_FAILURE
                   );
                return filter_var;
            }
            #endregion
            #region ValidateBoolean(System.Object, Boolean)
            if (fn == "Boolean ValidateBoolean(System.Object, Boolean)") // checked !!!!
                return Make_FilterVar(
                      ctx.TranslateValue(src.Arguments[0].MyValue),
                      FILTER_VALIDATE_BOOLEAN,
                      PhpArrayCreateExpression.MakeKeyValue(new PhpConstValue("default"), ctx.TranslateValue(src.Arguments[1].MyValue)),
                      null
                  );
            #endregion

            throw new NotImplementedException();
        }

        #endregion Methods

        #region Static Properties

        private static IPhpValue FILTER_NULL_ON_FAILURE
        {
            get
            {
                return new PhpDefinedConstExpression("FILTER_NULL_ON_FAILURE", null);
            }
        }

        private static IPhpValue FILTER_VALIDATE_BOOLEAN
        {
            get
            {
                return new PhpConstValue(KnownFilters.FilterValidateBoolean);
            }
        }

        private static IPhpValue FILTER_VALIDATE_INT
        {
            get
            {
                return new PhpConstValue(KnownFilters.FilterValidateInt);
            }
        }

        #endregion Static Properties
    }
}
