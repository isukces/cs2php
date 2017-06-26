namespace Lang.Php.Examples.HelloWorld
{

    [Page("index")]
    public class Index : PhpDummy
    {
        public static void PhpMain()
        {
            echo("Hello world!");
        }
    }
}
