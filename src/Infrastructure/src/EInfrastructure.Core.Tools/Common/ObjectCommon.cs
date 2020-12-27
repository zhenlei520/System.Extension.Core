// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    /// 对象帮助类
    /// </summary>
    public class ObjectCommon
    {
        #region 返回安全的结果

        /// <summary>
        /// 返回安全的结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="func">委托方法，返回值1:原值，返回值2：默认值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SafeObject<T>(bool state, Func<ValueTuple<T, T>> func)
        {
            var result = func.Invoke();
            if (state)
            {
                return result.Item1;
            }

            return result.Item2;
        }

        #endregion
    }
}
