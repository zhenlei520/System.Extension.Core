using System.ComponentModel;

namespace EInfrastructure.Core.HelpCommon.Files
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 特征码
        /// </summary>
        public string ConditionCode { get; set; }
    }

    /// <summary>
    /// 加密方式
    /// </summary>
    public enum EncryptTypeEnum
    {
        /// <summary>
        /// md5加密
        /// </summary>
        [Description("Md5")] Md5 = 0,

        /// <summary>
        /// Sha1加密
        /// </summary>
        [Description("Sha1")] Sha1 = 1,

        /// <summary>
        /// Sha256加密
        /// </summary>
        [Description("Sha256")] Sha256 = 2,

        /// <summary>
        /// Sha384加密
        /// </summary>
        [Description("Sha384")] Sha384 = 3,

        /// <summary>
        /// Sha512加密
        /// </summary>
        [Description("Sha512")] Sha512 = 4
    }
}