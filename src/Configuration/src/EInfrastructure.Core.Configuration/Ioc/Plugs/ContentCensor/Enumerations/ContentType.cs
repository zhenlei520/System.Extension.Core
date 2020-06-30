using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Enumerations
{
    /// <summary>
    /// 内容分类
    /// </summary>
    public class ContentType : Enumeration
    {
        /// <summary>
        /// 色情
        /// </summary>
        public static ContentType Porn = new ContentType(1, "色情");

        /// <summary>
        /// 性感低俗
        /// </summary>
        public static ContentType SexyVulgar = new ContentType(2, "性感低俗");

        /// <summary>
        /// 广告
        /// </summary>
        public static ContentType Advertising = new ContentType(3, "广告");

        /// <summary>
        /// 二维码
        /// </summary>
        public static ContentType QrCode = new ContentType(4, "二维码");

        /// <summary>
        /// 暴恐
        /// </summary>
        public static ContentType Violence = new ContentType(5, "暴恐");

        /// <summary>
        /// 违禁
        /// </summary>
        public static ContentType Violate = new ContentType(6, "违禁");

        /// <summary>
        /// 涉政
        /// </summary>
        public static ContentType Politics = new ContentType(7, "涉政");

        /// <summary>
        /// 其他
        /// </summary>
        public static ContentType Other = new ContentType(99, "其他");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ContentType(int id, string name) : base(id, name)
        {
        }
    }
}
