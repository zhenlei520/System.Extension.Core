// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    /// 批量得到多个文件
    /// </summary>
    internal class GetFileRangeParamValidator : AbstractValidator<GetFileRangeParam>
    {
        /// <summary>
        ///
        /// </summary>
        public GetFileRangeParamValidator()
        {
            RuleFor(x => x.Keys).Must(x => x.Count > 0).WithMessage("请选择文件");
        }
    }
}
