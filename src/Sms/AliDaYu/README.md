<a href="https://github.com/zhenlei520/System.Extension.Core/blob/master/Wiki/%e7%9b%ae%e5%bd%95.md">回到目录</a>

# 短信服务 #

在Nuget包市场中搜索`EInfrastructure.Core、EInfrastructure.Core.AutoFac`，并安装最新版本

### 阿里大于短信服务 ###
在Nuget包市场中搜索`EInfrastructure.Core.AliYun.DaYu`，并安装最新版本

在Starup中ConfigureServices中添加AutoFac自动注入，实例为：  
    
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddAliDaYu(option=>{
				option.SignName="签名名称";
				option.AccessKey="应用key";
				option.EncryptionKey="秘钥参数";
			});
			this._serviceProvider = new AutofacAutoRegister().Build(services,
                (builder) => { });
		}

通过控制器注入的方式可直接得到ISmsService，之后直接调用即可
例如：
		
	


		public class TestController{
			private readonly ISmsService _smsService;
			public TestController(ISmsService smsService){
				_smsService=smsService;
			}

			public void Check()
			{
				_smsService.Send("134****2132","短信模板",new {code="12344"});或者
				_smsService.Send("134****2132","短信模板",new {code="12344"},responseInfo=>{
					//responseInfo为发送失败原因信息
				},new JsonCommon().Serializer(new SmsConfig(){
					SignName = "签名名称",
	                AccessKey = "Access秘钥",
	                EncryptionKey = "EncryptionKey"
				}));//如果传最后的参数，则使用方法内的短信配置而不使用注入预配置
			}
		} 