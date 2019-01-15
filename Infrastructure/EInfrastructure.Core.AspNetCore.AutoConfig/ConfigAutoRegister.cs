using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace EInfrastructure.Core.AspNetCore.AutoConfig
{
    public static class ConfigAutoRegister
    {
        #region 自定义环境设置

        /// <summary>
        /// 使用设置
        /// </summary>
        /// <param name="evn"></param>
        /// <param name="isUseEnvironment">是否使用环境配置json</param>
        /// <returns></returns>
        public static IConfigurationRoot UseAppsettings(this IHostingEnvironment evn, bool isUseEnvironment = false)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(evn.ContentRootPath);

            if (isUseEnvironment)
            {
                builder = builder.AddJsonFile($"appsettings.{evn.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();
            }
            else
            {
                builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            }

            var config = builder.Build();
            return builder.Build();
        }

        #endregion
    }
}