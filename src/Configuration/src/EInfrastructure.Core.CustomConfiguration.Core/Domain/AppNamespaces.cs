// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.CustomConfiguration.Core.Enumerations;
using EInfrastructure.Core.Tools;

namespace EInfrastructure.Core.CustomConfiguration.Core.Domain
{
    /// <summary>
    /// 应用名称空间
    /// </summary>
    public class AppNamespaces : AggregateRoot<long>
    {
        /// <summary>
        ///
        /// </summary>
        public AppNamespaces()
        {
            this.Id = ServiceCollectionExtensions.SnowflakeId.NextId();
            this.AddTime = DateTime.Now;
            this.EditTime = DateTime.Now;
            this.IsDel = false;
            this.DelTime = null;
            this._namespaceItems = new List<NamespaceItems>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId">应用id</param>
        /// <param name="name">命名空间</param>
        /// <param name="remark">备注</param>
        public AppNamespaces(string appId, string name, string remark) : this()
        {
            AppId = appId;
            Name = name;
            Format = PathCommon.GetExtension(name).Contains("json") ? DataType.Json.Name : DataType.Property.Name;
            Remark = remark;
        }

        /// <summary>
        /// 应用id
        /// </summary>
        public string AppId { get; private set; }

        /// <summary>
        /// 名称空间
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 格式
        /// 目前仅支持Json、Property
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; private set; }

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

        #region 名称空间值

        /// <summary>
        ///
        /// </summary>
        private List<NamespaceItems> _namespaceItems;

        /// <summary>
        ///
        /// </summary>
        public IReadOnlyCollection<NamespaceItems> NamespaceItems => _namespaceItems;

        #endregion

        #region 添加名称空间值

        /// <summary>
        /// 添加名称空间值
        /// </summary>
        /// <param name="environmentName">环境信息</param>
        /// <param name="value">值</param>
        /// <param name="remark">备注</param>
        /// <param name="key">键</param>
        public void AddItem(string environmentName, string value, string remark, string key)
        {
            if (this.IsDel)
            {
                throw new BusinessException("当前名称空间不存在或者已经被删除");
            }

            if (this.Format.Equals(DataType.Json.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                if (this._namespaceItems.Count > 0)
                {
                    throw new BusinessException("请修改名称空间信息");
                }

                this._namespaceItems.Add(new NamespaceItems(environmentName, "content", value, remark));
            }
            else
            {
                if (this._namespaceItems.Any(x => x.Key == key))
                {
                    throw new BusinessException("请更换key，当前key已经存在");
                }

                this._namespaceItems.Add(new NamespaceItems(environmentName, key, value, remark));
            }
        }

        #endregion

        #region 更新应用名称空间

        /// <summary>
        /// 更新应用名称空间
        /// </summary>
        /// <param name="name">应用新名称（为空不更新）</param>
        /// <param name="remark">应用备注</param>
        public void Update(string name, string remark)
        {
            this.Name = name;
            Format = PathCommon.GetExtension(name).Contains("json") ? DataType.Json.Name : DataType.Property.Name;
            this.Remark = remark;
            this.EditTime = DateTime.Now;
        }

        #endregion

        #region 删除应用名称空间

        /// <summary>
        /// 删除应用名称空间
        /// </summary>
        public void Del()
        {
            if (!this.IsDel)
            {
                this.IsDel = true;
                this.DelTime = DateTime.Now;
            }
        }

        #endregion
    }
}