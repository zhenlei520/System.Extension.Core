// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.BaiDu.ContentIdentification.Config;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.BaiDu.ContentIdentification
{
    /// <summary>
    /// 加载百度鉴黄服务
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 加载此服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        public static IServiceCollection AddBaiduContentIdentification(this IServiceCollection services,
            Action<BaiDuConfig> action = null)
        {
            services.Configure(action);
            return services;
        }
    }
}