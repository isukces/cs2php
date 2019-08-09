@echo off
set CS2PHP=..\..\Lang.Cs2Php\bin\Release\net472\cs2php.exe
set OUTMAIN=..\..\_ExamplesOut\E03_ReferencedLibrary
set OUTLIB=..\..\_ExamplesOut\CommonLib

echo ---------- COMPILE Referenced library
%CS2PHP% ..\..\Extensions\Lang.Php.Mpdf\Lang.Php.Mpdf.csproj %OUTLIB%  

echo ---------- COMPILE Main program
%CS2PHP% E03_ReferencedLibrary.csproj %OUTMAIN% -lib Lang.Php.Mpdf=%OUTLIB%
rem  -r ..\..\Extensions\Lang.Php.Mpdf\bin\Release\Lang.Php.Mpdf.dll 


