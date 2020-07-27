// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.Entities.Configuration;

namespace EInfrastructure.Core.CustomConfiguration.Core.Domain
{
    /// <summary>
    /// 命名空间
    /// </summary>
    public class NamespaceItems : Entity<long>
    {
        /// <summary>
        ///
        /// </summary>
        public NamespaceItems()
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
        /// <param name="environmentName">环境名称</param>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="remark">备足</param>
        public NamespaceItems(string environmentName, string key, string value, string remark) : this()
        {
            EnvironmentName = environmentName;
            Key = key;
            Value = value;
            Remark = remark;
        }

        /// <summary>
        /// 环境名称
        /// </summary>
        public string EnvironmentName { get; private set; }

        /// <summary>
        /// 参数名
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }

        /// <summary>
        /// 应用名称空间id
        /// </summary>
        public long AppNamespaceId { get; private set; }

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

        /// <summary>
        /// 名称空间
        /// </summary>
        public virtual AppNamespaces AppNamespaces { get; private set; }
    }
}
