// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.Storage.Config
{
    /// <summary>
    /// 文件列表筛选
    /// </summary>
    public class ListFileFilter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="delimiter">指定目录分隔符，列出所有公共前缀（模拟列出目录效果）</param>
        /// <param name="marker">标明本次列举文件的起点</param>
        /// <param name="pageSize">每页数量（默认1000条）</param>
        /// <param name="persistentOps">策略</param>
        public ListFileFilter(string prefix, string delimiter, string marker = "", int pageSize = 1000,
            BasePersistentOps persistentOps = null)
        {
            Prefix = prefix;
            Delimiter = delimiter;
            Marker = marker;
            PageSize = pageSize;
            PersistentOps = persistentOps ?? new BasePersistentOps();
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// 指定目录分隔符，列出所有公共前缀（模拟列出目录效果）
        /// </summary>
        public string Delimiter { get; }

        /// <summary>
        /// 标明本次列举文件的起点
        /// 考虑到设置limit后返回的文件列表可能不全,首次执行时marker为null
        /// </summary>
        public string Marker { get; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// 策略
        /// </summary>
        public BasePersistentOps PersistentOps { get; }
    }
}
