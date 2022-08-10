@ECHO OFF

REM # A=%OLD% A=!NEW!
SETLOCAL EnableDelayedExpansion

REM # Timestamping
SET "yrs=%DATE:~-4%"
SET "mth=%DATE:~7,2%"
SET "day=%DATE:~4,2%"
SET "hrs=%TIME:~0,2%"
SET "min=%TIME:~3,2%"
SET "sec=%TIME:~6,2%"
SET "msc=%TIME:~9,2%"
SET "timestamp=%yrs%%mth%%day%-%hrs: =0%%min%%sec%%msc%"

(
echo IkM6XFByb2dyYW0gRmlsZXNcTXlTUUxcTXlTUUwgU2VydmVyIDUuN1xiaW5cbXlzcWxjaGVjay5leGUiIC11IHJvb3QgLXB0d19teXNxbF9yb290IC0tYXV0by1yZXBhaXIgLWMgbW9lIC0tY2hlY2s=
) >"%TEMP%\dedbquery.txt"

rem Do something with the text file like printing the text.
type "%TEMP%\dedbquery.txt"
set "MYSQLCHECK=C:\Program Files\MySQL\MySQL Server 5.7\bin\mysqlcheck.exe"
set "MYSQLCHECKLog=mysqlcheck_%timestamp%.log"

certutil -decode "%TEMP%\dedbquery.txt" "%TEMP%\dbquery.bat" 

set "MYSQLQUERY=%TEMP%\dbquery.bat"

cls

echo Start Fixing 
echo Wait Until The Window Exit

rem Finally run the batch and delete the text file no longer needed.
( 
!MYSQLQUERY! & del "%TEMP%\dedbquery.txt" & del "%TEMP%\dbquery.bat"
) > FixDB\!MYSQLCHECKLog!



pause