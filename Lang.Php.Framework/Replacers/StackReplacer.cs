using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lang.Php.Framework.Replacers
{
    [Replace(typeof(Stack<>))]
    public class StackReplacer<T>
    {
        [DirectCall("count", "this")]
        public int Count
        {
            get;
            set;
        }

        [DirectCall("array_push", "this,0")]
        public void Push(T item)
        {
            // int array_push ( array &$array , mixed $value1 [, mixed $... ] )
        }

        [DirectCall("NOT_SUPPORTED", "this")]
        public string Peek()
        {
            return "";
        }

        [DirectCall("array_pop", "this")]
        public string Pop()
        {
            return "";
        }
    }
}
