// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    ///
    /// </summary>
    internal class GetPrivateUrlParamValidator : AbstractValidator<GetPrivateUrlParam>
    {
        /// <summary>
        ///
        /// </summary>
        public GetPrivateUrlParamValidator()
        {
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请输入文件key");
            RuleFor(x => x.Expire).Must(x => x > 0).WithMessage("请输入过期时间");
        }
    }
}
