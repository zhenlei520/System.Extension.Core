// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Tests.Base;
using Xunit;

namespace EInfrastructure.Core.Tests
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageCommonUnitTest : BaseUnitTest
    {
        /// <summary>
        /// 得到所有的图片集合
        /// </summary>
        /// <param name="htmlStr"></param>
        [Theory]
        [InlineData("")]
        public void GetImageUrls(string htmlStr)
        {
        }

        [Theory]
        [InlineData("")]
        public void GetImageUrl(string html)
        {
        }
    }
}
