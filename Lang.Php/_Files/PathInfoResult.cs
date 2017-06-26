namespace Lang.Php
{
    [Skip]
    [AsArray]
    public class PathInfoResult
    {
        [ScriptName("dirname")]
        public string Dirname { get; set; }
        [ScriptName("basename")]
        public string Basename { get; set; }
        [ScriptName("extension")]
        public string Extension { get; set; }
        [ScriptName("filename")]
        [Since("5.2.0")]
        public string Filename { get; set; }
    }
}
