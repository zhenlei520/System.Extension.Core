// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Text;
using EInfrastructure.Core.Configuration.Configurations;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.Tools.Common;

namespace EInfrastructure.Core.Tools.Internal.Randoms
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public class RandomProvider : IdentifyDefault, IRandomProvider
    {
        /// <summary>
        /// 初始化随机数生成器
        /// </summary>
        /// <param name="generator">随机数字生成器</param>
        public RandomProvider(IRandomNumberGeneratorProvider generator = null)
        {
            _random = generator ?? new RandomNumberGeneratorProvider();
        }

        /// <summary>
        /// 随机数字生成器
        /// </summary>
        private readonly IRandomNumberGeneratorProvider _random;

        #region 生成指定长度的随机字符串

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">生成字符串长度</param>
        /// <param name="text">如果传入该参数，则从该文本中随机抽取（若未传值，默认为英文字母与数字串）</param>
        /// <returns></returns>
        public string GenerateSpecifiedLengthStr(int length, string text = null)
        {
            if (text == null)
                text = Const.Letters + Const.Numbers;
            var result = new StringBuilder();
            for (int i = 0; i < length; i++)
                result.Append(GetRandomChar(text));
            return result.ToString();
        }

        #endregion

        #region 生成指定长度限制的随机字母

        /// <summary>
        /// 生成指定长度限制的随机字母
        /// </summary>
        /// <param name="length">指定长度</param>
        public string GenerateSpecifiedLengthLetters(int length)
        {
            return GenerateSpecifiedLengthStr(length, Const.Letters);
        }

        #endregion

        #region 生成指定长度限制的随机汉字

        /// <summary>
        /// 生成指定长度限制的随机汉字
        /// </summary>
        /// <param name="length">指定长度</param>
        public string GenerateSpecifiedLengthChinese(int length)
        {
            return GenerateSpecifiedLengthStr(length, Const.SimplifiedChinese);
        }

        #endregion

        #region 生成指定长度限制的随机数字(指定最大长度)

        /// <summary>
        /// 生成指定长度限制的随机数字(指定最大长度)
        /// </summary>
        /// <param name="length">指定长度</param>
        public string GenerateSpecifiedLengthNumbers(int length)
        {
            return GenerateSpecifiedLengthStr(length, Const.Numbers);
        }

        #endregion

        #region 生成最大长度限制的随机字符串

        /// <summary>
        /// 生成最大长度限制的随机字符串
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        /// <param name="text">如果传入该参数，则从该文本中随机抽取</param>
        public string GenerateMaxLengthStr(int maxLength, string text = null)
        {
            if (text == null)
                text = Const.Letters + Const.Numbers;
            var result = new StringBuilder();
            var length = GetRandomLength(maxLength);
            for (int i = 0; i < length; i++)
                result.Append(GetRandomChar(text));
            return result.ToString();
        }

        #endregion

        #region 生成最大长度限制的随机字母

        /// <summary>
        /// 生成最大长度限制的随机字母
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public string GenerateMaxLengthLetters(int maxLength)
        {
            return GenerateMaxLengthStr(maxLength, Const.Letters);
        }

        #endregion

        #region 生成最大长度限制的随机汉字

        /// <summary>
        /// 生成最大长度限制的随机汉字
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public string GenerateMaxLengthChinese(int maxLength)
        {
            return GenerateMaxLengthStr(maxLength, Const.SimplifiedChinese);
        }

        #endregion

        #region 生成最大长度限制的随机数字(指定最大长度)

        /// <summary>
        /// 生成最大长度限制的随机数字(指定最大长度)
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public string GenerateMaxLengthNumbers(int maxLength)
        {
            return GenerateMaxLengthStr(maxLength, Const.Numbers);
        }

        #endregion

        #region 生成随机布尔值

        /// <summary>
        /// 生成随机布尔值
        /// </summary>
        public bool GenerateBool()
        {
            var random = _random.Generate(1, 100);
            if (random % 2 == 0)
                return false;
            return true;
        }

        #endregion

        #region 生成0-max的随机整数

        /// <summary>
        /// 生成0-max的随机整数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        public int GenerateInt(int maxValue)
        {
            return _random.Generate(0, maxValue + 1);
        }

        #endregion

        #region 生成随机日期

        /// <summary>
        /// 生成随机日期
        /// </summary>
        /// <param name="beginYear">起始年份</param>
        /// <param name="endYear">结束年份</param>
        public DateTime GenerateDate(int beginYear = 1980, int endYear = 2099)
        {
            var year = _random.Generate(beginYear, endYear);
            var month = _random.Generate(1, 13);
            var day = _random.Generate(1, 29);
            var hour = _random.Generate(1, 24);
            var minute = _random.Generate(1, 60);
            var second = _random.Generate(1, 60);
            return new DateTime(year, month, day, hour, minute, second);
        }

        #endregion

        #region 生成随机枚举

        /// <summary>
        /// 生成随机枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public TEnum GenerateEnum<TEnum>()
        {
            var list = EnumCommon.ToDescriptionDictionary<TEnum>().Select(x => x.Key).ToList();
            int index = _random.Generate(0, list.Count);
            return (TEnum) Enum.Parse(typeof(TEnum), list[index].ToString(), true);
        }

        #endregion

        #region 随机得到数组中的一个

        /// <summary>
        /// 随机得到数组中的一个
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">数组集合</param>
        public T GetRandom<T>(T[] arr)
        {
            if (arr == null || !arr.Any())
                return default;
            var r = new Random();
            return arr[r.Next(arr.Length)];
        }

        #endregion

        #region 对一个数组进行随机排序

        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public T[] GetRandomArray<T>(T[] arr)
        {
            //对数组进行随机排序的算法:随机选择两个位置，将两个位置上的值交换
            //交换的次数,这里使用数组的长度作为交换次数
            int count = arr.Length;
            //开始交换
            for (int i = 0; i < count; i++)
            {
                //生成两个随机数位置
                int randomNum1 = _random.Generate(0, arr.Length);
                int randomNum2 = _random.Generate(0, arr.Length);
                //定义临时变量
                //交换两个随机数位置的值
                var temp = arr[randomNum1];
                arr[randomNum1] = arr[randomNum2];
                arr[randomNum2] = temp;
            }

            return arr;
        }

        #endregion

        #region private methods

        #region 获取最大长度为maxlength的随机长度字符串

        /// <summary>
        /// 获取最大长度为maxlength的随机长度字符串
        /// </summary>
        /// <param name="maxLength">最大值（默认为int.MaxValue）</param>
        /// <param name="minLength">最小值（默认为0）</param>
        /// <returns></returns>
        private int GetRandomLength(int maxLength = int.MaxValue, int minLength = 1)
        {
            return _random.Generate(minLength, maxLength);
        }

        #endregion

        #region 获取随机字符

        /// <summary>
        /// 获取随机字符
        /// </summary>
        private string GetRandomChar(string text)
        {
            return text[_random.Generate(1, text.Length)].ToString();
        }

        #endregion

        #endregion
    }
}
