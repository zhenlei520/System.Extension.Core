// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    ///
    /// </summary>
    public static class DataTableCommon
    {
        #region 创建空表记录

        /// <summary>
        /// 创建空表记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataTable CreateEmptyTable<T>() where T : class
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
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
            rows?.ToList().ForEach(row => { list.Add(ConvertToItem<T>(row)); });
            return list;
        }

        #endregion

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

        #region 将DataTable转List对象

        /// <summary>
        /// 将DataTable转List对象
        /// </summary>
        /// <param name="table"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> ConvertDataTableToList<T>(this DataTable table) where T : class, new()
        {
            if (table == null)
            {
                return new List<T>();
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return rows.ConvertRowsToList<T>();
        }

        #endregion
    }
}
