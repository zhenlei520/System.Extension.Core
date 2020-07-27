// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EInfrastructure.Core.Config.Entities.Configuration;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.Configuration.Ioc;
using EInfrastructure.Core.MySql;
using Microsoft.EntityFrameworkCore;

namespace EInfrastructure.Core.CustomConfiguration.MySql
{
    /// <summary>
    ///
    /// </summary>
    public class CustomerConfigurationDbContext : DbContext, IUnitOfWork<CustomerConfigurationDbContext>, IPerRequest,
        IDbContext
    {
        private readonly ConfigurationMySqlOptions _configurationOptions;

        /// <summary>
        ///
        /// </summary>
        /// <param name="configurationOptions"></param>
        public CustomerConfigurationDbContext(ConfigurationMySqlOptions configurationOptions)
        {
            _configurationOptions = configurationOptions??throw new ArgumentNullException(nameof(configurationOptions));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_configurationOptions.DbConn, b => b.MaxBatchSize(30));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AutoMap(typeof(CustomerConfigurationDbContext));

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) && p.Relational().ColumnType == null))
            {
                property.Relational().ColumnType = "decimal(18, 2)";
            }
        }

        #region 提交保存

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            this.SaveChanges();
            return true;
        }

        #endregion

        #region 异步保存

        /// <summary>
        /// 异步保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        #endregion
    }
}
