using EInfrastructure.Core.Redis.Config;
using FluentValidation;

namespace EInfrastructure.Core.Redis.Validator
{
    /// <summary>
    /// Redis配置校验
    /// </summary>
    public class RedisConfigValidator : AbstractValidator<RedisConfig>
    {
        public RedisConfigValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Ip).NotNull()
                .WithMessage("Redis主机信息异常");
            RuleFor(x => x.Port).NotEqual(0)
                .WithMessage("Redis端口信息异常");
        }
    }
}
