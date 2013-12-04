@echo off



if "%NUGETEXE%" == "" goto a
if "%NUGETREPO%" == "" goto b

echo NUGETEXE = %NUGETEXE%
echo NUGETREPO = %NUGETREPO%

%NUGETEXE% pack Lang.Php.Wp.Compile.csproj  -IncludeReferencedProjects -Prop Configuration=RELEASE


copy *.nupkg %NUGETREPO%
del *.nupkg
goto end



:a
echo ERROR brak zmiennej NUGETEXE
goto end

:b
echo ERROR brak zmiennej NUGETREPO 
goto end


:end
pause
 