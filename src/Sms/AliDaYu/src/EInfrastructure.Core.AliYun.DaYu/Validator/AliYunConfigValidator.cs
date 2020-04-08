// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.AliYun.DaYu.Config;
using EInfrastructure.Core.Validation;
using FluentValidation;

namespace EInfrastructure.Core.AliYun.DaYu.Validator
{
    /// <summary>
    /// 阿里大于短信配置校验
    /// </summary>
    internal class AliYunConfigValidator : AbstractValidator<AliSmsConfig>, IFluentlValidator<AliSmsConfig>
    {
        /// <summary>
        ///
        /// </summary>
        public AliYunConfigValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.AccessKey).NotNull()
                .WithMessage("AccessKey ID信息异常");
            RuleFor(x => x.EncryptionKey).NotNull()
                .WithMessage("AccessKey Secret信息异常");
        }
    }
}
