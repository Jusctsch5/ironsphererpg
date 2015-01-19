REM @echo off

REM ISUpdater\tribes2rpg_update.exe

REM del .\base\console_end.cs.dso
del .\base\scripts\*.dso 1> nul 2>&1
del .\base\scripts\autoexec\*.dso 1> nul 2>&1
del .\base\scripts\packs\*.dso 1> nul 2>&1
del .\base\scripts\turrets\*.dso 1> nul 2>&1
del .\base\scripts\vehicles\*.dso 1> nul 2>&1
del .\base\scripts\weapons\*.dso 1> nul 2>&1

del .\ironsphererpg\console_end.cs.dso
del .\ironsphererpg\gui\*.dso 1> nul 2>&1
del .\ironsphererpg\scripts\*.dso 1> nul 2>&1
del .\ironsphererpg\scripts\autoexec\*.dso 1> nul 2>&1
del .\ironsphererpg\scripts\packs\*.dso 1> nul 2>&1
del .\ironsphererpg\scripts\turrets\*.dso 1> nul 2>&1
del .\ironsphererpg\scripts\vehicles\*.dso 1> nul 2>&1
del .\ironsphererpg\scripts\weapons\*.dso 1> nul 2>&1
rem pause

exit
