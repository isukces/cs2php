using System;

namespace Lang.Php
{
    [Skip]
    public sealed class Firebird
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database">The database argument has to be a valid path to database file on the server it resides on. If the server is not local, it must be prefixed with either 'hostname:' (TCP/IP), '//hostname/' (NetBEUI), depending on the connection protocol used.</param>
        /// <param name="username">The user name. Can be set with the ibase.default_user php.ini directive.</param>
        /// <param name="password">The password for username. Can be set with the ibase.default_password php.ini directive. </param>
        /// <param name="charset">charset is the default character set for a database. </param>
        /// <param name="buffers">buffers is the number of database buffers to allocate for the server-side cache. If 0 or omitted, server chooses its own default. </param>
        /// <param name="dialect">dialect selects the default SQL dialect for any statement executed within a connection, and it defaults to the highest one supported by client libraries. </param>
        /// <param name="role">Functional only with InterBase 5 and up. </param>
        /// <param name="sync"></param>
        /// <returns></returns>
        [DirectCall("ibase_connect")]
        public static Firebird Connect(string database = null,
            string username = null,
            string password = null,
            string charset = null,
            int buffers = 0,
            int dialect = 0,
            string role = null,
            int sync = 0)
        {
            return null;
        }

        [DirectCall("ibase_query", "this,0")]
        public FirebirdResult Query(string query)
        {
            throw new NotSupportedException();
        }


        /// <summary>
        /// bool ibase_close ([ resource $connection_id = NULL ] )
        /// </summary>
        /// <returns></returns>
        [DirectCall("ibase_close", "this")]
        public bool Close()
        {
            throw new NotSupportedException();
        }

        [UseBinaryExpression("!==", "this", "false")]
        public bool IsConnected
        {
            get
            {
                throw new NotImplementedException();
                // return this._connection != null && this._connection.State == System.Data.ConnectionState.Open;
            }
        }
        [DirectCall("ibase_commit", "this")]
        public bool Commit()
        {
            throw new NotImplementedException();
        }
        [DirectCall("ibase_rollback", "this")]
        public bool Rollback()
        {
            throw new NotImplementedException();
        }

        [DirectCall("ibase_commit_ret", "this")]
        public bool CommitRet()
        {
            throw new NotImplementedException();
        }
        [DirectCall("ibase_rollback_ret", "this")]
        public bool RollbackRet()
        {
            throw new NotImplementedException();
        }
        [DirectCall("ibase_trans", "this")]
        public bool BeginTransaction()
        {
            throw new NotImplementedException();
        }
        [DirectCall("ibase_trans", "this,0")]
        public bool BeginTransaction(IbaseTransactionOptions transactionOptions)
        {
            throw new NotImplementedException();
        }

        [DirectCall("ibase_errcode","")]
        public static int LastErrorcCode
        {
            get { throw new NotImplementedException(); }
        }
        [DirectCall("ibase_errmsg","")]
        public static string LastErrorMessage
        {
            get { throw new NotImplementedException(); }
        }
    }
}
