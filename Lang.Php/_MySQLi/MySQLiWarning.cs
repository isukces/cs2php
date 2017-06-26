namespace Lang.Php
{
    [Skip]
    [ScriptName("\\mysqli_warning")]
    public class MySQLiWarning
    {
		#region Methods 

		// Public Methods 

        [DirectCall("->next")]
        public void Next()
        {

        }

		#endregion Methods 

		#region Properties 

        [DirectCall("->$errno")]
        public object Eerrno { get; set; }

        [DirectCall("->$message")]
        public string message { get; set; }

        [DirectCall("->$sqlstate")]
        public object SqlState { get; set; }

		#endregion Properties 

        /* Properties 
public $message ;
public $sqlstate ;
public $errno ;
  Methods  
public __construct ( void )
public void next ( void )
         */
    }
}
