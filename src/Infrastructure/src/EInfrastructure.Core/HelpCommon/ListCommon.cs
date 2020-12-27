// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Configuration;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Common;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// List操作帮助类
    /// </summary>
    public static class ListCommon
    {
        #region List<T>操作

        #region List实体添加操作

        /// <summary>
        /// List实体添加操作
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t1">集合1</param>
        /// <param name="t2">集合2</param>
        /// <param name="isCheckRepeat">是否检查重复</param>
        /// <returns>返回t1与t2的和的集合</returns>
        public static List<T> Add<T>(this List<T> t1, List<T> t2, bool isCheckRepeat = false)
        {
            if (!isCheckRepeat)
            {
                t1.AddRange(t2);
                return t1;
            }

            return t1.AddCnki(t2);
        }

        /// <summary>
        /// 查重添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t1">集合1</param>
        /// <param name="t2">集合2</param>
        /// <returns></returns>
        private static List<T> AddCnki<T>(this List<T> t1, List<T> t2)
        {
            List<T> resultList = new CloneableClass().DeepClone(t1);
            List<string> resultStrList = new List<string>();
            foreach (var item in t2)
            {
                if (IsExist(item))
                    continue;
                resultList.Add(item);
            }

            return resultList;

            bool IsExist(T t)
            {
                if (t is string)
                {
                    return t1.Contains(t);
                }

                return GetResultList().Contains(new NewtonsoftJsonProvider().Serializer(t));
            }

            List<string> GetResultList()
            {
                if (resultStrList.Count == 0 && resultList.Count != 0)
                {
                    t1.ForEach(item => { resultStrList.Add(new NewtonsoftJsonProvider().Serializer(item)); });
                }

                return resultStrList;
            }
        }

        #endregion

        #region List实体减法操作

        /// <summary>
        /// List实体减法操作
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t1">集合1</param>
        /// <param name="t2">集合2</param>
        public static List<T> Except<T>(this List<T> t1, List<T> t2) where T : class, new()
        {
            return t1.ExceptNew(t2);
        }

        #endregion

        #endregion

        #region 两个集合计较

        /// <summary>
        /// 两个集合计较
        /// </summary>
        /// <param name="sourceList">源集合</param>
        /// <param name="optList">新集合</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static ListCompare<T, TKey> Compare<T, TKey>(this List<T> sourceList, List<T> optList)
            where T : IEntity<TKey> where TKey : struct
        {
            return new ListCompare<T, TKey>(sourceList, optList);
        }

        #endregion
    }
}
