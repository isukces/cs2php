namespace Lang.Php.Framework.Replacers
{

    [Replace(typeof(object))]
    class ObjectReplacer
    {
        [DirectCall("", "this")]
        public override string ToString()
        {
            throw new MockMethodException();
        }
    }
}
