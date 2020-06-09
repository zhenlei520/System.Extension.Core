// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EInfrastructure.Core.Tools.Tasks.Request
{
    /// <summary>
    /// 线程状态
    /// </summary>
    public class TaskStateRequest
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        public TaskStateRequest(Task<Guid> task)
        {
            this.Task = task;
            this.Stopwatch = new Stopwatch();
        }

        /// <summary>
        /// 是否空闲
        /// </summary>
        public bool IsFree { get; private set; }

        /// <summary>
        /// 线程
        /// </summary>
        public Task<Guid> Task { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public System.Diagnostics.Stopwatch Stopwatch { get; private set; }

        /// <summary>
        /// 是否启动
        /// </summary>
        public void Run()
        {
            this.Stopwatch.Start();
            this.IsFree = false;
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            this.Stopwatch.Stop();
            this.IsFree = true;
        }

        /// <summary>
        /// 得到休息总毫秒
        /// </summary>
        /// <returns></returns>
        public long GetRestTime()
        {
            return this.Stopwatch.ElapsedMilliseconds;
        }
    }
}
