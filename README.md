# System.Extension.Core

[![Build status](https://dev.azure.com/wangzhenlei520/System.Extension.Core/_apis/build/status/System.Extension.Core-2.0%20Build)](https://dev.azure.com/wangzhenlei520/System.Extension.Core/_build/latest?definitionId=3)

[![NuGet](https://img.shields.io/nuget/v/EInfrastructure.Core.svg?style=flat-square)](https://www.nuget.org/packages/EInfrastructure.Core)
[![NuGet Download](https://img.shields.io/nuget/dt/EInfrastructure.Core.svg?style=flat-square)](https://www.nuget.org/packages/EInfrastructure.Core)

<a class="ide" href="https://www.jetbrains.com/?from=System.Extension.Core">
    <p>Thanks for the sponsorship of jetbrains products，A useful development tool</p>
    <img src="./jetbrains.png" width="50" height="50">
</a>


[![NuGet](https://img.shields.io/nuget/v/EInfrastructure.Core.svg?style=flat-square)](https://www.nuget.org/packages/EInfrastructure.Core)
[![NuGet Download](https://img.shields.io/nuget/dt/EInfrastructure.Core.svg?style=flat-square)](https://www.nuget.org/packages/EInfrastructure.Core)

&emsp;&emsp;System.Extension.Core是一个netstandard2.0、netstandard2.1的基础类库，此类库中封装了我们常用的基础方法，通过此类库，可以大大提高我们的开发效率，其中扩展类库还封装了文件上传类库（七牛）、短信帮助类（阿里云）、缓存帮助类（Redis、Memcache）、以及与数据库的交互类库（mysql、sqlserver）、配置文件自动注入等，通过IOC注入的方式，极大的提高了开发效率

&emsp;&emsp;本项目已同步发布至nuget.org以及github，自2.0系列开始，两平台同步发布，因为项目还在不断地优化，建议您升级到最新的正式发布版，预发布版本虽然修复了很多bug，但为了更好的使用，方法的使用上以及命名上后期还有可能变更，如果您有任何问题可进行提问

&emsp;&emsp;如果对项目感兴趣，欢迎大家start，如果您有好建议，也十分欢迎与我留言，如果希望深一步的沟通，可以扫码下方二维码添加好友与我沟通。[点击查看完整文档](https://blog.bflove.cn/System.Extension.Core.Doc/#/zh-cn/abstract)，如果无法正常加载网页，建议通过以下命令下载文档项目

    git clone https://github.com/zhenlei520/System.Extension.Core.Doc

<div>
  <p align="right">
    <img style="width:150px;height:150px;" src="https://blog.bflove.cn/System.Extension.Core.Doc/_media/wechat.jpg">
    <img style="width:150px;height:170px;margin-left:50px;" src="https://blog.bflove.cn/System.Extension.Core.Doc/_media/qq.jpg">
  </p>
  <p align="right">
   
  </p>
</div>

&emsp;&emsp;nuget.org源地址：https://api.nuget.org/v3/index.json 

&emsp;&emsp;github源地址：https://nuget.pkg.github.com/zhenlei520/index.json

&emsp;&emsp;本项目使用的IDE为Rider，一款跨平台的开放工具，如果使用Visual Studio的朋友打开项目后会提示错误，是由于windows限制的长度导致的，如果出现此类问题，可以将本项目移到磁盘的根目录，并将本项目的跟目录文件夹改为比较简短的名字，项目结构不发生更改不影响项目使用，根目录名字叫什么都可以，我尝试了换成System.Extension.Core不会影响使用。

[更新记录](https://github.com/zhenlei520/System.Extension.Core.Doc/blob/2.0/docs/Update.md)

&emsp;&emsp;本项目以以.NetStandard2.1，.NetStandard2.0为目标框架，其中包含常用的基础方法以及Redis、七牛云存储、阿里云短信、词库等基础服务，对于快速搭建NetCore项目有很大的帮助，以下是一个基于NetCore3.1+GRPC的用户服务，是一个简单的小例子。

[wiki中每个包的用法Demo](https://github.com/zhenlei520/System.Extension.Core.Demo)

[Grpc+AspNetCore 3.1 用户服务Demo](https://github.com/zhenlei520/Wolf.User.Service.Demo)
