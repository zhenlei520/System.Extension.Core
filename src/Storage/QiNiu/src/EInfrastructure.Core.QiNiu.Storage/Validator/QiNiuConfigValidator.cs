// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Storage.Enum;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using FluentValidation;

namespace EInfrastructure.Core.ServiceDiscovery.Consul.AspNetCore.Validator
{
    /// <summary>
    /// 七牛配置信息校验
    /// </summary>
    public class QiNiuConfigValidator : AbstractValidator<QiNiuStorageConfig>
    {
        public QiNiuConfigValidator()
        {
            RuleFor(x => x.AccessKey).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("AccessKey信息异常");
            RuleFor(x => x.SecretKey).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("SecretKey信息异常");
            RuleFor(x => x.Zones).Cascade(CascadeMode.StopOnFirstFailure).IsInEnum().WithMessage("Zones信息配置异常");
            RuleFor(x => x.Bucket).Cascade(CascadeMode.StopOnFirstFailure).NotNull()
                .WithMessage("Bucket信息异常");

            RuleFor(x => x.CallbackBody).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(string.IsNullOrEmpty).WithMessage("CallbackBody信息异常")
                .When(item => !string.IsNullOrEmpty(item.CallbackUrl));
            RuleFor(x => x.CallbackBodyType).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(item => !((int) item).IsExist(typeof(CallbackBodyTypeEnum))).WithMessage("CallbackBodyType信息异常")
                .When(item => !string.IsNullOrEmpty(item.CallbackUrl));
        }
    }
}