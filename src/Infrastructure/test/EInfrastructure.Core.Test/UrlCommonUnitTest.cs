// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.Url;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    ///
    /// </summary>
    public class UrlCommonUnitTest : BaseUnitTest
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        [Theory]
        [InlineData("http://www.baidu.com")]
        [InlineData("//www.baidu.com/index?id=2")]
        public void GetFullUrl(string url)
        {
            var uri = new Url(url, true);
            var res = uri.GetFullQueryPath(isContainerScheme:true);
        }
    }
}
