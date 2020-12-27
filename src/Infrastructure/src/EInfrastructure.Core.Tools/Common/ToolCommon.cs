// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Logger;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class ToolCommon
    {
        #region 得到参数

        /// <summary>
        /// 得到参数
        /// </summary>
        /// <param name="data">对象 允许自定义参数名，可以从JsonProperty的属性中获取</param>
        /// <param name="customerAttributeTypeName"></param>
        /// <param name="func">委托方法，当属性类型为泛型时，可以修改最后的value返回值</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetParams(object data,
            string customerAttributeTypeName = "Newtonsoft.Json.JsonPropertyAttribute,Newtonsoft.Json",
            Func<object, object> func = null)
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

                var customerAttributeType = Type.GetType(customerAttributeTypeName);
                if (property.CustomAttributes.Any(x =>
                    x.AttributeType == customerAttributeType))
                {
                    var namedargument = property.CustomAttributes
                        .Where(x => x.AttributeType == customerAttributeType)
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

        #region 得到响应信息

        /// <summary>
        /// 得到响应信息
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="res"></param>
        /// <param name="errFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetResponse<T>(ILoggerProvider logger, Func<T> res, Func<string, T> errFunc)
        {
            try
            {
                return res.Invoke();
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ExtractAllStackTrace());
                return errFunc(ex.ExtractAllStackTrace());
            }
        }

        /// <summary>
        /// 得到响应信息
        /// </summary>
        /// <param name="res"></param>
        /// <param name="errFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetResponse<T>(Func<T> res, Func<string, Exception, T> errFunc)
        {
            try
            {
                return res.Invoke();
            }
            catch (Exception ex)
            {
                return errFunc(ex.ExtractAllStackTrace(), ex);
            }
        }

        #endregion
    }
}
