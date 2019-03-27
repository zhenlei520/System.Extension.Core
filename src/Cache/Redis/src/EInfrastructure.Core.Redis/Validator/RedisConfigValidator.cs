using EInfrastructure.Core.Redis.Config;
using FluentValidation;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator
{
    /// <summary>
    /// Redis配置校验
    /// </summary>
    public class RedisConfigValidator : AbstractValidator<RedisConfig>
    {
        public RedisConfigValidator()
        {
            RuleFor(x => x.Ip).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("Redis主机信息异常");
            RuleFor(x => x.Port).Cascade(CascadeMode.StopOnFirstFailure).Equal(0)
                .WithMessage("Redis端口信息异常");
        }
    }
}