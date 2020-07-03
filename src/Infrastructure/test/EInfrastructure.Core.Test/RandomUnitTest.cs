// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.HelpCommon.Randoms.Interface;
using EInfrastructure.Core.Test.Base;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// Random
    /// </summary>
    public class RandomUnitTest : BaseUnitTest
    {
        private readonly IRandomBuilder _randomBuilder;
        public RandomUnitTest() : base()
        {
            _randomBuilder = provider.GetService<IRandomBuilder>();
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
