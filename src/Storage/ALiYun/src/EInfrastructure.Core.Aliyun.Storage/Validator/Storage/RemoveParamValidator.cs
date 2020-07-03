// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
{
    /// <summary>
    /// 移除文件
    /// </summary>
    internal class RemoveParamValidator : AbstractValidator<RemoveParam>
    {
        /// <summary>
        ///
        /// </summary>
        public RemoveParamValidator()
        {
            RuleFor(x => x.Key).Must(x => !string.IsNullOrEmpty(x)).WithMessage("请输入文件key");
        }
    }
}
