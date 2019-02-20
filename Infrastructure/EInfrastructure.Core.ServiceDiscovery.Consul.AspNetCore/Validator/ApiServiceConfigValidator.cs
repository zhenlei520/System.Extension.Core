using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config;
using FluentValidation;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator
{
    /// <summary>
    /// ApiService配置校验
    /// </summary>
    public class ApiServiceConfigValidator : AbstractValidator<ApiServiceConfig>
    {
        public ApiServiceConfigValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("服务名称配置异常");
            RuleFor(x => x.Ip).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("服务器ip配置异常");
            RuleFor(x => x.Port).Cascade(CascadeMode.StopOnFirstFailure).NotEqual(0)
                .WithMessage("服务器端口配置异常");
        }
    }
}