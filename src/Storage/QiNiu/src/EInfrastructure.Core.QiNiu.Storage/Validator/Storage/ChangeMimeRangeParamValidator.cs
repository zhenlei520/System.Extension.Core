// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    /// 批量文件mime校验
    /// </summary>
    internal class ChangeMimeRangeParamValidator : AbstractValidator<ChangeMimeRangeParam>
    {
        /// <summary>
        ///
        /// </summary>
        public ChangeMimeRangeParamValidator()
        {
            RuleFor(x => x.Keys).Must(x => x.Count > 0).WithMessage("请选择文件");
            RuleFor(x => x.MimeType).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请输入文件MimeType");
        }
    }
}
