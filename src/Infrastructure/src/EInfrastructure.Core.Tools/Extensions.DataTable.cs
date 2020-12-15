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
