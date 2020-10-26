// Copyright (c) zhenlei520 All rights reserved.
/* 目前已知号段规则
 * 移动号段：134,135,136,137,138,139,147,150,151,152,157,158,159,165,1703,1705,1706,172,178,182,183,184,187,188,195,198
 * 联通号段：130,131,132,145,155,156,166,167,1704,1707,1708,1709,171,175,176,185,186
 * 电信号段：133,149,153,162,1700,1701,1702,173,177,180,181,189,191,193,199
 * 广电号段：
 * 其中133号段原属于中国联通，于2018年转到电信旗下
 * 153是原中国联通的CDMA手机号段，2008年后来划归新中国电信名下
 *
 * 165,1703,1705,1706是虚拟运行商，归属中国移动
 * 166,167,1704,1707,1708,1709是虚拟运行商，归属中国联通
 * 162,1700,1701,1702是虚拟运行商，归属中国电信
 * 其中1740工业和信息化部应急通讯保障中心
 */

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 通讯运营商
    /// </summary>
    public class CommunicationOperator : Enumeration
    {
        /// <summary>
        /// 中国移动
        /// </summary>
        public static CommunicationOperator ChinaMobile = new CommunicationOperator(1, "中国移动");

        /// <summary>
        /// 中国联通
        /// </summary>
        public static CommunicationOperator ChinaUnicom = new CommunicationOperator(2, "中国联通");

        /// <summary>
        /// 中国电信
        /// </summary>
        public static CommunicationOperator ChinaTelecom = new CommunicationOperator(3, "中国电信");

        /// <summary>
        /// 中国广播电视网络有限公司
        /// </summary>
        public static CommunicationOperator ChinaBroadcastingNetwork = new CommunicationOperator(4, "中国广播电视网络有限公司");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public CommunicationOperator(int id, string name) : base(id, name)
        {
        }
    }
}
