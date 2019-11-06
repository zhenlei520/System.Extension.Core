// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.HelpCommon;
using EInfrastructure.Core.Interface.Storage.Enum;
using EInfrastructure.Core.QiNiu.Storage.Config;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator
{
    /// <summary>
    /// 七牛配置信息校验
    /// </summary>
    public class QiNiuConfigValidator : AbstractValidator<QiNiuStorageConfig>
    {
        /// <summary>
        /// 
        /// </summary>
        public QiNiuConfigValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.AccessKey).NotNull()
                .WithMessage("AccessKey信息异常");
            RuleFor(x => x.SecretKey).NotNull()
                .WithMessage("SecretKey信息异常");
            RuleFor(x => x.Zones).IsInEnum().WithMessage("Zones信息配置异常");
            RuleFor(x => x.Bucket).NotNull()
                .WithMessage("Bucket信息异常");

            RuleFor(x => x.CallbackBody)
                .Must(string.IsNullOrEmpty).WithMessage("CallbackBody信息异常")
                .When(item => !string.IsNullOrEmpty(item.CallbackUrl));
            RuleFor(x => x.CallbackBodyType)
                .Must(item => !((int) item).IsExist(typeof(CallbackBodyTypeEnum))).WithMessage("CallbackBodyType信息异常")
                .When(item => !string.IsNullOrEmpty(item.CallbackUrl));
        }
    }
}