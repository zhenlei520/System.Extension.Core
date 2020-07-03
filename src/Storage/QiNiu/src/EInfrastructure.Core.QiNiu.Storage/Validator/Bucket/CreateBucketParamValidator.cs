// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using EInfrastructure.Core.Tools;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Bucket
{
    /// <summary>
    /// 创建空间检查
    /// </summary>
    internal class CreateBucketParamValidator : AbstractValidator<CreateBucketParam>
    {
        /// <summary>
        ///
        /// </summary>
        public CreateBucketParamValidator()
        {
            RuleFor(x => x.BucketName).Must(x => !string.IsNullOrEmpty(x)).WithMessage("空间名称不能为空");
            RuleFor(x => x.Zone).Must(x => x.IsExist(typeof(ZoneEnum))).WithMessage("不支持的存储区域")
                .When(x => x.Zone != null);
        }
    }
}
