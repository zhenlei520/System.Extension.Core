// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    ///
    /// </summary>
    public interface ICustomConfigurationOptionsExtension
    {
        /// <summary>
        /// Registered child service.
        /// </summary>
        /// <param name="services">add service to the <see cref="IServiceCollection" /></param>
        void AddServices(IServiceCollection services);
    }
}
