namespace EInfrastructure.Core.AliYun.Tbk.Param
{
    /// <summary>
    /// 淘宝客店铺查询
    /// </summary>
    public class ShopGetParam
    {
        /// <summary>
        /// 需返回的字段列表(必须)
        /// </summary>
        public string Fields { get; set; } = "user_id,shop_title,shop_type,seller_nick,pict_url,shop_url";

        /// <summary>
        /// 查询词(必须)
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 排序_des（降序），排序_asc（升序），佣金比率（commission_rate）， 商品数量（auction_count），销售总数量（total_auction）
        /// </summary>
        public string Sort { get; set; } = "commission_rate_des";

        /// <summary>
        /// 是否商城的店铺，设置为true表示该是属于淘宝商城的店铺，设置为false或不设置表示不判断这个属性
        /// </summary>
        public bool IsTmall { get; set; } = false;

        /// <summary>
        /// 信用等级下限，1~20
        /// </summary>
        public int StartCredit { get; set; }

        /// <summary>
        /// 信用等级上限，1~20
        /// </summary>
        public int EndCredit { get; set; }

        /// <summary>
        /// 淘客佣金比率下限，1~10000
        /// </summary>
        public int StartCommissionRate { get; set; }

        /// <summary>
        /// 淘客佣金比率上限，1~10000
        /// </summary>
        public int EndCommissionRate { get; set; }

        /// <summary>
        /// 店铺商品总数下限
        /// </summary>
        public int StartTotalAction { get; set; }

        /// <summary>
        /// 店铺商品总数上限
        /// </summary>
        public int EndTotalAction { get; set; }

        /// <summary>
        /// 累计推广商品下限
        /// </summary>
        public int StartAuctionCount { get; set; }

        /// <summary>
        /// 累计推广商品上限
        /// </summary>
        public int EndAuctionCount { get; set; }

        /// <summary>
        /// 链接形式：1：PC，2：无线，默认：１
        /// </summary>
        public int Platform { get; set; } = 1;

        /// <summary>
        /// 第几页，默认1，1~100
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// 页大小，默认20，1~100
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
