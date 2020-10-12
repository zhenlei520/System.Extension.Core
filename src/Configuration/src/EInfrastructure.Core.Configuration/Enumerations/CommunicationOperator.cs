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
        public static CommunicationOperator ChinaMobile = new CommunicationOperator(1, "中国移动",
            EInfrastructure.Core.Configuration.Enumerations.Nationality.China,@"(^1(3[4-9]|4[7]|5[0-27-9]|7[28]|8[2-478]|9[58])\d{8}$)|(^1705\d{7}$)");

        /// <summary>
        /// 中国联通
        /// </summary>
        public static CommunicationOperator ChinaUnicom = new CommunicationOperator(2, "中国联通",
            EInfrastructure.Core.Configuration.Enumerations.Nationality.China,@"(^1(3[0-2]|4[5]|5[56]|6[67]|7[156]|8[56])\d{8}$)|(^1709\d{7}$)");

        /// <summary>
        /// 中国电信
        /// </summary>
        public static CommunicationOperator ChinaTelecom = new CommunicationOperator(3, "中国电信",
            EInfrastructure.Core.Configuration.Enumerations.Nationality.China,@"^1((33|53|49|62|7[37]|8[019]|9[139])[0-9]|349|700)\d{7}$");

        /// <summary>
        /// 中国广播电视网络有限公司
        /// </summary>
        public static CommunicationOperator ChinaBroadcastingNetwork = new CommunicationOperator(4, "中国广播电视网络有限公司",
            EInfrastructure.Core.Configuration.Enumerations.Nationality.China);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">通讯运营商id</param>
        /// <param name="name">通讯运营商名称</param>
        /// <param name="nationality">国家</param>
        /// <param name="mobileRegex">手机号码规则</param>
        /// <param name="phoneRegex">固话号码规则</param>
        public CommunicationOperator(int id, string name,
            Nationality nationality, string mobileRegex = "", string phoneRegex = "") : this(id, name, nationality.Id,
            mobileRegex, phoneRegex)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">通讯运营商id</param>
        /// <param name="name">通讯运营商名称</param>
        /// <param name="nationality">国家</param>
        /// <param name="mobileRegex">手机号码规则</param>
        /// <param name="phoneRegex">固话号码规则</param>
        public CommunicationOperator(int id, string name, int nationality, string mobileRegex,
            string phoneRegex) : base(id, name)
        {
            this.Nationality = nationality;
            this.MobileRegex = mobileRegex;
            this.PhoneRegex = phoneRegex;
        }

        /// <summary>
        /// 国家
        /// </summary>
        public int Nationality { get; set; }

        /// <summary>
        /// 手机号码规则
        /// </summary>
        public string MobileRegex { get; set; }

        /// <summary>
        /// 固话号码规则
        /// </summary>
        public string PhoneRegex { get; set; }
    }
}
