dotnet restore .\vCardLib
REM dotnet restore .\vCardLib.Tests

REM dotnet test .\vCardLib.Tests\vCardLib.Tests.csproj
REM if not "%errorlevel%"=="0" goto failure

dotnet build .\vCardLib\vCardLib.csproj --configuration Release
if not "%errorlevel%"=="0" goto failure

dotnet pack .\vCardLib --configuration release

:success
exit 0

:failure
exit -1