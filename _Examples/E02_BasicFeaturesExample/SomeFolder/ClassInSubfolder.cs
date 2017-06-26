namespace Lang.Php.Examples.BasicFeaturesExample.SomeFolder
{

    [Module("SomeFolder/ClassInSubfolder")]
    public class ClassInSubfolder
    {
        public static void Hello()
        {
            PhpDummy.echo("ClassInSubfolder hello");
        }
    }
}
