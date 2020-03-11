@echo off


cd %1
echo 文件夹 %1
echo 项目名称：%2
dotnet build -c Release

echo 项目名称：%2
dotnet pack -c Release --output ./bin/Release/package

cd ./bin/Release/package
set "package=*.nupkg"

for /r %%i in (%package%) do ( 
dotnet nuget push %%~ni.nupkg --source nuget.org
)

pause