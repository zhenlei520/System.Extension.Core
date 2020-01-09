// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config;
using EInfrastructure.Core.Validation;
using FluentValidation;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator
{
    /// <summary>
    /// ApiService配置校验
    /// </summary>
    public class ApiServiceConfigValidator : AbstractValidator<ApiServiceConfig>, IFluentlValidator<ApiServiceConfig>
    {
        /// <summary>
        ///
        /// </summary>
        public ApiServiceConfigValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotNull()
                .WithMessage("服务名称配置异常");
            RuleFor(x => x.Ip).NotNull()
                .WithMessage("服务器ip配置异常");
            RuleFor(x => x.Port).NotEqual(0)
                .WithMessage("服务器端口配置异常");
        }
    }
}
