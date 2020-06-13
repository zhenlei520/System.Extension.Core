// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    ///
    /// </summary>
    internal class CopyFileParamValidator : AbstractValidator<CopyFileParam>
    {
        /// <summary>
        ///
        /// </summary>
        public CopyFileParamValidator()
        {
            RuleFor(x => x.SourceKey).Must(x => !string.IsNullOrEmpty(x)).WithMessage("源空间key不能为空");
            RuleFor(x => x.OptKey).Must(x => !string.IsNullOrEmpty(x)).WithMessage("目标文件key不能为空");
        }
    }
}
