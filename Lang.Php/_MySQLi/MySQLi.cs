using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{

    [Skip]
    [ScriptName("\\mysqli")]
    public class MySQLi
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="username"></param>
        /// <param name="passwd"></param>
        /// <param name="dbname"></param>
        /// <param name="port"></param>
        /// <param name="socket"></param>
        public MySQLi(
            string host = default_host,
            string username = default_user,
            string passwd = default_pw,
            string dbname = "",
            int port = default_port,
            string socket = default_socket)
        {

        }

        #endregion Constructors

        #region Methods

        // Public Methods 

        [DirectCall("->autocommit")]
        public bool AutoCommit(bool mode)
        {
            // http://www.php.net/manual/en/mysqli.autocommit.php
            throw new NotImplementedException();
        }

        [DirectCall("->character_set_name")]
        public string CharacterSetName()
        {
            // http://www.php.net/manual/en/mysqli.character-set-name.php
            throw new NotImplementedException();
        }

        [DirectCall("->close")]
        public bool Close()
        {
            return true;
        }

        [DirectCall("->commit")]
        public bool Commit(int flags = 0, string name = null)
        {
            // bool commit ([ int $flags [, string $name ]] )
            return true;
        }

        [DirectCall("->rollback")]
        public bool Rollback(int flags = 0, string name = null)
        {
            // bool rollback ([ int $flags [, string $name ]] )
            return true;
        }

        [DirectCall("->set_charset", "0")]
        public bool SetCharset(string charset)
        {
            // http://www.php.net/manual/en/mysqli.set-charset.php
            throw new NotImplementedException();
        }


        // public mixed query ( string $query [, int $resultmode = MYSQLI_STORE_RESULT ] )


        /// <summary>
        /// Query version for INSERT, UPDATE, DELETE etc.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [DirectCall("->query")]
        [Since("5.3.0")]
        public bool QueryNonResult(string query)
        {
            // mixed query ( string $query [, int $resultmode = MYSQLI_STORE_RESULT ] )
            // Returns FALSE on failure. For successful SELECT, SHOW, DESCRIBE or EXPLAIN queries mysqli_query() 
            // will return a mysqli_result object. 
            // For other successful queries mysqli_query() will return TRUE.
            throw new NotImplementedException();
        }
        /// <summary>
        /// Query version for SELECT, SHOW, DESCRIBE or EXPLAIN
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [DirectCall("->query")]
        [Since("5.3.0")]
        public Falsable<MySQLiResult> QueryResult(string query, MySQLiResultMode resultMode = MySQLiResultMode.StoreResult)
        {
            // mixed query ( string $query [, int $resultmode = MYSQLI_STORE_RESULT ] )
            // Returns FALSE on failure. For successful SELECT, SHOW, DESCRIBE or EXPLAIN queries mysqli_query() 
            // will return a mysqli_result object. 
            // For other successful queries mysqli_query() will return TRUE.
            throw new NotImplementedException();
        }

        #endregion Methods

        #region Fields

        /// <summary>
        /// ini_get("mysqli.default_host")
        /// </summary>
        public const string default_host = "*default*";
        /// <summary>
        /// ini_get("mysqli.default_port")
        /// </summary>
        public const int default_port = -1;
        /// <summary>
        /// ini_get("mysqli.default_pw")
        /// </summary>
        public const string default_pw = "*default*";
        /// <summary>
        /// ini_get("mysqli.default_socket")
        /// </summary>
        public const string default_socket = "*default*";
        /// <summary>
        /// ini_get("mysqli.default_user")
        /// </summary>
        public const string default_user = "*default*";

        #endregion Fields

        #region Properties

        [DirectCall("$->affected_rows")]
        public int AffectedRows
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [DirectCall("$->connect_errno")]
        public string ConnectErrno
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [DirectCall("->$connect_error")]
        public string ConnectError
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [DirectCall("->$errno")]
        public int errno
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [DirectCall("->$error")]
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion Properties

        /* Methods 
__construct ([ string $host = ini_get("mysqli.default_host") [, string $username = ini_get("mysqli.default_user") [, string $passwd = ini_get("mysqli.default_pw") [, string $dbname = "" [, int $port = ini_get("mysqli.default_port") [, string $socket = ini_get("mysqli.default_socket") ]]]]]] )
bool change_user ( string $user , string $password , string $database )
bool debug ( string $message )
bool dump_debug_info ( void )
object get_charset ( void )
string get_client_info ( void )
bool get_connection_stats ( void )
mysqli_warning get_warnings ( void )
mysqli init ( void )
bool kill ( int $processid )
bool more_results ( void )
bool multi_query ( string $query )
bool next_result ( void )
bool options ( int $option , mixed $value )
bool ping ( void )
public static int poll ( array &$read , array &$error , array &$reject , int $sec [, int $usec ] )
mysqli_stmt prepare ( string $query )

bool real_connect ([ string $host [, string $username [, string $passwd [, string $dbname [, int $port [, string $socket [, int $flags ]]]]]]] )
string escape_string ( string $escapestr )
bool real_query ( string $query )
public mysqli_result reap_async_query ( void )
public bool refresh ( int $options )
int rpl_query_type ( string $query )
bool select_db ( string $dbname )

bool set_local_infile_handler ( mysqli $link , callable $read_func )
bool ssl_set ( string $key , string $cert , string $ca , string $capath , string $cipher )
string stat ( void )
mysqli_stmt stmt_init ( void )
mysqli_result store_result ( void )
mysqli_result use_result ( void ) */
        /* Properties
string $client_info;
int $client_version;
        array $error_list;
int $field_count;
int $client_version;
string $host_info;
string $protocol_version;
string $server_info;
int $server_version;
string $info;
mixed $insert_id;
string $sqlstate;
int $thread_id;
int $warning_count;
         */
        /*  DEPRECATED Methods
 This function has been DEPRECATED and REMOVED as of PHP 5.3.0.
bool send_query ( string $query )
         * */
    }
}
