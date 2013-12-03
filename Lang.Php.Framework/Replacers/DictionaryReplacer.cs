using Lang.Cs.Compiler;
using Lang.Php.Compiler;
using Lang.Php.Compiler.Source;
using Lang.Php;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Framework.Replacers
{
    [Replace(typeof(Dictionary<,>))]
    class DictionaryReplacer<TKey, TValue>
    {

        [Translator]
        public static object __Translate(IExternalTranslationContext ctx, object src)
        {
            CallConstructor x = src as CallConstructor;
            if (x == null) return null;
            if (x.Arguments.Length != 0)
                throw new NotSupportedException();
            var g = new PhpArrayCreateExpression();
            var a = x.Initializers.Cast<IValueTable_PseudoValue>().ToArray();
            List<IPhpValue> o = new List<IPhpValue>();
            foreach (var i in a)
            {
                var a1 = i as IValueTable_PseudoValue;
                var a2 = a1.Items[0];
                var a3 = a1.Items[1];
                var key = ctx.TranslateValue(a2);
                var value = ctx.TranslateValue(a3);
                var t = new PhpAssignExpression(key, value);
                o.Add(t);
            }
            g.Initializers = o.ToArray();
            return g;            
        }


        [DirectCall("array_key_exists", "0,this")]
        public bool ContainsKey(TKey key)
        {
            throw new MockMethodException();
        }

        #region Methods

        // Public Methods 

        [DirectCall("array_key_exists", "0,this")]
        public bool ContainsKey(object key)
        {
            //  bool array_key_exists ( mixed $key , array $array )
            throw new MockMethodException();
        }

        #endregion Methods

        #region Properties

        [DirectCall("count", "this")]
        public int Count
        {
            get;
            set;
        }

        #endregion Properties
    }
}
