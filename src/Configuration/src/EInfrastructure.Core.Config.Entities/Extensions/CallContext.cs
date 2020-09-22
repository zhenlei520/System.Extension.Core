// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Concurrent;
using System.Threading;

namespace EInfrastructure.Core.Config.Entities.Extensions
{
    /// <summary>
    /// 从线程获取实例
    /// </summary>
    public class CallContext
    {
        /// <summary>
        ///
        /// </summary>
        private static ConcurrentDictionary<string, AsyncLocal<object>> State =
            new ConcurrentDictionary<string, AsyncLocal<object>>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public static void SetData(string name, object data) =>
            State.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetData(string name) =>
            State.TryGetValue(name, out AsyncLocal<object> data) ? data.Value : null;
    }
}
