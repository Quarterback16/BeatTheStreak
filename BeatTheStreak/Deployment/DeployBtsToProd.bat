::  Deploy bts to Prod
:: net use X: /DELETE
:: net use X: \\Katla\cdrive\apps

del x:\bts\backup\*.* /q
xcopy x:\bts\*.* x:\bts\backup\

xcopy BeatTheStreak.*.dll x:\bts /y
xcopy BeatTheStreak.*.pdb x:\bts /y

xcopy bts.exe x:\bts /y
xcopy bts.pdb x:\bts /y
xcopy bts.bat x:\bts /y

xcopy Application.* x:\bts /y
xcopy Domain.* x:\bts /y

::stable
::xcopy Cache.dll x:\bts /y
::xcopy Cache.pdb x:\bts /y
::xcopy StackExchange.Redis.* x:\bts /y
::xcopy Newtonsoft.Json.* x:\bts /y
::xcopy nlog.* x:\bts /y

pause