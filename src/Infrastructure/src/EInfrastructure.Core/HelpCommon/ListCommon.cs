// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.SerializeExtensions;
using EInfrastructure.Core.Config.SerializeExtensions.Interfaces;
using EInfrastructure.Core.Serialize.NewtonsoftJson;
using EInfrastructure.Core.Tools;

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

                return GetResultList().Contains(new JsonService(new List<IJsonProvider>()
                {
                    new NewtonsoftJsonProvider()
                }).Serializer(t));
            }

            List<string> GetResultList()
            {
                if (resultStrList.Count == 0 && resultList.Count != 0)
                {
                    t1.ForEach(item =>
                    {
                        resultStrList.Add(new JsonService(new List<IJsonProvider>
                        {
                            new NewtonsoftJsonProvider()
                        }).Serializer(item));
                    });
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
        /// <param name="isCheckRepeat">排除t1中包含t2的项</param>
        public static List<T> Except<T>(this List<T> t1, List<T> t2, bool isCheckRepeat ) where T : class, new()
        {
            if (!isCheckRepeat)
                return Tools.ListCommon.Except(t1, t2);
            return MinusExcept(t1, t2);
        }

        /// <summary>
        /// 查重添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t1">集合1</param>
        /// <param name="t2">集合2</param>
        /// <returns></returns>
        private static List<T> MinusExcept<T>(this List<T> t1, List<T> t2) where T : class, new()
        {
            JsonService jsonService = new JsonService(new List<IJsonProvider>
            {
                new NewtonsoftJsonProvider()
            });

            List<T> resultList = new CloneableClass().DeepClone(t1);
            List<string> resultStrList = new List<string>();
            if (resultStrList.Count == 0 && resultList.Count != 0)
            {
                t1.ForEach(item => { resultStrList.Add(jsonService.Serializer(item)); });
            }

            foreach (var item in t2)
            {
                var str = jsonService.Serializer(item);
                if (resultStrList.Contains(str))
                {
                    resultStrList.Remove(str);
                }
            }

            return resultStrList.Select(x => jsonService.Deserialize<T>(x)).ToList();
        }

        #endregion

        #endregion
    }
}
