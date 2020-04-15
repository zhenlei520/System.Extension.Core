// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using EInfrastructure.Core.QiNiu.Storage.Validator;
using EInfrastructure.Core.Validation.Common;
using Xunit;

namespace EInfrastructure.Core.QiNiu.Storage.Test
{
    /// <summary>
    ///
    /// </summary>
    public class QiNiuConfigValidatorUnitTest
    {
        [Fact]
        public void CheckQiNiuConfigValidaor()
        {
            QiNiuStorageConfig qiNiuStorageConfig =
                new QiNiuStorageConfig("access_key", "secretkey", ZoneEnum.ZoneUsNorth, "host", "bucket");
        }
    }
}
