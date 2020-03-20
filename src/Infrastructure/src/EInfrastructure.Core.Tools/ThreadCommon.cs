// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using EInfrastructure.Core.Tools.Tasks;

namespace EInfrastructure.Core.Tools
{
    /// <summary>
    /// 多线程任务
    /// </summary>
    public class ThreadCommon<T>
    {
        /// <summary>
        /// 最大线程数
        /// </summary>
        private int MaxThread;

        /// <summary>
        /// 等待中的任务
        /// </summary>
        private TaskQueue<T> AwaitList = new TaskQueue<T>();

        /// <summary>
        /// 进行中的任务
        /// </summary>
        private TaskQueue<T> OnGoingList = new TaskQueue<T>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxThread">最大线程数</param>
        public ThreadCommon(int maxThread)
        {
            MaxThread = maxThread;
        }


        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        public void AddTask<T>(T t)
        {

        }
    }
}
