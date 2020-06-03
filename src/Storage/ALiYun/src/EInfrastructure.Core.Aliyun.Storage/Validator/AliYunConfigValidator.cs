// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Validation;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator
{
    /// <summary>
    /// 七牛配置信息校验
    /// </summary>
    internal class AliYunConfigValidator : AbstractValidator<ALiYunStorageConfig>,IFluentlValidator<ALiYunStorageConfig>
    {
        /// <summary>
        ///
        /// </summary>
        public AliYunConfigValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.AccessKey).NotNull()
                .WithMessage("AccessKey信息异常");
            RuleFor(x => x.SecretKey).NotNull()
                .WithMessage("SecretKey信息异常");
            RuleFor(x => x.MaxRetryTimes).Must(x => x > 0).WithMessage("最大重试次数有误");

            RuleFor(x=>x.test).ScalePrecision()
        }
    }
}
