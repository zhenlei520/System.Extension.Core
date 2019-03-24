using EInfrastructure.Core.AliYun.DaYu.Config;
using FluentValidation;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator
{
    /// <summary>
    /// 阿里大于短信配置校验
    /// </summary>
    public class AliYunConfigValidator : AbstractValidator<AliSmsConfig>
    {
        public AliYunConfigValidator()
        {
            RuleFor(x => x.SignName).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("短信签名不能为空");
            RuleFor(x => x.AccessKey).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("AccessKey ID信息异常");
            RuleFor(x => x.EncryptionKey).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("AccessKey Secret信息异常");
        }
    }
}