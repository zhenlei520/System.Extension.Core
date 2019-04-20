// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.HelpCommon;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// stringcommon
    /// </summary>
    public class StringCommonUnitTest
    {
        #region hide mobile

        /// <summary>
        /// hide mobile
        /// </summary>
        /// <param name="mobile"></param>
        [Theory]
        [InlineData("13653841542")]
        public void HideMobile(string mobile)
        {
            string result = StringCommon.HideMobile(mobile);
            Check.True(result == "136****1542", "隐藏失败");
        }

        #endregion
    }
}