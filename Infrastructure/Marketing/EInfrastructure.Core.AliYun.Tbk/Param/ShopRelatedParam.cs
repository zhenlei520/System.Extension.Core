namespace EInfrastructure.Core.AliYun.Tbk.Param
{
    /// <summary>
    /// 淘宝客店铺关联推荐查询
    /// </summary>
    public class ShopRelatedParam
    {
        /// <summary>
        /// 需返回的字段列表(必须)
        /// </summary>
        public string Fields { get; set; } = "user_id,shop_title,shop_type,seller_nick,pict_url,shop_url";

        /// <summary>
        /// 卖家Id(必须)
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 返回数量，默认20，最大值40
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 链接形式：1：PC，2：无线，默认：１
        /// </summary>
        public int Platform { get; set; }
    }
}
