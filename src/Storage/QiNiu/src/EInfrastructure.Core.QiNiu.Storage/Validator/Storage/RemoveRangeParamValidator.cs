// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    /// 移除文件
    /// </summary>
    internal class RemoveRangeParamValidator : AbstractValidator<RemoveRangeParam>
    {
        /// <summary>
        ///
        /// </summary>
        public RemoveRangeParamValidator()
        {
            RuleFor(x => x.Keys).Must(x => x != null && x.Count > 0).WithMessage("请添加要移除的文件key");
        }
    }
}
