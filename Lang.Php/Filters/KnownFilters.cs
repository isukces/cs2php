namespace Lang.Php.Filters
{
    [EnumRender(EnumRenderOptions.UnderscoreUppercase, true)]
    public enum KnownFilters
    {
        FilterValidateBoolean,
        FilterValidateEmail,
        FilterValidateFloat,
        FilterValidateInt,
        FilterValidateIp,
        FilterValidateRegexp,
        FilterValidateUrl
    }
}
