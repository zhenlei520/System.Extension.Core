// Copyright (c) zhenlei520 All rights reserved.

using System.Collections.Generic;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 动态对象
    /// </summary>
    public partial class Extensions
    {
        #region 创建动态对象

        /// <summary>
        /// 创建动态对象
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static object ConvertToObject(this IEnumerable<KeyValuePair<string, object>> keyValuePairs)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            foreach (KeyValuePair<string, object> item in keyValuePairs)
            {
                ((IDictionary<string, object>)obj).Add(item.Key, item.Value);
            }

            return obj;
        }

        #endregion
    }
}
