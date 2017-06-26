namespace Lang.Php.Compiler
{
    public class KnownConstInfo
    {
        public KnownConstInfo(string Name, object Value, bool UseFixedValue)
        {
            if (!Name.StartsWith("\\"))
                Name = "\\" + Name;
            this.Name = Name;
            this.Value = Value;
            this.UseFixedValue = UseFixedValue;
        }
        /// <summary>
        /// Name of defined const
        /// </summary>
        public string Name { get; private set; }

        public object Value { get; set; }

        /// <summary>
        /// if true- const is not defined in cs2php.php and fixed value is used instead of const name
        /// if false- constant is defined in cs2php.php and it is used in expressions
        /// </summary>
        public bool UseFixedValue { get; private set; }
    }

}
