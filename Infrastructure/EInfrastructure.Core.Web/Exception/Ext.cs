using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Web.Exception
{
    public static class Ext
    {
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

        public static IConfigurationRoot UseAppsettings(this IHostingEnvironment evn)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(evn.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }


}