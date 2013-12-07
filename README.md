cs2php
======

C# to PHP compiler

1. Translate existing C# code into native PHP code (no supporting libraries required).
2. Test & debug C# code directly in IDE environment using dedicated web server that simulates APACHE+PHP.  Just call http://localhost:11000/somefile.php and C# method associated with this url will be invoked.
3. Divide your project into libraries, put them in preffered folder structure and forget about 'include' madness.
4. Reference native PHP libraries in your C# projects by facade .NET libraries. Original PHP library will be downloaded and uncompressed during 'compilation' process.
5. Create WordPress plugins using Lang.Php.Wp extension. 