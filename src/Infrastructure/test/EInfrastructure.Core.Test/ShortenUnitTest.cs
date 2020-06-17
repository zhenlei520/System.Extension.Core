// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.Test.Base;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Tasks;
using EInfrastructure.Core.Tools.Unique;
using Xunit;

namespace EInfrastructure.Core.Test
{
    /// <summary>
    /// 短参数方法
    /// </summary>
    public class ShortenUnitTest : BaseUnitTest
    {
        #region 得到短参数

        /// <summary>
        /// 得到短参数
        /// </summary>
        /// <param name="param"></param>
        [Theory]
        [InlineData("id=1&name=EInfrastructure.Core.Test")]
        public void GetShortParam(string param)
        {
            var stortUrls = ShortenCommon.GetShortParam(param);
        }

        #endregion

        #region 得到短参数

        /// <summary>
        /// 得到短参数
        /// </summary>
        /// <param name="number">生成多少短参数</param>
        [Theory]
        [InlineData(9999)]
        public void GetShortParam2(int number)
        {
            List<string> shortList = new List<string>();
            List<Guid> guidList = new List<Guid>();
            TaskPool<Guid> taskCommon = new TaskPool<Guid>(200, (guid) =>
            {
                lock (shortList)
                {
                    Console.WriteLine("线程2：" + Task.CurrentId + ",Guid:" + guid);
                    var param = ShortenCommon.GetShortParam(guid.ToString());
                    shortList.AddRange(param.Distinct());
                }
            });
            for (int index = 0; index < number; index++)
            {
                guidList.Add(Guid.NewGuid());
            }

            bool isEnd = false;
            var startNew = System.Diagnostics.Stopwatch.StartNew();
            taskCommon.AddJob(guidList.ToArray());
            startNew.Stop();
            Console.WriteLine($"生成时间为：{startNew.ElapsedMilliseconds}ms");
            taskCommon.SetFinishJobAction(() =>
            {
                isEnd = true;
                Console.WriteLine(
                    $"生成短参数数：{number}，生成短参数数量：{shortList.Count}，非重复的短参数numer为：{shortList.Distinct().Count()}");

                Check.True(number * 1.0 == shortList.Distinct().Count() / 4 * 1.0, "存在重复的短参数");
            });
            taskCommon.Run();
            while (!isEnd)
            {
                Thread.Sleep(1000);
            }
        }

        #endregion
    }
}
