@echo off
set CS2PHP=..\..\Lang.Cs2Php\bin\Release\cs2php.exe
set OUT=..\..\_ExamplesOut\E03_ReferencedLibrary

rem %CS2PHP% ..\..\Extensions\Lang.Php.Mpdf\Lang.Php.Mpdf.csproj %OUT%   
%CS2PHP% E03_ReferencedLibrary.csproj %OUT% -r ..\..\Extensions\Lang.Php.Mpdf\bin\Release\Lang.Php.Mpdf.dll 


