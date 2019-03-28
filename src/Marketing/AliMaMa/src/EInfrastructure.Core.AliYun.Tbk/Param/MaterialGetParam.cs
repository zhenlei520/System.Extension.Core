// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.AliYun.Tbk.Param
{
    /// <summary>
    /// 超级搜索
    /// </summary>
    public class MaterialGetParam
    {
        /// <summary>
        /// 店铺dsr评分，筛选高于等于当前设置的店铺dsr评分的商品0-50000之间
        /// (可选)
        /// </summary>
        public int StartDsr { get; set; } = 0;

        /// <summary>
        /// 页大小，默认20，1~100(可选)
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 第几页，默认：１(可选)
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// 链接形式：1：PC，2：无线，默认：１(可选)
        /// </summary>
        public int PlatForm { get; set; } = 1;

        /// <summary>
        /// 淘客佣金比率上限，如：1234表示12.34%(可选)
        /// </summary>
        public int EndTkRate { get; set; } = 1234;

        /// <summary>
        ///淘客佣金比率下限，如：1234表示12.34%(可选)
        /// </summary>
        public int StartTkRate { get; set; } = 1234;

        /// <summary>
        ///折扣价范围上限，单位：元(可选)
        /// </summary>
        public int EndPrice { get; set; } = 10;

        /// <summary>
        ///折扣价范围下限，单位：元(可选)
        /// </summary>
        public int StartPrice { get; set; } = 10;

        /// <summary>
        /// 是否海外商品，设置为true表示该商品是属于海外商品，设置为false或不设置表示不判断这个属性(可选)
        /// </summary>
        public bool IsOverSeas { get; set; } = false;

        /// <summary>
        /// 是否商城商品，设置为true表示该商品是属于淘宝商城商品，设置为false或不设置表示不判断这个属性(可选)
        /// </summary>
        public bool IsTmal { get; set; } = false;

        /// <summary>
        ///排序_des（降序），排序_asc（升序），销量（total_sales），淘客佣金比率（tk_rate）， 累计推广量（tk_total_sales），总支出佣金（tk_total_commi），价格（price）
        ///(可选)
        /// </summary>
        public string Sort { get; set; } = "tk_rate_des";

        /// <summary>
        ///所在地(可选)
        /// </summary>
        public string ItemLoc { get; set; } = "";

        /// <summary>
        ///后台类目ID，用,分割，最大10个，该ID可以通过taobao.itemcats.get接口获取到(可选)
        /// </summary>
        public string Cat { get; set; } = "";

        /// <summary>
        ///查询词(可选)
        /// </summary>
        public string Q { get; set; } = "";

        /// <summary>
        ///adzone_id（必填）
        /// </summary>
        public string AdzoneId { get; set; }

        /// <summary>
        ///是否有优惠券，设置为true表示该商品有优惠券，设置为false或不设置表示不判断这个属性(可选)
        /// </summary>
        public bool HasCoupon { get; set; } = false;

        /// <summary>
        ///ip参数影响邮费获取，如果不传或者传入不准确，邮费无法精准提供(可选)
        /// </summary>
        public string Ip { get; set; } = "";

        /// <summary>
        ///退款率是否低于行业均值(可选)
        /// </summary>
        public bool IncludeRfdRate { get; set; } = false;

        /// <summary>
        ///好评率是否高于行业均值(可选)
        /// </summary>
        public bool IncludeGoodRate { get; set; } = false;

        /// <summary>
        ///成交转化是否高于行业均值(可选)
        /// </summary>
        public bool IncludePayRate30 { get; set; } = false;

        /// <summary>
        ///是否加入消费者保障，true表示加入，空或false表示不限(可选)
        /// </summary>
        public bool NeedPrepay { get; set; } = false;

        /// <summary>
        ///是否包邮，true表示包邮，空或false表示不限(可选)
        /// </summary>
        public bool NeedFreeShipment { get; set; } = false;

        /// <summary>
        /// 牛皮癣程度，取值：1:不限，2:无，3:轻微(可选)
        /// </summary>
        public int NpxLevel { get; set; } = 1;
    }
}
