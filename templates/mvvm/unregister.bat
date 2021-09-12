@echo off

set a=%~dp0

cmd /v:on /c dotnet new -u !a:~0,-1!