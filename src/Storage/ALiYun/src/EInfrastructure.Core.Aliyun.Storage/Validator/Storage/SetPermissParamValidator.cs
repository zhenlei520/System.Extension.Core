// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    /// 空间访问权限验证
    /// </summary>
    public class SetPermissParamValidator : AbstractValidator<SetPermissParam>
    {
        /// <summary>
        ///
        /// </summary>
        public SetPermissParamValidator()
        {
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x) && x.Trim().Length > 0).WithMessage("请输入文件key");
        }
    }
}
