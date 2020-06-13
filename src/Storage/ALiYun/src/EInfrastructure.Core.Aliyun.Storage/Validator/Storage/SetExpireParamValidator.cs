// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    /// 设置生存时间
    /// </summary>
    public class SetExpireParamValidator : AbstractValidator<SetExpireParam>
    {
        /// <summary>
        ///
        /// </summary>
        public SetExpireParamValidator()
        {
            RuleFor(x => x.Expire).GreaterThan(0).WithMessage("过期时间需大于0");
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x)).WithMessage("文件key异常");
        }
    }
}
