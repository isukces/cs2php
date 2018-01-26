namespace Lang.Php.Compiler.Source
{
    public class PhpMethodArgument
    {
        // Public Methods 

        public string GetPhpCode(PhpEmitStyle s)
        {
            s      = s ?? new PhpEmitStyle();
            var eq = s.Compression == EmitStyleCompression.Beauty ? " = " : "=";
            var d  = DefaultValue != null ? eq + DefaultValue.GetPhpCode(s) : "";
            return string.Format("${0}{1}", _name, d);
        }

        public override string ToString()
        {
            return GetPhpCode(null);
        }


        /// <summary>
        ///     Nazwa argumentu
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = (value ?? string.Empty).Trim();
        }

        /// <summary>
        /// </summary>
        public IPhpValue DefaultValue { get; set; }

        private string _name = string.Empty;
    }
}