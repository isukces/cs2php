namespace Lang.Php.Compiler
{
    public class KnownConstInfo
    {
        public KnownConstInfo(string name, object value, bool useFixedValue)
        {
            if (!name.StartsWith("\\"))
                name      = "\\" + name;
            Name          = name;
            Value         = value;
            UseFixedValue = useFixedValue;
        }

        /// <summary>
        ///     Name of defined const
        /// </summary>
        public string Name { get; private set; }

        public object Value { get; set; }

        /// <summary>
        ///     if true- const is not defined in cs2php.php and fixed value is used instead of const name
        ///     if false- constant is defined in cs2php.php and it is used in expressions
        /// </summary>
        public bool UseFixedValue { get; private set; }
    }
}