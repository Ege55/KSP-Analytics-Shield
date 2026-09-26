@echo off
setlocal

if not "%~1"=="" set "KSP_ROOT=%~1"
if "%KSP_ROOT%"=="" set "KSP_ROOT=C:\GOG Games\Kerbal Space Program"

set "MANAGED=%KSP_ROOT%\KSP_x64_Data\Managed"
set "CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
if not exist "%CSC%" set "CSC=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe"
set "OUT=%~dp0GameData\KSPAnalyticsShield\Plugins\KSPAnalyticsShield.dll"
if not exist "%~dp0GameData\KSPAnalyticsShield\Plugins" mkdir "%~dp0GameData\KSPAnalyticsShield\Plugins"

if not exist "%MANAGED%\Assembly-CSharp.dll" (
    echo KSP assemblies not found under "%MANAGED%".
    echo Pass the KSP 1 installation folder as the first argument.
    exit /b 1
)
if not exist "%CSC%" (
    echo Microsoft .NET Framework C# compiler not found: "%CSC%"
    exit /b 1
)

"%CSC%" /nologo /target:library /optimize+ /out:"%OUT%" ^
  /reference:"%MANAGED%\Assembly-CSharp.dll" ^
  /reference:"%MANAGED%\UnityEngine.CoreModule.dll" ^
  /reference:"%MANAGED%\UnityEngine.UnityAnalyticsModule.dll" ^
  "%~dp0KSPAnalyticsShield.cs" "%~dp0AssemblyInfo.cs"
if errorlevel 1 exit /b 1

echo Built "%OUT%"
