// Copyright (c) zhenlei520 All rights reserved.

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
