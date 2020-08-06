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
        /// <param name="unique"></param>
        /// <param name="data">数据</param>
        internal TaskJobRequest(long unique, T data)
        {
            Id = unique;
            Data = data;
        }

        /// <summary>
        /// 任务id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        internal long Id { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        internal T Data { get; set; }
    }
}
