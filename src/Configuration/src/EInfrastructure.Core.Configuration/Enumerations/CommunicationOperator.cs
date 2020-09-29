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
        public static CommunicationOperator ChinaMobile = new CommunicationOperator(1, "中国移动",
            EInfrastructure.Core.Configuration.Enumerations.Nationality.China);

        /// <summary>
        /// 中国联通
        /// </summary>
        public static CommunicationOperator ChinaUnicom = new CommunicationOperator(2, "中国联通",
            EInfrastructure.Core.Configuration.Enumerations.Nationality.China);

        /// <summary>
        /// 中国电信
        /// </summary>
        public static CommunicationOperator ChinaTelecom = new CommunicationOperator(3, "中国电信",
            EInfrastructure.Core.Configuration.Enumerations.Nationality.China);

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
        public CommunicationOperator(int id, string name,
            Nationality nationality) : this(id, name, nationality.Id)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">通讯运营商id</param>
        /// <param name="name">通讯运营商名称</param>
        /// <param name="nationality">国家</param>
        public CommunicationOperator(int id, string name, int nationality) : base(id, name)
        {
            Nationality = nationality;
        }

        /// <summary>
        /// 国家
        /// </summary>
        public int Nationality { get; set; }
    }
}
