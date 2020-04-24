// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Aliyun.OSS;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Bucket;
using EInfrastructure.Core.Tools;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Bucket
{
    /// <summary>
    /// 创建空间验证
    /// </summary>
    public class CreateBucketParamValidator : AbstractValidator<CreateBucketParam>
    {
        /// <summary>
        ///
        /// </summary>
        public CreateBucketParamValidator()
        {
            RuleFor(x => x.BucketName).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请输入空间名");
            RuleFor(x => x.Region).Must(x => x.IsExist(typeof(ZoneEnum))).WithMessage("不支持的存储区域");
            RuleFor(x => x.StorageClass).Must(x => x.Id.IsExist(typeof(StorageClass))).WithMessage("不支持的存储类型")
                .When(x => x.StorageClass != null);
        }
    }
}
