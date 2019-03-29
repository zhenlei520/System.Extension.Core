<a href="https://github.com/zhenlei520/System.Extension.Core/blob/master/README.md">Home</a>

# storage service #    
<p align="right"><a href="https://github.com/zhenlei520/System.Extension.Core/tree/master/src/Storage/UCloud/README.zh-cn.md">Chinese</a></p>

In Nuget package market search ` EInfrastructure. Core, EInfrastructure. Core. The AutoFac `, and install the latest version

### ucloud storage ###
In Nuget package market search `EInfrastructure.Core.UCloud.Storage`, and install the latest version

Add the AutoFac automatic injection in Starup ConfigureServices,  
Example： 
    
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			//UCloud storage
			services.AddUCloudStorage();
			this._serviceProvider = new AutofacAutoRegister().Build(services,
                (builder) => { });
		}
        
Then in the configuration file (appsettings.json) add:

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
				_storageService=storageService.FirstOrDefault(x => x.GetIdentify() == "EInfrastructure.Core.UCloud.Storage");
			}

			public void Check()
			{
				_storageService.UploadStream(new UploadByStreamParam("key","file Stream",null));
			}
		} 
