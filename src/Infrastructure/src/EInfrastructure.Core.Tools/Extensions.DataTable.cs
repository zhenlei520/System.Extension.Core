// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

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
        /// <returns></returns>
        public static DataTable SafeDataTable(this DataTable table)
        {
            if (table == null)
            {
                return new DataTable();
            }

            return table;
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
            table = table.SafeDataTable();
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return rows.ConvertRowsToList<T>();
        }

        #endregion

        #region Datatable转换为Json
        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="dataTable">Datatable对象</param>
        /// <returns>Json字符串</returns>
        public static string ConvertToJson(this DataTable dataTable)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dataTable.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    string strKey = dataTable.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dataTable.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dataTable.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = str.FormatEscapes();
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
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

        #region DataTable 生成 CSV

        /// <summary>
        /// DataTable 生成 CSV
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="csvPath">csv文件路径</param>
        /// <param name="encoding">编码格式，默认Utf8</param>
        public static void ConvertDataTableToCsv(this DataTable dt, string csvPath, Encoding encoding = null)
        {
            if (null == dt)
                return;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            StringBuilder csvText = new StringBuilder();
            StringBuilder csvrowText = new StringBuilder();
            foreach (DataColumn dc in dt.Columns)
            {
                csvrowText.Append(",");
                csvrowText.Append(dc.ColumnName);
            }

            csvText.AppendLine(csvrowText.ToString().Substring(1));

            foreach (DataRow dr in dt.Rows)
            {
                csvrowText = new StringBuilder();
                foreach (DataColumn dc in dt.Columns)
                {
                    csvrowText.Append(",");
                    csvrowText.Append(dr[dc.ColumnName].ToString().Replace(',', ' '));
                }

                csvText.AppendLine(csvrowText.ToString().Substring(1));
            }

            File.WriteAllText(csvPath, csvText.ToString(), encoding);
        }

        #endregion
    }
}
