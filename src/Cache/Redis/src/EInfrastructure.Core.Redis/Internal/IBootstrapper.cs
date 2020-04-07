// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Redis.Internal
{
    /// <summary>
    /// Represents bootstrapping logic. For example, adding initial state to the storage or querying certain entities.
    /// </summary>
    public interface IBootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        Task BootstrapAsync(CancellationToken stoppingToken);
    }
}
