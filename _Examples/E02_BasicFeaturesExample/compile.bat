@echo off
set CS2PHP=..\..\Lang.Cs2Php\bin\Release\cs2php.exe
set PROJ=E02_BasicFeaturesExample
%CS2PHP% %PROJ%.csproj  ..\..\_ExamplesOut\%PROJ%


