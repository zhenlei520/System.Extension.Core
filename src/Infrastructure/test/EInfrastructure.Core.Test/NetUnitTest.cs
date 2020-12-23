// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.Common;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// Net帮助类测试
    /// </summary>
    public class NetUnitTest : BaseUnitTest
    {
        #region 得到局域网ip

        /// <summary>
        /// 得到局域网ip
        /// </summary>
        [Fact]
        public void LanIpList()
        {
            var ip = NetCommon.LanIpList;
        }

        #endregion
    }
}
