// Copyright (c) zhenlei520 All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

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
    }
}
