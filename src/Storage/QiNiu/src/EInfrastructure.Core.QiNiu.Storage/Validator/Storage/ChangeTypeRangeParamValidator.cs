// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    /// 批量更改文件类型校验
    /// </summary>
    internal class ChangeTypeRangeParamValidator : AbstractValidator<ChangeTypeRangeParam>
    {
        /// <summary>
        ///
        /// </summary>
        public ChangeTypeRangeParamValidator()
        {
            RuleFor(x => x.Keys).Must(x => x.Count > 0).WithMessage("请选择文件");
            RuleFor(x => x.Type).Must(x => x == 1 || x == 0).WithMessage("文件存储类型不支持，请输入1或者0");
        }
    }
}
