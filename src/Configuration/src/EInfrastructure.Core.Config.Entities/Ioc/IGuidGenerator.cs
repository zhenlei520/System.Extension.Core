// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Config.Entities.Ioc
{
    /// <summary>
    /// Used to generate Ids.
    /// </summary>
    public interface IGuidGenerator : IDependency
    {
        /// <summary>
        /// Creates a new <see cref="Guid"/>.
        /// </summary>
        Guid Create();
    }
}
