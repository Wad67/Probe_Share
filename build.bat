set mypath=%cd%
@echo off
@echo %mypath%
@echo "Building whatever this dogshit is that you call a game.."
setlocal EnableDelayedExpansion

set "startTime=%time: =0%"



"C:\Program Files\Unity\Hub\Editor\2020.3.26f1\Editor\Unity.exe" -quit -batchmode -logFile stdout.log -projectPath %mypath% -buildWindowsPlayer "builds\mygame.exe"
%SendKeys% {ENTER}

type %mypath%\stdout.log

set "endTime=%time: =0%"



rem Get elapsed time:
set "end=!endTime:%time:~8,1%=%%100)*100+1!"  &  set "start=!startTime:%time:~8,1%=%%100)*100+1!"
set /A "elap=((((10!end:%time:~2,1%=%%100)*60+1!%%100)-((((10!start:%time:~2,1%=%%100)*60+1!%%100), elap-=(elap>>31)*24*60*60*100"

rem Convert elapsed time to HH:MM:SS:CC format:
set /A "cc=elap%%100+100,elap/=100,ss=elap%%60+100,elap/=60,mm=elap%%60+100,hh=elap/60+100"

echo 
echo Elapsed:  %hh:~1%%time:~2,1%%mm:~1%%time:~2,1%%ss:~1%%time:~8,1%%cc:~1%

cmd /k
pause