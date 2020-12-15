// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// DataRow扩展
    /// </summary>
    public partial class Extensions
    {
        #region 将单行信息转换为对象

        /// <summary>
        /// 将单行信息转换为对象
        /// 其中对象需要有无参的构造函数
        /// </summary>
        /// <param name="row">单行数据</param>
        /// <param name="errAction">异常回执</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ConvertToItem<T>(this DataRow row, Action<Exception> errAction = null) where T : class, new()
        {
            if (row != null)
            {
                var obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        if (prop != null && prop.CanWrite)
                        {
                            object value = row[column.ColumnName];
                            prop.SetValue(obj, value, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        errAction?.Invoke(ex);
                        throw;
                    }
                }
            }

            return default(T);
        }

        #endregion

        #region 行集合转List对象

        /// <summary>
        /// 行集合转List对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<T> ConvertRowsToList<T>(this IEnumerable<DataRow> rows) where T : class, new()
        {
            List<T> list = new List<T>();
            rows?.ToList().ForEach(row => { list.Add(row.ConvertToItem<T>()); });
            return list;
        }

        #endregion
    }
}
