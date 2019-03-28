// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInfrastructure.Core.MySql
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    public interface IEntityMap<TEntityType> where TEntityType : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        void Map(EntityTypeBuilder<TEntityType> builder);
    }
}