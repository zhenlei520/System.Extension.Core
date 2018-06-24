using System;

namespace EInfrastructure.Core.Interface.Storage.FormModel
{
    /// <summary>
    /// 文件搜索条件
    /// </summary>
    public class FetchFileFormModel
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        public Guid FileNo { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 源图（必填）
        /// </summary>
        public string SourceFileKey { get; set; }

        /// <summary>
        /// 目标图（必填）
        /// </summary>
        public string Key { get; set; }
    }
}
