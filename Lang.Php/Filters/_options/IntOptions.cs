namespace Lang.Php.Filters
{
    [AsArray]
    public class IntOptions
    {
        [ScriptName("default")]
        public int Default;
        [ScriptName("min_range")]
        public int MinRange;
        [ScriptName("max_range")]
        public int MaxRange;
    }
}
