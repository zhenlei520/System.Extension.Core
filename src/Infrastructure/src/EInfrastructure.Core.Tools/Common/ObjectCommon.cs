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
        /// <param name="successRes">成功默认值</param>
        /// <param name="errorRes">失败默认值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SafeObject<T>(bool state, T successRes, T errorRes)
        {
            return SafeObject<T>(state, () => successRes, () => errorRes);
        }

        /// <summary>
        /// 返回安全的结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="successRes">成功默认值</param>
        /// <param name="errorFunc">失败回调</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SafeObject<T>(bool state, T successRes, Func<T> errorFunc)
        {
            return SafeObject<T>(state, () => successRes, errorFunc);
        }

        /// <summary>
        /// 返回安全的结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="successFunc">成功回调</param>
        /// <param name="errorRes">失败默认值</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SafeObject<T>(bool state, Func<T> successFunc, T errorRes)
        {
            return SafeObject<T>(state, successFunc, () => errorRes);
        }

        /// <summary>
        /// 返回安全的结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="successFun">成功回调</param>
        /// <param name="errorFun">失败回调</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SafeObject<T>(bool state, Func<T> successFun, Func<T> errorFun)
        {
            if (state)
            {
                return successFun.Invoke();
            }

            return errorFun.Invoke();
        }

        #endregion
    }
}
