Project E03_ReferencedLibrary references library Lang.Php.Mpdf. 
Lang.Php.Mpdf is 100% written in PHP and Lang.Php.Mpdf is only facade.

How to use this example?
========================
1. Make sure that CS2PHP solution is compiled in RELEASE mode. 
   We need following files
   a) Lang.Cs2Php\bin\Release\net472\cs2php.exe
   b) Extensions\Lang.Php.Mpdf\bin\Release\Lang.Php.Mpdf.dll 
2. Run compile.bat
   Two projects will be compiled
   a) Lang.Php.Mpdf.csproj - in this case complete php package will be downloaded an installed instead of translation c# to PHP.
   b) E03_ReferencedLibrary.csproj
3. Look into _ExamplesOut\E03_ReferencedLibrary and _ExamplesOut\CommonLib folder and see results

How it works?
=============
1. Lang.Php.Mpdf is automatically downloaded and unzipped into 
	_ExamplesOut\CommonLib\lib\mpdf
   This path comes from concatentation of 
   a) _ExamplesOut\CommonLib - compilation commandline parameter (see compile.bat)
   b) lib\mpdf - defined by assembly: Lang.Php.RootPath("/lib/mpdf")] inside Lang.Php.Mpdf source code
2. E03_ReferencedLibrary is compiled into 
   _ExamplesOut\E03_ReferencedLibrary
   This path is compilation commandline parameter (see compile.bat).
3. Config file cs2php is created where const MPDF_LIB_PATH is defined as
   define('MPDF_LIB_PATH', dirname(__FILE__) . '/../CommonLib/'); 
   Name MPDF_LIB_PATH comes from Lang.Php.Mpdf source code:
   [assembly: Lang.Php.ModuleIncludeConst("MPDF_LIB_PATH")]
4. Inside E03_ReferencedLibrary_Test.php file require statement is created
   require_once(\MPDF_LIB_PATH . 'lib/mpdf/mpdf.php');
 
Conclusions
===========
1. If you create library that will be referenced by others make sure that Lang.Php.RootPath and Lang.Php.ModuleIncludeConst attributes decorate your assembly 
   i.e. (taken from Lang.Php.Mpdf)
   [assembly: Lang.Php.RootPath("/lib/mpdf")]
   [assembly: Lang.Php.ModuleIncludeConst("MPDF_LIB_PATH")]
2. While compilation process use the same path for
   a) for referenced library: as output folder (see OUTLIB in compile.bat)
	  i.e.
	  cs2php myUtil.csproj OUTPUT_FOLDER
   b) for main library: in 'lib' commandline parameter
      i.e.
	  cs2php [other options] -lib myUtil=OUTPUT_FOLDER
       