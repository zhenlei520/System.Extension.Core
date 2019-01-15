using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Exception
{
    public static class Ext
    {
        /// <summary>
        /// 设置Json配置
        /// </summary>
        /// <param name="services"></param>
        public static void SetJsonOption(this IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions((options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                }));
        }

        /// <summary>
        /// 使用设置
        /// </summary>
        /// <param name="evn"></param>
        /// <param name="isUseEnvironment">是否使用环境配置json</param>
        /// <returns></returns>
        public static IConfigurationRoot UseAppsettings(this IHostingEnvironment evn, bool isUseEnvironment = false)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(evn.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            if (isUseEnvironment)
            {
                builder = builder.AddJsonFile($"appsettings.{evn.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
            }
            var config = builder.Build();
            return builder.Build();
        }
    }
}