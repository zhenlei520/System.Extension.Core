# System.Extension.Core

[![Build status](https://dev.azure.com/wangzhenlei520/System.Extension.Core/_apis/build/status/System.Extension.Core-2.0%20Push)](https://dev.azure.com/wangzhenlei520/System.Extension.Core/_build/latest?definitionId=3)

[![NuGet](https://img.shields.io/nuget/v/EInfrastructure.Core.svg?style=flat-square)](https://www.nuget.org/packages/EInfrastructure.Core)
[![NuGet Download](https://img.shields.io/nuget/dt/EInfrastructure.Core.svg?style=flat-square)](https://www.nuget.org/packages/EInfrastructure.Core)

<a class="ide" href="https://www.jetbrains.com/?from=System.Extension.Core">
    <p>Thanks for the sponsorship of jetbrains products，A useful development tool</p>
    <img src="./jetbrains.png" width="50" height="50">
</a>

[查看帮助](https://zhenlei520.github.io/System.Extension.Core.Doc/)


[更新记录](https://github.com/zhenlei520/System.Extension.Core.Doc/blob/2.0/docs/Update.md)

本项目使用的IDE为Rider，一款跨平台的开放工具，如果使用Visual Studio的朋友打开项目后会提示错误，是由于windows限制的长度导致的，如果出现此类问题，可以将本项目移到磁盘的根目录，并将本项目的跟目录文件夹改为比较简短的名字，项目结构不发生更改不影响项目使用，根目录名字叫什么都可以，我尝试了换成System.Extension.Core不会影响使用。

本项目以以.NetStandard2.1，.NetStandard2.0为目标框架，其中包含常用的基础方法以及Redis、七牛云存储、阿里云短信、词库等基础服务，对于快速搭建NetCore项目有很大的帮助，以下是一个基于NetCore3.1+GRPC的用户服务，是一个简单的小例子。

[点击查看用户服务Demo](https://github.com/zhenlei520/Wolf.User.Service.Demo)
