using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Enumerations
{
    /// <summary>
    /// 内容细分
    /// </summary>
    public class ContentSubType : Enumeration
    {
        /// <summary>
        /// 色情
        /// </summary>
        public static ContentSubType Porn = new ContentSubType(01, "色情", ContentType.Porn.Id);

        /// <summary>
        /// 女下体
        /// </summary>
        public static ContentSubType Vaginal = new ContentSubType(2, "女下体", ContentType.Porn.Id);

        /// <summary>
        /// 女胸
        /// </summary>
        public static ContentSubType GirlBreast = new ContentSubType(3, "女胸", ContentType.Porn.Id);

        /// <summary>
        /// 男下体
        /// </summary>
        public static ContentSubType Penis = new ContentSubType(4, "男下体", ContentType.Porn.Id);

        /// <summary>
        /// 性行为
        /// </summary>
        public static ContentSubType Sexual = new ContentSubType(5, "性行为", ContentType.Porn.Id);

        /// <summary>
        /// 臀部
        /// </summary>
        public static ContentSubType Buttocks = new ContentSubType(6, "臀部", ContentType.Porn.Id);

        /// <summary>
        /// 口交
        /// </summary>
        public static ContentSubType OralSex = new ContentSubType(7, "口交", ContentType.Porn.Id);

        /// <summary>
        /// 卡通色情
        /// </summary>
        public static ContentSubType CartoonPorn = new ContentSubType(8, "卡通色情", ContentType.Porn.Id);

        /// <summary>
        /// 儿童色情
        /// </summary>
        public static ContentSubType ChildPorn = new ContentSubType(9, "儿童色情", ContentType.Porn.Id);

        /// <summary>
        /// 性感低俗
        /// </summary>
        public static ContentSubType SexyAndVulgar = new ContentSubType(10, "性感低俗", ContentType.SexyVulgar.Id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="parentId">父内容类型id</param>
        public ContentSubType(int id, string name, int parentId) : base(id, name)
        {
            this.ParentId = parentId;
        }

        /// <summary>
        /// 内容类型id
        /// </summary>
        public int ParentId { get; private set; }
    }
}
