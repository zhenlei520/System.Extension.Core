// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EInfrastructure.Core.MySql
{
    /// <summary>
    ///
    /// </summary>
    public static class Ext
    {
        #region 启动调试日志

        /// <summary>
        /// 启动追踪日志
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public static void EnableTrace(this DbContextOptionsBuilder optionsBuilder)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        #endregion

        #region 自动映射

        /// <summary>
        /// 自动映射（只处理映射到数据库，对参数类型以及查询的时候类型转换不做处理）
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assType">Map迁移中实现的接口</param>
        public static void AutoMap<TDbContext>(this ModelBuilder modelBuilder, Type assType)
            where TDbContext : DbContext
        {
            if (assType == typeof(IEntityMap<>))
            {
                modelBuilder.AutoMap<TDbContext>(typeof(IEntityMap<>), "Map");
            }
            else if (assType == typeof(IEntityTypeConfiguration<>))
            {
                modelBuilder.AutoMap<TDbContext>(typeof(IEntityTypeConfiguration<>), "Configure");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="mappingInterface"></param>
        /// <param name="methodName"></param>
        /// <typeparam name="TDbContext"></typeparam>
        private static void AutoMap<TDbContext>(this ModelBuilder modelBuilder, Type mappingInterface,
            string methodName) where TDbContext : DbContext
        {
            // Types that do entity mapping
            var mappingTypes = typeof(TDbContext).GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y =>
                    y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));

            // Get the generic Entity method of the ModelBuilder type
            var entityMethod = typeof(ModelBuilder).GetMethods()
                .Single(x => x.Name == "Entity" &&
                             x.IsGenericMethod &&
                             x.ReturnType.Name == "EntityTypeBuilder`1");

            foreach (var mappingType in mappingTypes)
            {
                // Get the type of entity to be mapped
                var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();

                // Get the method builder.Entity<TEntity>
                var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

                // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
                var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

                // Create the mapping type and do the mapping
                var mapper = Activator.CreateInstance(mappingType);
                mapper.GetType().GetMethod(methodName)?.Invoke(mapper, new[] {entityBuilder});
            }
        }

        #endregion

        #region 设置默认参数类型

        /// <summary>
        /// 设置默认参数类型
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="value">设置的参数类型值</param>
        /// <typeparam name="T">对哪些参数类型的值设置默认参数类型</typeparam>
        public static void SetColumnType<T>(this ModelBuilder modelBuilder, string value) where T : IComparable
        {
            // foreach (var property in modelBuilder.Model.GetEntityTypes()
            //     .SelectMany(t => t.GetProperties())
            //     .Where(p => p.ClrType == typeof(decimal)))
            // {
            //     property.SetColumnType("decimal(18, 2)");
            // }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(T)))
            {
                property.Relational().ColumnType = value;
            }
        }

        #endregion
    }

    /// <summary>
    /// 跟踪日志
    /// </summary>
    public class TraceLogger : ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _categoryName;

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryName"></param>
        public TraceLogger(string categoryName) => this._categoryName = categoryName;

        /// <summary>
        /// 是否启用日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// 增加日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        /// <typeparam name="TState"></typeparam>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            Trace.WriteLine($"{DateTime.Now:o} {logLevel} {eventId.Id} {_categoryName}");
            Trace.WriteLine(formatter(state, exception));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="state"></param>
        /// <typeparam name="TState"></typeparam>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state) => null;
    }

    /// <summary>
    ///
    /// </summary>
    public class TraceLoggerProvider : ILoggerProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName) => new TraceLogger(categoryName);

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
        }
    }
}