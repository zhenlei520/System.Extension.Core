// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.QiNiu.Storage.Config;
using EInfrastructure.Core.QiNiu.Storage.Enum;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Attributes;

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
            BasePersistentOps persistentOps)
        {
            var config = new Qiniu.Storage.Config
            {
                Zone = GetZone(qiNiuConfig, persistentOps.Zone),
                UseHttps = GetHttpsState(qiNiuConfig, persistentOps.IsUseHttps),
                UseCdnDomains = GetCdn(qiNiuConfig, persistentOps.UseCdnDomains),
                ChunkSize = (Qiniu.Storage.ChunkUnit) (GetChunkUnit(qiNiuConfig, persistentOps.ChunkUnit).Id),
                MaxRetryTimes = GetMaxRetryTimes(qiNiuConfig, persistentOps.MaxRetryTimes),
            };

            config.UseHttps = GetHttpsState(qiNiuConfig, persistentOps.IsUseHttps);
            config.UseCdnDomains = GetCdn(qiNiuConfig, persistentOps.UseCdnDomains);
            config.ChunkSize = (Qiniu.Storage.ChunkUnit) (GetChunkUnit(qiNiuConfig, persistentOps.ChunkUnit).Id);
            config.MaxRetryTimes = GetMaxRetryTimes(qiNiuConfig, persistentOps.MaxRetryTimes);

            return config;
        }

        #endregion

        #region 得到默认空间域

        /// <summary>
        /// 得到默认空间域
        /// </summary>
        /// <param name="qiNiuConfig">七牛配置</param>
        /// <param name="host">空间域</param>
        /// <returns></returns>
        internal static string GetHost(QiNiuStorageConfig qiNiuConfig, string host)
        {
            if (string.IsNullOrEmpty(host) == string.IsNullOrEmpty(qiNiuConfig.DefaultHost))
            {
                throw new BusinessException("请输入默认空间域");
            }

            if (!string.IsNullOrEmpty(host))
            {
                return host;
            }

            return qiNiuConfig.DefaultHost;
        }

        #endregion

        #region 得到空间名

        /// <summary>
        /// 得到空间名
        /// </summary>
        /// <param name="qiNiuStorageConfig">七牛配置</param>
        /// <param name="bucket">空间名</param>
        /// <returns></returns>
        internal static string GetBucket(QiNiuStorageConfig qiNiuStorageConfig, string bucket)
        {
            if (string.IsNullOrEmpty(bucket) && string.IsNullOrEmpty(qiNiuStorageConfig.DefaultBucket))
            {
                throw new BusinessException("请选择存储空间");
            }

            if (!string.IsNullOrEmpty(bucket))
            {
                return bucket;
            }

            return qiNiuStorageConfig.DefaultBucket;
        }

        /// <summary>
        /// 得到空间名
        /// </summary>
        /// <param name="qiNiuStorageConfig">七牛配置</param>
        /// <param name="bucket">空间名</param>
        /// <param name="optBucket">目标空间</param>
        /// <returns></returns>
        internal static string GetBucket(QiNiuStorageConfig qiNiuStorageConfig, string bucket, string optBucket)
        {
            if (!string.IsNullOrEmpty(optBucket))
            {
                return optBucket;
            }

            return GetBucket(qiNiuStorageConfig, bucket);
        }

        #endregion

        #region 得到空间区域

        /// <summary>
        /// 得到空间区域
        /// </summary>
        /// <param name="qiNiuConfig">七牛配置</param>
        /// <param name="zone">空间配置</param>
        /// <param name="defaultZone">默认空间区域</param>
        /// <returns></returns>
        internal static Qiniu.Storage.Zone GetZone(QiNiuStorageConfig qiNiuConfig, int? zone,
            Func<ZoneEnum> defaultZone = null)
        {
            switch (GetZonePrivate(qiNiuConfig, zone, defaultZone))
            {
                case ZoneEnum.ZoneCnEast:
                default:
                    return Qiniu.Storage.Zone.ZONE_CN_East;
                case ZoneEnum.ZoneCnNorth:
                    return Qiniu.Storage.Zone.ZONE_CN_North;
                case ZoneEnum.ZoneCnSouth:
                    return Qiniu.Storage.Zone.ZONE_CN_South;
                case ZoneEnum.ZoneUsNorth:
                    return Qiniu.Storage.Zone.ZONE_US_North;
                case ZoneEnum.ZoneAsSingapore:
                    return Qiniu.Storage.Zone.ZONE_AS_Singapore;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="qiNiuConfig"></param>
        /// <param name="zone"></param>
        /// <param name="defaultZone">默认空间委托</param>
        /// <returns></returns>
        internal static ZoneEnum GetZonePrivate(QiNiuStorageConfig qiNiuConfig, int? zone,
            Func<ZoneEnum> defaultZone = null)
        {
            if (zone == null && qiNiuConfig.DefaultZones == null)
            {
                if (defaultZone == null)
                {
                    throw new ArgumentNullException(nameof(zone));
                }

                return defaultZone.Invoke();
            }

            if (zone != null)
            {
                return (ZoneEnum) zone.Value;
            }

            return qiNiuConfig.DefaultZones.Value;
        }

        #endregion

        #region 得到空间区域

        /// <summary>
        ///
        /// </summary>
        /// <param name="zone">空间区域</param>
        /// <returns></returns>
        internal static string GetRegion(ZoneEnum zone)
        {
            return zone.GetCustomerObj<ENameAttribute>()?.Name ?? "";
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

        #region 得到Message

        /// <summary>
        /// 得到Message
        /// </summary>
        /// <returns></returns>
        internal static string GetMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return $"Message：{ex.Message}，InnerExceptionMessage：{ex.InnerException?.Message}";
            }

            return $"Message：{ex.Message}";
        }

        #endregion
    }
}
