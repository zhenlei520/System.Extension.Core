using System;
using System.Threading;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Tools.Tasks
{
    /// <summary>
    /// 线程任务
    /// </summary>
    internal class TaskJobBaseParam<T>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="data">数据</param>
        internal TaskJobBaseParam(Guid guid, T data)
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

    internal class TaskJobParam<T> : TaskJobBaseParam<T>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="data">数据</param>
        /// <param name="action"></param>
        internal TaskJobParam(Guid guid, T data, Action<T> action) : base(guid, data)
        {
            this.Action = action;
            this.IsCanCancel = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="data">数据</param>
        /// <param name="action"></param>
        internal TaskJobParam(Guid guid, T data, Action<T, CancellationTokenSource> action) : base(guid, data)
        {
            this.Action2 = action;
            this.IsCanCancel = true;
        }

        /// <summary>
        /// 是否可以取消
        /// </summary>
        [JsonProperty(PropertyName = "is_can_cancel")]
        internal bool IsCanCancel { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        internal Action<T> Action { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "action2")]
        internal Action<T, CancellationTokenSource> Action2 { get; set; }
    }

    internal class TaskJobParam2<T, T2> : TaskJobBaseParam<T>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="data">数据</param>
        /// <param name="func"></param>
        internal TaskJobParam2(Guid guid, T data, Func<T, T2> func) : base(guid, data)
        {
            this.Func = func;
            this.IsCanCancel = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="data">数据</param>
        /// <param name="func"></param>
        internal TaskJobParam2(Guid guid, T data, Func<T, CancellationTokenSource, T2> func) : base(guid, data)
        {
            this.Func2 = func;
            this.IsCanCancel = true;
        }

        /// <summary>
        /// 是否可以取消
        /// </summary>
        [JsonProperty(PropertyName = "is_can_cancel")]
        internal bool IsCanCancel { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "func")]
        internal Func<T, T2> Func { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "func2")]
        internal Func<T, CancellationTokenSource, T2> Func2 { get; set; }
    }
}
