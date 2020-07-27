// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    ///
    /// </summary>
    public class CustomConfigurationSource : IConfigurationSource
    {
        private readonly ICustomConfigurationDataProvider _customConfigurationDataProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="customConfigurationDataProvider"></param>
        public CustomConfigurationSource(ICustomConfigurationDataProvider customConfigurationDataProvider)
        {
            this._customConfigurationDataProvider = customConfigurationDataProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigurationProvider(_customConfigurationDataProvider);
        }
    }
}
