<a href="https://github.com/zhenlei520/System.Extension.Core/blob/master/README.md">Home</a>

# sms service #

In Nuget package market search `EInfrastructure.Core、EInfrastructure.Core.AutoFac`, and install the latest version

### Ali is bigger than SMS ###
In Nuget package market search `EInfrastructure.Core.AliYun.DaYu`, and install the latest version

Add the AutoFac automatic injection in Starup ConfigureServices,  
Example：   
    
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{

        //The first method：
		services.AddAliDaYu(option=>{
            option.SignName="sign";
            option.AccessKey="accesskey";
            option.EncryptionKey="encryption key";
        });
        this._serviceProvider = new AutofacAutoRegister().Build(services,(builder) => { });

        //The second method：
        services.AddAliDaYu();//The configuration file is shown belo
        this._serviceProvider = new AutofacAutoRegister().Build(services,(builder) => { });

        //The third method：
         services.AddAliDaYu(Configuration);//The configuration file is shown belo

        this._serviceProvider = new AutofacAutoRegister().Build(services,
            (builder) => { });

            
        }

Then in the configuration file (appsettings.json) add:

    {
        "AliSmsConfig": {
            "SignName": "sign",
            "AccessKey": "access key",
            "EncryptionKey": "encryption key"
        },
    }        

Example：
		
		public class TestController{
			private readonly ISmsService _smsService;
			public TestController(ISmsService smsService){
				_smsService=smsService;
			}

			public void Check()
			{
				_smsService.Send("134****2132","sms template",new {code="12344"});//Or
				_smsService.Send("134****2132","sms template",new {code="12344"},responseInfo=>{
					//ResponseInfo sends the reason for the failure
				},new JsonCommon().Serializer(new SmsConfig(){
					SignName = "sign",
	                AccessKey = "Access key",
	                EncryptionKey = "EncryptionKey"
				}));//If the last parameter is passed, the SMS configuration within the method is used instead of the injection preconfiguration
			}
		} 

Note: when IStorageService has multiple implementation classes, the reference may be ambiguous, such as if you want to specify the implementation class:

        public class TestController{
			private readonly ISmsService _smsService;
			public TestController(ICollection<ISmsService> smsServices){
				_smsService=smsServices.FirstOrDefault(x => x.GetIdentify() == "EInfrastructure.Core.AliYun.DaYu");
			}

			public void Check()
			{
				_smsService.Send("134****2132","sms template",new {code="12344"});//Or
				_smsService.Send("134****2132","sms template",new {code="12344"},responseInfo=>{
					//ResponseInfo sends the reason for the failure
				},new JsonCommon().Serializer(new SmsConfig(){
					SignName = "sign",
	                AccessKey = "Access key",
	                EncryptionKey = "EncryptionKey"
				}));//If the last parameter is passed, the SMS configuration within the method is used instead of the injection preconfiguration
			}
		} 