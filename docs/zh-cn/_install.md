### 版本要求：
&emsp;&emsp;System.Extension.Core需要在NetCore环境下运行。

可以使用下列任一命令通过[Nuget](https://www.nuget.org/packages/EInfrastructure.Core/)包管理器进行安装：

    Install-Package EInfrastructure.Core -Version 2.0.0-beta-021
    # OR
    dotnet add package EInfrastructure.Core --version 2.0.0-beta-021
    # OR
    paket add EInfrastructure.Core --version 2.0.0-beta-021

或者直接编辑*.csproj文件，

    在 <ItemGroup>下增加
    <PackageReference Include="EInfrastructure.Core" Version="2.0.0-beta-021" />

例如：

    <ItemGroup>
        <PackageReference Include="EInfrastructure.Core" Version="2.0.0-beta-021" />
    </ItemGroup>

安装后通过查看项目的工程文件（即csproj文件），查看是否存在EInfrastructure.Core，来检查是否正确安装。