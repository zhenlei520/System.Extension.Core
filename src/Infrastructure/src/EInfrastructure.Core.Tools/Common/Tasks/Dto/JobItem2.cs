// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Tools.Common.Tasks.Dto
{
    /// <summary>
    /// 任务
    /// </summary>
    public class JobItem2 : JobItem
    {
        /// <summary>
        /// 任务回调
        /// </summary>
        public Action<JobItem> Action { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source">源数据</param>
        protected JobItem2(object source) : base(source)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        /// <param name="source"></param>
        public JobItem2(Action<JobItem> action, object source) : this(source)
        {
            Action = action;
        }
    }
}
