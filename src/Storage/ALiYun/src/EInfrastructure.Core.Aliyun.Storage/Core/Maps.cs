// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Aliyun.OSS;
using EInfrastructure.Core.Configuration.Enumerations;
using EInfrastructure.Core.Configuration.Exception;
using EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Enumerations;

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
        private static List<KeyValuePair<BucketPermiss, CannedAccessControlList>> CannedAccessControl =
            new List<KeyValuePair<BucketPermiss, CannedAccessControlList>>()
            {
                new KeyValuePair<BucketPermiss, CannedAccessControlList>(BucketPermiss.Private,
                    CannedAccessControlList.Private),
                new KeyValuePair<BucketPermiss, CannedAccessControlList>(BucketPermiss.Public,
                    CannedAccessControlList.PublicRead),
                new KeyValuePair<BucketPermiss, CannedAccessControlList>(BucketPermiss.PublicReadWrite,
                    CannedAccessControlList.PublicReadWrite),
            };

        /// <summary>
        /// 得到访问权限
        /// </summary>
        /// <param name="bucketPermiss"></param>
        /// <returns></returns>
        internal static CannedAccessControlList GetCannedAccessControl(BucketPermiss bucketPermiss)
        {
            var cannedAccessControl = CannedAccessControl.Where(x => x.Key.Id == bucketPermiss.Id).Select(x => x.Value)
                .FirstOrDefault();
            if (cannedAccessControl == default(CannedAccessControlList))
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
        internal static List<KeyValuePair<ChunkUnit, int>>
            ChunkUnitList = new List<KeyValuePair<ChunkUnit, int>>()
            {
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U128K, 128 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U256K, 256 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U512K, 512 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U1024K, 1024 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U2048K, 2 * 1024 * 1024),
                new KeyValuePair<ChunkUnit, int>(ChunkUnit.U4096K, 4 * 1024 * 1024),
            };

        /// <summary>
        /// 得到分片大小
        /// </summary>
        /// <param name="chunkUnit"></param>
        /// <returns></returns>
        internal static long GetPartSize(ChunkUnit chunkUnit)
        {
            return ChunkUnitList.Where(x => x.Key == chunkUnit).Select(x => x.Value).FirstOrDefault();
        }

        #endregion
    }
}
