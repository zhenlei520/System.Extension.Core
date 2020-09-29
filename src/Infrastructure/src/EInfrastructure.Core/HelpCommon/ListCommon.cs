// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Data;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
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
        public static ListCompare<T, TKey> CompareNew<T, TKey>(this List<T> sourceList, List<T> optList)
            where T : IEntity<TKey> where TKey : struct
        {
            return new ListCompare<T, TKey>(sourceList, optList);
        }

        /// <summary>
        /// 两个集合计较
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ListCompare<T, string> CompareNew<T>(this List<T> sourceList, List<T> optList)
            where T : IEntity<string>
        {
            return new ListCompare<T, string>(sourceList, optList);
        }

        #endregion

        #region 对list集合分页

        /// <summary>
        /// 对list集合分页
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageSize">页码</param>
        /// <param name="pageIndex">页大小</param>
        /// <param name="isTotal">是否计算总条数</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PageData<T> ListPager<T>(this ICollection<T> query, int pageSize, int pageIndex, bool isTotal)
        {
            PageData<T> list = new PageData<T>();

            if (isTotal)
            {
                list.RowCount = query.Count();
            }

            if (pageIndex - 1 < 0)
            {
                throw new BusinessException("页码必须大于等于1", HttpStatus.Err.Id);
            }

            query = query.Skip((pageIndex - 1) * pageSize).ToList();
            if (pageSize > 0)
            {
                list.Data = query.Take(pageSize).ToList();
            }
            else if (pageSize != -1)
            {
                throw new BusinessException("页大小须等于-1或者大于0", HttpStatus.Err.Id);
            }
            else
            {
                list.Data = query.ToList();
            }

            return list;
        }

        #endregion

        /// <summary>
        ///
        /// </summary>
        public class ListCompare<T, TKey> where T : IEntity<TKey>
        {
            /// <summary>
            /// 初始化列表比较结果
            /// </summary>
            /// <param name="sourceList">原列表</param>
            /// <param name="optList">新列表</param>
            public ListCompare(List<T> sourceList, List<T> optList)
            {
                SourceList = sourceList ?? new List<T>();
                OptList = optList ?? new List<T>();
            }

            /// <summary>
            /// 原列表
            /// </summary>
            private IEnumerable<T> SourceList { get; }

            /// <summary>
            /// 新列表
            /// </summary>
            private IEnumerable<T> OptList { get; }

            #region 创建列表

            private List<T> _createList;

            /// <summary>
            /// 创建列表
            /// </summary>
            public List<T> CreateList => _createList ?? (_createList =
                OptList.Where(x => !
                        SourceList.Select(source => source.Id).ToList().Contains(x.Id))
                    .ToList());

            #endregion

            #region 更新列表

            private List<T> _updateList;

            /// <summary>
            /// 更新列表
            /// </summary>
            public List<T> UpdateList => _updateList ??
                                         (_updateList = SourceList.Where(x =>
                                                 OptList.Select(source => source.Id).ToList().Contains(x.Id))
                                             .ToList());

            #endregion

            #region 删除列表

            private List<T> _deleteList;

            /// <summary>
            /// 删除列表
            /// </summary>
            public List<T> DeleteList => _deleteList ??
                                         (_deleteList =
                                             SourceList.Where(x => !
                                                     OptList.Select(source => source.Id).ToList().Contains(x.Id))
                                                 .ToList());

            #endregion
        }
    }
}
