using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lang.Php;

namespace Lang.Php.Test.Code
{
    [IgnoreNamespace]
    [ScriptName("MyCodePhp")]
    public class MyCode
    {
        public static void BasicMath1()
        {
            var a = 1;
            var b = 2;
            var d = (a + b) / Math.PI;
        }

        public static void CostantsAndVariables()
        {
            object a = null;
            int b = 0x12;
            double c = 1.2;
            double pi = Math.PI;
            string h = "Some text backslash\\ quote\" single' dollar$";

        }

        public static void Collections()
        {
            // List
            List<string> list = new List<string>();
            foreach (var i in list)
                PhpDummy.echo(i);
            var count = list.Count;

            // Dictionary
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var i in dict)
                PhpDummy.echo(i.Key + " " + i.Value);
            foreach (var i in dict.Values)
                PhpDummy.echo(i);
            foreach (var i in dict.Keys)
                PhpDummy.echo(i);
            // var values = dict.Values;
            // var keys = dict.Keys;
            count = dict.Count;

            // Stack
            Stack<string> stack = new Stack<string>();
            stack.Push("X");
            stack.Push("Y");
            var s1 = stack.Peek();
            var s2 = stack.Pop();
            count = stack.Count;
        }
    }
}
