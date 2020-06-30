using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Enumerations
{
    /// <summary>
    /// 审查级别
    /// </summary>
    public class LevelOfReview : Enumeration
    {
        /// <summary>
        /// 正常
        /// </summary>
        public static LevelOfReview Normal = new LevelOfReview(1, "正常");

        /// <summary>
        /// 确认
        /// </summary>
        public static LevelOfReview Confirm = new LevelOfReview(2, "确认");

        /// <summary>
        /// 疑似，不确认
        /// </summary>
        public static LevelOfReview Doubt = new LevelOfReview(3, "疑似");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public LevelOfReview(int id, string name) : base(id, name)
        {
        }
    }
}
