::find filename with .csproj or .sln extension
for /f "delims=" %%a in ('dir /b "*.csproj";"*.sln"') do set appName= %%~na
echo %appName%
::set service binPath
set binPath="D:\AutomationTools\Jenkins\.jenkins\workspace\publish\%appName%\%appName%.exe"
sc query %appName%
if %errorlevel%==0 (
    echo [app]Found The Service %appName%
	powershell -Command "& {Restart-Service %appName%}"
	exit 0
) else (
	echo [app]Not Found The Service %appName%
	sc create %appName% binPath= %binPath% start= auto
	echo [app]Create Service %appName%
	sc start %appName%
	echo [app]Start The Service %appName%
	exit 0
)