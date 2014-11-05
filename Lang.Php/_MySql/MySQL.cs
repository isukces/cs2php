using System.Data;
using Lang.Php.Runtime;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Lang.Php
{
    [Skip]
    public sealed class MySQL : IDisposable
    {
        #region Constructors

        ~MySQL()
        {
            Dispose(false);

        }

        #endregion Constructors

        [DirectCall("@mysql_escape_string")]
        public static string EscapeString(object o)
        {
            throw new NotImplementedException();
        }
        [DirectCall("@mysql_escape_string")]
        public static string EscapeString(MySqlDateTime o)
        {
            throw new NotImplementedException();
        }

        [DirectCall("mysql_real_escape_string", "0,this")]
        public string RealEscapeString(object o)
        {
            throw new NotImplementedException();
        }

        #region Static Methods

        // Public Methods 

        /// <summary>
        /// resource mysql_connect 
        ///     ([ string $server = ini_get("mysql.default_host") 
        ///     [, string $username = ini_get("mysql.default_user") 
        ///     [, string $password = ini_get("mysql.default_password") 
        ///     [, bool $new_link = false [, int $client_flags = 0 ]]]]] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("@mysql_connect")]
        public static MySQL Connect()
        {
            throw new NotSupportedException();
        }

        [DirectCall("@mysql_connect")]
        public static MySQL Connect(string server)
        {
            throw new NotSupportedException();
        }

        [DirectCall("@mysql_connect")]
        public static MySQL Connect(string server, string username)
        {
            return Connect(server, username, null);
        }

        [DirectCall("@mysql_connect")]
        public static MySQL Connect(string server, string username, string password, bool new_link = false, int client_flags = 0)
        {
            MySQL a = new MySQL();
            MySqlConnectionStringBuilder b = new MySqlConnectionStringBuilder();
            b.Server = server;
            b.UserID = username;
            if (!string.IsNullOrEmpty(password))
                b.Password = password;
            a._connection = new MySqlConnection(b.ToString());
            try
            {
                a._connection.Open();
            }
            catch (Exception e)
            {
                a._lastError = e.Message;
            }
            return a;
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        /// <summary>
        /// bool mysql_close ([ resource $link_identifier = NULL ] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("mysql_close", "this")]
        public bool Close()
        {
            throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        [DirectCall("mysql_error", "this")]
        public string MysqlError()
        {
            return _lastError;
        }

        /// <summary>
        /// resource mysql_query ( string $query [, resource $link_identifier = NULL ] )
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        [DirectCall("mysql_query", "this,0")]
        public MysqlResult Query(string query)
        {
            var cmd = new MySqlCommand(query, _connection);
            RuntimeMysqlResult result = new RuntimeMysqlResult();
            try
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    result._SetFromDR(reader);
                }
            }
            catch (Exception e)
            {
                _lastError = e.Message;
            }
            return result;
        }

        public IEnumerable<T> QueryReader<T>(string query)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// bool mysql_select_db ( string $database_name [, resource $link_identifier = NULL ] )
        /// </summary>
        /// <param name="database_name"></param>
        /// <returns></returns>
        [DirectCall("@mysql_select_db", "0, this")]
        public bool SelectDb(string database_name)
        {
            return _internalExecuteNonQuery("use " + database_name);
        }

        /// <summary>
        /// bool mysql_set_charset ( string $charset [, resource $link_identifier = NULL ] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("mysql_set_charset", "0,this")]
        public bool SetCharset(string charset)
        {
            return _internalExecuteNonQuery("SET NAMES " + charset);
        }

        [UseBinaryExpression("!==", "false", "$0")]
        public bool ValidRow<T>(T obj)
        {
            throw new MockMethodException();
        }
        // Protected Methods 

        private void Dispose(bool disposing)
        {
            if (disposing && (_connection != null))
                _connection.Dispose();
        }
        // Private Methods 

        private bool _internalExecuteNonQuery(string cmdText)
        {
            MySqlCommand cmd = new MySqlCommand(cmdText, _connection);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                _lastError = e.Message;
                return false;
            }
        }

        #endregion Methods

        #region Fields

        MySqlConnection _connection;
        string _lastError;

        #endregion Fields

        #region Properties

        [UseBinaryExpression("!==", "this", "false")]
        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.State == ConnectionState.Open;
            }
        }

        #endregion Properties
    }
}
