// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator
{
    /// <summary>
    ///
    /// </summary>
    internal class GetTagsBucketParamValidator : AbstractValidator<GetTagsBucketParam>
    {
        /// <summary>
        ///
        /// </summary>
        public GetTagsBucketParamValidator()
        {
            RuleFor(x => x.BucketName).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请输入空间名称");
        }
    }
}
