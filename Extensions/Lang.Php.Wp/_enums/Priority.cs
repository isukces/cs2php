namespace Lang.Php.Wp
{
    public enum Priority
    {
        [RenderValue("'high'")]
        High,
        [RenderValue("'core'")]
        Core,
        [RenderValue("'default'")]
        Default,
        [RenderValue("'low'")]
        Low
    }
}
