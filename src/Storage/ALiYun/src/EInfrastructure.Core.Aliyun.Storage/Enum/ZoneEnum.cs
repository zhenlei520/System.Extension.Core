// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.ComponentModel;
using EInfrastructure.Core.Tools.Attributes;

namespace EInfrastructure.Core.Aliyun.Storage.Enum
{
    /// <summary>
    /// 空间区域
    /// </summary>
    public enum ZoneEnum
    {
        /// <summary>
        /// 杭州
        /// </summary>
        [EName("oss-cn-hangzhou.aliyuncs.com")] [Description("杭州")]
        HangZhou = 0,

        /// <summary>
        /// 上海
        /// </summary>
        [EName("oss-cn-shanghai.aliyuncs.com")] [Description("上海")]
        ShangHai = 1,

        /// <summary>
        /// 青岛
        /// </summary>
        [EName("oss-cn-qingdao.aliyuncs.com")] [Description("青岛")]
        QingDao = 2,

        /// <summary>
        /// 北京
        /// </summary>
        [EName("oss-cn-beijing.aliyuncs.com")] [Description("北京")]
        BeiJing = 3,

        /// <summary>
        /// 张家口
        /// </summary>
        [EName("oss-cn-zhangjiakou.aliyuncs.com")] [Description("张家口")]
        ZhangJiaKou = 4,

        /// <summary>
        /// 呼和浩特
        /// </summary>
        [EName("oss-cn-huhehaote.aliyuncs.com")] [Description("呼和浩特")]
        HuHeHaoTe = 5,

        /// <summary>
        /// 深圳
        /// </summary>
        [EName("oss-cn-shenzhen.aliyuncs.com")] [Description("深圳")]
        ShenZhen = 6,

        /// <summary>
        /// 河源
        /// </summary>
        [EName("oss-cn-heyuan.aliyuncs.com")] [Description("河源")]
        HeYuan = 7,

        /// <summary>
        /// 成都
        /// </summary>
        [EName("oss-cn-chengdu.aliyuncs.com")] [Description("成都")]
        ChengDu = 8,

        /// <summary>
        /// 香港
        /// </summary>
        [EName("oss-cn-hongkong.aliyuncs.com")] [Description("香港")]
        Hongkong = 9,

        /// <summary>
        /// 美国西部1 硅谷
        /// </summary>
        [EName("oss-us-west-1.aliyuncs.com")] [Description("美国西部1 硅谷")]
        UsWest1 = 10,

        /// <summary>
        /// 美国东部 1 （弗吉尼亚）
        /// </summary>
        [EName("oss-us-east-1.aliyuncs.com")] [Description("美国东部 1 （弗吉尼亚）")]
        UsEase1 = 11,

        /// <summary>
        /// 亚太东南 1 （新加坡）
        /// </summary>
        [EName("oss-ap-southeast-1.aliyuncs.com")] [Description("亚太东南 1 （新加坡）")]
        SouthEase1 = 12,

        /// <summary>
        /// 亚太东南 2 （悉尼）
        /// </summary>
        [EName("oss-ap-southeast-2.aliyuncs.com")] [Description("亚太东南 2 （悉尼）")]
        SouthEase2 = 13,

        /// <summary>
        /// 亚太东南 3 （吉隆坡）
        /// </summary>
        [EName("oss-ap-southeast-3.aliyuncs.com")] [Description("亚太东南 3 （吉隆坡）")]
        SouthEase3 = 14,

        /// <summary>
        /// 亚太东南 5 （雅加达）
        /// </summary>
        [EName("oss-ap-southeast-5.aliyuncs.com")] [Description("亚太东南 5 （雅加达）")]
        SouthEase5 = 15
    }
}
