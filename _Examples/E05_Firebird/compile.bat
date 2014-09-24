@echo off
set CS2PHP=..\..\Lang.Cs2Php\bin\Release\cs2php.exe
set PROJ=E05_Firebird
%CS2PHP% %PROJ%.csproj  ..\..\_ExamplesOut\%PROJ%


