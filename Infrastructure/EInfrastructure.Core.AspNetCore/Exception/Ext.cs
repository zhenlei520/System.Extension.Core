using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EInfrastructure.Core.AspNetCore.Exception
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class Ext
    {
        #region 设置Json配置

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

        #endregion
    }
}