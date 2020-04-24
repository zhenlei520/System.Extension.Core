// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Validation.Common;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Aliyun.Storage
{
    /// <summary>
    /// 加载阿里云Oss服务
    /// </summary>
    public static class Startup
    {
        #region 加载阿里云oss服务

        /// <summary>
        /// 加载阿里云oss服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="func">委托</param>
        public static IServiceCollection AddQiNiuStorage(this IServiceCollection services,
            Func<ALiYunStorageConfig> func)
        { 
            EInfrastructure.Core.StartUp.Run();
            var aliyunConfig = func.Invoke();
            ValidationCommon.Check(aliyunConfig, "阿里云存储配置异常", HttpStatus.Err.Name);
            services.AddSingleton(aliyunConfig);
            return services;
        }

        #endregion
    }
}
