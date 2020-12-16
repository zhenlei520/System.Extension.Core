// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Data;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public partial class Extensions
    {
        /// <summary>
        /// 将DataTable转List对象
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable SafeDataTable(this DataTable table)
        {
            if (table == null)
            {
                return new DataTable();
            }

            return table;
        }

        #region 将DataTable转List对象

        /// <summary>
        /// 将DataTable转List对象
        /// </summary>
        /// <param name="table"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> ConvertDataTableToList<T>(this DataTable table) where T : class, new()
        {
            table = table.SafeDataTable();
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return rows.ConvertRowsToList<T>();
        }

        #endregion

        #region 检查DataTable 是否有数据行


        /// <summary>
        /// 检查DataTable 是否有数据行
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>是否有数据行</returns>
        public static bool HasRows(this DataTable table)
        {
            return table.Rows.Count > 0;
        }

        #endregion
    }
}
