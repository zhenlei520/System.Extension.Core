// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    /// 更改文件类型校验
    /// </summary>
    internal class ChangeTypeParamValidator : AbstractValidator<ChangeTypeParam>
    {
        /// <summary>
        ///
        /// </summary>
        public ChangeTypeParamValidator()
        {
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请选择文件");
            RuleFor(x => x.Type).Must(x => StorageClass.GetAll<StorageClass>().Any(y => y.Equals(x)))
                .WithMessage("文件存储类型不支持");
        }
    }
}
