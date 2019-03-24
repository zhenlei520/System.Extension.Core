namespace EInfrastructure.Core.AliYun.Tbk.Param
{
    /// <summary>
    /// 淘宝客商品信息查询
    /// </summary>
    public class GoodsGetParam
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Fields { get; set; } = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick";

        /// <summary>
        /// 查询词
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 后台类目id
        /// 后台类目ID，用,分割，最大10个，该ID可以通过taobao.itemcats.get接口获取到
        /// </summary>
        public string Cat { get; set; }

        /// <summary>
        /// 所在地
        /// </summary>
        public string Itemloc { get; set; }

        /// <summary>
        /// 排序
        /// 排序_des（降序），排序_asc（升序），销量（total_sales），淘客佣金比率（tk_rate）， 累计推广量（tk_total_sales），总支出佣金（tk_total_commi）
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 是否商城商品，设置为true表示该商品是属于淘宝商城商品，设置为false或不设置表示不判断这个属性
        /// </summary>
        public bool IsTmall { get; set; } = false;

        /// <summary>
        /// 是否海外商品，设置为true表示该商品是属于海外商品，设置为false或不设置表示不判断这个属性
        /// </summary>
        public bool IsOverseas { get; set; } = false;

        /// <summary>
        /// 折扣价范围下限，单位：元
        /// </summary>
        public int StartPrice { get; set; }

        /// <summary>
        /// 折扣价范围上限，单位：元
        /// </summary>
        public int EndPrice { get; set; }

        /// <summary>
        /// 淘客佣金比率上限，如：1234表示12.34%
        /// </summary>
        public int StartTkRate { get; set; }

        /// <summary>
        /// 淘客佣金比率下限，如：1234表示12.34%
        /// </summary>
        public int EndTkRate { get; set; }

        /// <summary>
        /// 链接形式：1：PC，2：无线，默认：１
        /// </summary>
        public int Platform { get; set; } = 1;

        /// <summary>
        /// 第几页，默认：１
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// 页大小，默认20，1~100
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
