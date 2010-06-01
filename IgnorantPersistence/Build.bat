@ECHO off
PUSHD "%~dp0"

SET MSBuild=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
IF NOT EXIST "%MSBuild%" (
	ECHO Installation of .NET Framework 3.5 is required to build this project
	ECHO http://msdn.microsoft.com/en-us/netframework/cc378097.aspx
	START /d "~\iexplore.exe" http://msdn.microsoft.com/en-us/netframework/cc378097.aspx
	EXIT /b 1
	GOTO END
)

"%MSBuild%" IgnorantPersistence.sln /target:rebuild /property:Configuration=Release

:END
POPD
