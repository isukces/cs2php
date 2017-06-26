namespace Lang.Php.Framework.Replacers
{
    [Replace(typeof(char))]
    class CharReplacer
    {
        [DirectCall("", "this")]
        public override string ToString()
        {
            throw new MockMethodException();
        }
    }
}
