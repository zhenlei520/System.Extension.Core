// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EInfrastructure.Core.Tools.Expressions
{
    /// <summary>
    /// Extension methods for add And and Or with parameters rebinder
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Compose two expression and merge all in a new expression
        /// </summary>
        /// <typeparam name="T">Type of params in expression</typeparam>
        /// <param name="first">Expression instance</param>
        /// <param name="second">Expression to merge</param>
        /// <param name="merge">Function to merge</param>
        /// <returns>New merged expressions</returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new {f, s = second.Parameters[i]})
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // apply composition of lambda expression bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// And operator
        /// </summary>
        /// <typeparam name="T">Type of params in expression</typeparam>
        /// <param name="first">Right Expression in AND operation</param>
        /// <param name="second">Left Expression in And operation</param>
        /// <returns>New AND expression</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            if (first == null)
                return second;
            if (second == null)
                return first;
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// Or operator
        /// </summary>
        /// <typeparam name="T">Type of param in expression</typeparam>
        /// <param name="first">Right expression in OR operation</param>
        /// <param name="second">Left expression in OR operation</param>
        /// <returns>New Or expressions</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second)
        {
            if (first == null)
                return second;
            if (second == null)
                return first;
            return first.Compose(second, Expression.Or);
        }

        /// <summary>
        /// 根据指定属性名称对序列进行排序
        /// </summary>
        /// <typeparam name="T">source中的元素的类型</typeparam>
        /// <param name="source">一个要排序的值序列</param>
        /// <param name="property">属性名称</param>
        /// <param name="descending">是否降序，降序：true，升序：false</param>
        /// <returns></returns>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string property, bool descending)
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
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, List<KeyValuePair<string, bool>> sorts)
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
                    var iquerable = source.AsQueryable();
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
                        new Type[] {typeof(T), pi.PropertyType}, iquerable.Expression, le);
                    source = iquerable.Provider.CreateQuery<T>(resultExp);
                }
            });

            return source;
        }
    }
}
