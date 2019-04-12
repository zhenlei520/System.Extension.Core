// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Config.EntitiesExtensions
{
    /// <summary>
    /// 聚合跟接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAggregateRoot<T> : IEntity<T>
    {
    }
}
