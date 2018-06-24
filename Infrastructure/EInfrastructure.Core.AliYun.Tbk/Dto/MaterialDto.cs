using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EInfrastructure.Core.AliYun.Tbk.Dto
{
    /// <summary>
    /// 通用物料搜索API（导购）
    /// </summary>
    public class MaterialDto
    {
        /// <summary>
        /// 总数
        /// </summary>
        [JsonProperty(PropertyName = "total_results")]
        public long Total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "result_list")]
        public List<Item> List { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public class Item
        {
            /// <summary>
            /// 优惠券开始时间
            /// </summary>
            [JsonProperty(PropertyName = "coupon_start_time")]
            public string CouponStartTime { get; set; }

            /// <summary>
            /// 优惠券结束时间
            /// </summary>
            [JsonProperty(PropertyName = "coupon_end_time")]
            public string CouponEndTime { get; set; }

            /// <summary>
            /// 定向计划信息
            /// </summary>
            [JsonProperty(PropertyName = "info_dxjh")]
            public string InfoDxjh { get; set; }

            /// <summary>
            /// 淘客30天月推广量
            /// </summary>
            [JsonProperty(PropertyName = "tk_total_sales")]
            public string TkTotalSales { get; set; }

            /// <summary>
            /// 月支出佣金
            /// </summary>
            [JsonProperty(PropertyName = "tk_total_commi")]
            public string TkTotalCommi { get; set; }

            /// <summary>
            /// 优惠券id
            /// </summary>
            [JsonProperty(PropertyName = "coupon_id")]
            public string CouponId { get; set; }

            /// <summary>
            /// 宝贝id
            /// </summary>
            [JsonProperty(PropertyName = "num_iid")]
            public long NumIid { get; set; }

            /// <summary>
            /// 商品标题
            /// </summary>
            [JsonProperty(PropertyName = "title")]
            public string Title { get; set; }

            /// <summary>
            /// 商品主图
            /// </summary>
            [JsonProperty(PropertyName = "pict_url")]
            public string PictUrl { get; set; }

            /// <summary>
            /// 商品小图列表
            /// </summary>
            [JsonProperty(PropertyName = "small_images")]
            public string[] SmallImages { get; set; }

            /// <summary>
            /// 商品一口价格
            /// </summary>
            [JsonProperty(PropertyName = "reserve_price")]
            public string ReservePrice { get; set; }

            /// <summary>
            /// 商品折扣价格
            /// </summary>
            [JsonProperty(PropertyName = "zk_final_price")]
            public string ZkFinalPrice { get; set; }

            /// <summary>
            /// 卖家类型，0表示集市，1表示商城
            /// </summary>
            [JsonProperty(PropertyName = "user_type")]
            public int UserType { get; set; }

            /// <summary>
            /// 宝贝所在地
            /// </summary>
            [JsonProperty(PropertyName = "provcity")]
            public string Provcity { get; set; }

            /// <summary>
            /// 商品地址
            /// </summary>
            [JsonProperty(PropertyName = "item_url")]
            public string ItemUrl { get; set; }

            /// <summary>
            /// 是否包含营销计划
            /// </summary>
            [JsonProperty(PropertyName = "include_mkt")]
            public string IncludeMkt { get; set; }

            /// <summary>
            /// 是否包含定向计划
            /// </summary>
            [JsonProperty(PropertyName = "include_dxjh")]
            public string IncludeDxjh { get; set; }

            /// <summary>
            /// 佣金比率
            /// </summary>
            [JsonProperty(PropertyName = "commission_rate")]
            public string CommissionRate { get; set; }

            /// <summary>
            /// 30天销量
            /// </summary>
            [JsonProperty(PropertyName = "volume")]
            public long Volume { get; set; }

            /// <summary>
            /// 卖家id
            /// </summary>
            [JsonProperty(PropertyName = "seller_id")]
            public long SellerId { get; set; }

            /// <summary>
            /// 优惠券总量
            /// </summary>
            [JsonProperty(PropertyName = "coupon_total_count")]
            public long CouponTotalCount { get; set; }

            /// <summary>
            /// 优惠券剩余量
            /// </summary>
            [JsonProperty(PropertyName = "coupon_remain_count")]
            public long CouponRemainCount { get; set; }

            /// <summary>
            /// 优惠券面额
            /// </summary>
            [JsonProperty(PropertyName = "coupon_info")]
            public string CouponInfo { get; set; }

            /// <summary>
            /// 优惠价价格
            /// </summary>
            [JsonProperty(PropertyName = "coupon_money")]
            public string CouponMoney => CouponInfo.Substring(CouponInfo.IndexOf("减", StringComparison.Ordinal) + 1,
                CouponInfo.LastIndexOf("元", StringComparison.Ordinal) - CouponInfo.LastIndexOf("减", StringComparison.Ordinal) - 1);

            /// <summary>
            /// 佣金类型
            /// </summary>
            [JsonProperty(PropertyName = "commission_type")]
            public string CommissionType { get; set; }

            /// <summary>
            /// 店铺名称
            /// </summary>
            [JsonProperty(PropertyName = "shop_title")]
            public string ShopTitle { get; set; }

            /// <summary>
            /// 店铺dsr评分
            /// </summary>
            [JsonProperty(PropertyName = "shop_dsr")]
            public long ShopDsr { get; set; }
            
            /// <summary>
            /// 券二合一页面链接
            /// </summary>
            [JsonProperty(PropertyName = "coupon_share_url")]
            public string CouponShareUrl { get; set; }
            
            /// <summary>
            /// 商品淘客链接
            /// </summary>
            [JsonProperty(PropertyName = "url")]
            public string Url { get; set; }
        }
    }
}