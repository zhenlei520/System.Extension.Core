namespace EInfrastructure.Core.AliYun.Tbk.Param
{
    /// <summary>
    /// 营销参数
    /// </summary>
    public class ConvertToMarketingParam
    {
        public ConvertToMarketingParam(string url,string numIds)
        {
            Url = url;
            NumIds = numIds;
        }

        /// <summary>
        /// 商品url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 需返回的字段列表
        /// </summary>
        public string Fields { get; set; } = "num_iid,click_url";

        /// <summary>
        /// 商品ID串，用','分割，从taobao.tbk.item.get接口获取num_iid字段，最大40个
        /// </summary>
        public string NumIds { get;private set; }

        /// <summary>
        ///	广告位ID，区分效果位置
        /// </summary>
        public string AdzoneId { get; set; } = "227984573";

        /// <summary>
        /// 链接形式
        /// 链接形式：1：PC，2：无线，默认：１
        /// </summary>
        public int Platform { get; set; } = 1;

        /// <summary>
        /// 自定义输入串，英文和数字组成，长度不能大于12个字符，区分不同的推广渠道
        /// </summary>
        public string Unid { get; set; } = "";

        /// <summary>
        /// 1表示商品转通用计划链接，其他值或不传表示转营销计划链接
        /// </summary>
        public string Dx { get; set; } = "";
    }
}
