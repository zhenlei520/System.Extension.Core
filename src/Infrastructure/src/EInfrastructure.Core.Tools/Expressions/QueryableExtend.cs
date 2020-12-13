// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EInfrastructure.Core.Tools.Expressions
{
    /// <summary>
    /// Queryable扩展方法
    /// </summary>
    public static class QueryableExtend
    {
        #region 返回IQueryable<T>前几条数据

        /// <summary>
        /// 返回IQueryable前几条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="topN"></param>
        /// <returns></returns>
        public static IQueryable<T> TopN<T>(this IQueryable<T> query, int topN)
        {
            return query.Take(topN);
        }

        #endregion

        #region 根据指定属性名称对序列进行排序

        /// <summary>
        /// 根据指定属性名称对序列进行排序
        /// </summary>
        /// <typeparam name="T">source中的元素的类型</typeparam>
        /// <param name="source">一个要排序的值序列</param>
        /// <param name="property">属性名称</param>
        /// <param name="descending">是否降序</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string property, bool descending)
            where T : class
        {
            return source.OrderBy(new List<KeyValuePair<string, bool>>()
            {
                new KeyValuePair<string, bool>(property, descending)
            });
        }

        /// <summary>
        /// 根据指定属性名称对序列进行排序
        /// </summary>
        /// <typeparam name="T">source中的元素的类型</typeparam>
        /// <param name="source">一个要排序的值序列</param>
        /// <param name="sorts">排序条件，其中key为属性名称，value为是否降序（降序：true。升序：false）</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, List<KeyValuePair<string, bool>> sorts)
            where T : class
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            var index = 0;
            sorts.ForEach(sort =>
            {
                PropertyInfo pi = typeof(T).GetProperty(sort.Key);
                if (pi != null)
                {
                    index++;
                    MemberExpression selector = Expression.MakeMemberAccess(parameter, pi);
                    LambdaExpression le = Expression.Lambda(selector, parameter);
                    string methodName;
                    if (index == 1)
                    {
                        methodName = sort.Value ? "OrderByDescending" : "OrderBy";
                    }
                    else
                    {
                        methodName = sort.Value ? "ThenByDescending" : "ThenBy";
                    }

                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                        new Type[] {typeof(T), pi.PropertyType}, source.Expression, le);
                    source = source.Provider.CreateQuery<T>(resultExp);
                }
            });

            return source;
        }

        #endregion

        #region 对IQueryable<T>进行分页

        /// <summary>
        /// 对IQueryable进行分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageSize">每页多少条数据（页大小须等于-1或者大于0）</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        public static IQueryable<T> QueryPager<T>(this IQueryable<T> query, int pageSize, int pageIndex)
        {
            if (pageSize <= 0 && pageSize != -1)
            {
                throw new Exception("页大小设置有误");
            }

            if (pageIndex - 1 < 0 && pageSize != -1)
            {
                throw new Exception("页码必须大于等于1");
            }

            if (pageIndex - 1 >= 0 && pageSize > 0)
            {
                query = query.Skip((pageIndex - 1) * pageSize);
            }

            if (pageSize > 0)
                query = query.Take(pageSize);

            return query;
        }

        #endregion

        #region 得到IQueryable<T>分页（若query中存在条件，则可能会导致数据分页查询出现问题）

        /// <summary>
        /// 得到IQueryable分页（若query中存在条件，则可能会导致数据分页查询出现问题）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="action">分页需要执行的委托方法</param>
        /// <param name="pageSize">每页几条数据（默认20条，-1则不分页）</param>
        /// <param name="pageIndex">当前页数（默认第一页）</param>
        /// <returns></returns>
        public static void ListPager<T>(this IQueryable<T> query, Action<List<T>> action, int pageSize = 20,
            int pageIndex = 1)
        {
            if (pageSize <= 0 && pageSize != -1)
            {
                throw new Exception("页大小必须为正数");
            }

            if (pageIndex - 1 < 0 && pageSize != -1)
            {
                throw new Exception("页码必须大于等于1");
            }

            var totalCount = query.Count() * 1.0d;
            if (pageSize != -1)
            {
                int.TryParse(Math.Ceiling(totalCount / pageSize).ToString(CultureInfo.InvariantCulture),
                    out int pageMax);
                for (int index = pageIndex; index <= pageMax; index++)
                {
                    action(query.Skip((index - 1) * pageSize).Take(pageSize).ToList());
                }
            }
            else
            {
                action(query.ToList());
            }
        }

        #endregion

        #region 添加linq查询扩展(仅在Debug下生效，不建议在数据太多时使用)

        /// <summary>
        /// 添加linq查询扩展(仅在Debug下生效，不建议在数据太多时使用)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="logName">日志名称</param>
        /// <param name="logMethod">输出日志</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> LogLinq<T>(this IQueryable<T> query, string logName,
            Func<T, string> logMethod)
        {
#if DEBUG
            int count = 0;
            query.ToList().ForEach(item =>
            {
                if (logMethod != null)
                {
                    Debug.WriteLine($"{logName}|item {count} = {logMethod(item)}");
                }

                count++;
            });

            Debug.WriteLine($"{logName}|count = {count}");
#endif
            return query;
        }

        #endregion
    }
}
