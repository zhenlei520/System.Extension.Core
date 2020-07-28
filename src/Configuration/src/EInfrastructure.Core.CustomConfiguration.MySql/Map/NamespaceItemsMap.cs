// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.CustomConfiguration.Core.Domain;
using EInfrastructure.Core.MySql;
using EInfrastructure.Core.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInfrastructure.Core.CustomConfiguration.MySql.Map
{
    public class NamespaceItemsMap : IEntityMap<NamespaceItems>
    {
        private readonly ConfigurationMySqlOptions _options;

        public NamespaceItemsMap() : this(null)
        {
        }

        public NamespaceItemsMap(ConfigurationMySqlOptions options)
        {
            _options = options ?? ConfigurationMySqlOptions.Default;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void Map(EntityTypeBuilder<NamespaceItems> builder)
        {
            builder.ToTable("namespace.items".AppendLeft(_options.Pre, "."));
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasMaxLength(36).HasColumnName("id").IsRequired();

            builder.Property(t => t.EnvironmentName).HasMaxLength(50).HasColumnName("environment_name").IsRequired();
            builder.Property(t => t.Key).HasMaxLength(50).HasColumnName("key").IsRequired();
            builder.Property(t => t.Value).HasMaxLength(50).HasColumnName("value").IsRequired();
            builder.Property(t => t.Remark).HasMaxLength(200).HasColumnName("remark");
            builder.Property(t => t.AppNamespaceId).HasMaxLength(36).HasColumnName("app_namespace_id");

            builder.Property(t => t.AddTime).HasColumnName("add_time").IsRequired();
            builder.Property(t => t.EditTime).HasColumnName("edit_time").IsRequired();
            builder.Property(t => t.IsDel).HasColumnName("is_del").IsRequired();
            builder.Property(t => t.DelTime).HasColumnName("del_time");
        }
    }
}