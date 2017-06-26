using Lang.Php.Runtime;
using MySql.Data.MySqlClient;
using System;

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
            string host = TAKE_FROM_INI,
            string username = TAKE_FROM_INI,
            string passwd = TAKE_FROM_INI,
            string dbname = "",
            int port = default_port,
            string socket = TAKE_FROM_INI)
        {
            if (host == TAKE_FROM_INI)
                PhpIni.TryGetValue("mysqli.default_host", out host);
            if (username == TAKE_FROM_INI)
                PhpIni.TryGetValue("mysqli.default_user", out username);
            if (passwd == TAKE_FROM_INI)
                PhpIni.TryGetValue("mysqli.default_pw", out passwd);
            if (port == default_port)
            {
                string portStr;
                if (PhpIni.TryGetValue("mysqli.default_port", out portStr))
                    port = int.Parse(portStr);
                else
                    port = PhpIni.MYSQL_DEFAULT_PORT;
            }
            if (socket == TAKE_FROM_INI)
                PhpIni.TryGetValue("mysqli.default_socket", out socket);
            //  MySQL a = new MySQL();
            MySqlConnectionStringBuilder b = new MySqlConnectionStringBuilder();
            b.Server = host;
            b.UserID = username;
            b.Port = (uint)port;
            if (!string.IsNullOrEmpty(passwd))
                b.Password = passwd;
            _connection = new MySqlConnection(b.ToString());
            try
            {
                _connection.Open();
                if (!string.IsNullOrEmpty(dbname))
                    _connection.ChangeDatabase(dbname);
            }
            catch (MySqlException e)
            {
                connectError = e.Message;
                connectErrno = e.Number;
            }
            catch (Exception e)
            {
                connectError = e.Message;
            }

        }

        #endregion Constructors

        public const int ERR_UNABLE_TO_CONNECT_TO_MYSQL_HOST = 1042;
        public const int ERR_ACCESS_DENIED = 1044;

        #region Methods

        /// <summary>
        /// Prepares the SQL query, and returns a statement handle to be used for further 
        /// operations on the statement. 
        /// The query must consist of a single SQL statement. 
        /// See http://www.php.net/manual/en/mysqli.prepare.php
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [DirectCall("->prepare")]
        public Falsable<MySQLiStatement> Prepare(string query)
        {
            throw new NotImplementedException();
        }
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
            _connection.Close();
            return true;
        }

        [DirectCall("->commit")]
        public bool Commit(int flags = 0, string name = null)
        {
            // bool commit ([ int $flags [, string $name ]] )
            return true;
        }

        [DirectCall("->escape_string")]
        public string EscapeString(string escapestr)
        {
            return escapestr
                .Replace("\\", "\\\\")
                .Replace("\r", "\\\r")
                .Replace("\n", "\\\n")
                .Replace("\t", "\\\t")
                .Replace("\b", "\\\b")
                .Replace("'", "''");

            /*
             * mysql> SELECT 'hello', '"hello"', '""hello""', 'hel''lo', '\'hello';
            +-------+---------+-----------+--------+--------+
            | hello | "hello" | ""hello"" | hel'lo | 'hello |
            +-------+---------+-----------+--------+--------+

            mysql> SELECT "hello", "'hello'", "''hello''", "hel""lo", "\"hello";
            +-------+---------+-----------+--------+--------+
            | hello | 'hello' | ''hello'' | hel"lo | "hello |
            +-------+---------+-----------+--------+--------+

            mysql> SELECT 'This\nIs\nFour\nLines';
            +--------------------+
            | This
            Is
            Four
            Lines |
            +--------------------+

            mysql> SELECT 'disappearing\ backslash';
            +------------------------+
            | disappearing backslash |
            +------------------------+
             */
        }

        [DirectCall("->get_charset")]
        public MySQLiCharsetInfo GetCharset()
        {
            throw new NotImplementedException();
        }

        [DirectCall("->get_warnings")]
        public MySQLiWarning GetWarnings()
        {
            //mysqli_warning get_warnings ( void )
            return null;
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
            var q = new MySqlCommand(query, _connection);
            using (MySqlDataReader reader = q.ExecuteReader())
                return new RuntimeMySQLiResult(this, reader);
        }

        [DirectCall("->rollback")]
        public bool Rollback(int flags = 0, string name = null)
        {
            // bool rollback ([ int $flags [, string $name ]] )
            return true;
        }

        [DirectCall("->set_charset")]
        public bool SetCharset(string charset)
        {
            // http://www.php.net/manual/en/mysqli.set-charset.php
            if (WasSuccessfulConnection)
            {
                var a = new MySqlCommand("set names " + charset, _connection);
                a.ExecuteNonQuery();
                _lastCharset = charset;
                return true;
            }
            else
                return false;
        }

        #endregion Methods

        #region Fields

        MySqlConnection _connection;
        string _lastCharset;
        int connectErrno;
        string connectError;
        /// <summary>
        /// ini_get("mysqli.default_port")
        /// </summary>
        public const int default_port = -1;
        internal RuntimeMySQLiResult OpenResult;
        /// <summary>
        /// It means "take value from INI"
        /// </summary>
        public const string TAKE_FROM_INI = "\uf8ff";

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
        public int ConnectErrno
        {
            get
            {
                return connectErrno;
            }
        }

        [DirectCall("->$connect_error")]
        public string ConnectError
        {
            get
            {
                return connectError;
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

        [DirectCall("->$host_info")]
        public string HostInfo { get; set; }

        [DirectCall("->$protocol_version")]
        public string ProtocolVersion { get; set; }

        [DirectCall("->$server_info")]
        public string ServerInfo { get; set; }

        [DirectCall("->$server_version")]
        public int ServerVersion { get; set; }

        /// <summary>
        /// Translated into !empty($mysqli->connect_error)
        /// </summary>
        public bool WasConnectionError
        {
            get
            {
                return !WasSuccessfulConnection;
            }
        }

        /// <summary>
        /// Translated into empty($mysqli->connect_error)
        /// </summary>
        public bool WasSuccessfulConnection
        {
            get
            {
                return string.IsNullOrEmpty(connectError);
            }
        }

        #endregion Properties

        /* Methods 
__construct ([ string $host = ini_get("mysqli.default_host") [, string $username = ini_get("mysqli.default_user") [, string $passwd = ini_get("mysqli.default_pw") [, string $dbname = "" [, int $port = ini_get("mysqli.default_port") [, string $socket = ini_get("mysqli.default_socket") ]]]]]] )
bool change_user ( string $user , string $password , string $database )
bool debug ( string $message )
bool dump_debug_info ( void )
string get_client_info ( void )
bool get_connection_stats ( void )
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
