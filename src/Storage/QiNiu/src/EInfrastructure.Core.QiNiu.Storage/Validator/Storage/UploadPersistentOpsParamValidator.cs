// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    /// 上传凭证检查
    /// </summary>
    public class UploadPersistentOpsParamValidator : AbstractValidator<UploadPersistentOpsParam>
    {
        /// <summary>
        ///
        /// </summary>
        public UploadPersistentOpsParamValidator()
        {
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x)).WithMessage("文件key不能为空");
        }
    }
}
