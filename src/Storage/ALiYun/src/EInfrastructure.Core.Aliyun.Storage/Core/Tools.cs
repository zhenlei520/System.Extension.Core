// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

namespace EInfrastructure.Core.Aliyun.Storage.Core
{
    /// <summary>
    ///
    /// </summary>
    internal class Tools
    {
        #region 得到空间区域

        /// <summary>
        /// 得到空间区域
        /// </summary>
        /// <param name="aliyunConfig">阿里云存储配置</param>
        /// <param name="zone">自定义空间区域</param>
        /// <returns></returns>
        internal static ZoneEnum GetZone(ALiYunStorageConfig aliyunConfig, int? zone)
        {
            if (zone == null && aliyunConfig.DefaultZones == null)
            {
                throw new BusinessException("请选择存储空间区域信息");
            }

            if (zone != null)
            {
                return (ZoneEnum) zone.Value;
            }

            return aliyunConfig.DefaultZones.Value;
        }

        #endregion

        #region 得到空间名

        /// <summary>
        /// 得到空间名
        /// </summary>
        /// <param name="aliyunConfig">阿里云配置</param>
        /// <param name="bucket">空间名</param>
        /// <returns></returns>
        internal static string GetBucket(ALiYunStorageConfig aliyunConfig, string bucket)
        {
            if (string.IsNullOrEmpty(bucket) && string.IsNullOrEmpty(aliyunConfig.DefaultBucket))
            {
                throw new BusinessException("请选择存储空间");
            }

            if (!string.IsNullOrEmpty(bucket))
            {
                return bucket;
            }

            return aliyunConfig.DefaultBucket;
        }

        #endregion

        #region 得到分片大小

        /// <summary>
        /// 得到分片大小
        /// </summary>
        /// <param name="aliyunConfig">阿里云配置</param>
        /// <param name="chunkUnit">分片大小</param>
        /// <returns></returns>
        internal static ChunkUnit GetChunkUnit(ALiYunStorageConfig aliyunConfig, ChunkUnit chunkUnit)
        {
            if (chunkUnit == null && aliyunConfig.ChunkUnit == null)
            {
                throw new BusinessException<string>("未设置分片大小", HttpStatus.Err.Name);
            }

            if (chunkUnit != null)
            {
                return chunkUnit;
            }

            return aliyunConfig.ChunkUnit;
        }

        #endregion
    }
}
