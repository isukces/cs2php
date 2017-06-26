namespace Lang.Php 
{
    [Skip]    
    public class MySQLiCharsetInfo
    {
		#region Properties 
        /// <summary>
        /// Character set name
        /// </summary>
        [DirectCall("->$charset")]
        public string Charset { get; set; }

        /// <summary>
        /// Collation name
        /// </summary>
        [DirectCall("->$collation")]
        public string Collation { get; set; }

        [DirectCall("->$comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Directory the charset description was fetched from (?) or "" for built-in character sets
        /// </summary>
        [DirectCall("->$dir")]
        public string Dir { get; set; }

        /// <summary>
        /// Maximum character length in bytes
        /// </summary>
        [DirectCall("->$max_length")]
        public int MaxLength { get; set; }
        /// <summary>
        /// Minimum character length in bytes
        /// </summary>
        [DirectCall("->$min_length")]
        public int MinLength { get; set; }
        /// <summary>
        /// Internal character set number
        /// </summary>
        [DirectCall("->$number")]
        public int Number { get; set; }

        /// <summary>
        /// Character set status (?)
        /// </summary>
        [DirectCall("->$state")]
        public int State { get; set; }

		#endregion Properties 

        /*
          object(stdClass)[4]
  public 'charset' => string 'latin1' (length=6)
  public 'collation' => string 'latin1_swedish_ci' (length=17)
  public 'dir' => string '' (length=0)
  public 'min_length' => int 1
  public 'max_length' => int 1
  public 'number' => int 8
  public 'state' => int 1
  public 'comment' => string '' (length=0)
         */
    }
}
