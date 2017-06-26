using System;

namespace Lang.Php.Filters
{
    [Flags]
    public enum UrlFlags
    {
        [RenderValue("FILTER_FLAG_PATH_REQUIRED")]
        PathRequired,
        [RenderValue("FILTER_FLAG_QUERY_REQUIRED")]
        QueryRequired
    }
}
