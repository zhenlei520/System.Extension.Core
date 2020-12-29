// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.ComponentModel;
using System.Data;

namespace EInfrastructure.Core.Tools.Common
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
        /// <param name="tableName">表名，默认为空（如果为空，则以当前类名为表名）</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataTable CreateEmptyTable<T>(string tableName="") where T : class
        {
            Type entityType = typeof(T);
            string name = ObjectCommon.SafeObject(tableName.IsNullOrEmpty(),()=>entityType.Name,()=>tableName);
            DataTable table = new DataTable(name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }

        #endregion
    }
}
