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
            TaskPool<Guid> taskCommon = new TaskPool<Guid>(1, (guid) =>
            {
                lock (shortList)
                {
                   string param= HashAlgorithmHelper.ComputeHash<MD5>(guid.ToString());
                    // var param = ShortenCommon.GetShortParam(s);
                    // shortList.AddRange(param);
                    shortList.Add(param);
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

    static class THashAlgorithmInstances<THashAlgorithm> where THashAlgorithm : HashAlgorithm
    {
        /// <summary>
        /// 线程静态变量。
        /// 即：这个变量在每个线程中都是唯一的。
        /// 再结合泛型类实现了该变量在不同泛型或不同的线程先的变量都是唯一的。
        /// 这样做的目的是为了避开多线程问题。
        /// </summary>
        [ThreadStatic]
        static THashAlgorithm instance;

        public static THashAlgorithm Instance => instance ?? Create(); // C# 语法糖，低版本可以改为 { get { return instance != null ? instance : Create(); } }

        /// <summary>
        /// 寻找 THashAlgorithm 类型下的 Create 静态方法，并执行它。
        /// 如果没找到，则执行 Activator.CreateInstance 调用构造方法创建实例。
        /// 如果 Activator.CreateInstance 方法执行失败，它会抛出异常。
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        static THashAlgorithm Create()
        {
            var createMethod = typeof(THashAlgorithm).GetMethod(
                nameof(HashAlgorithm.Create), // 这段代码同 "Create"，低版本 C# 可以替换掉
                BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
                Type.DefaultBinder,
                Type.EmptyTypes,
                null);

            if (createMethod != null)
            {
                instance = (THashAlgorithm)createMethod.Invoke(null, new object[] { });
            }
            else
            {
                instance = Activator.CreateInstance<THashAlgorithm>();
            }

            return instance;
        }
    }

    public static class HashAlgorithmHelper
    {
        static readonly char[] Digitals = {'0','1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        static string ToString(byte[] bytes)
        {
            const int byte_len = 2; // 表示一个 byte 的字符长度。

            var chars = new char[byte_len * bytes.Length];

            var index = 0;

            foreach (var item in bytes)
            {
                chars[index] = Digitals[item >> 4/* byte high */]; ++index;
                chars[index] = Digitals[item & 15/* byte low  */]; ++index;
            }

            return new string(chars);
        }

        public static string ComputeHash<THashAlgorithm>(string input) where THashAlgorithm : HashAlgorithm
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            return ToString(THashAlgorithmInstances<THashAlgorithm>.Instance.ComputeHash(bytes));
        }
    }
}
