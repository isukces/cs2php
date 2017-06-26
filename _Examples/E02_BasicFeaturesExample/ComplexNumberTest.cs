namespace Lang.Php.Examples.BasicFeaturesExample
{
    [Page("complex-number-test")]
    public class ComplexNumberTest
    {
        public static void PhpMain()
        {
            var a = new ComplexNumber(3, 2);
            ComplexNumber b = 12;
            var c = a * b;
            var d = a + b;

            var e = (a + 17) * 3;
            var m1 = new ComplexNumber(3, 2) * new ComplexNumber(4, 5);
            var m2 = new ComplexNumber(3, 2) * 5;

        }
    }
}
