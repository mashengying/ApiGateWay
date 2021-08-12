set appName="ApiGateWay"
sc query %appName%
if %errorlevel%==0 (
    echo "found the service %appName%"
	sc stop %appName%
	echo "stop the service %appName%"
	sc start %appName%
	echo "start the service %appName%"
) else (
	echo "Not Found The Service"
	sc create %appName% binPath= "..publish\%appName%.exe"
	echo "create service %appName%"
	sc start %appName%
	echo "start the service %appName%"
)