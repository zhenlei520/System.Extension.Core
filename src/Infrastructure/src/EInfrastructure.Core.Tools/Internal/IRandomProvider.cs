// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Ioc;

namespace EInfrastructure.Core.Tools.Internal
{
    /// <summary>
    /// 随机数
    /// </summary>
    public interface IRandomProvider : ISingleInstance, IIdentify
    {
        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">生成字符串长度</param>
        /// <param name="text">如果传入该参数，则从该文本中随机抽取（若未传值，默认为英文字母与数字串）</param>
        /// <returns></returns>
        string GenerateSpecifiedLengthStr(int length, string text = null);

        /// <summary>
        /// 生成指定长度限制的随机字母
        /// </summary>
        /// <param name="length">指定长度</param>
        string GenerateSpecifiedLengthLetters(int length);

        /// <summary>
        /// 生成指定长度限制的随机汉字
        /// </summary>
        /// <param name="length">指定长度</param>
        string GenerateSpecifiedLengthChinese(int length);

        /// <summary>
        /// 生成指定长度限制的随机数字(指定最大长度)
        /// </summary>
        /// <param name="length">指定长度</param>
        string GenerateSpecifiedLengthNumbers(int length);

        /// <summary>
        /// 生成最大长度限制的随机字符串
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <param name="text">如果传入该参数，则从该文本中随机抽取</param>
        string GenerateMaxLengthStr(int maxLength, string text = null);

        /// <summary>
        /// 生成最大长度限制的随机字母
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        string GenerateMaxLengthLetters(int maxLength);

        /// <summary>
        /// 生成最大长度限制的随机汉字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        string GenerateMaxLengthChinese(int maxLength);

        /// <summary>
        /// 生成最大长度限制的随机数字(指定最大长度)
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        string GenerateMaxLengthNumbers(int maxLength);

        /// <summary>
        /// 生成随机布尔值
        /// </summary>
        bool GenerateBool();

        /// <summary>
        /// 生成0-max的随机整数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        int GenerateInt(int maxValue);

        /// <summary>
        /// 生成随机日期
        /// </summary>
        /// <param name="beginYear">起始年份</param>
        /// <param name="endYear">结束年份</param>
        DateTime GenerateDate(int beginYear = 1980, int endYear = 2099);

        /// <summary>
        /// 生成随机枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        TEnum GenerateEnum<TEnum>();

        /// <summary>
        /// 随机得到数组中的一个
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">数组集合</param>
        T GetRandom<T>(T[] arr);

        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        T[] GetRandomArray<T>(T[] arr);
    }
}
