// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    /// 设置文件过期自动删除校验
    /// </summary>
    internal class SetExpireParamValidator : AbstractValidator<SetExpireParam>
    {
        /// <summary>
        ///
        /// </summary>
        public SetExpireParamValidator()
        {
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请选择文件");
            RuleFor(x => x.Expire).Must(x => x > 0).WithMessage("过期时间设置有误");
        }
    }
}
