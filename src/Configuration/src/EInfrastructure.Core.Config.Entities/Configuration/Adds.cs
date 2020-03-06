// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace EInfrastructure.Core.Config.Entities.Configuration
{
    /// <summary>
    /// 添加信息聚合根
    /// </summary>
    public class Adds<T> : AggregateRoot<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Adds()
        {
            AddTime = DateTime.Now;
        }

        /// <summary>
        /// 构造函数（默认对添加账户赋值）
        /// </summary>
        /// <param name="accountId"></param>
        public Adds(T accountId)
        {
            AddAccountId = accountId;
            AddTime = DateTime.Now;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public T AddAccountId { get; set; }
    }
}
