using System;

namespace EInfrastructure.Core.Interface.Storage.FormModel
{
    /// <summary>
    /// 文件搜索条件
    /// </summary>
    public class FileFormModel
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        public Guid FileNo { get; set; }

        /// <summary>
        /// 文件key
        /// </summary>
        public string Key { get; set; }
    }
}
