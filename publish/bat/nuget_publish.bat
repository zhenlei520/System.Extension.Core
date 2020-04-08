@echo off

set "solutionPath=../../src"
set "project=*.csproj"
set  "batPath=%~dp0"

setlocal enabledelayedexpansion  
echo 开始搜索文件，请等待程序提示“搜索完成”再退出 ...  

echo %solutionPath%

cd %solutionPath%

@echo off
rem 指定待搜索的文件
echo 正在搜索，请稍候...

for /r %%i in (%project%) do ( 
    echo %%i 
    echo %%~ni
    echo %%~fi
    echo %%~pi
    echo %%~di
    cd %batPath%
    call publish.bat %%~di%%~pi,%%~ni,nuget.org
)

echo.  
echo 发布成功
pause >nul 