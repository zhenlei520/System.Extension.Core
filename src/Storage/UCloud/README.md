<a href="https://github.com/zhenlei520/System.Extension.Core/blob/master/Wiki/%e7%9b%ae%e5%bd%95.md">回到目录</a>

# 存储服务 #

在Nuget包市场中搜索`EInfrastructure.Core、EInfrastructure.Core.AutoFac`，并安装最新版本

### UCloud云存储 ###
在Nuget包市场中搜索`EInfrastructure.Core.UCloud.Storage`，并安装最新版本

在Starup中ConfigureServices中添加AutoFac自动注入，实例为：  
    
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			//UCloud云存储
			services.AddUCloudStorage();
			this._serviceProvider = new AutofacAutoRegister().Build(services,
                (builder) => { });
		}
        
通过控制器注入的方式可直接得到IStorageService，之后直接调用即可
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
