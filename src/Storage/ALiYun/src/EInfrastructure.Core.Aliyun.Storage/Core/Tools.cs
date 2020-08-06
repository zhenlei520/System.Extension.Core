// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Linq;
using Aliyun.OSS;
using EInfrastructure.Core.Aliyun.Storage.Config;
using EInfrastructure.Core.Aliyun.Storage.Enum;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using EInfrastructure.Core.Tools;
using EInfrastructure.Core.Tools.Attributes;
using StorageClass = Aliyun.OSS.StorageClass;

namespace EInfrastructure.Core.Aliyun.Storage.Core
{
    /// <summary>
    ///
    /// </summary>
    internal class Tools : Maps
    {
        #region 根据endpoint得到存储区域

        /// <summary>
        /// 根据endpoint得到存储区域
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        internal static ZoneEnum? GetZoneByLocation(string location)
        {
            var ret = typeof(ZoneEnum).ToEnumAndAttributes<ENameAttribute>()
                .Where(x => x.Value.Name.Contains(location))
                .Select(x => x.Key).FirstOrDefault();
            return (ZoneEnum?) ret;
        }

        #endregion

        #region 得到访问权限

        /// <summary>
        /// 得到访问权限
        /// </summary>
        /// <param name="permiss"></param>
        /// <returns></returns>
        internal static CannedAccessControlList GetCannedAccessControl(Permiss permiss)
        {
            var cannedAccessControl = CannedAccessControl.Where(x => x.Key.Id == permiss.Id).Select(x => x.Value)
                .FirstOrDefault();
            if (cannedAccessControl == default)
            {
                throw new BusinessException<string>("不支持的访问权限", HttpStatus.Err.Name);
            }

            return cannedAccessControl;
        }

        /// <summary>
        /// 得到访问权限
        /// </summary>
        /// <param name="permiss"></param>
        /// <returns></returns>
        internal static Permiss GetPermiss(CannedAccessControlList permiss)
        {
            var cannedAccessControl = CannedAccessControl.Where(x => x.Value == permiss).Select(x => x.Key)
                .FirstOrDefault();
            if (cannedAccessControl == null)
            {
                throw new BusinessException<string>("不支持的访问权限", HttpStatus.Err.Name);
            }

            return cannedAccessControl;
        }

        #endregion

        #region 得到空间区域

        /// <summary>
        /// 得到空间区域
        /// </summary>
        /// <param name="aliyunConfig">阿里云存储配置</param>
        /// <param name="zone">自定义空间区域</param>
        /// <param name="defaultZone">默认空间设置</param>
        /// <returns></returns>
        internal static ZoneEnum GetZone(ALiYunStorageConfig aliyunConfig, int? zone, Func<ZoneEnum> defaultZone = null)
        {
            if (zone == null && aliyunConfig.DefaultZones == null)
            {
                if (defaultZone == null)
                {
                    throw new BusinessException("请选择存储空间区域信息");
                }

                return defaultZone.Invoke();
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

        /// <summary>
        /// 得到空间名
        /// </summary>
        /// <param name="qiNiuStorageConfig">七牛配置</param>
        /// <param name="bucket">空间名</param>
        /// <param name="optBucket">目标空间</param>
        /// <returns></returns>
        internal static string GetBucket(ALiYunStorageConfig qiNiuStorageConfig, string bucket, string optBucket)
        {
            if (!string.IsNullOrEmpty(optBucket))
            {
                return optBucket;
            }

            return GetBucket(qiNiuStorageConfig, bucket);
        }

        #endregion

        #region 得到分片大小

        /// <summary>
        /// 得到分片大小
        /// </summary>
        /// <param name="aliyunConfig">阿里云配置</param>
        /// <param name="chunkUnit">分片大小</param>
        /// <param name="defaultChunkUnit">默认分片大小</param>
        /// <returns></returns>
        internal static ChunkUnit GetChunkUnit(
            ALiYunStorageConfig aliyunConfig,
            ChunkUnit chunkUnit,
            Func<ChunkUnit> defaultChunkUnit = null)
        {
            if (chunkUnit == null && aliyunConfig.ChunkUnit == null)
            {
                if (defaultChunkUnit == null)
                {
                    throw new BusinessException<string>("未设置分片大小", HttpStatus.Err.Name);
                }

                return defaultChunkUnit.Invoke();
            }

            if (chunkUnit != null)
            {
                return chunkUnit;
            }

            return aliyunConfig.ChunkUnit;
        }

        /// <summary>
        /// 得到分片大小
        /// </summary>
        /// <returns></returns>
        internal static long GetPartSize(
            ChunkUnit chunkUnit)
        {
            return ChunkUnitList.Where(x => x.Key.Id == chunkUnit.Id).Select(x => x.Value).FirstOrDefault();
        }

        #endregion

        #region 得到存储类型

        /// <summary>
        /// 得到存储类型
        /// </summary>
        /// <param name="storageClass"></param>
        /// <returns></returns>
        internal static EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass GetStorageClass(
            string storageClass)
        {
            return StorageClassList.Where(x => x.Value.ToString() == storageClass).Select(x => x.Key).FirstOrDefault();
        }

        /// <summary>
        /// 得到存储类型
        /// </summary>
        /// <param name="storageClass"></param>
        /// <returns></returns>
        internal static StorageClass GetStorageClass(
            EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass storageClass)
        {
            return StorageClassList.Where(x => x.Key.Id == storageClass.Id).Select(x => x.Value).FirstOrDefault();
        }

        #endregion

        #region 得到新的ObjectMetadata（从原来旧的基础上）

        /// <summary>
        /// 得到新的ObjectMetadata（从原来旧的基础上）
        /// </summary>
        /// <param name="sourceObjectMetadata"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static ObjectMetadata GetObjectMetadataBySourceObjectMetadata(ObjectMetadata sourceObjectMetadata,
            string key,
            string value)
        {
            ObjectMetadata objectMetadata = new ObjectMetadata();
            if (key != "CacheControl")
            {
                objectMetadata.CacheControl = sourceObjectMetadata.CacheControl;
            }

            if (key != "ContentDisposition")
            {
                objectMetadata.ContentDisposition = sourceObjectMetadata.ContentDisposition;
            }

            if (key != "ContentEncoding")
            {
                objectMetadata.ContentEncoding = sourceObjectMetadata.ContentEncoding;
            }

            if (key != "ContentLength")
            {
                objectMetadata.ContentLength = sourceObjectMetadata.ContentLength;
            }

            if (key != "ContentMd5")
            {
                objectMetadata.ContentMd5 = sourceObjectMetadata.ContentMd5;
            }

            if (key != "Crc64")
            {
                objectMetadata.Crc64 = sourceObjectMetadata.Crc64;
            }

            if (key != "ETag")
            {
                objectMetadata.ETag = sourceObjectMetadata.ETag;
            }

            if (key != "ExpirationTime")
            {
                objectMetadata.ExpirationTime = sourceObjectMetadata.ExpirationTime;
            }

            if (objectMetadata.HttpMetadata.Any(x => x.Key != key))
            {
                objectMetadata.AddHeader(key, value);
            }

            return objectMetadata;
        }

        #endregion
    }
}
