// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace EInfrastructure.Core.Http.Common
{
    /// <summary>
    ///
    /// </summary>
    public class ObjectCommon
    {
        #region 得到参数

        /// <summary>
        /// 得到参数
        /// </summary>
        /// <param name="data">对象 允许自定义参数名，可以从JsonProperty的属性中获取</param>
        /// <param name="func">委托方法，当属性类型为泛型时，可以修改最后的value返回值</param>
        /// <returns></returns>
        internal static Dictionary<string, object> GetParams(object data, Func<object, object> func = null)
        {
            if (data == null || data is string || !data.GetType().IsClass)
            {
                return new Dictionary<string, object>();
            }

            var type = data.GetType();
            var properties = type.GetProperties();

            Dictionary<string, object> objectDic = new Dictionary<string, object>();
            foreach (var property in properties)
            {
                object value;
                string name;
                if (property.CustomAttributes.Any(x =>
                    x.AttributeType == typeof(Newtonsoft.Json.JsonPropertyAttribute)))
                {
                    var namedargument = property.CustomAttributes
                        .Where(x => x.AttributeType == typeof(Newtonsoft.Json.JsonPropertyAttribute))
                        .Select(x => x.NamedArguments).FirstOrDefault();
                    name = namedargument.Select(x => x.TypedValue.Value).FirstOrDefault()?.ToString();
                }
                else
                {
                    name = property.Name;
                }

                if (!property.PropertyType.IsGenericType)
                {
                    value = property.GetValue(data, null)?.ToString() ?? "";
                }
                else
                {
                    // object subObj = property.GetValue(data, null);
                    // if (subObj != null)
                    // {
                    //     // 获取列表List<Man>长度
                    //     int count=Convert.ToInt32(subObj.GetType().GetProperty("Count")?.GetValue(subObj,null));
                    //     for (int i = 0; i < count; i++)
                    //     {
                    //         // 获取列表子元素Man，然后子元素Man其实也是一个类，然后递归调用当前方法获取类Man的所有公共属性
                    //         object item=subObj.GetType().GetProperty("Item").GetValue(subObj,new object[]{i});
                    //     }
                    // }
                    value = func != null ? func.Invoke(property.GetValue(data, null)) : property.GetValue(data, null);
                }


                if (objectDic.All(x => x.Key != name) && name != null)
                {
                    objectDic.Add(name, value);
                }
            }

            return objectDic;
        }

        #endregion
    }
}
