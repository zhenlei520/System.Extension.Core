// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.Common;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 删除文件
    /// </summary>
    public class FileCommonUnitTest: BaseUnitTest
    {
        /// <summary>
        /// 删除文件夹
        /// </summary>
        [Theory]
        [InlineData("E:\\temp")]
        public void DeleteDirectory(string dir)
        {
            FileCommon.DeleteDirectory(dir);
        }
    }
}
