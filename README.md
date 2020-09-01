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

&emsp;&emsp;System.Extension.Core是一个netstandard2.0、netstandard2.1的基础类库，此类库中封装了我们常用的基础方法，极大的提高了开发效率，目前最新版已经提供的类库有

1. 基础方法类库（提供常用的加解密方法、验证、类型转换等常用的方法）
1. HTTP类库（基于基于RestSharp做了一次封装，使其使用起来更简单方便）
1. UserAgent解析类库，快速解析UserAgent，得到浏览器信息、内核信息、操作系统信息以及设备信息（新增）
1. Ioc自动注入，通过自动注入，使得我们在开发时，能更简单的完成注入
1. 阿里短信类库扩展
1. 七牛云存储支持
1. 阿里云存储支持
1. MemoryCache缓存支持
1. Redis缓存支持
1. 词库支持（支持多音词转换等）
1. 文件夹压缩打包解压功能
1. 支持与MySql数据库开发
1. 支持与SQLService数据库开发

其他的就不一一列举了，感兴趣的可以查看[文档](https://docs.bflove.cn/#/zh-cn/abstract)，有疑问的可以<a href="https://github.com/zhenlei520/System.Extension.Core/issues/new">发起Issues</a>


&emsp;&emsp;本项目已同步发布至nuget.org以及github，自2.0系列开始，两平台同步发布，因为项目还在不断地优化，建议您升级到最新的正式发布版，预发布版本虽然修复了很多bug，但为了更好的使用，方法的使用上以及命名上后期还有可能变更，如果您有任何问题可进行提问

&emsp;&emsp;如果对项目感兴趣，欢迎大家start，如果您有好建议，也十分欢迎与我留言，如果希望深一步的沟通，可以扫码下方二维码添加好友与我沟通。[点击查看完整文档](https://docs.bflove.cn/System.Extension.Core.Doc/#/zh-cn/abstract)，如果无法正常加载网页，建议通过以下命令下载文档项目

    git clone https://github.com/zhenlei520/System.Extension.Core.Doc

&emsp;&emsp;netstandard框架与netframework框架在引用上也有不一样的地方，底层依赖了某个包，那么上层就无须再次引用这个包，大家在使用的时候可以发现，demo中都有这样操作，因为不喜欢耦合，所以并未做全家桶这样的类库，我的本意是按需引用，不用引那些自己不需要的东西，我希望自己写的类库可以简洁且实用，当然我也能明白这样一来所需要的花费的时间成本就会增加，需要大家更加了解类库后才能发挥到更大的作用，所以大家在看文档很多遍之后还是不能解决的，可以发起提问，我会一一做出解释，也希望更多伙伴可以与我一起将这个基础包做的越来越好，重复的轮子虽然造起来很过瘾，但很浪费我们宝贵的开发时间，并且很大程度上会影响我们的开发效率，我希望我们能把更多的时间用到更专业的事情上，让我们的思考时间变得更多。


&emsp;&emsp;最后如果大家在使用过程中有问题，可以随时<a href="https://github.com/zhenlei520/System.Extension.Core/issues/new">发起issues</a>，我每天都会登录github，会及时的对问题作出回复，如果大家有更好的建议，对原来的包有更好的建议也可以提出来，我相信一个再简单的东西，如果每天、每月、每年不断坚持的维护，只要不间断的去维护，那么终有一天它也会变得很强大，中间参与的人越多，提出的问题越多，那么后期的潜力也就越大。前移的工作都是自己思考的怎么做更方便，更灵活，但我希望越来越多的人加入，你们的建议可以让这个包更好更快的成长。

<div>
  <p align="right">
    <img width="150" height="150" src="https://docs.bflove.cn/System.Extension.Core.Doc/_media/wechat.jpg">
    <img width="150" height="150"  src="https://docs.bflove.cn/System.Extension.Core.Doc/_media/qq.jpg">
  </p>
  <p align="right">

  </p>
</div>

&emsp;&emsp;nuget.org源地址：https://api.nuget.org/v3/index.json

&emsp;&emsp;github源地址：https://nuget.pkg.github.com/zhenlei520/index.json

&emsp;&emsp;本项目使用的IDE为Rider，一款跨平台的开放工具，如果使用Visual Studio的朋友打开项目后会提示错误，是由于windows限制的长度导致的，如果出现此类问题，可以将本项目移到磁盘的根目录，并将本项目的跟目录文件夹改为比较简短的名字，项目结构不发生更改不影响项目使用，根目录名字叫什么都可以，我尝试了换成System.Extension.Core不会影响使用。


&emsp;&emsp;本项目以以.NetStandard2.1，.NetStandard2.0为目标框架，其中包含常用的基础方法以及Redis、七牛云存储、阿里云短信、词库等基础服务，对于快速搭建NetCore项目有很大的帮助，以下是一个基于NetCore3.1+GRPC的用户服务，是一个简单的小例子。

[wiki中每个包的用法Demo](https://github.com/zhenlei520/System.Extension.Core.Demo)

[Grpc+AspNetCore 3.1 用户服务Demo](https://github.com/zhenlei520/Wolf.User.Service.Demo)

## Stargazers over time

[![Stargazers over time](https://starchart.cc/zhenlei520/System.Extension.Core.svg)](https://starchart.cc/zhenlei520/System.Extension.Core)
