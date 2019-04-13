// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.UCloud.Storage.Config;
using FluentValidation;

namespace EInfrastructure.Core.UCloud.Storage.Validator
{
    /// <summary>
    /// UCloud存储配置校验
    /// </summary>
    public class UCloudConfigValidator : AbstractValidator<UCloudStorageConfig>
    {
        public UCloudConfigValidator()
        {

        }
    }
}
