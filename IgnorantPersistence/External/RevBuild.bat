@ECHO off

SET AutoVer=AutoVersioning\AutoVersioning.exe
SET SolnDir=..\
SET BaseVersion=1 0

IF NOT EXIST %AutoVer% (
	ECHO ERROR %AutoVer% not found.
	GOTO :Done
)

%AutoVer% "%SolnDir%IgnorantPersistence\Properties\AssemblyVersion.cs" %BaseVersion%
%AutoVer% "%SolnDir%IgnorantPersistence.L2S\Properties\AssemblyVersion.cs" %BaseVersion%

:Done
PAUSE