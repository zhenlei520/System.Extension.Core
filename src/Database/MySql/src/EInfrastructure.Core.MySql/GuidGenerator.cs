// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Tools.Enumerations;
using EInfrastructure.Core.Tools.Unique;

namespace EInfrastructure.Core.MySql
{
    /// <summary>
    ///
    /// </summary>
    public class GuidGenerator : IGuidGenerator
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Guid Create()
        {
            return UniqueCommon.Guids(SequentialGuidType.SequentialAsString);
        }
    }
}
