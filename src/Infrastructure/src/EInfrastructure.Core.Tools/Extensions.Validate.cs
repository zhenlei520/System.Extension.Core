// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Tools.Component;
using EInfrastructure.Core.Tools.Internal;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 校验类
    /// </summary>
    public partial class Extensions
    {
        #region 初始化校验

        private static IRegexConfigurationsProvider _regexConfigurations;
        private static ICollection<IMobileRegexConfigurationsProvider> _mobileRegexConfigurations;

        /// <summary>
        /// 初始化校验
        /// </summary>
        static void InitValidate()
        {
            _regexConfigurations = new RegexConfigurationsValidateDefaultProvider();
            _mobileRegexConfigurations = ServiceProvider.GetServiceProvider()
                .GetServices<IMobileRegexConfigurationsProvider>().ToList();
        }

        #endregion

        #region 是否为Double类型

        /// <summary>
        /// 是否为Double类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDouble(this object expression)
        {
            return expression.ConvertToDouble().IsNull() == false;
        }

        #endregion

        #region 是否为Decimal类型

        /// <summary>
        /// 是否为Decimal类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDecimal(this object expression)
        {
            return expression.ConvertToDecimal().IsNull() == false;
        }

        #endregion

        #region 是否为Long类型

        /// <summary>
        /// 是否为Long类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsLong(this object expression)
        {
            return expression.ConvertToLong().IsNull() == false;
        }

        #endregion

        #region 是否为Int类型

        /// <summary>
        /// 是否为Int类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsInt(this object expression)
        {
            return expression.ConvertToInt().IsNull() == false;
        }

        #endregion

        #region 是否为Short类型

        /// <summary>
        /// 是否为Short类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsShort(this object expression)
        {
            return expression.ConvertToShort().IsNull() == false;
        }

        #endregion

        #region 是否为Guid类型

        /// <summary>
        /// 是否为Guid类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsGuid(this object expression)
        {
            return expression.ConvertToGuid().IsNull() == false;
        }

        #endregion

        #region 是否为Char类型

        /// <summary>
        /// 是否为Char类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsChar(this object expression)
        {
            return expression.ConvertToChar().IsNull() == false;
        }

        #endregion

        #region 是否为Float类型

        /// <summary>
        /// 是否为Float类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsFloat(this object expression)
        {
            return expression.ConvertToFloat().IsNull() == false;
        }

        #endregion

        #region 是否为DateTime类型

        /// <summary>
        /// 是否为DateTime类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsDateTime(this object expression)
        {
            return expression.ConvertToDateTime().IsNull() == false;
        }

        #endregion

        #region 是否为Byte类型

        /// <summary>
        /// 是否为Byte类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsByte(this object expression)
        {
            return expression.ConvertToByte().IsNull() == false;
        }

        #endregion

        #region 是否为SByte类型

        /// <summary>
        /// 是否为SByte类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsSByte(this object expression)
        {
            return expression.ConvertToSByte().IsNull() == false;
        }

        #endregion

        #region 是否为Bool类型

        /// <summary>
        /// 是否为Bool类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsBool(this object expression)
        {
            return expression.ConvertToBool().IsNull() == false;
        }

        #endregion

        #region 设置正则表达式配置驱动（不建议更换默认配置）

        /// <summary>
        /// 设置正则表达式配置驱动（不建议更换默认配置）
        /// </summary>
        /// <param name="regexConfigurations"></param>
        public static void SetRegexConfigurations(IRegexConfigurationsProvider regexConfigurations)
        {
            _regexConfigurations = regexConfigurations ?? throw new ArgumentNullException(nameof(regexConfigurations));
        }

        #endregion

        #region 得到正则表达式配置驱动

        /// <summary>
        /// 得到正则表达式配置驱动
        /// </summary>
        /// <returns></returns>
        public static IRegexConfigurationsProvider GetRegexConfigurations()
        {
            return _regexConfigurations;
        }

        #endregion

        #region 判断值是否在指定范围内

        /// <summary>
        /// 判断值是否在指定范围内
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="rangeMode">区间模式，如果为null，则视为开区间</param>
        public static bool InRange<T>(this T value, T minValue, T maxValue, RangeMode rangeMode = null)
            where T : IComparable
        {
            if (rangeMode == null || rangeMode.Equals(RangeMode.Open))
            {
                return value.GreaterThanOrEqualTo(minValue) && value.LessThanOrEqualTo(maxValue);
            }

            if (rangeMode.Equals(RangeMode.Close))
            {
                return value.GreaterThan(minValue) && value.LessThan(maxValue);
            }

            if (rangeMode.Equals(RangeMode.OpenClose))
            {
                return value.GreaterThanOrEqualTo(minValue) && value.LessThan(maxValue);
            }

            if (rangeMode.Equals(RangeMode.CloseOpen))
            {
                return value.GreaterThan(minValue) && value.LessThanOrEqualTo(maxValue);
            }

            throw new NotImplementedException("不支持的区间模式");
        }

        #endregion

        #region 参数1大于参数2

        /// <summary>
        /// 参数1大于参数2
        /// </summary>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        public static bool GreaterThan<T>(this T param1, T param2) where T : IComparable
        {
            return param1.CompareTo(param2) == 1;
        }

        /// <summary>
        /// 参数1大于等于参数2
        /// </summary>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        public static bool GreaterThanOrEqualTo<T>(this T param1, T param2) where T : IComparable
        {
            return param1.CompareTo(param2) == 0 || param1.CompareTo(param2) == 1;
        }

        #endregion

        #region 参数1小于等于参数2

        /// <summary>
        /// 参数1小于参数2
        /// </summary>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        public static bool LessThan<T>(this T param1, T param2) where T : IComparable
        {
            return param1.CompareTo(param2) == -1;
        }

        /// <summary>
        /// 参数1小于等于参数2
        /// </summary>
        /// <param name="param1">参数1</param>
        /// <param name="param2">参数2</param>
        public static bool LessThanOrEqualTo<T>(this T param1, T param2) where T : IComparable
        {
            return param1.CompareTo(param2) == 0 || param1.CompareTo(param2) == -1;
        }

        #endregion

        #region 判断是否在/不在指定列表内

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsIn<T>(this T source, params T[] list) where T : IComparable =>
            list.Any(t => t.CompareTo(source) == 0);

        /// <summary>
        /// 判断不在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsNotIn<T>(this T source, params T[] list) where T : IComparable =>
            list.All(t => t.CompareTo(source) != 0);

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsIn<T>(this T source, IEnumerable<T> list) where T : IComparable =>
            list.Any(t => t.CompareTo(source) == 0);

        /// <summary>
        /// 判断不在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsNotIn<T>(this T source, IEnumerable<T> list) where T : IComparable =>
            list.All(t => t.CompareTo(source) != 0);

        /// <summary>
        /// 是否在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsIn<T>(this T source, HashSet<T> list) where T : IComparable => list.Contains(source);

        /// <summary>
        /// 判断不在指定列表内
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="list">列表</param>
        public static bool IsNotIn<T>(this T source, HashSet<T> list) where T : IComparable => !list.Contains(source);

        #endregion

        #region 刷新手机号验证

        /// <summary>
        /// 刷新手机号验证
        /// </summary>
        /// <param name="regexConfigurationses"></param>
        public static void RefreshMobileRegexConfigurations(
            ICollection<IMobileRegexConfigurationsProvider> regexConfigurationses)
        {
            _mobileRegexConfigurations = regexConfigurationses ?? new List<IMobileRegexConfigurationsProvider>();
        }

        #endregion
    }
}
