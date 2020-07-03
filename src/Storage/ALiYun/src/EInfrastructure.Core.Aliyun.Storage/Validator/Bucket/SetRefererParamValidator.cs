// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Bucket
{
    /// <summary>
    ///
    /// </summary>
    public class SetRefererParamValidator : AbstractValidator<SetRefererParam>
    {
        /// <summary>
        ///
        /// </summary>
        public SetRefererParamValidator()
        {
            RuleFor(x => x.RefererList).Must(x => x.Count > 0).WithMessage("请设置白名单").When(x => !x.IsAllowNullReferer);
        }
    }
}
