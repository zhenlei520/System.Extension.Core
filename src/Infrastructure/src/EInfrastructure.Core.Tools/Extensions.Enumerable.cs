// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Tools.Common;
using EInfrastructure.Core.Tools.Expressions;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// Enumerable扩展
    /// </summary>
    public partial class Extensions
    {
        #region List<T>操作

        #region List实体减法操作，从集合1中去除集合2的内容

        /// <summary>
        /// List实体减法操作
        /// 从集合1中去除集合2的内容
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t1">集合1（新集合）</param>
        /// <param name="t2">集合2（旧集合）</param>
        /// <returns>排除t1中包含t2的项</returns>
        public static List<T> ExceptNew<T>(this IEnumerable<T> t1, IEnumerable<T> t2)
        {
            return t1.Except(t2).ToList();
        }

        #endregion

        #region 获取t1与t2的相同项（交集）

        /// <summary>
        /// 获取t1与t2的相同项（交集）
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> IntersectNew<T>(this IEnumerable<T> t1, IEnumerable<T> t2)
        {
            return t1.Intersect(t2).ToList();
        }

        #endregion

        #region 连接t1,t2集合，自动过滤相同项

        /// <summary>
        /// 连接t1,t2集合，自动过滤相同项
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> UnionNew<T>(this IEnumerable<T> t1, IEnumerable<T> t2)
        {
            return t1.Union(t2).ToList();
        }

        #endregion

        #region 连接t1,t2集合，不会自动过滤相同项

        /// <summary>
        /// 连接t1,t2集合，不会自动过滤相同项
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ConcatNew<T>(this IEnumerable<T> t1, IEnumerable<T> t2)
        {
            return t1.Concat(t2).ToList();
        }

        #endregion

        #endregion

        #region List对象转Table

        /// <summary>
        /// List对象转Table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(this IEnumerable<T> list, string tableName = null) where T : class
        {
            DataTable table = DataTableCommon.CreateEmptyTable<T>(tableName);
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        #endregion

        #region list 生成 CSV

        /// <summary>
        /// list 生成 CSV（等待测试）
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="csvPath">csv文件路径</param>
        /// <param name="tableName">默认表名</param>
        public static void DataTableToCsv<T>(this IEnumerable<T> list, string csvPath, string tableName) where T : class
        {
            if (null == list)
                return;

            StringBuilder csvText = new StringBuilder();
            StringBuilder csvrowText = new StringBuilder();

            Type entityType = typeof(T);
            string name = ObjectCommon.SafeObject(tableName.IsNullOrEmpty(),
                () => ValueTuple.Create(entityType.Name, tableName));
            DataTable table = new DataTable(name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                csvrowText.Append(",");
                csvrowText.Append(prop.Name);
            }

            foreach (T item in list)
            {
                csvrowText = new StringBuilder();

                foreach (PropertyDescriptor prop in properties)
                {
                    csvrowText.Append(",");
                    csvrowText.Append(prop.GetValue(item)?.ToString().SafeString() ?? "");
                }

                csvText.AppendLine(csvrowText.ToString().Substring(1));
            }

            File.WriteAllText(csvPath, csvText.ToString(), Encoding.Default);
        }

        #endregion

        #region 获取list列表的数量

        /// <summary>
        /// 获取list列表的数量
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int GetListCount<T>(this IEnumerable<T> list)
        {
            return list?.Count() ?? 0;
        }

        #endregion

        #region IEnumerable转String

        #region IEnumerable转换为string

        /// <summary>
        /// IEnumerable转换为string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">待转换的list集合</param>
        /// <param name="split">分割字符</param>
        /// <param name="isReplaceEmpty">是否移除Null或者空字符串</param>
        /// <param name="isReplaceSpace">是否去除空格(仅当为string有效)</param>
        /// <returns></returns>
        public static string ConvertToString<T>(this IEnumerable<T> str, char split = ',',
            bool isReplaceEmpty = true,
            bool isReplaceSpace = true) where T : struct
        {
            return str.ConvertToString(split + "", isReplaceEmpty, isReplaceSpace);
        }

        /// <summary>
        /// IEnumerable转换为string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">待转换的list集合</param>
        /// <param name="split">分割字符</param>
        /// <param name="isReplaceEmpty">是否移除Null或者空字符串</param>
        /// <param name="isReplaceSpace">是否去除空格(仅当为string有效)</param>
        /// <returns></returns>
        public static string ConvertToString<T>(this IEnumerable<T> str, string split = ",",
            bool isReplaceEmpty = true,
            bool isReplaceSpace = true) where T : struct
        {
            var enumerable = str as T[] ?? str.ToArray();
            if (!enumerable.Any())
            {
                return "";
            }

            return ConvertToString(enumerable.Select(x => x.ToString()), split, isReplaceEmpty,
                isReplaceSpace);
        }

        #endregion

        #region IEnumerable转换为string

        /// <summary>
        /// IEnumerable转换为string
        /// </summary>
        /// <param name="str">待转换的list集合</param>
        /// <param name="split">分割字符</param>
        /// <param name="isReplaceEmpty">是否移除Null或者空字符串</param>
        /// <param name="isReplaceSpace">是否去除空格(仅当为string有效)</param>
        /// <returns></returns>
        public static string ConvertToString(this IEnumerable<string> str, char split = ',',
            bool isReplaceEmpty = true,
            bool isReplaceSpace = true)
        {
            return str.ConvertToString(split + "", isReplaceEmpty, isReplaceSpace);
        }

        /// <summary>
        /// IEnumerable转换为string
        /// </summary>
        /// <param name="str">待转换的list集合</param>
        /// <param name="split">分割字符</param>
        /// <param name="isReplaceEmpty">是否移除Null或者空字符串</param>
        /// <param name="isReplaceSpace">是否去除空格(仅当为string有效)</param>
        /// <returns></returns>
        public static string ConvertToString(this IEnumerable<string> str, string split = ",",
            bool isReplaceEmpty = true,
            bool isReplaceSpace = true)
        {
            var enumerable = str as string[] ?? str.ToArray();
            if (!enumerable.Any())
            {
                return "";
            }

            IEnumerable<string> tempList = enumerable.ToList();
            if (isReplaceEmpty)
            {
                if (isReplaceSpace)
                {
                    tempList = tempList.Select(x => x.Trim());
                }

                tempList = tempList.Where(x => !string.IsNullOrEmpty(x));
            }

            return string.Join(split + "", tempList);
        }

        #endregion

        #region 字符串数组转String(简单转换)

        /// <summary>
        /// 字符串数组转String(简单转换)
        /// </summary>
        /// <param name="s">带转换的list集合</param>
        /// <param name="c">分割字符</param>
        /// <param name="isReplaceEmpty">是否移除Null或者空字符串</param>
        /// <param name="isReplaceSpace">是否去除空格(仅当为string有效)</param>
        /// <returns></returns>
        public static string ConvertToString(this string[] s, char c = ',', bool isReplaceEmpty = true,
            bool isReplaceSpace = true)
        {
            if (s == null || s.Length == 0)
            {
                return "";
            }

            return ConvertToString(s.ToList(), c, isReplaceEmpty, isReplaceSpace);
        }

        /// <summary>
        /// 字符串数组转String(简单转换)
        /// </summary>
        /// <param name="s">带转换的list集合</param>
        /// <param name="c">分割字符</param>
        /// <param name="isReplaceEmpty">是否移除Null或者空字符串</param>
        /// <param name="isReplaceSpace">是否去除空格(仅当为string有效)</param>
        /// <returns></returns>
        public static string ConvertToString(this string[] s, string c = ",", bool isReplaceEmpty = true,
            bool isReplaceSpace = true)
        {
            if (s == null || s.Length == 0)
            {
                return "";
            }

            return ConvertToString(s.ToList(), c, isReplaceEmpty, isReplaceSpace);
        }

        #endregion

        #endregion

        #region 对IEnumerable集合分页执行某个方法

        /// <summary>
        /// 对IEnumerable集合分页执行某个方法
        /// </summary>
        /// <param name="query"></param>
        /// <param name="action"></param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页数（默认第一页）</param>
        /// <param name="errCode">错误码</param>
        /// <typeparam name="T"></typeparam>
        public static void ListPager<T>(this IEnumerable<T> query, Action<List<T>> action, int pageSize = -1,
            int pageIndex = 1, int? errCode = null)
        {
            if (pageSize <= 0 && pageSize != -1)
            {
                throw new BusinessException("页大小必须为正数", errCode ?? HttpStatus.Err.Id);
            }

            if (query == null)
            {
                return;
            }

            var enumerable = query as T[] ?? query.ToArray();
            var totalCount = enumerable.Count() * 1.0d;
            int pageMax = 1;
            if (pageSize != -1)
            {
                pageMax = Math.Ceiling(totalCount / pageSize).ConvertToInt(1);
            }
            else
            {
                pageSize = totalCount.ConvertToInt(0) * 1;
            }

            for (int index = pageIndex; index <= pageMax; index++)
            {
                action(enumerable.Skip((index - 1) * pageSize).Take(pageSize).ToList());
            }
        }

        #endregion

        #region IEnumerable转Dictionary类型，如果key存在会跳过

        /// <summary>
        /// IEnumerable转Dictionary类型，如果key存在会跳过
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="keyGetter">Key键</param>
        /// <param name="valueGetter">Key值</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TElement, TKey, TValue>(
            this IEnumerable<TElement> source,
            Func<TElement, TKey> keyGetter,
            Func<TElement, TValue> valueGetter)
        {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (var e in source)
            {
                var key = keyGetter(e);
                if (dict.ContainsKey(key))
                {
                    continue;
                }

                dict.Add(key, valueGetter(e));
            }

            return dict;
        }

        #endregion

        #region 返回安全的集合

        /// <summary>
        /// 返回安全的集合
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>如果param不为空，则返回param，否则返回空集合</returns>
        public static List<T> SafeList<T>(this IEnumerable<T> param)
        {
            return ObjectCommon.SafeObject(param != null,
                () => ValueTuple.Create(param?.ToList(), new List<T>()));
        }

        #endregion

        #region 返回安全的集合数组

        /// <summary>
        /// 返回安全的集合数组
        /// </summary>
        /// <param name="param"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SafeArray<T>(this IEnumerable<T> param)
        {
            return SafeList(param).ToArray();
        }

        #endregion

        #region 根据条件查询不同的数据

        /// <summary>
        /// 根据条件查询不同的数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TOpt"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource, TOpt>(this IEnumerable<TSource> source,
            Func<TSource, TOpt> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<TSource, TOpt>(keySelector));
        }

        /// <summary>
        ///根据条件查询不同的数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector,
            IEqualityComparer<V> comparer)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector, comparer));
        }

        #endregion

        #region 验证列表是否是null或者空

        /// <summary>
        /// 验证列表是否是null或者空
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list.GetListCount() == 0;
        }

        #endregion

        #region 添加linq查询扩展(仅在Debug下生效)

        /// <summary>
        /// 添加linq查询扩展(仅在Debug下生效)
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="logName">日志名称</param>
        /// <param name="logMethod">输出日志</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> LogLinq<T>(this IEnumerable<T> enumerable, string logName,
            Func<T, string> logMethod)
        {
#if DEBUG
            int count = 0;
            foreach (var item in enumerable)
            {
                if (logMethod != null)
                {
                    Debug.WriteLine($"{logName}|item {count} = {logMethod(item)}");
                }

                count++;
                yield return item;
            }

            Debug.WriteLine($"{logName}|count = {count}");
#else
            return enumerable;
#endif
        }

        #endregion
    }
}
