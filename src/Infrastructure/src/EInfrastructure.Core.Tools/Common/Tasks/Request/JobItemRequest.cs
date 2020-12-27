// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading;

namespace EInfrastructure.Core.Tools.Common.Tasks.Request
{
    /// <summary>
    /// 任务信息
    /// </summary>
    internal class JobItemRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public JobItemRequest()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="job"></param>
        /// <param name="action"></param>
        /// <param name="eventWaitHandle"></param>
        public JobItemRequest(Dto.JobItem job, Action<Dto.JobItem> action, EventWaitHandle eventWaitHandle) : this()
        {
            Job = job;
            Action = action;
            EventWaitHandle = eventWaitHandle;
        }

        public Action<Dto.JobItem> Action { get; private set; }

        /// <summary>
        /// 任务信息
        /// </summary>
        public Dto.JobItem Job { get; private set; }

        /// <summary>
        /// EventWaitHandle
        /// </summary>
        public EventWaitHandle EventWaitHandle { get; private set; }
    }
}
