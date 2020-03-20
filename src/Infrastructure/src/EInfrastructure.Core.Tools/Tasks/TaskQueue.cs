// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace EInfrastructure.Core.Tools.Tasks
{
    /// <summary>
    /// 对列
    /// </summary>
    public class TaskQueue<T>
    {
        /// <summary>
        ///
        /// </summary>
        private Queue<T> _queue; //实际的队列

        /// <summary>
        ///
        /// </summary>
        public TaskQueue()
        {
            _queue = new Queue<T>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="n">对列数量</param>
        public TaskQueue(int n)
        {
            _queue = new Queue<T>(n);
        }

        /// <summary>
        /// 对列的数量
        /// </summary>
        public int Count => _queue.Count;
    }
}
