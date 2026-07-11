@echo off
setlocal enabledelayedexpansion

:: --- Flag Check ---
if "%~1"=="--gui" (
    echo Launching GUI version directly...
    python paycalc-gui.py
    exit /b 0
)
if "%~1"=="--cli" (
    echo Launching CLI version directly...
    python paycalc-cli.py
    exit /b 0
)
if "%~1"=="--skip-install" (
    goto MENU
)

:: --- Installation Block ---
echo Checking environment...
where python >nul 2>nul
if %errorlevel% neq 0 (
    echo Error: Python is not installed or not added to your system PATH.
    echo Please download it from https://python.org
    pause
    exit /b 1
) else (
    echo Python detected. ^(Tkinter is pre-bundled on Windows Python builds^).
)

:MENU
echo ===================================
echo     Pay Calculator Launcher       
echo ===================================
echo Which version would you like to run?
echo 1^) Graphical Interface ^(GUI^)
echo 2^) Command Line Interface ^(CLI^)
echo 3^) Exit
echo Hint: Use flags '--gui' or '--cli' to skip this menu!
echo -----------------------------------

set /p choice="Enter your choice [1-3]: "

if "%choice%"=="1" (
    echo Launching GUI version...
    python paycalc-gui.py
    exit /b 0
)
if "%choice%"=="2" (
    echo Launching CLI version...
    python paycalc-cli.py
    exit /b 0
)
if "%choice%"=="3" (
    echo Exiting.
    exit /b 0
)

echo Invalid option. Please run the script again.
pause
exit /b 1

