// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.AliYun.Tbk.Param
{
    /// <summary>
    /// 获取淘抢购的数据，淘客商品转淘客链接，非淘客商品输出普通链接
    /// </summary>
    public class TqgGetParam
    {
        /// <summary>
        /// 推广位id（推广位申请方式：http://club.alimama.com/read.php?spm=0.0.0.0.npQdST&tid=6306396&ds=1&page=1&toread=1）（必须）
        /// </summary>
        public string AdzoneId { get; set; }

        /// <summary>
        /// 需返回的字段列表（必须）
        /// </summary>
        public string Fields { get; set; } = "click_url,pic_url,reserve_price,zk_final_price,total_amount,sold_num,title,category_name,start_time,end_time";

        /// <summary>
        /// 最早开团时间（必须）
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 	最晚开团时间（必须）
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 第几页，默认1，1~100
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// 页大小，默认40，1~40
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
