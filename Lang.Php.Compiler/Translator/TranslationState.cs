using System;
using System.Linq;
using Lang.Cs.Compiler;

namespace Lang.Php.Compiler.Translator
{
    public sealed class TranslationState : IExternalTranslationContext
    {
        /// <summary>
        ///     Tworzy instancję obiektu
        ///     <param name="principles"></param>
        /// </summary>
        public TranslationState(TranslationInfo principles)
        {
            Principles = principles;
        }
        // Public Methods 

        public ClassReplaceInfo FindOneClassReplacer(Type srcType)
        {
            var type = srcType;
            if (type.IsGenericType)
                type      = type.GetGenericTypeDefinition();
            var replacers = ClassReplacers.Where(i => i.SourceType == type).ToArray();
            if (!replacers.Any()) return null;
            if (replacers.Length > 1)
                throw new Exception(string.Format("wise gecko, class replacers has more than 1 replacer for {0}",
                    type.FullName));
            var atype = replacers[0];

            if (srcType.IsGenericType)
            {
                var a     = srcType.GetGenericArguments();
                var gtype = atype.ReplaceBy.MakeGenericType(a);
                atype     = new ClassReplaceInfo(srcType, gtype);
            }

            return atype;
        }
        // Private Methods 

        TranslationInfo IExternalTranslationContext.GetTranslationInfo()
        {
            return Principles;
        }

        IPhpValue IExternalTranslationContext.TranslateValue(IValue srcValue)
        {
            //var aa = new ExpressionSimplifier(new OptimizeOptions());
            //srcValue = aa.Simplify(srcValue);

            var t = new PhpValueTranslator(this);
            var g = t.TransValue(srcValue);

            return g;
        }

        public ClassReplaceInfo[] ClassReplacers => _cl ?? (_cl = (from assembly in Principles.TranslationAssemblies
                                                        from type in assembly.GetTypes()
                                                        let attributes = type.GetCustomAttributes(false)
                                                            .OfType<ReplaceAttribute>().ToArray()
                                                        where attributes != null && attributes.Any()
                                                        from attribute in attributes
                                                        select new ClassReplaceInfo(attribute.ReplacedType, type)
                                                    ).Distinct().ToArray());


        /// <summary>
        ///     Własność jest tylko do odczytu.
        /// </summary>
        public TranslationInfo Principles { get; }

        /// <summary>
        /// </summary>
        public Version PhpVersion { get; set; } = new Version(5, 3, 0);

        private ClassReplaceInfo[] _cl;
    }
}