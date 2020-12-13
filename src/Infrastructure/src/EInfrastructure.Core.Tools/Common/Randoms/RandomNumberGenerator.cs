// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Text;
using EInfrastructure.Core.Tools.Common.Randoms.Interface;

namespace EInfrastructure.Core.Tools.Common.Randoms
{
    /// <summary>
    /// 随机数字生成器
    /// </summary>
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        /// <summary>
        /// 随机数
        /// </summary>
        protected Random Random;

        /// <summary>
        /// 初始化随机数字生成器
        /// </summary>
        public RandomNumberGenerator()
        {
            Random = new Random();
        }

        #region 生成随机数字

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="isHighPerformance">高性能</param>
        public int Generate(int min, int max, bool isHighPerformance = false)
        {
            if (isHighPerformance)
            {
                return Random.Next(min, max);
            }

            return new Random(GetRandomSeed()).Next(min, max);
        }

        #endregion

        #region 生成指定位数的随机数字

        /// <summary>
        /// 生成指定位数的随机数字
        /// </summary>
        /// <param name="num">长度</param>
        /// <returns></returns>
        public int Generate(int num)
        {
            Random ran = new Random(GetRandomSeed());

            StringBuilder sb1 = new StringBuilder();

            sb1.Append("1");

            for (int i = 1; i < num; i++)
            {
                sb1.Append("0");
            }

            int startNum = Convert.ToInt32(sb1.ToString());

            int endNum = Convert.ToInt32(sb1.Append("0").ToString()) - 1;

            return ran.Next(startNum, endNum);
        }

        #endregion

        #region 随机数

        /// <summary>
        /// 随机数
        /// </summary>
        /// <returns></returns>
        internal int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng =
                new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        #endregion
    }
}
