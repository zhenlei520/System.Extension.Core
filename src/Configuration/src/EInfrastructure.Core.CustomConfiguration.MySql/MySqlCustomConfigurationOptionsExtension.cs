// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Config.Entities.Ioc;
using EInfrastructure.Core.CustomConfiguration.Core.Domain;
using EInfrastructure.Core.CustomConfiguration.Core.Internal;
using EInfrastructure.Core.Infrastructure;
using EInfrastructure.Core.MySql.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EInfrastructure.Core.CustomConfiguration.MySql
{
    /// <summary>
    ///
    /// </summary>
    public class MySqlCustomConfigurationOptionsExtension : ICustomConfigurationOptionsExtension
    {
        private readonly Action<ConfigurationMySqlOptions> _options;

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        public MySqlCustomConfigurationOptionsExtension(Action<ConfigurationMySqlOptions> options)
        {
            this._options = options;
        }

        /// <summary>
        /// 添加data提供者
        /// </summary>
        /// <param name="services"></param>
        public void AddServices(IServiceCollection services)
        {
            var mySqlOption = new ConfigurationMySqlOptions();
            _options.Invoke(mySqlOption);
            services.AddSingleton(mySqlOption);

            #region 注入数据库信息

            services.AddScoped(typeof(IUnitOfWork<CustomerConfigurationDbContext>),
                typeof(CustomerConfigurationDbContext));

            services.AddScoped(typeof(IQuery<Apps, long, CustomerConfigurationDbContext>),
                typeof(QueryBase<Apps, long, CustomerConfigurationDbContext>));

            services.AddScoped(typeof(IQuery<AppNamespaces, long, CustomerConfigurationDbContext>),
                typeof(QueryBase<AppNamespaces, long, CustomerConfigurationDbContext>));

            services.AddScoped(typeof(IQuery<NamespaceItems, long, CustomerConfigurationDbContext>),
                typeof(QueryBase<NamespaceItems, long, CustomerConfigurationDbContext>));

            services.AddScoped(typeof(IRepository<Apps, long, CustomerConfigurationDbContext>),
                typeof(RepositoryBase<Apps, long, CustomerConfigurationDbContext>));

            services.AddScoped(typeof(IRepository<AppNamespaces, long, CustomerConfigurationDbContext>),
                typeof(RepositoryBase<AppNamespaces, long, CustomerConfigurationDbContext>));

            services.AddDbContext<CustomerConfigurationDbContext>();

            #endregion

            services.AddTransient<ICustomConfigurationDataProvider, CustomConfigurationDataProvider>();
        }
    }
}
