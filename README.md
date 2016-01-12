cs2php
======

C# to PHP compiler package

There is only short description of the project. Please read http://www.cs2php.com/how-to-begin.htm before you start using cs2php.

###Translate existing C# code into native PHP code###

Three scenarios have been prepared for translation process:

1. If you like PHP programming philosophy you can derive C# classes from `PhpDummy` and use well-known methods like
`echo`, `array_merge`, `empty`, `file_get_contents` and many more.

2. If you're unwilling to use PHP (like me) and need substitute you're in right place. Use special designed classes i.e. 

 - `Lang.Php.MySQL` and `Lang.Php.MysqlResult` that collect MySQL methods in one place and classes
 
 - `Lang.Php.Filters.FilterInput` that serves PHP specific functionality in more attractive form.

3. If you need to translate existing C# library cs2php offers you extendable translator that is able to convert methods (i.e. `string.IsNullOrEmpty` into `empty`), properties (i.e. `DateTime.Now` into `new DateTime`)
and classes (i.e. `System.TimeSpan` into `DateInterval`).

All scenarios allows to take advantages of static type checking. C# code is compiled into dll before it is translated to PHP, so 
result code is more reliable. 

Forget about mistyped variable or method names, forget about mistakes like = instead of ==.

Enums are used willingly. In example well known round function can be used 

    var a = round(value, 2, RoundMode.Up);
 
### Test & debug C# code directly in IDE environment... ###

...using dedicated web server that simulates APACHE+PHP.  
Just call http://localhost:11000/somefile.php and C# method associated with this url will be invoked.
See results directly in web browser.

### Modularisation support ###
Divide your project into libraries, put them in preffered folder structure and forget about 'include' madness. Necessary files will be found and included behind the scenes.

### Reference native PHP libraries... ###
in your C# projects by facade .NET libraries. Original PHP library will be downloaded and uncompressed during 'compilation' process.

### Take control over translation ###

Control translation process using attributes: change class, fields and method names. Manage namespaces and output file names.

### Create WordPress plugins ###

Create WordPress plugins using Lang.Php.Wp extension. 

## Need some examples? ##

C# source

        public static void MyDemo()
        {
            var connection = MySQL.Connect(Config.HOST, Config.USER);
            if (!connection.IsConnected)
            {
                echo("Connection error<br/> <b>" + htmlspecialchars(connection.MysqlError()) + "</b>");
                return;
            }
            if (!connection.SetCharset(Config.CODEPAGE_MYSQLCHARSET))
            {
                echo("Unable to change charset  <b>" + Config.CODEPAGE_MYSQLCHARSET + "</b><br/> <b>" + htmlspecialchars(connection.MysqlError()) + "</b>");
                return;
            }
            echo("Connection OK !!!<br/>");
            if (!connection.SelectDb(Config.DB))
            {
                echo("Unable to change database " + Config.DB + "<br/> <b>" + htmlspecialchars(connection.MysqlError()) + "</b>");
                connection.Close();
                return;
            }
            var sql = "select id, first_name, last_name from persons";
            var result = connection.Query(sql);
            if (!result.IsOk)
            {
                echo("SQL error " + htmlspecialchars(sql) + "<br/> <b>" + htmlspecialchars(connection.MysqlError()) + "</b>");
                connection.Close();
                return;
            }
            echo("Sql query <b>" + htmlspecialchars(sql) + "</b><br/>rows= <b style=\"color: blue;\">" + result.NumRows + "</b>." + Config.BR);
            Person person;
            while (result.FetchAssoc(out person))
            {
                echo(htmlspecialchars(person.FirstName));
                echo(htmlspecialchars(person.LastName));
                echo("<br />");
            }
            result.FreeResult();
        }

PHP code

    public static function MyDemo() {
        global $MYSQL_USER;
        $connection = @mysql_connect(MYSQL_HOST, $MYSQL_USER);
        if ($connection === false)
            {
                echo 'Connection error<br/> <b>' . htmlspecialchars(mysql_error($connection)) . '</b>';
                return;
            }
        if (!mysql_set_charset(CODEPAGE_MYSQLCHARSET, $connection))
            {
                echo 'Unable to change charset  <b>' . CODEPAGE_MYSQLCHARSET . '</b><br/> <b>' . htmlspecialchars(mysql_error($connection)) . '</b>';
                return;
            }
        echo 'Connection OK !!!<br/>';
        if (!@mysql_select_db(MYSQL_DB, $connection))
            {
                echo 'Unable to change database ' . MYSQL_DB . '<br/> <b>' . htmlspecialchars(mysql_error($connection)) . '</b>';
                mysql_close($connection);
                return;
            }
        $sql = 'select id, first_name, last_name from persons';
        $result = mysql_query($sql, $connection);
        if ($result === false)
            {
                echo 'SQL error ' . htmlspecialchars($sql) . '<br/> <b>' . htmlspecialchars(mysql_error($connection)) . '</b>';
                mysql_close($connection);
                return;
            }
        echo 'Sql query <b>' . htmlspecialchars($sql) . '</b><br/>rows= <b style="color: blue;">' . mysql_num_rows($result) . '</b>.<br />';
        while($person = mysql_fetch_assoc($result))
            echo htmlspecialchars($person['first_name']) . htmlspecialchars($person['last_name']) . '<br />';
        mysql_free_result($result);
    }


[![githalytics.com alpha](https://cruel-carlota.pagodabox.com/d7704be271547041f28d36ce4320701e "githalytics.com")](http://githalytics.com/isukces/cs2php)