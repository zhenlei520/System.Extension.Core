@echo off

set "name=zhenlei520"
set "token="

nuget source Add -Name "github" -Source "https://nuget.pkg.github.com/%name%/index.json" -Username %name% -Password %token%

nuget setApiKey %token% -s github

pause >nul 