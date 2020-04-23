// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    /// 检查文件是否存在
    /// </summary>
    internal class ExistParamValidator : AbstractValidator<ExistParam>
    {
        /// <summary>
        ///
        /// </summary>
        public ExistParamValidator()
        {
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请选择文件");
        }
    }
}
