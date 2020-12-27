// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.Common;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 地址帮助
    /// </summary>
    public class PathCommonUnitTest : BaseUnitTest
    {
        [Fact]
        public void GetPath()
        {
            PathCommon.GetExtension("http://img.bflove.cn/1.jpg?a=2.gif"); //输出结果.jpg
            PathCommon.GetExtension("http://img.bflove.cn/1.gif"); //输出结果.gif
        }
    }
}
