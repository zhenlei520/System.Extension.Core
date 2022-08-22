﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Tests.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Attributes;
using EInfrastructure.Core.Tools.Systems;
using Xunit;

namespace EInfrastructure.Core.Tests
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
            var str = CustomAttributeCommon<ENameAttribute, string>.GetCustomAttributeValue(
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
