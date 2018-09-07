using System.ComponentModel;

namespace EInfrastructure.Core.Interface.Storage.Enum
{
    public enum UploadStateEnum
    {
        /// <summary>
        /// 任务状态:激活
        /// </summary>
        [Description("激活")] Activated,

        /// <summary>
        /// 任务状态:暂停
        /// </summary>
        [Description("暂停")]Suspended,

        /// <summary>
        /// 任务状态:退出
        /// </summary>
        [Description("退出")]  Aborted
    }
}