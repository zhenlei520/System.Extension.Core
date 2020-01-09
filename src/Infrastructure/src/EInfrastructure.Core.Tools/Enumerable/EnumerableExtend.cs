// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace EInfrastructure.Core.Tools.Enumerable
{
    /// <summary>
    /// Enumerable扩展
    /// </summary>
    public static class EnumerableExtend
    {
        #region IEnumerable转Dictionary类型
        /// <summary>
        /// IEnumerable转Dictionary类型
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="keyGetter">Key键</param>
        /// <param name="valueGetter">Key值</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionaryExt<TElement, TKey, TValue>(
        this IEnumerable<TElement> source,
        Func<TElement, TKey> keyGetter,
        Func<TElement, TValue> valueGetter)
        {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (var e in source)
            {
                var key = keyGetter(e);
                if (dict.ContainsKey(key))
                {
                    continue;
                }

                dict.Add(key, valueGetter(e));
            }
            return dict;
        }
        #endregion
    }
}
