set appName=ApiGateWay
set binPath="D:\AutomationTools\Jenkins\.jenkins\workspace\publish\%appName%\%appName%.exe"
sc query %appName%
if %errorlevel%==0 (
    echo [app]Found The Service %appName%
	sc stop %appName%
	echo [app]Stop The Service %appName%
	sc start %appName%
	echo [app]Start The Service %appName%
	exit 0
) else (
	echo [app]Not Found The Service %appName%
	sc create %appName% binPath= %binPath%
	echo [app]Create Service %appName%
	sc start %appName%
	echo [app]Start The Service %appName%
	exit 0
)