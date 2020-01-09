// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config;
using EInfrastructure.Core.Validation;
using FluentValidation;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator
{
    /// <summary>
    /// Api服务健康检查配置校验
    /// </summary>
    public class ApiServiceHealthyConfigValidator : AbstractValidator<ApiServiceHealthyConfig>, IFluentlValidator<ApiServiceHealthyConfig>
    {
        /// <summary>
        ///
        /// </summary>
        public ApiServiceHealthyConfigValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.CheckApi).NotNull()
                .WithMessage("校验api不能为空");
        }
    }
}
