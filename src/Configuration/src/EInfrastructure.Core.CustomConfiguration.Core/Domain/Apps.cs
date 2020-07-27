// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.Entities.Configuration;

namespace EInfrastructure.Core.CustomConfiguration.Core.Domain
{
    /// <summary>
    /// 应用
    /// </summary>
    public class Apps : AggregateRoot<long>
    {
        /// <summary>
        ///
        /// </summary>
        public Apps()
        {
            this.Id = ServiceCollectionExtensions.SnowflakeId.NextId();
            this.AddTime = DateTime.Now;
            this.EditTime = DateTime.Now;
            this.IsDel = false;
            this.DelTime = null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="appId">应用id</param>
        /// <param name="name">应用名称</param>
        public Apps(string appId, string name) : this()
        {
            AppId = appId;
            Name = name;
        }

        /// <summary>
        /// 应用id
        /// </summary>
        public string AppId { get; private set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; private set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime EditTime { get; private set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; private set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DelTime { get; private set; }
    }
}
