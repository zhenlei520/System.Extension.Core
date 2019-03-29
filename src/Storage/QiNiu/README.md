<a href="https://github.com/zhenlei520/System.Extension.Core/blob/master/README.md">Home</a>

# storage service #
<p align="right"><a href="https://github.com/zhenlei520/System.Extension.Core/tree/master/src/Storage/QiNiu/README.zh-cn.md">Chinese</a></p>

In Nuget package market search ` EInfrastructure. Core, EInfrastructure. Core. The AutoFac `, and install the latest version

### qiniu storage ###
In Nuget package market search `EInfrastructure.Core.QiNiu.Storage`, and install the latest version  
  
Add the AutoFac automatic injection in Starup ConfigureServices,  
Example：  
    
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{

            //The first method：

			services.AddQiNiuStorage(qiniuConfig=>{
				
				#region required
				qiniuConfig.AccessKey="access Key";
				qiniuConfig.SecretKey="secret Key";
				qiniuConfig.Zones="zone";
				qiniuConfig.Host="file host";
				qiniuConfig.Bucket="bucket";

				#endregion

				#region optional
				qiniuConfig.UserAgent="agent";
				qiniuConfig.RsHost="qiniu resource server address";
				qiniuConfig.RsfHost="qiniu resource list server address";
				qiniuConfig.PrefetchHost="";
				qiniuConfig.PersistentPipeline="pipeline";
				qiniuConfig.PersistentNotifyUrl="notifyUrl";
				qiniuConfig.CallbackUrl="";
				qiniuConfig.CallbackBody="";
				qiniuConfig.CallbackBodyType="";
				qiniuConfig.CallbackAuthHost="auth host";
				#endregion
			});

            //The second method：

                services.AddQiNiuStorage();//The configuration file is shown belo

		    this._serviceProvider = new AutofacAutoRegister().Build(services,
                (builder) => { });

           //The third method：

                services.AddQiNiuStorage(Configuration);//The configuration file is shown belo

		    this._serviceProvider = new AutofacAutoRegister().Build(services,
                (builder) => { });
		}

Then in the configuration file (appsettings.json) add:

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

Example：

		public class TestController{
			private readonly IStorageService _storageService;
			public TestController(IStorageService storageService){
				_storageService=storageService;
			}

			public void Check()
			{
				_storageService.UploadStream(new UploadByStreamParam("key","file Stream",null));
			}
		} 

Note: when IStorageService has multiple implementation classes, the reference may be ambiguous, such as if you want to specify the implementation class:

		public class TestController{
			private readonly IStorageService _storageService;
			public TestController(ICollection<IStorageService> storageService){
				_storageService=storageService.FirstOrDefault(x => x.GetIdentify() == "EInfrastructure.Core.QiNiu.Storage");
			}

			public void Check()
			{
				_storageService.UploadStream(new UploadByStreamParam("key","file Stream",null));
			}
		} 


