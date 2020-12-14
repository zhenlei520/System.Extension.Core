// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EInfrastructure.Core.Tools.Configuration;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// List扩展
    /// </summary>
    public partial class Extensions
    {
        #region 返回集合原来的第一个元素的值

        /// <summary>
        /// 返回集合原来的第一个元素的值,list集合中移除第一个值
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回集合原来的第一个元素的值</returns>
        public static T Shift<T>(this List<T> list)
        {
            if (list == null || !list.Any())
            {
                return default(T);
            }

            T res = list.FirstOrDefault();
            list = list.Skip(1).ToList();
            return res;
        }

        #endregion

        #region 添加单个对象

        /// <summary>
        /// 添加单个对象
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> AddNew<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        #endregion

        #region 添加多个集合

        /// <summary>
        /// 添加多个集合
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> AddNewMult<T>(this List<T> list, List<T> item)
        {
            list.AddRange(item);
            return list;
        }

        #endregion

        #region 两个集合计较

        /// <summary>
        /// 两个集合计较
        /// </summary>
        /// <param name="sourceList">源集合</param>
        /// <param name="optList">新集合</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ListCompare<T> Compare<T>(this List<T> sourceList, List<T> optList)
            where T : struct
        {
            return new ListCompare<T>(sourceList, optList);
        }

        #endregion

        #region 移除

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RemoveNew<T>(this List<T> list, T item)
        {
            list.Remove(item);
        }

        /// <summary>
        /// 移除集合
        /// </summary>
        /// <param name="list">源集合</param>
        /// <param name="delList">待删除的集合</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RemoveRangeNew<T>(this List<T> list, ICollection<T> delList)
        {
            delList.ToList().ForEach(item => { list.Remove(item); });
        }

        #endregion

        #region 按条件移除

        #region 移除单条符合条件的数据

        /// <summary>
        /// 移除单条符合条件的数据
        /// </summary>
        /// <param name="list">源集合</param>
        /// <param name="condtion">条件</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RemoveNew<T>(this List<T> list, Expression<Func<T, bool>> condtion)
        {
            var item = list.FirstOrDefault(condtion.Compile());
            list.RemoveNew(item);
        }

        #endregion

        #region 移除多条满足条件

        /// <summary>
        /// 移除多条满足条件
        /// </summary>
        /// <param name="list"></param>
        /// <param name="condition">条件</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RemoveRangeNew<T>(this List<T> list, Expression<Func<T, bool>> condition)
        {
            var items = list.Where(condition.Compile()).ToList();
            list.RemoveRangeNew(items);
        }

        #endregion

        #endregion
    }
}
