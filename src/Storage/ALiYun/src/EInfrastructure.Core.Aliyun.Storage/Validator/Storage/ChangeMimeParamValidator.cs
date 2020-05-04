// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    /// 文件mime校验
    /// </summary>
    internal class ChangeMimeParamValidator : AbstractValidator<ChangeMimeParam>
    {
        /// <summary>
        ///
        /// </summary>
        public ChangeMimeParamValidator()
        {
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请选择文件");
            RuleFor(x => x.MimeType).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请输入文件MimeType");
        }
    }
}
