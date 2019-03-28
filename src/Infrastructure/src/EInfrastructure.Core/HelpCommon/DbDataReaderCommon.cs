// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// DataReader帮助类
    /// </summary>
    public static class DbDataReaderCommon
    {
        /// <summary>
        /// DataReader转泛型
        /// </summary>
        /// <typeparam name="T">传入的实体类</typeparam>
        /// <param name="objReader">DataReader对象</param>
        /// <returns></returns>
        public static List<T> ConvertReaderToList<T>(this DbDataReader objReader)
        {
            using (objReader)
            {
                List<T> list = new List<T>();

                //获取传入的数据类型
                Type modelType = typeof(T);

                //遍历DataReader对象
                while (objReader.Read())
                {
                    //使用与指定参数匹配最高的构造函数，来创建指定类型的实例
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < objReader.FieldCount; i++)
                    {
                        //判断字段值是否为空或不存在的值
                        if (!(objReader[i] == null || objReader[i] is DBNull))
                        {
                            //匹配字段名
                            PropertyInfo pi = modelType.GetProperty(objReader.GetName(i),
                                BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance |
                                BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                //绑定实体对象中同名的字段  
                                pi.SetValue(model, objReader[i].ConvertToSpecifyType(pi.PropertyType), null);
                            }
                        }
                    }

                    list.Add(model);
                }

                return list;
            }
        }
    }
}