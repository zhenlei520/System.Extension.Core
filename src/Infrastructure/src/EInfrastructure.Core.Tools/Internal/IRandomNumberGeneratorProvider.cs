// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    /// 随机数字生成器
    /// </summary>
    public interface IRandomNumberGeneratorProvider : ISingleInstance, IIdentify
    {
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="isHighPerformance">是否高性能</param>
        int Generate(int min, int max, bool isHighPerformance = false);

        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        double GenerateDouble();

        /// <summary>
        /// 生成指定位数的随机数字
        /// </summary>
        /// <param name="num">长度</param>
        /// <returns></returns>
        int Generate(int num);
    }
}
