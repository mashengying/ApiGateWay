set appName=ApiGateWay
set binPath="D:\AutomationTools\Jenkins\.jenkins\workspace\%appName%"
sc query %appName%
if %errorlevel%==0 (
    echo found the service %appName%
	sc stop %appName%
	echo stop the service %appName%
	sc start %appName%
	echo start the service %appName%
) else (
	echo Not Found The Service %appName%
	sc create %appName% binPath= %binPath%
	echo create service %appName%
	sc start %appName%
	echo start the service %appName%
)