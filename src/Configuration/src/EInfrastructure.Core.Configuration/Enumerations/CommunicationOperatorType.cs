// Copyright (c) zhenlei520 All rights reserved.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Enumerations
{
    /// <summary>
    /// 运营商类型
    /// </summary>
    public class CommunicationOperatorType : Enumeration
    {
        /// <summary>
        /// 传统运营商
        /// </summary>
        public static CommunicationOperatorType Traditional = new CommunicationOperatorType(1, "传统运营商");

        /// <summary>
        /// 虚拟运营商
        /// </summary>
        public static CommunicationOperatorType Virtual = new CommunicationOperatorType(2, "虚拟运营商");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public CommunicationOperatorType(int id, string name) : base(id, name)
        {
        }
    }
}
