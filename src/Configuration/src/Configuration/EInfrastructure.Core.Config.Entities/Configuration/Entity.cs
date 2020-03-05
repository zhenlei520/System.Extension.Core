// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Config.Entities.Ioc;

namespace EInfrastructure.Core.Config.Entities.Configuration
{
    /// <summary>
    /// 实体实现
    /// </summary>
    /// <typeparam name="T">主键类型</typeparam>
    public abstract class Entity<T> : IEntity<T>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual T Id { get; set; }
    }
}
