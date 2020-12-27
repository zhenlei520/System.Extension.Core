// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Linq;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public partial class Extensions
    {
        #region 安全获取值，当值为null时，不会抛出异常

        /// <summary>
        /// 安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }

        #endregion

        #region 值互换(左边最小值,右边最大值)

        /// <summary>
        /// 值互换(左边最小值,右边最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        /// <typeparam name="T">执行后，参数1为较小者，参数2为较大者</typeparam>
        public static void ChangeResult<T>(ref T parameter1, ref T parameter2) where T : IComparable
        {
            if (parameter2.LessThan(parameter1))
            {
                var temp = parameter2;
                parameter2 = parameter1;
                parameter1 = temp;
            }
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref ushort parameter1, ref ushort parameter2)
        {
            ChangeResult<ushort>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref short parameter1, ref short parameter2)
        {
            ChangeResult<short>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref int parameter1, ref int parameter2)
        {
            ChangeResult<int>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref long parameter1, ref long parameter2)
        {
            ChangeResult<long>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref sbyte parameter1, ref sbyte parameter2)
        {
            ChangeResult<sbyte>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref byte parameter1, ref byte parameter2)
        {
            ChangeResult<byte>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref float parameter1, ref float parameter2)
        {
            ChangeResult<float>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref double parameter1, ref double parameter2)
        {
            ChangeResult<double>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref decimal parameter1, ref decimal parameter2)
        {
            ChangeResult<decimal>(ref parameter1, ref parameter2);
        }

        /// <summary>
        /// 值互换(转换后变成：参数1：最小值,参数2：最大值)
        /// </summary>
        /// <param name="parameter1">参数1</param>
        /// <param name="parameter2">参数2</param>
        public static void ChangeResult(this ref DateTime parameter1, ref DateTime parameter2)
        {
            ChangeResult<DateTime>(ref parameter1, ref parameter2);
        }
        #endregion

        #region ForEach循环

        /// <summary>
        /// ForEach循环
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this T[] collection, Action<T> action)
        {
            collection.ToList().ForEach(item =>
            {
                action?.Invoke(item);
            });
        }

        #endregion
    }
}
