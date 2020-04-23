// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator
{
    /// <summary>
    /// 清空空间标签
    /// </summary>
    internal class ClearTagBucketParamValidator : AbstractValidator<ClearTagBucketParam>
    {
        /// <summary>
        ///
        /// </summary>
        public ClearTagBucketParamValidator()
        {
            RuleFor(x => x.BucketName).Must(x => !string.IsNullOrEmpty(x)).WithMessage("空间名称不能为空");
        }
    }
}
