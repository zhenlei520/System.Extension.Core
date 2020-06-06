// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Params.Storage;
using FluentValidation;

namespace EInfrastructure.Core.Aliyun.Storage.Validator.Storage
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
        }
    }
}
