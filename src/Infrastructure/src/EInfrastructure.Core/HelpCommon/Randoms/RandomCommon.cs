// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.HelpCommon.Randoms.Interface;

namespace EInfrastructure.Core.HelpCommon.Randoms
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public class RandomCommon : Tools.Randoms.RandomCommon, IRandomBuilder
    {
        /// <summary>
        /// 初始化随机数生成器
        /// </summary>
        /// <param name="generator">随机数字生成器</param>
        public RandomCommon(IRandomNumberGenerator generator = null)
        {
            _random = generator ?? new RandomNumberGenerator();
        }

        /// <summary>
        /// 随机数字生成器
        /// </summary>
        private readonly IRandomNumberGenerator _random;
    }
}
