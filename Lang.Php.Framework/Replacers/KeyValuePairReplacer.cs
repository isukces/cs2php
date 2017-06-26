using System.Collections.Generic;

namespace Lang.Php.Framework.Replacers
{

    [Replace(typeof(KeyValuePair<,>))]
    class KeyValuePairReplacer<TKey, TValue>
    {

        //// [UseTranslator("Lang.Php.Compiler.Translator.AdvancedTranslator", "__KeyValue__PseudoTranslate")]
        //public TKey Key { get; set; }
        //// [UseTranslator("Lang.Php.Compiler.Translator.AdvancedTranslator", "__KeyValue__PseudoTranslate")]
        //public TValue Value { get; set; }
    }
}
