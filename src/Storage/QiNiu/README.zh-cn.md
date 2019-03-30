<a href="https://github.com/zhenlei520/System.Extension.Core/blob/master/README.zh-cn.md">回到目录</a>

# 存储服务 #
<p align="right"><a href="https://github.com/zhenlei520/System.Extension.Core/tree/master/src/Storage/QiNiu/README.md">英文</a></p>

在Nuget包市场中搜索`EInfrastructure.Core、EInfrastructure.Core.AutoFac`，并安装最新版本

### 七牛云存储 ###
在Nuget包市场中搜索`EInfrastructure.Core.QiNiu.Storage`，并安装最新版本

在Starup中ConfigureServices中添加AutoFac自动注入，实例为：  
    
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{

            第一种办法：
			services.AddQiNiuStorage(qiniuConfig=>{
				
				#region 必填
				qiniuConfig.AccessKey="七牛提供的公钥";
				qiniuConfig.SecretKey="七牛提供的秘钥";
				qiniuConfig.Zones="空间";
				qiniuConfig.Host="文件访问域";
				qiniuConfig.Bucket="存储的空间名";

				#endregion

				#region 选填
				qiniuConfig.UserAgent="代理";
				qiniuConfig.RsHost="七牛资源管理服务器地址";
				qiniuConfig.RsfHost="七牛资源列表服务器地址";
				qiniuConfig.PrefetchHost="空间";
				qiniuConfig.PersistentPipeline="传输队列";
				qiniuConfig.PersistentNotifyUrl="持久化结果通知";
				qiniuConfig.CallbackUrl="上传成功后，七牛云向业务服务器发送 POST 请求的 URL";//其中CallbackHost属性不要赋值，会导致接收不到七牛回调
				qiniuConfig.CallbackBody="回调内容";
				qiniuConfig.CallbackBodyType="回调内容类型";
				qiniuConfig.CallbackAuthHost="鉴权回调域";
				#endregion
			});

            第二种办法：
                services.AddQiNiuStorage();//配置文件如下所示

		    this._serviceProvider = new AutofacAutoRegister().Build(services,
                (builder) => { });

            第三种办法：
                services.AddQiNiuStorage(Configuration);//配置文件如下所示

		    this._serviceProvider = new AutofacAutoRegister().Build(services,
                (builder) => { });
		}

然后在配置文件（appsettings.json）中添加：

    {
        "QiNiuConfig": {
            "AccessKey": "",
            "SecretKey": "",
            "Zones": 2,
            "Host": "",
            "Bucket": "",
            "PersistentPipeline": "",
            "PersistentNotifyUrl": "",
            "CallbackUrl": "",
            "CallbackHost": "",
            "CallbackBody": "",
            "CallbackBodyType": 1,
            "CallbackAuthHost": ""
        },
    }

例如：

		public class TestController{
			private readonly IStorageService _storageService;
			public TestController(IStorageService storageService){
				_storageService=storageService;
			}

			public void Check()
			{
				_storageService.UploadStream(new UploadByStreamParam("文件key","文件Stream流",null));
			}
		} 

备注：当IStorageService有多个实现类，会导致引用可能不明确，如希望指定实现类：

		public class TestController{
			private readonly IStorageService _storageService;
			public TestController(ICollection<IStorageService> storageServices){
				_storageService=storageServices.FirstOrDefault(x => x.GetIdentify() == "EInfrastructure.Core.QiNiu.Storage");
			}

			public void Check()
			{
				_storageService.UploadStream(new UploadByStreamParam("文件key","文件Stream流",null));
			}
		} 


