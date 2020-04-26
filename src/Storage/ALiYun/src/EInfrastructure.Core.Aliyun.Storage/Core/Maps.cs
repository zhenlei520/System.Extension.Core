// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Aliyun.OSS;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;
using StorageClass = Aliyun.OSS.StorageClass;

namespace EInfrastructure.Core.Aliyun.Storage.Core
{
    /// <summary>
    ///
    /// </summary>
    internal class Maps
    {
        #region 得到访问权限

        /// <summary>
        ///
        /// </summary>
        protected static List<KeyValuePair<Permiss, CannedAccessControlList>> CannedAccessControl =
            new List<KeyValuePair<Permiss, CannedAccessControlList>>()
            {
                new KeyValuePair<Permiss, CannedAccessControlList>(Permiss.Private,
                    CannedAccessControlList.Private),
                new KeyValuePair<Permiss, CannedAccessControlList>(Permiss.Public,
                    CannedAccessControlList.PublicRead),
                new KeyValuePair<Permiss, CannedAccessControlList>(Permiss.PublicReadWrite,
                    CannedAccessControlList.PublicReadWrite),
                new KeyValuePair<Permiss, CannedAccessControlList>(Permiss.Default,
                    CannedAccessControlList.Default),
            };

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

        #region 分片

        /// <summary>
        ///
        /// </summary>
        protected static List<KeyValuePair<ChunkUnit, int>>
            ChunkUnitList = new List<KeyValuePair<ChunkUnit, int>>()
            {
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U128K, 128 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U256K, 256 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U512K, 512 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U1024K, 1024 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U2048K, 2 * 1024 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U4096K, 4 * 1024 * 1024),
            };


        #endregion

        #region 存储类型

        protected static
            List<KeyValuePair<EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass,
                StorageClass>> StorageClassList =
                new List<KeyValuePair<Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass, StorageClass>>()
                {
                    new KeyValuePair<Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass, StorageClass>(
                        Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass.Archive, StorageClass.Archive),
                    new KeyValuePair<Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass, StorageClass>(
                        Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass.Standard, StorageClass.Standard),
                    new KeyValuePair<Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass, StorageClass>(
                        Configuration.Ioc.Plugs.Storage.Enumerations.StorageClass.IA, StorageClass.IA)
                };

        #endregion
    }
}
