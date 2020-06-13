// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    ///
    /// </summary>
    internal class MoveFileParamRangeValidator : AbstractValidator<MoveFileRangeParam>
    {
        /// <summary>
        ///
        /// </summary>
        public MoveFileParamRangeValidator()
        {
            RuleFor(x => x.MoveFiles).Must(x => x.Count > 0).WithMessage("移动文件信息异常");
        }
    }
}
