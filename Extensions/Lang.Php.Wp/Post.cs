namespace Lang.Php.Wp
{
    [BuiltIn]
    public class Post
    {
        [GlobalVariable("$post")]
        public static Post Current;

        public int ID;
    }
}
