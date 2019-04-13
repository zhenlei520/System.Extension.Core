// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.Validation.AspNetCore
{
    /// <summary>
    /// 加载EInfrastructure.Core.Validation校验类服务
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// 加载类验证服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="cascadeMode">开启全局级联验证模式，当出现一个错误时，不再继续执行;默认开启级联验证</param>
        public static IServiceCollection AddModelValidation(this IServiceCollection services,
            Func<List<IEnumerable<KeyValuePair<System.Exception, string>>>, object> func,
            CascadeMode cascadeMode = CascadeMode.StopOnFirstFailure)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    List<IEnumerable<KeyValuePair<System.Exception, string>>> errors = context.ModelState.Values.Select(
                            x =>
                                x.Errors.Select(
                                    y => new KeyValuePair<System.Exception, string>(y.Exception, y.ErrorMessage)))
                        .ToList();
                    return new BadRequestObjectResult(func.Invoke(errors));
                };
            });

            ValidatorOptions.CascadeMode = cascadeMode;
            return services;
        }
    }
}
