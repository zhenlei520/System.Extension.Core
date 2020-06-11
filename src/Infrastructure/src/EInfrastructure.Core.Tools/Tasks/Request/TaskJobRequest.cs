using System;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Tools.Tasks.Request
{
    /// <summary>
    /// 线程任务
    /// </summary>
    internal class TaskJobRequest<T>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="data">数据</param>
        internal TaskJobRequest(Guid guid, T data)
        {
            Id = guid;
            Data = data;
        }

        /// <summary>
        /// 任务id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        internal Guid Id { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        internal T Data { get; set; }
    }
}
