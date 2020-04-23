// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.QiNiu.Storage.Validator.Storage
{
    /// <summary>
    /// 得到管理Token检查
    /// </summary>
    public class GetManageTokenParamValidator : AbstractValidator<GetManageTokenParam>
    {
        /// <summary>
        ///
        /// </summary>
        public GetManageTokenParamValidator()
        {
            RuleFor(x => x.Url).Must(x => !string.IsNullOrEmpty(x)).WithMessage("url不能为空");
        }
    }
}
