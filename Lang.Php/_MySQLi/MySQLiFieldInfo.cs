namespace Lang.Php
{
    [Skip]
    [IgnoreNamespace]
    [ScriptName("stdClass")]

    public class MySQLiFieldInfo
    {

		#region Fields 

        /// <summary>
        /// The name of the column
        /// </summary>
        [ScriptName("name")]
        public string Name;

        /// <summary>
        /// Original column name if an alias was specified
        /// </summary>
        [ScriptName("orgname")]
        public string OrgName;

        /// <summary>
        /// The name of the table this field belongs to (if not calculated)
        /// </summary>
        [ScriptName("table")]
        public string Table;

        /// <summary>
        /// Original table name if an alias was specified
        /// </summary>
        [ScriptName("orgtable")]
        public string OrgTable;

        /// <summary>
        /// Database (since PHP 5.3.6)
        /// </summary>
        [ScriptName("db")]
        [Since("5.3.6")]
        public string Database;

        /// <summary>
        /// The maximum width of the field for the result set.
        /// </summary>
        [ScriptName("max_length")]
        public int MaxLength;

        /// <summary>
        /// The width of the field, as specified in the table definition.
        /// </summary>
        [ScriptName("length")]
        public int Length;

        /// <summary>
        /// The character set number for the field.
        /// </summary>
        [ScriptName("charsetnr")]
        public int CharsetNr;

        /// <summary>
        /// An integer representing the bit-flags for the field.
        /// </summary>
        [ScriptName("flags")]
        public MySQLFlags Flags;

        /// <summary>
        /// The data type used for this field
        /// </summary>
        [ScriptName("type")]
        public MySQLFieldTypes Type;

        /// <summary>
        /// The number of decimals used (for integer fields)
        /// </summary>
        [ScriptName("decimals")]
        public int Decimals;

		#endregion Fields 

        /*
def 	Reserved for default value, currently always ""
catalog 	The catalog name, always "def" (since PHP 5.3.6)
         */
    }
}
