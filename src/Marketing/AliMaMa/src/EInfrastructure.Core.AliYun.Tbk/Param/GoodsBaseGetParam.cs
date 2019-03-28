// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.AliYun.Tbk.Param
{
    /// <summary>
    /// 淘宝客商品详情（简版）
    /// </summary>
    public class GoodsBaseGetParam
    {
        /// <summary>
        /// 商品ID串，用,分割，最大40个
        /// </summary>
        public string NumIids { get; set; }

        /// <summary>
        /// 链接形式：1：PC，2：无线，默认：１
        /// </summary>
        public int Platform { get; set; } = 1;

        /// <summary>
        /// ip地址，影响邮费获取，如果不传或者传入不准确，邮费无法精准提供
        /// </summary>
        public string Ip { get; set; }
    }
}
