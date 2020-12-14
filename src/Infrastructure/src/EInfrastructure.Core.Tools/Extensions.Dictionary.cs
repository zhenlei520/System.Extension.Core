// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 字典类型扩展
    /// </summary>
    public partial class Extensions
    {
        #region 字典转对象

        /// <summary>
        /// 字典类型转化为对象
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T DicToObject<T>(this Dictionary<string, object> dic) where T : new()
        {
            var md = new T();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            foreach (var d in dic)
            {
                var filed = textInfo.ToTitleCase(d.Key);
                try
                {
                    var value = d.Value;
                    md.GetType().GetProperty(filed)?.SetValue(md, value);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }

            return md;
        }

        #endregion
    }
}
