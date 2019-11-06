// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Config.StorageExtensions.Enumeration;
using EInfrastructure.Core.Configuration.SeedWork;
using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator
{
    /// <summary>
    /// 七牛配置信息校验
    /// </summary>
    public class QiNiuConfigValidator : AbstractValidator<QiNiuStorageConfig>
    {
        public QiNiuConfigValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;;
            RuleFor(x => x.AccessKey).NotNull()
                .WithMessage("AccessKey信息异常");
            RuleFor(x => x.SecretKey).NotNull()
                .WithMessage("SecretKey信息异常");

            RuleFor(x => (int) x.Zones).IsInEnum().WithMessage("Zones 信息异常");

            RuleFor(x => x.Bucket).NotNull()
                .WithMessage("Bucket信息异常");

            RuleFor(x => x.CallbackBody)
                .Must(item => !string.IsNullOrEmpty(item)).WithMessage("CallbackBody信息异常")
                .When(item => !string.IsNullOrEmpty(item.CallbackUrl));

            RuleFor(x => (int) x.CallbackBodyType)
                .Must(x => Enumeration.GetAll<CallbackBodyType>().Any(y => y.Id == x)).WithMessage(
                    "CallbackBodyType信息异常")
                .When(item => !string.IsNullOrEmpty(item.CallbackUrl));
        }
    }
}
