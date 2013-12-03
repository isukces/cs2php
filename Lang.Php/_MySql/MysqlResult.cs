using System;
using System.Collections.Generic;
using System.Linq;

namespace Lang.Php
{
    /*
     *  For other type of SQL statements, INSERT, UPDATE, DELETE, DROP, etc, mysql_query() returns TRUE on success or FALSE on error.

The returned result resource should be passed to mysql_fetch_array(), and other functions for dealing with result tables, to access the returned data.

Use mysql_num_rows() to find out how many rows were returned for a SELECT statement or mysql_affected_rows() to find out how many rows were affected by a DELETE, INSERT, REPLACE, or UPDATE statement.

mysql_query() will also fail and return FALSE if the user does not have permission to access the table(s) referenced by the query. 
     * 
     * 
     * 
     * while ($row = mysql_fetch_assoc($result)) {
            echo $row['firstname'];
            echo $row['lastname'];
            echo $row['address'];
            echo $row['age'];
}
     */
    [Skip]
    public class MysqlResult : IDisposable
    {
        #region Methods

        // Public Methods 

        [DirectCall("XXX", "this")]
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// object mysql_fetch_object ( resource $result [, string $class_name [, array $params ]] )
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [DirectCall("mysql_fetch_array", "this")]
        public T FetchArray<T>()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// array mysql_fetch_assoc ( resource $result )
        /// </summary>
        /// <returns></returns>
        [DirectCall("mysql_fetch_assoc", "this")]
        public Dictionary<string, object> FetchAssoc()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// array mysql_fetch_assoc ( resource $result )
        /// </summary>
        /// <returns></returns>
        [DirectCall("mysql_fetch_assoc", "this")]
        public Falsable<T> FetchAssoc<T>()
        {
            throw new NotSupportedException();
        }


        [DirectCall("mysql_fetch_assoc", "0,this", 0)]
        public virtual bool FetchAssoc<T>(out T row)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// object mysql_fetch_object ( resource $result [, string $class_name [, array $params ]] )
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [DirectCall("mysql_fetch_object", "this")]
        public T FetchObject<T>()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// bool mysql_free_result ( resource $result )
        /// </summary>
        [DirectCall("mysql_free_result", "this")]
        public void FreeResult()
        {
            _phpLevelDisposed = true;
        }
        // Protected Methods 

        protected virtual int getNumRows()
        {
            throw new NotSupportedException();
        }

        #endregion Methods

        #region Fields

        protected bool _ok;
        protected bool _phpLevelDisposed;

        #endregion Fields

        #region Properties

        [UseBinaryExpression("!==", "this", "false")]
        public bool IsOk
        {
            get
            {
                return _ok;
            }
        }

        [DirectCall("mysql_num_rows", "this")]
        public int NumRows
        {
            get
            {
                return getNumRows();
            }
        }

        #endregion Properties
    }
}
