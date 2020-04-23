// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.QiNiu.Storage.Config;

namespace EInfrastructure.Core.QiNiu.Storage.Core
{
    /// <summary>
    ///
    /// </summary>
    internal class Tools
    {
        #region 得到七牛配置文件

        /// <summary>
        /// 得到七牛配置文件
        /// </summary>
        /// <param name="qiNiuConfig">七牛配置</param>
        /// <param name="persistentOps">基础策略</param>
        /// <returns></returns>
        internal static Qiniu.Storage.Config GetConfig(QiNiuStorageConfig qiNiuConfig,
            BasePersistentOps persistentOps = null)
        {
            var config = new Qiniu.Storage.Config()
            {
                Zone = qiNiuConfig.GetZone(),
            };
            if (persistentOps != null)
            {
                config.UseHttps = GetHttpsState(qiNiuConfig, persistentOps.IsUseHttps);
                config.UseCdnDomains = GetCdn(qiNiuConfig, persistentOps.UseCdnDomains);
                config.ChunkSize = (Qiniu.Storage.ChunkUnit) (GetChunkUnit(qiNiuConfig, persistentOps.ChunkUnit).Id);
                config.MaxRetryTimes = GetMaxRetryTimes(qiNiuConfig, persistentOps.MaxRetryTimes);
            }

            return config;
        }

        #endregion

        #region 得到Scheme

        /// <summary>
        /// 得到Scheme
        /// </summary>
        /// <param name="qiNiuConfig">七牛存储配置</param>
        /// <param name="isUseHttps">是否使用https</param>
        /// <returns></returns>
        internal static bool GetHttpsState(QiNiuStorageConfig qiNiuConfig, bool? isUseHttps)
        {
            return isUseHttps != null ? isUseHttps.Value : qiNiuConfig.IsUseHttps;
        }

        /// <summary>
        /// 得到Scheme
        /// </summary>
        /// <param name="qiNiuConfig">七牛存储配置</param>
        /// <param name="isUseHttps">是否使用https</param>
        /// <returns></returns>
        internal static string GetScheme(QiNiuStorageConfig qiNiuConfig, bool? isUseHttps)
        {
            return GetHttpsState(qiNiuConfig, isUseHttps) ? "https://" : "http://";
        }

        #endregion

        #region 得到Cdn

        /// <summary>
        /// 得到Scheme
        /// </summary>
        /// <param name="qiNiuConfig">七牛存储配置</param>
        /// <param name="isUseCdn">是否使用cdn</param>
        /// <returns></returns>
        internal static bool GetCdn(QiNiuStorageConfig qiNiuConfig, bool? isUseCdn)
        {
            return isUseCdn != null ? isUseCdn.Value : qiNiuConfig.UseCdnDomains;
        }

        #endregion

        #region 得到ChunkSize

        /// <summary>
        /// 得到ChunkSize
        /// </summary>
        /// <param name="qiNiuConfig">七牛配置</param>
        /// <param name="chunkUnit">请求Unit</param>
        /// <returns></returns>
        internal static ChunkUnit GetChunkUnit(QiNiuStorageConfig qiNiuConfig, ChunkUnit chunkUnit)
        {
            if (chunkUnit != null)
            {
                return chunkUnit;
            }

            return qiNiuConfig.ChunkUnit;
        }

        #endregion

        #region 得到最大重试次数

        /// <summary>
        /// 得到最大重试次数
        /// </summary>
        /// <param name="qiNiuConfig">七牛配置</param>
        /// <param name="maxRetryTime">最大重试次数</param>
        /// <returns></returns>
        internal static int GetMaxRetryTimes(QiNiuStorageConfig qiNiuConfig, int? maxRetryTime)
        {
            if (maxRetryTime != null)
            {
                return maxRetryTime.Value;
            }

            return qiNiuConfig.MaxRetryTimes;
        }

        #endregion
    }
}
