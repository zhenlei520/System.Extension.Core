using System.ComponentModel;

namespace EInfrastructure.Core.Interface.Storage.Enum
{
    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileTypeEnum
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("other")]Other = 0,
        /// <summary>
        /// 图片
        /// </summary>
        [Description("img")] Image = 1,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("video")] Video = 2,
        /// <summary>
        /// 文本
        /// </summary>
        [Description("text")] Text = 3,
        /// <summary>
        /// 音乐
        /// </summary>
        [Description("mp3")] Mp3 = 4,
        /// <summary>
        /// 压缩包
        /// </summary>
        [Description("rar")] Rar = 5,
    }
}
