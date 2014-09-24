using Lang.Php;

namespace E05_Firebird
{
    [Page("index")]
    public class Demo : PhpDummy
    {
        #region Static Methods

        // Public Methods 

        public static void PhpMain()
        {
            var a = new Demo();
            a.RunMe();
        }
        // Private Methods 

        private static void ShowError()
        {
            echo("Error (" + Firebird.LastErrorcCode + ":" + Firebird.LastErrorMessage + ")\r\n");
        }

        #endregion Static Methods

        #region Methods

        // Public Methods 

        public void RunMe()
        {
            error_reporting(ErrorLevel.All);
            header("Content-type:text/plain");
            echo("Version 1.03\r\n");
            const string host = "localhost:sample";
            const string username = "sysdba";
            const string password = "masterkey";

            echo("Connectig\r\n");
            _dbConnection = Firebird.Connect(host, username, password);
            echo("...\r\n");
            if (!_dbConnection.IsConnected)
            {
                echo("Error: NOT Connected\r\n");
                ShowError();
                return;
            }
            echo("Connected\r\n");
            string sql = "SELECT " + Person.FieldNames + " FROM PERSONS";
            int count = 0;
            var result = _dbConnection.Query(sql);
            if (!result.IsOk)
            {
                echo("Error: executing " + sql + "\r\n");
                ShowError();
                return;
            }
            Person person;
            while (result.FetchAssoc(out person))
            {
                echo(person.Id + ": ");
                echo(htmlspecialchars(person.FirstName) + " ");
                echo(htmlspecialchars(person.LastnNme) + "\r\n");
                count++;
                //                        echo("<br />");
            }
            result.FreeResult();
            echo("We have ");
            echo(count == 0 ? "no rows" : count == 1 ? "1 row" : (count + " rows"));
            if (count == 0)
            {
                echo("inserting person\r\n");
                _dbConnection.BeginTransaction(IbaseTransactionOptions.Committed | IbaseTransactionOptions.Write);
                sql = "insert into PERSONS(firstName, lastName) values ('John', 'Smith')";
                echo("sql: " + sql + "\r\n");
                result = _dbConnection.Query(sql);
                echo(result.IsOk ? "Query OK" : "Query Error");
                echo("\r\n");
                if (result.IsOk)
                {
                    echo(_dbConnection.Commit() ? "Commited" : "Not commited");
                    echo("\r\n");
                }
                else
                {
                    ShowError();
                    if (!_dbConnection.Rollback())
                    {
                        echo("rollback error");
                        ShowError();
                    }
                }

            }
            _dbConnection.Close();
            /* 
                 taken from http://pl1.php.net/manual/en/function.ibase-connect.php
$host = 'localhost:/path/to/your.gdb';
$dbh = ibase_connect($host, $username, $password);
$stmt = 'SELECT * FROM tblname';
$sth = ibase_query($dbh, $stmt);
while ($row = ibase_fetch_object($sth)) {
    echo $row->email, "\n";
}
ibase_free_result($sth);
ibase_close($dbh); */
        }

        #endregion Methods

        #region Fields

        [ScriptName("dbh")]
        private Firebird _dbConnection;

        #endregion Fields
    }
}
