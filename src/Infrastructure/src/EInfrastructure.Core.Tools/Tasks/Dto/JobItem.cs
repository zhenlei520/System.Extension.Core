// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Tools.Tasks.Dto
{
    /// <summary>
    /// 任务
    /// </summary>
    public class JobItem
    {
        /// <summary>
        ///
        /// </summary>
        public JobItem()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        public JobItem(object source) : this()
        {
            Source = source;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data">数据</param>
        internal JobItem(object source, object data) : this(source)
        {
            Data = data;
        }

        /// <summary>
        /// 源数据（源请求数据）
        /// </summary>
        public virtual object Source { get; private set; }

        /// <summary>
        /// 任务响应值
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// 设置响应值
        /// </summary>
        /// <param name="data"></param>
        public void SetResponse(object data)
        {
            Data = data;
        }
    }
}
