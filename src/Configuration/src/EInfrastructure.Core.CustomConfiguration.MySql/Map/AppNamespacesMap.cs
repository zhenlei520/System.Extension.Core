// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.CustomConfiguration.Core.Domain;
using EInfrastructure.Core.MySql;
using EInfrastructure.Core.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EInfrastructure.Core.CustomConfiguration.MySql.Map
{
    /// <summary>
    ///
    /// </summary>
    public class AppNamespacesMap : IEntityMap<AppNamespaces>
    {
        private readonly ConfigurationMySqlOptions _options;

        public AppNamespacesMap() : this(null)
        {
        }

        public AppNamespacesMap(ConfigurationMySqlOptions options)
        {
            _options = options ?? ConfigurationMySqlOptions.Default;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void Map(EntityTypeBuilder<AppNamespaces> builder)
        {
            builder.ToTable("app.namespaces".AppendLeft(_options.Pre, "."));
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasMaxLength(36).HasColumnName("id").IsRequired();

            builder.Property(t => t.AppId).HasMaxLength(50).HasColumnName("appid").IsRequired();
            builder.Property(t => t.Name).HasMaxLength(50).HasColumnName("name").IsRequired();
            builder.Property(t => t.Format).HasMaxLength(50).HasColumnName("format").IsRequired();
            builder.Property(t => t.Remark).HasMaxLength(200).HasColumnName("remark");


            builder.Property(t => t.AddTime).HasColumnName("add_time").IsRequired();
            builder.Property(t => t.EditTime).HasColumnName("edit_time").IsRequired();
            builder.Property(t => t.IsDel).HasColumnName("is_del").IsRequired();
            builder.Property(t => t.DelTime).HasColumnName("del_time");

            builder.HasMany(t => t.NamespaceItems).WithOne().HasForeignKey(t => t.AppNamespaceId);
        }
    }
}