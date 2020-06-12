// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    ///
    /// </summary>
    public class SetExpireRangeParamValidator : AbstractValidator<SetExpireRangeParam>
    {
        public SetExpireRangeParamValidator()
        {
            RuleFor(x => x.Expire).GreaterThan(0).WithMessage("过期时间需大于0");
            RuleFor(x => x.Keys).Must(x => x.All(y=>!string.IsNullOrEmpty(y))).WithMessage("文件key集合异常");
        }
    }
}
