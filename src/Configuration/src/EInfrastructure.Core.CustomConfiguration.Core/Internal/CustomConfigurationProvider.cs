// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Extensions.Configuration;

namespace EInfrastructure.Core.CustomConfiguration.Core.Internal
{
    /// <summary>
    ///
    /// </summary>
    public class CustomConfigurationProvider : ConfigurationProvider
    {
        private readonly ICustomConfigurationDataProvider _customConfigurationDataProvider;

        public CustomConfigurationProvider(ICustomConfigurationDataProvider customConfigurationDataProvider)
        {
            this._customConfigurationDataProvider = customConfigurationDataProvider;
        }

        /// <summary>
        ///
        /// </summary>
        public override void Load()
        {
            var data = this._customConfigurationDataProvider.GetAllData();
            if (data != null)
            {
                Data = data;
            }
        }
    }
}
