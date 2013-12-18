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
    [Replace(typeof(List<>))]
    [Replace(typeof(IList<>))]
    [Replace(typeof(ICollection<>))]
    class ListReplacer<TValue>
    {

        [DirectCall("array_push", "this,0")]
        public void Add(TValue item)
        {
            //int array_push ( array &$array , mixed $value1 [, mixed $... ] )
            throw new MockMethodException();
        }

        [DirectCall("count", "this")]
        public int Count
        {
            get
            {
                throw new MockMethodException();
            }
        }

        //[Translator]
        //public static object __Translate(IExternalTranslationContext ctx, object src1)
        //{
        //    CallConstructor src = src1 as CallConstructor;
        //    if (src == null) return null;
        //    if (src.Arguments.Length != 0)
        //        throw new NotSupportedException();
        //    var g = new PhpArrayCreateExpression();
        //    return g;
        //}

        [DirectCall("", "this")]
        public TValue[] ToArray()
        {
            throw new MockMethodException();
        }

    }
}
