using System;

namespace Lang.Php.Examples.BasicFeaturesExample
{
    [Module("generics")]
    public class Generics
    {

        public static void CallIssue2()
        {
            DateTime a = Issue2<DateTime>();
            var b = Issue2<Object>();
        }

        public static T Issue2<T>() where T : new()
        {
            return new T();
        }

    }
}
