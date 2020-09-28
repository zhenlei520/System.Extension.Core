// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using EInfrastructure.Core.Config.Entities.Enumerations;
using Newtonsoft.Json;

namespace EInfrastructure.Core.Config.Entities.Configuration
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbContextConfigOptions
    {
        /// <summary>
        ///
        /// </summary>
        [JsonConstructor]
        public DbContextConfigOptions()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionString">写库连接字符串</param>
        public DbContextConfigOptions(string connectionString) : this()
        {
            this.ConnectionString = connectionString;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this.readConnectionStringList = new List<string>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionString">写库连接字符串</param>
        /// <param name="readConnectionStrings">从库连接字符串</param>
        public DbContextConfigOptions(string connectionString, params string[] readConnectionStrings) : this(
            connectionString)
        {
            this.ConnectionString = connectionString;
            this.ReadConnectionStrings =
                readConnectionStrings.Select(x => new DbContextConfigWeightOption(x, 1)).ToList();
            if (this.ReadConnectionStrings.Sum(x => x.Weight) > 100)
            {
                throw new ArgumentException("读库最大为100个");
            }

            readConnectionStrings.ToList().ForEach(readConnectionString =>
            {
                this.readConnectionStringList.Add(readConnectionString);
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="connectionString">写库连接字符串</param>
        /// <param name="readConnectionStrings">从库连接配置</param>
        public DbContextConfigOptions(string connectionString,
            List<DbContextConfigWeightOption> readConnectionStrings) :
            this(
                connectionString)
        {
            this.ReadConnectionStrings = readConnectionStrings;
            if (readConnectionStrings.Any(x => x.Weight <= 0))
            {
                throw new ArgumentException("权重最大为100", nameof(readConnectionStrings));
            }

            readConnectionStrings.ForEach(item =>
            {
                for (int i = 0; i < item.Weight; i++)
                {
                    this.readConnectionStringList.Add(item.ConnectionString);
                }
            });
        }

        /// <summary>
        /// 写库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        ///
        /// </summary>
        private List<string> readConnectionStringList = null;

        /// <summary>
        /// 读库配置集合
        /// </summary>
        private List<string> ReadConnectionStringList
        {
            get
            {
                if (readConnectionStringList == null)
                {
                    readConnectionStringList = new List<string>();
                    ReadConnectionStrings.ForEach(item =>
                    {
                        if (item.Weight > 0)
                        {
                            for (int i = 0; i < item.Weight; i++)
                            {
                                readConnectionStringList.Add(item.ConnectionString);
                            }
                        }
                        else
                        {
                            readConnectionStringList.Add(item.ConnectionString);
                        }
                    });
                }

                return readConnectionStringList;
            }
        }

        /// <summary>
        /// 读库连接字符串与权重
        /// </summary>
        public List<DbContextConfigWeightOption> ReadConnectionStrings { get; set; }

        /// <summary>
        /// 得到连接字符串
        /// </summary>
        /// <param name="dbOptionType"></param>
        /// <returns></returns>
        public string GetConnectionString(DbOptionType dbOptionType)
        {
            if (dbOptionType.Equals(DbOptionType.Write) || ReadConnectionStrings.Count == 0)
            {
                return this.ConnectionString;
            }

            int i = 0;
            i = ReadConnectionStringList.Count <= 1 ? 0 : new Random().Next(0, ReadConnectionStringList.Count);
            return this.ReadConnectionStringList[i];
        }

        /// <summary>
        /// 权重配置
        /// </summary>
        public class DbContextConfigWeightOption
        {
            /// <summary>
            ///
            /// </summary>
            public DbContextConfigWeightOption()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="connectionString"></param>
            /// <param name="weight"></param>
            public DbContextConfigWeightOption(string connectionString, int weight) : this()
            {
                this.ConnectionString = connectionString;
                this.Weight = weight;
            }

            /// <summary>
            /// 链接地址
            /// </summary>
            public string ConnectionString { get; set; }

            /// <summary>
            /// 权重
            /// </summary>
            public int Weight { get; set; }
        }
    }
}
