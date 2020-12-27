// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Tools.Common
{
    /// <summary>
    ///
    /// </summary>
    public static class TaskExtend
    {
        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        public static void ToTaskNoResult(Action action)
        {
            Task.Run(action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Task ToTask(Action action)
        {
            return Task.Run(action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="function"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ToTask<T>(Func<T> function)
        {
            return Task.Run(function);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task ToTaskAsync(Action action)
        {
            await Task.Run(action);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="function"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ToTaskAsync<T>(Func<T> function)
        {
            return await Task.Run(function);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="function"></param>
        /// <typeparam name="T"></typeparam>
        public static void TaskNoResult<T>(Func<T> function)
        {
            Task.Run(function);
        }

        #endregion Public Methods

        #region 扩展Task

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        public static async void ToTaskAsyncExt(Action action)
        {
            await Task.Run(action);
        }

        #endregion
    }
}
