// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// Random
    /// </summary>
    public class RandomUnitTest : BaseUnitTest
    {
        private readonly IRandomProvider _randomProvider;
        public RandomUnitTest() : base()
        {
            _randomProvider = provider.GetService<IRandomProvider>();
        }

        /// <summary>
        ///
        /// </summary>
        [Fact]
        public void GetIdentify()
        {

        }
    }
}
