// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Threading;

namespace EInfrastructure.Core.Tools.Tasks.Request
{
    /// <summary>
    /// 任务信息
    /// </summary>
    internal class JobItem
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="job"></param>
        /// <param name="action"></param>
        /// <param name="eventWaitHandle"></param>
        public JobItem(EInfrastructure.Core.Tools.Tasks.Dto.JobItem job,Action<EInfrastructure.Core.Tools.Tasks.Dto.JobItem> action, EventWaitHandle eventWaitHandle)
        {
            Job = job;
            Action = action;
            EventWaitHandle = eventWaitHandle;
        }

        public Action<EInfrastructure.Core.Tools.Tasks.Dto.JobItem> Action { get; private set; }

        /// <summary>
        /// 任务信息
        /// </summary>
        public EInfrastructure.Core.Tools.Tasks.Dto.JobItem Job { get; private set; }

        /// <summary>
        /// EventWaitHandle
        /// </summary>
        public EventWaitHandle EventWaitHandle { get; private set; }
    }
}
