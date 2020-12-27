// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools.Attributes;
using EInfrastructure.Core.Tools.Common;
using EInfrastructure.Core.Tools.Common.Systems;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 得到Cpu核心数
    /// </summary>
    public class EnvironmentUnitTest : BaseUnitTest
    {
        /// <summary>
        /// 得到Cpu核心数
        /// </summary>
        /// <returns></returns>
        [Fact]
        public int GetProcessorCount()
        {
            var version = Environment.Version;
            var runInfo = EnvironmentCommon.GetRun;
            var str = CustomAttributeCommon.GetCustomAttribute<ENameAttribute, string>(
                typeof(EnvironmentCommon.RunInfo), x => x.Name,"UseMemory");
            // while (true)
            // {
            //     Console.WriteLine(str+"："+runInfo.UseMemory);
            //     Thread.Sleep(500);
            // }

            return EnvironmentCommon.GetRun.ProcessorCount;
        }
    }
}
