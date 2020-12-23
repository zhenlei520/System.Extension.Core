// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// DataReader扩展
    /// </summary>
    public partial class Extensions
    {
        #region DataReader转泛型

        /// <summary>
        /// DataReader转泛型
        /// </summary>
        /// <typeparam name="T">传入的实体类</typeparam>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns></returns>
        public static List<T> ConvertReaderToList<T>(this DbDataReader dataReader)
        {
            using (dataReader)
            {
                List<T> list = new List<T>();

                //获取传入的数据类型
                Type modelType = typeof(T);

                //遍历DataReader对象
                while (dataReader.Read())
                {
                    //使用与指定参数匹配最高的构造函数，来创建指定类型的实例
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        //判断字段值是否为空或不存在的值
                        if (!(dataReader[i] == null || dataReader[i] is DBNull))
                        {
                            //匹配字段名
                            PropertyInfo pi = modelType.GetProperty(dataReader.GetName(i),
                                BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance |
                                BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                //绑定实体对象中同名的字段
                                pi.SetValue(model, dataReader[i].ConvertToSpecifyType(pi.PropertyType), null);
                            }
                        }
                    }

                    list.Add(model);
                }

                return list;
            }
        }

        #endregion

        #region 根据DataReader映射到实体模型

        /// <summary>
        /// 根据DataReader映射到实体模型
        /// </summary>
        /// <typeparam name="T">实体模型</typeparam>
        /// <param name="dataReader">IDataReader</param>
        /// <returns>映射后的实体模型</returns>
        public static T ConvertReaderToEntity<T>(this IDataReader dataReader) where T : class
        {
            T obj = Activator.CreateInstance<T>();
            Type type = typeof(T);
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                //判断字段值是否为空或不存在的值
                if (!dataReader[i].IsNullOrDbNull())
                {
                    //匹配字段名
                    PropertyInfo pi = type.GetProperty(dataReader.GetName(i),
                        BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance |
                        BindingFlags.IgnoreCase);
                    if (pi != null)
                    {
                        //绑定实体对象中同名的字段
                        pi.SetValue(obj, dataReader[i].ConvertToSpecifyType(pi.PropertyType), null);
                    }
                }
            }

            return obj;
        }

        #endregion
    }
}
