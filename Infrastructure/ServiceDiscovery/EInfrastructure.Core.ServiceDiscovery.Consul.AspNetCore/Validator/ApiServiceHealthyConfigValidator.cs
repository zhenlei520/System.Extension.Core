using EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Config;
using FluentValidation;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator
{
    /// <summary>
    /// Api服务健康检查配置校验
    /// </summary>
    public class ApiServiceHealthyConfigValidator : AbstractValidator<ApiServiceHealthyConfig>
    {
        public ApiServiceHealthyConfigValidator()
        {
            RuleFor(x => x.CheckApi).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("校验api不能为空");
        }
    }
}