// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Enumerations
{
    /// <summary>
    /// 结论
    /// </summary>
    public class ConclusionState : Enumeration
    {
        /// <summary>
        /// 合规
        /// </summary>
        public static ConclusionState Compliance = new ConclusionState(1, "合规");

        /// <summary>
        /// 不合规
        /// </summary>
        public static ConclusionState NonCompliant = new ConclusionState(2, "不合规");

        /// <summary>
        /// 疑似
        /// </summary>
        public static ConclusionState Suspected = new ConclusionState(3, "疑似");

        /// <summary>
        /// 审核失败
        /// </summary>
        public static ConclusionState Failure = new ConclusionState(4, "审核失败");

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ConclusionState(int id, string name) : base(id, name)
        {
        }
    }
}
